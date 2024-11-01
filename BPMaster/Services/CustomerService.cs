using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;

namespace BPMaster.Services
{
    [ScopedService]
    public class CustomerService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly CustomerRepository _CustomerRepository = new(connection);

        public async Task<List<CustomerDto>> GetAllCustomer()
        {
            var customers =  await _CustomerRepository.GetAllCustomer();
            var result = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                var image = await _CustomerRepository.GetImagesByCustomer(customer.Id);
                var dto = _mapper.Map<CustomerDto>(customer);

                if (customer.choose_room != Guid.Empty)
                {
                    var room = await _CustomerRepository.GetChooseRoombyId(customer.choose_room);

                    if (room != null)
                    {
                        dto.RoomName = room.room_name;
                    }
                }
                else
                {
                    dto.RoomName = "Chưa có phòng";
                }

                dto.imageCCCDs = image;
                result.Add(dto);
            }
            return result;
        }

        public async Task<CustomerDto> GetByIDCustomer(Guid CustomerId)
        {
            var Customer = await _CustomerRepository.GetByIDCustomer(CustomerId);

            if (Customer == null)
            {
                throw new NonAuthenticateException();
            }
            var image = await _CustomerRepository.GetImagesByCustomer(CustomerId);

            var dto = _mapper.Map<CustomerDto>(Customer);

            if (Customer.choose_room != Guid.Empty)
            {
                var room = await _CustomerRepository.GetChooseRoombyId(Customer.choose_room);

                // Kiểm tra nếu room không phải là null trước khi truy cập room_name
                if (room != null)
                {
                    dto.RoomName = room.room_name; // Đảm bảo CustomerDto có thuộc tính RoomName
                }
                
            }
            else
            {
                dto.RoomName = "Chưa có phòng";
            }
            dto.imageCCCDs = image;


            return dto;
        }

        public async Task<Customer> CreateCustomerAsync(CustomerDto dto)
        {
            var Customer = _mapper.Map<Customer>(dto);

            Customer.Id = Guid.NewGuid();

            await _CustomerRepository.CreateAsync(Customer);

            if(dto.choose_room != null)
            {
                await _CustomerRepository.UpdateRoomStatus(dto.choose_room);
            }

            if (dto.imageCCCDs != null && dto.imageCCCDs.Count > 0)
            {
                await _CustomerRepository.AddImagesToCustomer(Customer.Id, dto.imageCCCDs);
            }
            return Customer;
        }
        public async Task<Customer> UpdateCustomerAsync(Guid id, CustomerDto dto)
        {
            var existingCustomer = await _CustomerRepository.GetByIDCustomer(id);

            if (existingCustomer == null)
            {
                throw new Exception("Error");
            }
            var Customer = _mapper.Map(dto, existingCustomer);

            await _CustomerRepository.RemoveImagesFromCustomer(id);

            await _CustomerRepository.UpdateAsync(Customer);

            if(dto.imageCCCDs != null && dto.imageCCCDs.Count > 0) 
            {
             await _CustomerRepository.AddImagesToCustomer(id, dto.imageCCCDs);
            }

            return Customer;
        }
        public async Task DeleteCustomerAsync(Guid id)
        {
            var Customer = await _CustomerRepository.GetByIDCustomer(id);
            if (Customer == null)
            {
                throw new Exception("Customer not found !");
            }
            await _CustomerRepository.RemoveImagesFromCustomer(id);
            await _CustomerRepository.DeleteAsync(Customer);
        }

    }
}

