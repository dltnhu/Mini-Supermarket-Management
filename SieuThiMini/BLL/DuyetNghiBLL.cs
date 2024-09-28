using SieuThiMini.DAO;
using SieuThiMini.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SieuThiMini.BLL
{
    class DuyetNghiBLL
    {
        private DuyetNghiDAO DAO;
        private List<NghiPhepDTO> listDTO;

        // DuyetNghiBLL
        public DataTable GetDataTable()
        {
            return this.DAO.SelectAll2();
        }

        public DuyetNghiBLL()
        {
            this.DAO = new DuyetNghiDAO();
        }

        public List<NghiPhepDTO> GetList()
        {
            return this.DAO.SelectAll();
        }

        public int Insert(NghiPhepDTO dto)
        {
            return this.DAO.Insert(dto);
        }

        public void Update(NghiPhepDTO dto)
        {
            this.DAO.Update(dto);
        }

        public void Delete(int maNghiPhep)
        {
            this.DAO.Delete(maNghiPhep);
        }

        public List<NghiPhepDTO> SelectByMaNhanVien(int maNhanVien)
        {
            return this.DAO.SelectByMaNhanVien(maNhanVien);
        }

        public NghiPhepDTO SelectByMaNghiPhep(int maNghiPhep)
        {
            return this.DAO.SelectByMaNghiPhep(maNghiPhep);
        }

        // DuyetNghiBLL
        public void Approve(int maNghiPhep)
        {
            this.DAO.Approve(maNghiPhep);
        }

        public void Reject(int maNghiPhep)
        {
            this.DAO.Reject(maNghiPhep);
        }

    }
}
