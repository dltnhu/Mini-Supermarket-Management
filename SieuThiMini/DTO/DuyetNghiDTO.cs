using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThiMini.DTO
{
    internal class DuyetNghiDTO
    {
        public int MaNghiPhep { get; set; }
        public int MaNhanVien { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }

        public DuyetNghiDTO(int maNghiPhep, int maNhanVien, DateTime ngayBatDau, DateTime ngayKetThuc, string lyDo, string trangThai)
        {
            MaNghiPhep = maNghiPhep;
            MaNhanVien = maNhanVien;
            NgayBatDau = ngayBatDau;
            NgayKetThuc = ngayKetThuc;
            LyDo = lyDo;
            TrangThai = trangThai;
        }
    }
}
