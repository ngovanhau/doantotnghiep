using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;
using Domain.Entities;
using Utilities;

namespace BPMaster.Services
{
    [ScopedService]
    public class CustomerService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly PasswordSetting _passwordSetting = setting.PasswordSetting;
        private readonly CustomerRepository _CustomerRepository = new(connection);
        private readonly RoomRepository _RoomRepository = new(connection);
        private readonly IdentityUserRepository _IdentityUserRepository = new(connection);  
        private readonly SendEmailRepository _SendEmailRepository = new(connection);

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
            var existingUser = await _IdentityUserRepository.GetByUsernameAsync(dto.email);
            if (existingUser != null)
            {
                throw new Exception("Email đã tồn tại. Vui lòng chọn một email khác.");
            }

            var nameParts = dto.customer_name.Split(' ');
            var firstName = nameParts.First(); 
            var lastName = nameParts.Last();
            var Passdefault = "RPMS@2024";

            var createaccount = new IdentityUser
            {
                Id = Guid.NewGuid(),
                Username = dto.email,
                Password = PasswordUtil.HashPBKDF2(Passdefault, _passwordSetting, out var salts),
                Salts = Convert.ToHexString(salts),
                FirstName = firstName,
                LastName = lastName,
                Role = "USER",
                Email = dto.email,
                Phone = dto.phone_number,
            };
            // gửi gmail
            string subject = "Thông tin tài khoản đăng nhập";
            string body = $"Chào {dto.customer_name},<br/><br/>" +
                          $"Tài khoản của bạn đã được tạo thành công.<br/>" +
                          $"<strong>Username:</strong> {dto.email}<br/>" +
                          $"<strong>Password:</strong> {Passdefault}<br/><br/>" +
                          "Vui lòng đăng nhập và thay đổi mật khẩu để đảm bảo an toàn.";

            try
            {
                await _SendEmailRepository.SendEmailAsync(dto.email, subject, body);
            }
            catch (Exception ex)
            {
                throw new Exception("Tạo khách hàng thất bại vui lòng kiểm tra lại gmail.");
            }

            await _IdentityUserRepository.CreateAsync(createaccount);

            var Customer = _mapper.Map<Customer>(dto);
            
            Customer.UserId = createaccount.Id;

            Customer.Id = Guid.NewGuid();

            await _CustomerRepository.CreateAsync(Customer);

            if(dto.choose_room != Guid.Empty)
            {
                await _CustomerRepository.UpdateRoomStatus(dto.choose_room, 1);
            }

            if (dto.imageCCCDs != null && dto.imageCCCDs.Count > 0)
            {
                await _CustomerRepository.AddImagesToCustomer(Customer.Id, dto.imageCCCDs);
            }

            await _RoomRepository.UpdateCustomerIDforRoom(Customer.choose_room, Customer.Id);

            return Customer;
        }
        public async Task<Customer> UpdateCustomerAsync(Guid id, CustomerDto dto)
        {
            var existingCustomer = await _CustomerRepository.GetByIDCustomer(id);

            if (existingCustomer == null)
            {
                throw new Exception("Error");
            }

            if (existingCustomer.choose_room != dto.choose_room)
            {
                await _CustomerRepository.UpdateRoomStatus(existingCustomer.choose_room, 0);

                Guid idnoexist = Guid.NewGuid();

                await _RoomRepository.UpdateCustomerIDforRoom(existingCustomer.choose_room, idnoexist);

                await _CustomerRepository.UpdateRoomStatus(dto.choose_room, 1);
            }

            var Customer = _mapper.Map(dto, existingCustomer);

            await _RoomRepository.UpdateCustomerIDforRoom(existingCustomer.choose_room, Customer.Id);

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

            Guid idnoexist = Guid.NewGuid();

            await _RoomRepository.UpdateCustomerIDforRoom(Customer.choose_room, idnoexist);

            await _CustomerRepository.UpdateRoomStatus(Customer.choose_room, 0);

            await _CustomerRepository.RemoveImagesFromCustomer(id);

            await _CustomerRepository.DeleteAsync(Customer);
        }

        public async Task<List<CustomerDto>> GetListByNoChooseRoom()
        {
            var customers = await _CustomerRepository.GetAllCustomer();
            var result = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                var image = await _CustomerRepository.GetImagesByCustomer(customer.Id);

                var dto = _mapper.Map<CustomerDto>(customer);

                // Kiểm tra xem phòng có tồn tại không
                if (customer.choose_room != Guid.Empty)
                {
                    var room = await _CustomerRepository.GetChooseRoombyId(customer.choose_room);

                    if (room == null) // Nếu không tìm thấy phòng
                    {
                        dto.RoomName = "không có phòng";
                        dto.imageCCCDs = image;
                        result.Add(dto);
                    }
                }
            }
            return result;
        }
    }
}

