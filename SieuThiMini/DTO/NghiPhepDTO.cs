using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThiMini.DTO
{
    internal class NghiPhepDTO
    {
        public int maNghiPhep { get; set; }
        public int maNhanVien { get; set; }
        public DateTime ngayBatDau { get; set; }
        public DateTime ngayKetThuc { get; set; }
        public string lyDo { get; set; }
        public string trangThai { get; set; }

        public NghiPhepDTO(int maNghiPhep, int maNhanVien, DateTime ngayBatDau, DateTime ngayKetThuc, string lyDo, string trangThai)
        {
            this.maNghiPhep = maNghiPhep;
            this.maNhanVien = maNhanVien;
            this.ngayBatDau = ngayBatDau;
            this.ngayKetThuc = ngayKetThuc;
            this.lyDo = lyDo;
            this.trangThai = trangThai;
        }
    }
}
