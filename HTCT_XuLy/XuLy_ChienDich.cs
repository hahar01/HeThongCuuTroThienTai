using HTCT_Entities;
using HTCT_LuuTru;
using Microsoft.EntityFrameworkCore;

public class XuLy_ChienDich
{
    private readonly LuuTru_ChienDich _luuTruChienDich;
    private readonly LuuTru_QuyTuThien _luuTruQuyTuThien;

    public XuLy_ChienDich(LuuTru_ChienDich luuTruChienDich, LuuTru_QuyTuThien luuTruQuyTuThien)
    {
        _luuTruChienDich = luuTruChienDich;
        _luuTruQuyTuThien = luuTruQuyTuThien;
    }

    //Kiểm tra trạng thái quỹ từ thiện
    private bool KiemTraQuyTuThienDuyet(int maQuyTuThien)
    {
        var quyTuThien = _luuTruQuyTuThien.GetQuyTuThienById(maQuyTuThien);
        if (quyTuThien == null || quyTuThien.TrangThai != "Đã Duyệt")
        {
            return false;
        }
        return true;
    }

    // Thêm chiến dịch
    public void ThemChienDich(ChienDich chienDich, out string errorMessage)
    {
        errorMessage = string.Empty;
        if (!KiemTraQuyTuThienDuyet(chienDich.MaQuyTuThien))
        {
            errorMessage = "Quỹ từ thiện chưa được duyệt.";
            return;
        }
        // Kiểm tra nếu ngày bắt đầu là null hoặc không được nhập
        if (chienDich.NgayBatDau == null || chienDich.NgayBatDau == DateTime.MinValue)
        {
            errorMessage = "Ngày bắt đầu không được để trống.";
            return;
        }

        // Kiểm tra nếu ngày kết thúc là null hoặc không được nhập
        if (chienDich.NgayKetThuc == null || chienDich.NgayKetThuc == DateTime.MinValue)
        {
            errorMessage = "Ngày kết thúc không được để trống.";
            return;
        }
        if (chienDich.NgayBatDau <= DateTime.Now.Date)
        {
            errorMessage = "Ngày bắt đầu chiến dịch phải lớn hơn ngày hiện tại.";
            return;
        }
        if (chienDich.NgayKetThuc <= chienDich.NgayBatDau)
        {
            errorMessage = "Ngày kết thúc phải lớn hơn ngày bắt đầu.";
            return;
        }
        if (chienDich == null)
        {
            errorMessage = "Chiến dịch không được null";
            return;
        }
        if (string.IsNullOrEmpty(chienDich.TenChienDich) || string.IsNullOrEmpty(chienDich.MoTa) || string.IsNullOrEmpty(chienDich.SoTaiKhoan) || string.IsNullOrEmpty(chienDich.TenNganHang))
        {
            errorMessage = "Thông tin chiến dịch không đầy đủ";
            return;
        }
        chienDich.TrangThai = "Mới tạo";

        _luuTruChienDich.AddChienDich(chienDich);
    }
    // Cập nhật chiến dịch
    public void UpdateChienDichStatus()
    {
        var danhSachChienDich = _luuTruChienDich.GetAllChienDichs();

        foreach (var chienDich in danhSachChienDich)
        {
            if (!chienDich.IsPublic)
            {
                continue; // Nếu chiến dịch chưa công khai, bỏ qua việc thay đổi trạng thái
            }
            // Kiểm tra nếu ngày bắt đầu đã đến và trạng thái chưa Bắt đầu
            if (chienDich.NgayBatDau.Date <= DateTime.Now.Date && chienDich.TrangThai != "Bắt đầu")
            {
                chienDich.TrangThai = "Bắt đầu";
                _luuTruChienDich.UpdateChienDich(chienDich); 
            }

            // Kiểm tra nếu ngày kết thúc đã đến và trạng thái chưa Kết thúc
            if (chienDich.NgayKetThuc.Date <= DateTime.Now.Date && chienDich.TrangThai != "Kết thúc")
            {
                chienDich.TrangThai = "Kết thúc";
                _luuTruChienDich.UpdateChienDich(chienDich); 
            }
        }
    }

    // Lấy tất cả chiến dịch
    public List<ChienDich> LayTatCaChienDich()
    {
        
        return _luuTruChienDich.GetAllChienDichs();
    }

    // Tìm chiến dịch theo ID
    public ChienDich LayChienDichTheoId(int maChienDich)
    {
        if (maChienDich <= 0)
        {
            throw new ArgumentException("Mã chiến dịch không hợp lệ");
        }
        return _luuTruChienDich.GetChienDichById(maChienDich);
    }
       
    // Xóa mềm chiến dịch
    public void XoaMemChienDich(int maChienDich, out string errorMessage)
    {
        errorMessage = string.Empty;
        var chienDich = _luuTruChienDich.GetChienDichById(maChienDich);
        if (chienDich == null)
        {
            errorMessage = "Không tìm thấy chiến dịch";
        }

        _luuTruChienDich.SoftDeleteChienDich(maChienDich);
    }

    // Lấy chiến dịch theo ID Quỹ từ thiện
    public List<ChienDich> LayChienDichTheoQuyTuThien(int maQuyTuThien)
    {        
        return _luuTruChienDich.GetTatCaChienDichTheoIDQuy(maQuyTuThien);
    }

    // Lấy tên chiến dịch theo ID
    public string LayTenChienDich(int maChienDich)
    {
        var chienDich = _luuTruChienDich.GetChienDichById(maChienDich);
        return chienDich?.TenChienDich ?? "Không rõ";
    }

    // Lấy tất cả chiến dịch công khai
    public void CongKhaiChienDich(int maChienDich)
    {
        // Lấy chiến dịch theo ID
        var chienDich = _luuTruChienDich.GetChienDichById(maChienDich);

        if (chienDich == null)
        {
            throw new Exception("Chiến dịch không tồn tại.");
        }

        // Kiểm tra nếu chiến dịch đang ở trạng thái "Mới Tạo"
        if (chienDich.TrangThai != "Mới tạo")
        {
            throw new Exception("Chiến dịch không thể công khai vì không ở trạng thái 'Mới Tạo'.");
        }

        // Kiểm tra ngày bắt đầu có hợp lệ không
        if (chienDich.NgayBatDau.Date <= DateTime.Now.Date)
        {
            throw new Exception("Ngày bắt đầu chiến dịch phải lớn hơn ngày hiện tại.");
        }

        // Kiểm tra ngày kết thúc có hợp lệ không
        if (chienDich.NgayKetThuc.Date <= chienDich.NgayBatDau.Date)
        {
            throw new Exception("Ngày kết thúc chiến dịch phải lớn hơn ngày bắt đầu.");
        }

        // Đánh dấu chiến dịch là công khai
        chienDich.IsPublic = true;
        if (chienDich.NgayBatDau.Date <= DateTime.Now.Date)
        {
            chienDich.TrangThai = "Mới tạo";
        }

        // Cập nhật chiến dịch trong cơ sở dữ liệu mà không thay đổi trạng thái "Mới Tạo"
        _luuTruChienDich.UpdateChienDich(chienDich);
    }
    public List<ChienDich> LayTatCaChienDichCongKhai()
    {

        return _luuTruChienDich.GetAllChienDichCongKhai();
    }


}
