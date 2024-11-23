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
                throw new Exception("Deposit not found !");
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
                string vnp_TmnCode = "2QXUI4J4"; // Thay bằng TMN Code của bạn
                string vnp_HashSecret = "5Knb6NvWfQ8692nENy1oD0SUjC76yE"; // Thay bằng Hash Secret của bạn
                string vnp_Url = "https://sandbox.vnpayment.vn/paymentv2/vpcpay.html";
                string returnUrl = "https://yourdomain.com/api/v1/bill/vnpay-return";

                var vnp_Params = new SortedDictionary<string, string>
                {
                    { "vnp_Version", "2.1.0" },
                    { "vnp_Command", "pay" },
                    { "vnp_TmnCode", vnp_TmnCode },
                    { "vnp_Amount", (bill.final_amount * 100).ToString() }, // VNPay yêu cầu số tiền nhân 100
                    { "vnp_CurrCode", "VND" },
                    { "vnp_TxnRef", transactionRef },
                    { "vnp_OrderInfo", Uri.EscapeDataString($"Thanh toán hóa đơn {bill.bill_name}") },
                    { "vnp_Locale", "vn" },
                    { "vnp_ReturnUrl", returnUrl },
                    { "vnp_IpAddr", "127.0.0.1" },
                    { "vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss") }
                };

                // Tạo URL với checksum
                string queryString = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={Uri.EscapeDataString(kvp.Value)}"));
                string vnp_SecureHash = CreateHmacSHA512(vnp_HashSecret, queryString);
                string paymentUrl = $"{vnp_Url}?{queryString}&vnp_SecureHash={vnp_SecureHash}";

                // Ghi log để debug
                Console.WriteLine("Query String: " + queryString);
                Console.WriteLine("Secure Hash: " + vnp_SecureHash);
                Console.WriteLine("Full URL: " + paymentUrl);

                return paymentUrl;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] CreateVNPayPayment: {ex.Message}");
                Console.WriteLine($"[STACKTRACE] {ex.StackTrace}");
                throw;
            }
        }

        public async Task ProcessVNPayReturn(SortedDictionary<string, string> vnp_Params)
        {
            try
            {
                // Cấu hình hash secret
                string vnp_HashSecret = "5Knb6NvWfQ8692nENy1oD0SUjC76yE";

                // Ghi log tham số nhận được từ VNPay
                Console.WriteLine("[INFO] VNPay Params Received:");
                foreach (var param in vnp_Params)
                {
                    Console.WriteLine($"{param.Key}: {param.Value}");
                }

                // Lấy checksum từ VNPay
                string vnp_SecureHash = vnp_Params["vnp_SecureHash"];
                vnp_Params.Remove("vnp_SecureHash");

                // Xác minh checksum
                string rawData = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                string calculatedHash = CreateHmacSHA512(vnp_HashSecret, rawData);
                Console.WriteLine($"[INFO] Calculated Secure Hash: {calculatedHash}");
                if (vnp_SecureHash != calculatedHash)
                {
                    Console.WriteLine("[ERROR] Checksum không hợp lệ.");
                    throw new Exception("Checksum không hợp lệ.");
                }

                // Kiểm tra trạng thái giao dịch
                string responseCode = vnp_Params["vnp_ResponseCode"];
                string transactionRef = vnp_Params["vnp_TxnRef"];
                Console.WriteLine($"[INFO] Response Code: {responseCode}");
                Console.WriteLine($"[INFO] Transaction Ref: {transactionRef}");

                var bill = await _Repository.GetByTransactionId(transactionRef);
                if (bill == null)
                {
                    Console.WriteLine("[ERROR] Hóa đơn không tồn tại.");
                    throw new Exception("Hóa đơn không tồn tại.");
                }

                if (responseCode == "00")
                {
                    // Cập nhật trạng thái thành công
                    Console.WriteLine("[INFO] Giao dịch thành công. Cập nhật trạng thái.");
                    await _Repository.UpdateTransactionStatus(transactionRef, 1, DateTime.Now);
                }
                else
                {
                    // Cập nhật trạng thái thất bại
                    Console.WriteLine($"[INFO] Giao dịch thất bại với Response Code: {responseCode}");
                    await _Repository.UpdateTransactionStatus(transactionRef, 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] ProcessVNPayReturn: {ex.Message}");
                Console.WriteLine($"[STACKTRACE] {ex.StackTrace}");
                throw;
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



