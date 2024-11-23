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
        public async Task ProcessVNPayReturn(string fullUrl)
        {
            try
            {
                Console.WriteLine($"[INFO] Received fullUrl: {fullUrl}");

                // Giải mã URL
                fullUrl = HttpUtility.UrlDecode(fullUrl);
                Console.WriteLine($"[INFO] Decoded fullUrl: {fullUrl}");

                // Phân tích URL
                Uri uri = new Uri(fullUrl);
                var queryParams = HttpUtility.ParseQueryString(uri.Query);

                // Log tham số nhận được từ URL
                Console.WriteLine("[DEBUG] Query Parameters from URL:");
                foreach (string key in queryParams)
                {
                    Console.WriteLine($"Key: {key}, Value: {queryParams[key]}");
                }

                // Chuyển tham số sang SortedDictionary
                var vnp_Params = new SortedDictionary<string, string>();
                foreach (string key in queryParams)
                {
                    if (key != "vnp_SecureHash" && !string.IsNullOrEmpty(key))
                    {
                        vnp_Params.Add(key, queryParams[key]);
                    }
                }

                // Lấy vnp_SecureHash từ URL
                string vnp_SecureHash = queryParams["vnp_SecureHash"];
                if (string.IsNullOrEmpty(vnp_SecureHash))
                {
                    Console.WriteLine("[ERROR] Thiếu vnp_SecureHash trong URL.");
                    throw new Exception("Thiếu vnp_SecureHash trong URL.");
                }

                Console.WriteLine($"[INFO] Received vnp_SecureHash: {vnp_SecureHash}");

                // Tạo chuỗi rawData để kiểm tra hash
                string rawData = string.Join("&", vnp_Params.Select(kvp => $"{kvp.Key}={kvp.Value}"));
                Console.WriteLine($"[DEBUG] rawData for hash: {rawData}");

                // Tính toán hash
                string vnp_HashSecret = "7VQZ64MFZGC8ZTFS22CM6DDO6WV7ZYGA"; // Thay bằng giá trị đúng
                string calculatedHash = CreateHmacSHA512(vnp_HashSecret, rawData);
                Console.WriteLine($"[DEBUG] Calculated Secure Hash: {calculatedHash}");

                // So sánh hash
                if (calculatedHash != vnp_SecureHash)
                {
                    Console.WriteLine($"[ERROR] Calculated hash: {calculatedHash}");
                    Console.WriteLine($"[ERROR] Received hash: {vnp_SecureHash}");
                    throw new Exception("Checksum không hợp lệ.");
                }

                Console.WriteLine("[INFO] Checksum hợp lệ. Tiến hành xử lý giao dịch.");

                // Xử lý giao dịch
                string responseCode = vnp_Params["vnp_ResponseCode"];
                string transactionRef = vnp_Params["vnp_TxnRef"];
                Console.WriteLine($"[INFO] Response Code: {responseCode}");
                Console.WriteLine($"[INFO] Transaction Ref: {transactionRef}");

                var bill = await _Repository.GetByTransactionId(transactionRef);
                if (bill == null)
                {
                    throw new Exception("Hóa đơn không tồn tại.");
                }

                if (responseCode == "00")
                {
                    await _Repository.UpdateTransactionStatus(transactionRef, 1, DateTime.Now);
                    Console.WriteLine("[INFO] Giao dịch thành công. Cập nhật trạng thái hóa đơn thành công.");
                }
                else
                {
                    await _Repository.UpdateTransactionStatus(transactionRef, 0);
                    Console.WriteLine($"[INFO] Giao dịch thất bại với mã lỗi: {responseCode}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] VNPayReturn: {ex.Message}");
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



