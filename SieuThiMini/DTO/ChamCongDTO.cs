using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThiMini.DTO
{
    internal class ChamCongDTO
    {
        public int maChamCong { get; set; }
        public int maNhanVien { get; set; }
        public DateTime ngayLamViec { get; set; }
        public int soGioLam { get; set; }

        public ChamCongDTO(int maChamCong, int maNhanVien, DateTime ngayLamViec, int soGioLam)
        {
            this.maChamCong = maChamCong;
            this.maNhanVien = maNhanVien;
            this.ngayLamViec = ngayLamViec;
            this.soGioLam = soGioLam;
        }
    }
}
