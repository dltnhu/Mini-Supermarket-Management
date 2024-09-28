using MySql.Data.MySqlClient;
using SieuThiMini.BLL;
using SieuThiMini.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SieuThiMini.GUI
{
    public partial class DuyetNghi : Form
    {
        private DuyetNghiBLL bll = new DuyetNghiBLL();
        public DuyetNghi()
        {
            InitializeComponent();
            btnXoa.Visible = false;
            btnSua.Visible = false;
            btnKhoiPhuc.Visible = false;
        }


        private void DuyetNghi_Load(object sender, EventArgs e)
        {
            try
            {
                // Get the list of NghiPhepDTO from the BLL
                List<NghiPhepDTO> list = bll.GetList();

                if (list != null)
                {
                    // Create a new BindingSource
                    BindingSource source = new BindingSource();

                    // Set the DataSource of the BindingSource to the list
                    source.DataSource = list;

                    // Set the DataSource of the DataGridView to the BindingSource
                    dataGridViewNghiPhep.DataSource = source;

                    // Set the width of the maNhanVien column
                    dataGridViewNghiPhep.Columns["maNhanVien"].Width = 300;

                    // Set the header text of the columns
                    dataGridViewNghiPhep.Columns["maNghiPhep"].HeaderText = "Mã nghỉ phép";
                    dataGridViewNghiPhep.Columns["maNhanVien"].HeaderText = "Mã nhân viên";
                    dataGridViewNghiPhep.Columns["ngayBatDau"].HeaderText = "Ngày bắt đầu";
                    dataGridViewNghiPhep.Columns["ngayKetThuc"].HeaderText = "Ngày kết thúc";
                    dataGridViewNghiPhep.Columns["lyDo"].HeaderText = "Lý do";
                    dataGridViewNghiPhep.Columns["trangThai"].HeaderText = "Trạng thái";

                    // Simulate a cell click event to load the first row into the text boxes
                    if (dataGridViewNghiPhep.Rows.Count > 0)
                    {
                        var datagridviewArgs = new DataGridViewCellEventArgs(0, 0);
                        dataGridViewNghiPhep_CellClick(null, datagridviewArgs);
                    }
                }
                else
                {
                    MessageBox.Show("Error: List is null", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Display any errors that occur during data loading
                MessageBox.Show("Error loading data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            ThemNghiPhep tl = new ThemNghiPhep();
            tl.ShowDialog();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaNghiPhep.Text != "")
            {
                int maNghiPhep = int.Parse(txtMaNghiPhep.Text.ToString());
                bll.Delete(maNghiPhep);

                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                btnRefesh_Click(null, null);
                var datagridviewArgs = new DataGridViewCellEventArgs(0, 0);
                dataGridViewNghiPhep_CellClick(null, datagridviewArgs);
            }
            else
            {
                MessageBox.Show("Hãy chọn 1 đơn nghỉ phép để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {

        }


        private void btnSua_Click(object sender, EventArgs e)
        {

            if (txtMaNghiPhep.Text == "")
            {
                MessageBox.Show("Hãy chọn 1 đơn nghỉ phép để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btnHuy.Visible = true;
                btnLuu.Visible = true;
                txtLyDo.Enabled = true;
                dateTimePickerNgayBatDau.Enabled = true;
                dateTimePickerNgayKetThuc.Enabled = true;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            int maNghiPhep = int.Parse(txtMaNghiPhep.Text);
            string lyDo = txtLyDo.Text;
            DateTime ngayBatDau = dateTimePickerNgayBatDau.Value;
            DateTime ngayKetThuc = dateTimePickerNgayKetThuc.Value;

            if (lyDo != "")
            {
                NghiPhepDTO nghiPhep = new NghiPhepDTO(maNghiPhep, 0, ngayBatDau, ngayKetThuc, lyDo, "Đang chờ duyệt");
                DuyetNghiBLL bLL = new DuyetNghiBLL();
                bLL.Update(nghiPhep);

                MessageBox.Show("Sửa thông tin nghỉ phép thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnHuy_Click(null, null);
                btnRefesh_Click(null, null);
            }
            else
            {
                MessageBox.Show("Không được để trống lý do!", "Thông tin không hợp lệ! Hãy kiểm tra lại", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            int index = dataGridViewNghiPhep.SelectedRows[0].Index;
            var datagridviewArgs = new DataGridViewCellEventArgs(0, index);
            dataGridViewNghiPhep_CellClick(null, datagridviewArgs);
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            dataGridViewNghiPhep.DataSource = bll.GetList();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            String timkiem = txtTimKiem.Text;
            List<NghiPhepDTO> list = bll.SelectByMaNhanVien(int.Parse(timkiem));
            dataGridViewNghiPhep.DataSource = list;
        }
    


    private void lblTimKiem_Click(object sender, EventArgs e)
        {

        }

        private void txtTenLoaiSp_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaLoaiSp_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridViewLoaiSanPham_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        // DuyetNghi
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridViewNghiPhep.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridViewNghiPhep.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewNghiPhep.Rows[selectedRowIndex];
                int maNghiPhep = int.Parse(selectedRow.Cells["maNghiPhep"].Value.ToString());

                bll.Approve(maNghiPhep);

                MessageBox.Show("Đã duyệt nghỉ phép!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DuyetNghi_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Hãy chọn 1 đơn nghỉ phép để duyệt!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridViewNghiPhep.SelectedRows.Count > 0)
            {
                int selectedRowIndex = dataGridViewNghiPhep.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridViewNghiPhep.Rows[selectedRowIndex];
                int maNghiPhep = int.Parse(selectedRow.Cells["maNghiPhep"].Value.ToString());

                bll.Reject(maNghiPhep);

                MessageBox.Show("Đã từ chối nghỉ phép!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DuyetNghi_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Hãy chọn 1 đơn nghỉ phép để từ chối!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void dataGridViewNghiPhep_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnHuy.Visible = false;
            btnLuu.Visible = false;
            txtLyDo.Enabled = false;
            dateTimePickerNgayBatDau.Enabled = false;
            dateTimePickerNgayKetThuc.Enabled = false;

            if (e.RowIndex == -1) return;

            DataGridViewRow row = new DataGridViewRow();
            row = dataGridViewNghiPhep.Rows[e.RowIndex];

            txtLyDo.Text = Convert.ToString(row.Cells["lyDo"].Value);
            txtMaNghiPhep.Text = Convert.ToString(row.Cells["maNghiPhep"].Value);
            dateTimePickerNgayBatDau.Value = Convert.ToDateTime(row.Cells["ngayBatDau"].Value);
            dateTimePickerNgayKetThuc.Value = Convert.ToDateTime(row.Cells["ngayKetThuc"].Value);
        }

        private void dataGridViewNghiPhep_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
