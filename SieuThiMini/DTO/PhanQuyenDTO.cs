using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SieuThiMini.DTO
{
    class PhanQuyenDTO
    {
        public int maQuyen { get; set; }
        public string tenQuyen { get; set; }
        public decimal luongCoBan { get; set; }

        public PhanQuyenDTO(int maQuyen, string tenQuyen, decimal luongCoBan)
        {
            this.maQuyen = maQuyen;
            this.tenQuyen = tenQuyen;
            this.luongCoBan = luongCoBan;
        }
    }
}
