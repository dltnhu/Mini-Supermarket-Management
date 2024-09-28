using SieuThiMini.DAO;
using SieuThiMini.DTO;
using System;
using System.Collections.Generic;
using System.Data;

class DuyetNghiDAO : DataConection
{
    public DuyetNghiDAO() { }

    // DuyetNghiDAO
    public DataTable SelectAll2()
    {
        string queryStr = "select * from nghi_phep";
        return DataProvider.Instance.ExecuteQuery(queryStr);
    }


    public List<NghiPhepDTO> SelectAll()
    {
        List<NghiPhepDTO> dtoList = new List<NghiPhepDTO>();

        string queryStr = "select * from nghi_phep";

        DataTable result = null;
        try
        {
            result = DataProvider.Instance.ExecuteQuery(queryStr);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error executing query: " + ex.Message);
            return dtoList; // Return the empty list
        }

        for (int i = 0; i < result.Rows.Count; i++)
        {
            DataRow datarow = result.Rows[i];

            int maNghiPhep = int.Parse(datarow.ItemArray[0].ToString());
            int maNhanVien = int.Parse(datarow.ItemArray[1].ToString());
            DateTime ngayBatDau = DateTime.Parse(datarow.ItemArray[2].ToString());
            DateTime ngayKetThuc = DateTime.Parse(datarow.ItemArray[3].ToString());
            string lyDo = datarow.ItemArray[4].ToString();
            string trangThai = datarow.ItemArray[5].ToString();

            NghiPhepDTO dto = new NghiPhepDTO(maNghiPhep, maNhanVien, ngayBatDau, ngayKetThuc, lyDo, trangThai);
            dtoList.Add(dto);
        }

        return dtoList;
    }

    public int Insert(NghiPhepDTO target)
    {
        Moketnoi();
        string insertStr = $"insert into nghi_phep values ('', '{target.maNhanVien}', '{target.ngayBatDau.ToString("yyyy-MM-dd")}', " +
            $"'{target.ngayKetThuc.ToString("yyyy-MM-dd")}', '{target.lyDo}', '{target.trangThai}')";

        return DataProvider.Instance.ExecuteNonQuery(insertStr);
    }

    public void Update(NghiPhepDTO target)
    {
        Moketnoi();
        string updateStr = "update nghi_phep set ";
        updateStr += $"ma_nhan_vien = '{target.maNhanVien}', ";
        updateStr += $"ngay_bat_dau = '{target.ngayBatDau.ToString("yyyy-MM-dd")}', ";
        updateStr += $"ngay_ket_thuc = '{target.ngayKetThuc.ToString("yyyy-MM-dd")}', ";
        updateStr += $"ly_do = '{target.lyDo}', ";
        updateStr += $"trang_thai = '{target.trangThai}' ";
        updateStr += $"where ma_nghi_phep='{target.maNghiPhep}'";

        DataProvider.Instance.ExecuteNonQuery(updateStr);
    }

    public int Delete(int id)
    {
        Moketnoi();
        string deleteStr = $"delete from nghi_phep where ma_nghi_phep = '{id}'";

        return DataProvider.Instance.ExecuteNonQuery(deleteStr);
    }

    public List<NghiPhepDTO> SelectByMaNhanVien(int ma_nhan_vien)
    {
        List<NghiPhepDTO> dtoList = new List<NghiPhepDTO>();

        string queryStr = $"select * from nghi_phep where ma_nhan_vien='{ma_nhan_vien}'";

        DataTable result = DataProvider.Instance.ExecuteQuery(queryStr);
        for (int i = 0; i < result.Rows.Count; i++)
        {
            DataRow datarow = result.Rows[i];

            int maNghiPhep = int.Parse(datarow.ItemArray[0].ToString());
            int maNhanVien = int.Parse(datarow.ItemArray[1].ToString());
            DateTime ngayBatDau = DateTime.Parse(datarow.ItemArray[2].ToString());
            DateTime ngayKetThuc = DateTime.Parse(datarow.ItemArray[3].ToString());
            string lyDo = datarow.ItemArray[4].ToString();
            string trangThai = datarow.ItemArray[5].ToString();

            NghiPhepDTO dto = new NghiPhepDTO(maNghiPhep, maNhanVien, ngayBatDau, ngayKetThuc, lyDo, trangThai);
            dtoList.Add(dto);
        }

        return dtoList;
    }

    public NghiPhepDTO SelectByMaNghiPhep(int ma_nghi_phep)
    {
        string queryStr = $"select * from nghi_phep where ma_nghi_phep='{ma_nghi_phep}'";

        DataTable result = DataProvider.Instance.ExecuteQuery(queryStr);
        if (result.Rows.Count > 0)
        {
            DataRow datarow = result.Rows[0];

            int maNghiPhep = int.Parse(datarow.ItemArray[0].ToString());
            int maNhanVien = int.Parse(datarow.ItemArray[1].ToString());
            DateTime ngayBatDau = DateTime.Parse(datarow.ItemArray[2].ToString());
            DateTime ngayKetThuc = DateTime.Parse(datarow.ItemArray[3].ToString());
            string lyDo = datarow.ItemArray[4].ToString();
            string trangThai = datarow.ItemArray[5].ToString();

            NghiPhepDTO dto = new NghiPhepDTO(maNghiPhep, maNhanVien, ngayBatDau, ngayKetThuc, lyDo, trangThai);
            return dto;
        }

        return null;
    }

    // DuyetNghiDAO
    public void Approve(int maNghiPhep)
    {
        Moketnoi();
        string updateStr = $"UPDATE nghi_phep SET trang_thai = 'Được chấp nhận' WHERE ma_nghi_phep = {maNghiPhep}";
        DataProvider.Instance.ExecuteNonQuery(updateStr);
    }

    public void Reject(int maNghiPhep)
    {
        Moketnoi();
        string updateStr = $"UPDATE nghi_phep SET trang_thai = 'Bị từ chối' WHERE ma_nghi_phep = {maNghiPhep}";
        DataProvider.Instance.ExecuteNonQuery(updateStr);
    }

}
