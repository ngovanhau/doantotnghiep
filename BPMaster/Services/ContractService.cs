using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using System.Data;
using Common.Application.Settings;
using Repositories;
using BPMaster.Domains.Entities;
using Common.Application.Exceptions;
using BPMaster.Domains.Dtos;
using PdfSharp.Drawing;
using PdfSharp.Pdf;


namespace BPMaster.Services
{
    [ScopedService]
    public class ContractService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection) : BaseService(services)
    {
        private readonly ContractRepository _ContractRepository = new(connection);
        private readonly CustomerRepository _CustomerRepository = new(connection);
        private readonly RoomRepository _RoomRepository = new(connection);
        private readonly BuildingRepository _BuildingRepository = new(connection);


        public async Task<List<ContractDto>> GetAllContract()
        {
            var contracts = await _ContractRepository.GetAllContract();
            var result = new List<ContractDto>();

            foreach (var contract in contracts)
            {
                var dto = _mapper.Map<ContractDto>(contract);
                if (dto.CustomerId != Guid.Empty)
                {
                    var tenantname = await _CustomerRepository.GetByIDCustomer(dto.CustomerId);
                    if (tenantname != null)
                    {
                        dto.CustomerName = tenantname.customer_name;
                    }
                }
                result.Add(dto);
            }
            return result;
        }

        public async Task<ContractDto> GetByIDContract(Guid ContractId)
        {
            var Contract = await _ContractRepository.GetByIDContract(ContractId);

            if (Contract == null)
            {
                throw new NonAuthenticateException();
            }

            var dto = _mapper.Map<ContractDto>(Contract);

            if (Contract.CustomerId != Guid.Empty)
            {
                var tenantname = await _CustomerRepository.GetByIDCustomer(Contract.CustomerId);

                if (tenantname != null)
                {
                    dto.CustomerName = tenantname.customer_name;
                }

            }
            return dto;
        }

        public async Task<ContractDto> GetByUserID(Guid UserId)
        {

            var Customer = await _CustomerRepository.GetbyUserId(UserId);
            if (Customer == null)
            {
                throw new NonAuthenticateException("not found");
            }

            var Contract = await _ContractRepository.GetByCustomerId(Customer.Id);

            if (Contract == null)
            {
                throw new NonAuthenticateException();
            }

            var dto = _mapper.Map<ContractDto>(Contract);

            if (Contract.CustomerId != Guid.Empty)
            {
                var tenantname = await _CustomerRepository.GetByIDCustomer(Contract.CustomerId);

                if (tenantname != null)
                {
                    dto.CustomerName = tenantname.customer_name;
                }

            }
            return dto;
        }

        public async Task<List<ContractDto>> GetAllByBuildingId(Guid id)
        {
            var contracts = await _ContractRepository.GetByBuildingId(id);
            var result = new List<ContractDto>();

            foreach (var contract in contracts)
            {
                var dto = _mapper.Map<ContractDto>(contract);
                if (dto.CustomerId != Guid.Empty)
                {
                    var tenantname = await _CustomerRepository.GetByIDCustomer(dto.CustomerId);
                    if (tenantname != null)
                    {
                        dto.CustomerName = tenantname.customer_name;
                    }
                }
                result.Add(dto);
            }
            return result;
        }

        public async Task<Contract> CreateContractAsync(ContractDto dto)
        {
            var Contract = _mapper.Map<Contract>(dto);

            Contract.Id = Guid.NewGuid();

            await _RoomRepository.UpdateStatusForRoom(Contract.roomId, 1);

            await _RoomRepository.UpdateCustomerIDforRoom(Contract.roomId, Contract.CustomerId);

            await _CustomerRepository.UpdateChooseRoomForCustomer(Contract.CustomerId, Contract.roomId);

            await _ContractRepository.CreateAsync(Contract);

            return Contract;
        }
        public async Task<Contract> UpdateContractAsync(Guid id, ContractDto dto)
        {
            var existingContract = await _ContractRepository.GetByIDContract(id);

            if (existingContract == null)
            {
                throw new Exception("Contract not found !");
            }

            if (existingContract.roomId != dto.roomId)
            {

                await _RoomRepository.UpdateStatusForRoom(existingContract.roomId, 0);

                Guid IdRoomnoexist = Guid.NewGuid();

                await _RoomRepository.UpdateCustomerIDforRoom(existingContract.roomId, IdRoomnoexist);

                await _RoomRepository.UpdateCustomerIDforRoom(dto.roomId, dto.CustomerId);
            }

            if (existingContract.CustomerId != dto.CustomerId)
            {
                Guid Idnoexist = Guid.NewGuid();

                await _CustomerRepository.RemoveChooseRoomFromCustomer(existingContract.CustomerId, Idnoexist);

                await _CustomerRepository.UpdateChooseRoomForCustomer(dto.CustomerId, dto.roomId);

            }

            var Contract = _mapper.Map<Contract>(dto);

            await _RoomRepository.UpdateStatusForRoom(Contract.roomId, 1);

            await _ContractRepository.UpdateAsync(Contract);

            return Contract;
        }
        public async Task DeleteContractAsync(Guid id)
        {
            var Contract = await _ContractRepository.GetByIDContract(id);
            if (Contract == null)
            {
                throw new Exception("Contract not found !");
            }

            Guid IdRoomnoexist = Guid.NewGuid();

            await _RoomRepository.UpdateCustomerIDforRoom(Contract.roomId, IdRoomnoexist);

            Guid Idnoexist = Guid.NewGuid();

            await _RoomRepository.UpdateStatusForRoom(Contract.roomId, 0);

            await _CustomerRepository.RemoveChooseRoomFromCustomer(Contract.CustomerId, Idnoexist);

            await _ContractRepository.DeleteAsync(Contract);
        }

        public async Task<string> GenerateContractPdf(Guid contractId)
        {
            // Lấy dữ liệu từ Repository (giả định dữ liệu đã được lấy)
            var contract = await _ContractRepository.GetByIDContract(contractId);
            if (contract == null)
                throw new Exception($"Không tìm thấy hợp đồng với ID {contractId}!");

            var customer = await _CustomerRepository.GetByIDCustomer(contract.CustomerId);
            var room = await _RoomRepository.GetByIDRoom(contract.roomId);
            var building = await _BuildingRepository.GetByIDBuilding(room?.Building_Id ?? Guid.Empty);

            // Xử lý giá trị null
            string customerName = customer?.customer_name ?? "Không rõ";
            string customerCCCD = customer?.CCCD ?? "Không rõ";
            string customerAddress = customer?.address ?? "Không rõ";

            string buildingAddress = building?.address ?? "Không rõ";
            string roomAcreage = room?.acreage.ToString() ?? "0";
            string roomPrice = room?.room_price.ToString("N0") ?? "0";

            string contractStartDay = contract.start_day != null ? contract.start_day.ToString("dd/MM/yyyy") : "Không rõ";
            string contractEndDay = contract.end_day != null ? contract.end_day.ToString("dd/MM/yyyy") : "Không rõ";
            string contractDeposit = contract.deposit != null ? contract.deposit.ToString("N0") : "0";
            string paymentDay = building.payment_date.ToString() ?? "Không rõ";

            // Đường dẫn lưu file PDF
            var outputDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "contracts");
            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            var fileName = $"Contract_{contractId}.pdf";
            var outputPath = Path.Combine(outputDirectory, fileName);

            // Tạo tài liệu PDF
            var document = new PdfDocument();
            document.Info.Title = "Hợp đồng thuê nhà";

            var page = document.AddPage();
            page.Size = PdfSharp.PageSize.A4;

            var gfx = XGraphics.FromPdfPage(page);
            var fontTitle = new XFont("Times New Roman", 16);
            var fontSubTitle = new XFont("Times New Roman", 14);
            var fontText = new XFont("Times New Roman", 12);
            var fontBold = new XFont("Times New Roman", 12);

            int yPoint = 50;
            int lineSpacing = 20;

            // Số dòng tối đa trên một trang
            int maxLinesPerPage = (int)((page.Height - 100) / lineSpacing);
            int currentLineCount = 0;

            // Hàm chia dòng tự động
            string[] SplitText(string text, XFont font, XGraphics gfx, double maxWidth)
            {
                List<string> lines = new List<string>();
                string[] words = text.Split(' ');
                string currentLine = "";

                foreach (var word in words)
                {
                    string testLine = currentLine + (currentLine == "" ? "" : " ") + word;
                    double textWidth = gfx.MeasureString(testLine, font).Width;

                    if (textWidth < maxWidth)
                    {
                        currentLine = testLine;
                    }
                    else
                    {
                        lines.Add(currentLine);
                        currentLine = word;
                    }
                }

                if (!string.IsNullOrEmpty(currentLine))
                {
                    lines.Add(currentLine);
                }

                return lines.ToArray();
            }

            // Hàm thêm nội dung vào PDF và chia trang
            void AddText(string text, XFont font, double x, double maxWidth)
            {
                var lines = SplitText(text, font, gfx, maxWidth);
                foreach (var line in lines)
                {
                    if (currentLineCount >= maxLinesPerPage)
                    {
                        page = document.AddPage();
                        gfx = XGraphics.FromPdfPage(page);
                        currentLineCount = 0;
                        yPoint = 50; // Đặt lại điểm bắt đầu
                    }
                    gfx.DrawString(line, font, XBrushes.Black, x, yPoint + currentLineCount * lineSpacing);
                    currentLineCount++;
                }
            }
            double topMargin = 30; // Khoảng cách từ mép trên
            double titleLineSpacing = 20; // Khoảng cách giữa các dòng tiêu đề

            gfx.DrawString("CỘNG HÒA XÃ HỘI CHỦ NGHĨA VIỆT NAM", fontBold, XBrushes.Black,
                new XRect(0, topMargin, page.Width, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("Độc lập - Tự do - Hạnh phúc", fontBold, XBrushes.Black,
                new XRect(0, topMargin + titleLineSpacing, page.Width, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("………., ngày .... tháng .... năm ....", fontText, XBrushes.Black,
                new XRect(0, topMargin + 2 * titleLineSpacing, page.Width, page.Height), XStringFormats.TopCenter);

            gfx.DrawString("HỢP ĐỒNG THUÊ NHÀ", fontTitle, XBrushes.Black,
                new XRect(0, topMargin + 4 * titleLineSpacing, page.Width, page.Height), XStringFormats.TopCenter);

            // Đẩy nội dung hợp đồng lên gần tiêu đề
            yPoint = (int)(topMargin + 6 * titleLineSpacing); // Đặt nội dung sát với tiêu đề

            // Giảm khoảng cách dòng trong nội dung hợp đồng
            lineSpacing = 15;

            AddText("BÊN CHO THUÊ (Bên A):", fontBold, 50, page.Width - 100);
            AddText($"Ông: Nguyễn Văn A", fontText, 50, page.Width - 100);
            AddText($"CMND số: 05122006322", fontText, 50, page.Width - 100);
            AddText($"Nơi ĐKTT: P14, Q5, thành phố Hồ Chí Minh", fontText, 50, page.Width - 100);

            AddText("BÊN THUÊ (Bên B):", fontBold, 50, page.Width - 100);
            AddText($"Ông/Bà: {customerName}", fontText, 50, page.Width - 100);
            AddText($"CMND số: {customerCCCD}", fontText, 50, page.Width - 100);
            AddText($"Nơi ĐKTT: {customerAddress}", fontText, 50, page.Width - 100);

            AddText("Điều 1. Nhà ở và các tài sản cho thuê kèm theo nhà ở:", fontSubTitle, 50, page.Width - 100);
            AddText($"- Địa chỉ: {buildingAddress}", fontText, 70, page.Width - 100);
            AddText($"- Diện tích: {roomAcreage} m²;", fontText, 70, page.Width - 100);
            AddText($"- Giá thuê nhà: {roomPrice} VNĐ/tháng;", fontText, 70, page.Width - 100);

            AddText("Điều 2. Bàn giao và sử dụng diện tích thuê:", fontSubTitle, 50, page.Width - 100);
            AddText($"- Ngày bắt đầu: {contractStartDay};", fontText, 70, page.Width - 100);
            AddText($"- Ngày kết thúc: {contractEndDay};", fontText, 70, page.Width - 100);

            AddText("Điều 3. Thời hạn thuê:", fontSubTitle, 50, page.Width - 100);
            AddText("- Bên A cam kết cho Bên B thuê tài sản thuê với thời hạn là ......... năm kể từ ngày bàn giao Tài sản thuê.", fontText, 70, page.Width - 100);
            AddText("- Hết thời hạn thuê nêu trên nếu bên B có nhu cầu tiếp tục sử dụng thì Bên A phải ưu tiên cho Bên B tiếp tục thuê.", fontText, 70, page.Width - 100);

            AddText("Điều 4. Đặt cọc tiền thuê nhà:", fontSubTitle, 50, page.Width - 100);
            AddText($"- Bên B sẽ giao cho Bên A một khoản tiền là: {contractDeposit} VNĐ (bằng chữ: ............................................) ngay sau khi ký hợp đồng này.", fontText, 70, page.Width - 100);
            AddText("- Nếu Bên B đơn phương chấm dứt hợp đồng mà không thực hiện nghĩa vụ báo trước tới Bên A thì Bên A sẽ không phải hoàn trả lại Bên B số tiền đặt cọc này.", fontText, 70, page.Width - 100);
            AddText("- Nếu Bên A đơn phương chấm dứt hợp đồng mà không thực hiện nghĩa vụ báo trước tới Bên B thì Bên A sẽ phải hoàn trả lại Bên B số tiền đặt cọc và phải bồi thường thêm một khoản bằng chính tiền đặt cọc.", fontText, 70, page.Width - 100);

            AddText("Điều 5. Thanh toán tiền thuê nhà:", fontSubTitle, 50, page.Width - 100);
            AddText($"- Tiền thuê nhà đối với diện tích thuê nêu tại Điều 1 là {roomPrice} VNĐ/tháng.", fontText, 70, page.Width - 100);
            AddText($"- Tiền thuê nhà không bao gồm các chi phí khác như tiền điện, nước, vệ sinh.", fontText, 70, page.Width - 100);

            AddText("Điều 6. Quyền và nghĩa vụ của Bên A:", fontSubTitle, 50, page.Width - 100);
            AddText("- Yêu cầu Bên B thanh toán tiền thuê và các chi phí khác đúng hạn.", fontText, 70, page.Width - 100);
            AddText("- Bàn giao nhà đúng thời hạn.", fontText, 70, page.Width - 100);

            AddText("Điều 7. Quyền và nghĩa vụ của Bên B:", fontSubTitle, 50, page.Width - 100);
            AddText("- Sử dụng nhà đúng mục đích thuê.", fontText, 70, page.Width - 100);
            AddText("- Thanh toán đầy đủ tiền thuê và các khoản chi phí khác.", fontText, 70, page.Width - 100);

            AddText("Điều 8. Chấm dứt hợp đồng:", fontSubTitle, 50, page.Width - 100);
            AddText("- Một trong hai bên có quyền chấm dứt hợp đồng với điều kiện thông báo trước 30 ngày.", fontText, 70, page.Width - 100);

            AddText("Điều 9. Điều khoản thi hành:", fontSubTitle, 50, page.Width - 100);
            AddText("- Hợp đồng có hiệu lực kể từ ngày ký.", fontText, 70, page.Width - 100);

            // Lưu file PDF
            document.Save(outputPath);

            // Trả về link tải file
            return $"/contracts/{fileName}";
        }
    }
}


