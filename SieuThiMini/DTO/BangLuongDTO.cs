using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThiMini.DTO
{
    internal class BangLuongDTO
    {
        public int maBangLuong { get; set; }
        public int maNhanVien { get; set; }
        public string tenNhanVien { get; set; }
        public string tenQuyen { get; set; }
        public DateTime thangLuong { get; set; }
        public DateTime ngayBatDauTinhLuong { get; set; }
        public DateTime ngayKetThucTinhLuong { get; set; }
        public int soNgayLamViec { get; set; }
        public int soNgayNghiCoPhep { get; set; }
        public int soNgayNghiKhongPhep { get; set; }
        public decimal luongCoBan { get; set; }
        public decimal thuong { get; set; }
        public decimal tongLuong { get; set; }

        public BangLuongDTO(int maBangLuong, int maNhanVien, string tenNhanVien, string tenQuyen, DateTime thangLuong, DateTime ngayBatDauTinhLuong, DateTime ngayKetThucTinhLuong, int soNgayLamViec, int soNgayNghiCoPhep, int soNgayNghiKhongPhep, decimal luongCoBan, decimal thuong, decimal tongLuong)
        {
            this.maBangLuong = maBangLuong;
            this.maNhanVien = maNhanVien;
            this.tenNhanVien = tenNhanVien;
            this.tenQuyen = tenQuyen;
            this.thangLuong = thangLuong;
            this.ngayBatDauTinhLuong = ngayBatDauTinhLuong;
            this.ngayKetThucTinhLuong = ngayKetThucTinhLuong;
            this.soNgayLamViec = soNgayLamViec;
            this.soNgayNghiCoPhep = soNgayNghiCoPhep;
            this.soNgayNghiKhongPhep = soNgayNghiKhongPhep;
            this.luongCoBan = luongCoBan;
            this.thuong = thuong;
            this.tongLuong = tongLuong;
        }
    }
}
