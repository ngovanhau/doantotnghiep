using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace BPMaster.Services
{
    [ScopedService]
    public class BillService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly BillRepository _Repository = new(connection);

        public async Task<List<BillDto>> GetAll()
        {
            var deposits = await _Repository.GetAll();
            var dto = _mapper.Map<List<BillDto>>(deposits);
            return dto;
        }

        public async Task<BillDto> GetById(Guid Id)
        {
            var bill = await _Repository.GetByID(Id);

            if (bill == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<BillDto>(bill);
            return dto;
        }
        public async Task<List<BillDto>> GetByBuildingId(Guid Id)
        {
            var bill = await _Repository.GetBillByBuildingId(Id);

            if (bill == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<List<BillDto>>(bill);
            return dto;
        }
        public async Task<List<BillDto>> GetByRoomId(Guid Id)
        {
            var bill = await _Repository.GetBillByRoomId(Id);

            if (bill == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var dto = _mapper.Map<List<BillDto>>(bill);
            return dto;
        }
        public async Task<Bill> UpdateAsync(Guid id, BillDto dto)
        {
            var existing = await _Repository.GetByID(id);

            if (existing == null)
            {
                throw new Exception("not found !");
            }

            var bill = _mapper.Map(dto, existing);

            await _Repository.Updatebill(bill);

            return bill;
        }
        // update payment status
        public async Task<Bill> Updatepaymentstatus(Guid id)
        {
            var existing = await _Repository.GetByID(id);
            if (existing == null)
            {
                throw new Exception("not found");
            }
            existing.status_payment = 1;

            await _Repository.Updatebill(existing);

            return existing;
        }
        public async Task<Bill> CreateAsync(BillDto dto)
        {
            var Bill = _mapper.Map<Bill>(dto);

            Bill.Id = Guid.NewGuid();

            await _Repository.CreateAsync(Bill);

            return Bill;
        }
        public async Task DeleteAsync(Guid id)
        {
            var Bill = await _Repository.GetByID(id);
            if (Bill == null)
            {
                throw new Exception(" not found !");
            }

            await _Repository.Delete(Bill);
        }
        
        //thanh toan vnpay
        public async Task<string> CreateVNPayPayment(Guid billId)
        {
            try
            {
                var bill = await _Repository.GetByID(billId);
                if (bill == null || bill.status_payment == 1)
                {
                    throw new Exception("Hóa đơn không tồn tại hoặc đã thanh toán.");
                }

                // Tạo mã giao dịch
                var transactionRef = Guid.NewGuid().ToString();

                // Cập nhật mã giao dịch vào bảng Bill
                bill.transaction_id = transactionRef;
                await _Repository.Updatebill(bill);

                // Cấu hình tham số VNPay
                string vnp_TmnCode = "4JM44UVK";
                string vnp_HashSecret = "7VQZ64MFZGC8ZTFS22CM6DDO6WV7ZYGA";
                string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
                string returnUrl = "https://sandbox.domainmerchant.vn/vnpay_return";

                var vnp_Params = new SortedDictionary<string, string>
                {
                    { "vnp_Version", "2.1.0" },
                    { "vnp_Command", "pay" },
                    { "vnp_TmnCode", vnp_TmnCode },
                    { "vnp_Amount", ((int)(bill.final_amount * 100)).ToString() },
                    { "vnp_CurrCode", "VND" },
                    { "vnp_TxnRef", transactionRef },
                    { "vnp_OrderInfo", Uri.EscapeDataString("Thanh toan hoa don test") },
                    { "vnp_OrderType", "other" },
                    { "vnp_Locale", "vn" },
                    { "vnp_ReturnUrl", Uri.EscapeDataString(returnUrl) },
                    { "vnp_IpAddr", "127.0.0.1" },
                    { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") },
                    { "vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss") }
                };

                // Tạo chuỗi rawData để mã hóa checksum
                string rawData = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));

                string vnp_SecureHash = CreateHmacSHA512(vnp_HashSecret, rawData);
                string paymentUrl = $"{vnp_Url}?{rawData}&vnp_SecureHash={vnp_SecureHash}";

                return paymentUrl;
            }
            catch (Exception ex)
            {
                throw new Exception("Tạo hóa đơn thất bại");
            }
        }
       
        private static string CreateHmacSHA512(string key, string inputData)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.UTF8.GetBytes(inputData));
                return BitConverter.ToString(hashValue).Replace("-", "").ToLower();
            }
        }

    }
}



