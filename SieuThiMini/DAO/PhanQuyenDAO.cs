using SieuThiMini.DTO;

using System.Collections.Generic;
using System.Data;

namespace SieuThiMini.DAO
{
    class PhanQuyenDAO: DataConection
    {
		public PhanQuyenDAO() { }
        public List<PhanQuyenDTO> SelectAll()
        {
            List<PhanQuyenDTO> dtoList = new List<PhanQuyenDTO>();

            string queryStr = "select * from phan_quyen";

            DataTable result = DataProvider.Instance.ExecuteQuery(queryStr);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DataRow datarow = result.Rows[i];

                int maQuyen = int.Parse(datarow.ItemArray[0].ToString());
                string tenQuyen = datarow.ItemArray[1].ToString();
                decimal luongCoBan = decimal.Parse(datarow.ItemArray[2].ToString());

                PhanQuyenDTO dto = new PhanQuyenDTO(maQuyen, tenQuyen, luongCoBan);
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public int Insert(PhanQuyenDTO target)
        {
            Moketnoi();
            string insertStr = $"insert into phan_quyen values ('', '{target.tenQuyen}', '{target.luongCoBan}')";

            return DataProvider.Instance.ExecuteNonQuery(insertStr);
        }

        public void Update(PhanQuyenDTO target)
        {
            Moketnoi();
            string updateStr = "update phan_quyen set ";
            updateStr += $"ten_quyen = '{target.tenQuyen}', ";
            updateStr += $"luong_co_ban = '{target.luongCoBan}' ";
            updateStr += $"where ma_quyen='{target.maQuyen}'";

            DataProvider.Instance.ExecuteNonQuery(updateStr);
        }

        public int Delete(string id)
        {
            Moketnoi();
            string deleteStr = $"delete from phan_quyen where ma_quyen = '{id}'";

            return DataProvider.Instance.ExecuteNonQuery(deleteStr);
        }

        public List<PhanQuyenDTO> getPQByMaPQ(string ma_phan_quyen)
        {
            List<PhanQuyenDTO> dtoList = new List<PhanQuyenDTO>();

            string queryStr = $"select * from phan_quyen where ma_quyen='{ma_phan_quyen}'";

            DataTable result = DataProvider.Instance.ExecuteQuery(queryStr);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DataRow datarow = result.Rows[i];

                int maQuyen = int.Parse(datarow.ItemArray[0].ToString());
                string tenQuyen = datarow.ItemArray[1].ToString();
                decimal luongCoBan = decimal.Parse(datarow.ItemArray[2].ToString());

                PhanQuyenDTO dto = new PhanQuyenDTO(maQuyen, tenQuyen, luongCoBan);
                dtoList.Add(dto);
            }

            return dtoList;
        }

        public List<PhanQuyenDTO> getPQByNamePQ(string ten_quyen)
        {
            List<PhanQuyenDTO> dtoList = new List<PhanQuyenDTO>();

            string queryStr = $"select * from phan_quyen where ten_quyen like '%{ten_quyen}%'";

            DataTable result = DataProvider.Instance.ExecuteQuery(queryStr);
            for (int i = 0; i < result.Rows.Count; i++)
            {
                DataRow datarow = result.Rows[i];

                int maQuyen = int.Parse(datarow.ItemArray[0].ToString());
                string tenQuyen = datarow.ItemArray[1].ToString();
                decimal luongCoBan = decimal.Parse(datarow.ItemArray[2].ToString());

                PhanQuyenDTO dto = new PhanQuyenDTO(maQuyen, tenQuyen, luongCoBan);
                dtoList.Add(dto);
            }

            return dtoList;
        }

    }
}
