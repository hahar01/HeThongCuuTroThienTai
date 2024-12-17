using HTCT_Entities;
using HTCT_LuuTru;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTCT_XuLy
{
    public class XuLy_GiayPhep
    {
        private readonly LuuTru_NguoiThuHuong _luuTruNguoiThuHuong;
        private readonly LuuTru_QuyTuThien _luuTruQuyTuThien;
        private readonly LuuTru_GiayPhep _luuTruGiayPhep;

        public XuLy_GiayPhep(LuuTru_NguoiThuHuong luuTruNguoiThuHuong, LuuTru_QuyTuThien luuTruQuyTuThien, LuuTru_GiayPhep luuTruGiayPhep)
        {
            _luuTruNguoiThuHuong = luuTruNguoiThuHuong;
            _luuTruQuyTuThien = luuTruQuyTuThien;
            _luuTruGiayPhep = luuTruGiayPhep;
        }

        //Duyệt giấy phép người thụ hưởng
        public void DuyetGiayPhepNguoiThuHuong(int maNguoiThuHuong)
        {           
            // Bước 1: Lấy thông tin và giấy phép
            var nguoiThuHuong = _luuTruNguoiThuHuong.GetNguoiThuHuongById(maNguoiThuHuong);
            if (nguoiThuHuong == null)
            {
                throw new Exception("Không tìm thấy thông tin Người thụ hưởng.");
            }

            // Bước 2: Kiểm tra xem đã có giấy phép hay chưa
            var giayPhep = _luuTruGiayPhep.GetGiayPhepById(nguoiThuHuong.MaGiayPhep ?? 0);
            if (giayPhep == null)
            {
                throw new Exception("Không tìm thấy giấy phép của Người thụ hưởng.");
            }

            // Bước 3: Kiểm tra nếu giấy phép chưa được duyệt
            if (!giayPhep.TrangThai)
            {
                // Duyệt giấy phép và cập nhật trạng thái giấy phép
                giayPhep.TrangThai = true;  
                _luuTruGiayPhep.UpdateGiayPhep(giayPhep); 

                // Bước 4: Cập nhật trạng thái quỹ từ thiện
                nguoiThuHuong.TrangThai = "Đã Duyệt"; 
                nguoiThuHuong.IsApproved = true; 
                _luuTruNguoiThuHuong.UpdateNguoiThuHuong(nguoiThuHuong); 
            }
            else
            {
                throw new Exception("Giấy phép đã được duyệt.");
            }
        }
        //Từ chối giấy phép người thụ hưởng
        public void TuChoiDuyetGiayPhepNguoiThuHuong(int maNguoiThuHuong)
        {// Lấy thông tin quỹ từ thiện
            var nguoiThuHuong = _luuTruNguoiThuHuong.GetNguoiThuHuongById(maNguoiThuHuong);
            if (nguoiThuHuong == null)
            {
                throw new Exception("Không tìm thấy thông tin Người thụ hưởng.");
            }

            // Lấy thông tin giấy phép của quỹ từ thiện
            var giayPhep = _luuTruGiayPhep.GetGiayPhepById(nguoiThuHuong.MaGiayPhep ?? 0);
            if (giayPhep == null)
            {
                throw new Exception("Không tìm thấy giấy phép của Người thụ hưởng.");
            }

            // Kiểm tra nếu giấy phép đã duyệt
            if (giayPhep.TrangThai) 
            {
                giayPhep.TrangThai = false; 
                _luuTruGiayPhep.UpdateGiayPhep(giayPhep); 

                // Cập nhật trạng thái của quỹ từ thiện
                nguoiThuHuong.TrangThai = "Từ chối"; 
                nguoiThuHuong.IsApproved = false; 
                _luuTruNguoiThuHuong.UpdateNguoiThuHuong(nguoiThuHuong); 
            }
            else
            {
                throw new Exception("Giấy phép từ chối.");
            }
        }
        //Duyệt giấy phép Quỹ
        public void DuyetGiayPhepQuyTuThien(int maQuyTuThien)
        {
            // Bước 1: Lấy thông tin quỹ từ thiện và giấy phép
            var quyTuThien = _luuTruQuyTuThien.GetQuyTuThienById(maQuyTuThien);
            if (quyTuThien == null)
            {
                throw new Exception("Không tìm thấy thông tin quỹ từ thiện.");
            }

            // Bước 2: Kiểm tra xem quỹ từ thiện đã có giấy phép hay chưa
            var giayPhep = _luuTruGiayPhep.GetGiayPhepById(quyTuThien.MaGiayPhep ?? 0);
            if (giayPhep == null)
            {
                throw new Exception("Không tìm thấy giấy phép của quỹ từ thiện.");
            }

            // Bước 3: Kiểm tra nếu giấy phép chưa được duyệt
            if (!giayPhep.TrangThai)
            {
                // Duyệt giấy phép và cập nhật trạng thái giấy phép
                giayPhep.TrangThai = true;  // Đánh dấu giấy phép là đã duyệt
                _luuTruGiayPhep.UpdateGiayPhep(giayPhep);  // Cập nhật bảng GiayPhep

                // Bước 4: Cập nhật trạng thái quỹ từ thiện
                quyTuThien.TrangThai = "Đã Duyệt";  // Cập nhật trạng thái quỹ từ thiện
                quyTuThien.IsApproved = true;  // Đánh dấu quỹ từ thiện đã được duyệt
                _luuTruQuyTuThien.UpdateQuyTuThien(quyTuThien);  // Cập nhật bảng QuyTuThien
            }
            else
            {
                throw new Exception("Giấy phép đã được duyệt.");
            }
        }
        //Từ chối giấy phép Quỹ
        public void TuChoiDuyetGiayPhepQuyTuThien(int maQuyTuThien)
        {// Lấy thông tin quỹ từ thiện
            var quyTuThien = _luuTruQuyTuThien.GetQuyTuThienById(maQuyTuThien);
            if (quyTuThien == null)
            {
                throw new Exception("Không tìm thấy thông tin quỹ từ thiện.");
            }

            // Lấy thông tin giấy phép của quỹ từ thiện
            var giayPhep = _luuTruGiayPhep.GetGiayPhepById(quyTuThien.MaGiayPhep ?? 0);
            if (giayPhep == null)
            {
                throw new Exception("Không tìm thấy giấy phép của quỹ từ thiện.");
            }

            // Kiểm tra nếu giấy phép đã duyệt
            if (giayPhep.TrangThai) 
            {
                giayPhep.TrangThai = false; 
                _luuTruGiayPhep.UpdateGiayPhep(giayPhep); 

                // Cập nhật trạng thái của quỹ từ thiện
                quyTuThien.TrangThai = "Từ chối"; 
                quyTuThien.IsApproved = false; 
                _luuTruQuyTuThien.UpdateQuyTuThien(quyTuThien); 
            }
            else
            {
                throw new Exception("Giấy phép từ chối.");
            }
        }
       

    }
}
