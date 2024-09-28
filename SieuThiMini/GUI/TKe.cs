using System;
using System.Data;
using System.Windows.Forms;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using SieuThiMini.DAO;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using SieuThiMini.DTO;

namespace SieuThiMini.GUI
{
    public partial class TKe : Form
    {
        private DataTable dataTable;

        public TKe()
        {
            InitializeComponent();
            LoadData();
            UpdateLabels();
            LoadData1();
            UpdateLabels1();

            // Create an instance of ThongKe form
            ThongKe thongKeForm = new ThongKe();

            // Set TopLevel to false to be able to add it to the TabPage
            thongKeForm.TopLevel = false;

            // Add the form to the TabPage
            tabPage2.Controls.Add(thongKeForm);

            // Call Show method to display the form
            thongKeForm.Show();


        }

        private void TKe_Load(object sender, EventArgs e)
        {
            LoadData();
            LoadData1();
        }

        private void LoadData()
        {
            try
            {
                string query = @"
                    SELECT sp.ma_san_pham, lsp.ten_loai, sp.ten_san_pham, SUM(cthd.so_luong) AS so_luong_da_ban
                    FROM san_pham sp
                    JOIN loai_san_pham lsp ON sp.ma_loai = lsp.ma_loai
                    JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
                    GROUP BY sp.ma_san_pham, lsp.ten_loai, sp.ten_san_pham";
                dataTable = DataProvider.Instance.ExecuteQuery(query);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadData1()
        {
            try
            {
                string query = @"
            SELECT lsp.ma_loai, lsp.ten_loai, SUM(cthd.so_luong) AS so_luong_da_ban
            FROM san_pham sp
            JOIN loai_san_pham lsp ON sp.ma_loai = lsp.ma_loai
            JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
            GROUP BY lsp.ma_loai, lsp.ten_loai";
                dataTable = DataProvider.Instance.ExecuteQuery(query);
                dataGridView2.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnReload_Click(object sender, EventArgs e)
        {
            // Tải lại dữ liệu
            LoadData();
            txtSearch.Clear();
            // Cập nhật lại label tổng xuất và đã xuất
            UpdateLabels();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Tạo hộp thoại lựa chọn tệp
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Chọn nơi lưu tệp Excel";
            saveFileDialog.FileName = "exported_data.xlsx"; // Tên mặc định của tệp

            // Hiển thị hộp thoại lựa chọn tệp và kiểm tra xem người dùng đã chọn đường dẫn lưu chưa
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo một tệp Excel mới
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");

                // Lấy dữ liệu từ DataGridView
                DataTable dt = (DataTable)dataGridView1.DataSource;

                // Thêm dòng tiêu đề vào tệp Excel
                IRow headerRow = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }

                // Ghi dữ liệu từ DataTable vào Excel (bắt đầu từ dòng 2)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow excelRow = sheet.CreateRow(i + 1); // +1 để bỏ qua dòng tiêu đề

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell excelCell = excelRow.CreateCell(j);
                        excelCell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                // Lưu tệp Excel vào đĩa tại đường dẫn được chọn
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                }

                MessageBox.Show("Dữ liệu đã được xuất ra tệp Excel và lưu tại đường dẫn: " + filePath);
            }
        }

        private void UpdateLabels()
        {
            // Tính toán tổng số lượng đã xuất từ cơ sở dữ liệu
            string totalQuery = "SELECT SUM(so_luong) AS TongSoLuongDaXuat FROM chi_tiet_hoa_don";
            DataTable totalResult = DataProvider.Instance.ExecuteQuery(totalQuery);
            int totalExported = Convert.ToInt32(totalResult.Rows[0]["TongSoLuongDaXuat"]);

            // Hiển thị tổng số lượng đã xuất
            lblTotalExported.Text = totalExported.ToString();

            // Tính toán và hiển thị số lượng đã xuất hiện tại (nếu có)
            int displayedExported = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["so_luong_da_ban"].Value != null)
                {
                    int quantity = Convert.ToInt32(row.Cells["so_luong_da_ban"].Value);
                    displayedExported += quantity;
                }
            }

            if (dataGridView1.RowCount > 0)
            {
                lblDisplayedExported.Text = displayedExported.ToString();
                lblDisplayedExported.Visible = true;
            }
            else
            {
                lblDisplayedExported.Visible = false;
            }
        }

        private void UpdateLabels1()
        {
            // Tính toán tổng số lượng đã xuất của loại sản phẩm hiện tại từ cơ sở dữ liệu
            if (dataGridView2.CurrentRow != null)
            {
                string currentProductType = dataGridView2.CurrentRow.Cells["ten_loai"].Value.ToString();
                string totalQuery = @"
                    SELECT SUM(cthd.so_luong) AS TongSoLuongDaXuat
                    FROM san_pham sp
                    JOIN loai_san_pham lsp ON sp.ma_loai = lsp.ma_loai
                    JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
                    WHERE lsp.ten_loai = @productType";
                        Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@productType", currentProductType }
                };
                        DataTable totalResult = DataProvider.Instance.ExecuteQuery(totalQuery, parameters);
                        int totalExported = Convert.ToInt32(totalResult.Rows[0]["TongSoLuongDaXuat"]);

                        // Hiển thị tổng số lượng đã xuất của loại sản phẩm hiện tại
                        label7.Text = totalExported.ToString();
                    }

                    // Tính toán và hiển thị số lượng đã xuất hiện tại (nếu có)
                    int label = 0;
                    foreach (DataGridViewRow row in dataGridView2.Rows)
                    {
                        if (row.Cells["so_luong_da_ban"].Value != null)
                        {
                            int quantity = Convert.ToInt32(row.Cells["so_luong_da_ban"].Value);
                            label += quantity;
                        }
                    }

                    if (dataGridView2.RowCount > 0)
                    {
                        label4.Text = label.ToString();
                        label4.Visible = true;
                    }
                    else
                    {
                        label4.Visible = false;
                    }
            }



        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            // Nếu txtSearch không rỗng, thực hiện tìm kiếm
            if (!string.IsNullOrEmpty(txtSearch.Text))
            {
                string searchValue = txtSearch.Text;
                string searchQuery = @"
            SELECT sp.ma_san_pham, lsp.ten_loai, sp.ten_san_pham, SUM(cthd.so_luong) AS so_luong_da_ban
            FROM san_pham sp
            JOIN loai_san_pham lsp ON sp.ma_loai = lsp.ma_loai
            JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
            WHERE sp.ten_san_pham LIKE @searchValue
            GROUP BY sp.ma_san_pham, lsp.ten_loai, sp.ten_san_pham";
                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@searchValue", "%" + searchValue + "%" }
        };
                dataTable = DataProvider.Instance.ExecuteQuery(searchQuery, parameters);
            }
            else
            {
                // Nếu txtSearch rỗng, tải lại tất cả dữ liệu
                string getAllQuery = @"
            SELECT sp.ma_san_pham, lsp.ten_loai, sp.ten_san_pham, SUM(cthd.so_luong) AS so_luong_da_ban
            FROM san_pham sp
            JOIN loai_san_pham lsp ON sp.ma_loai = lsp.ma_loai
            JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
            GROUP BY sp.ma_san_pham, lsp.ten_loai, sp.ten_san_pham";
                dataTable = DataProvider.Instance.ExecuteQuery(getAllQuery);
            }

            dataGridView1.DataSource = dataTable;
            UpdateLabels();
        }


        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Nếu textBox1 không rỗng, thực hiện tìm kiếm
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string searchValue = textBox1.Text;
                string searchQuery = @"
            SELECT lsp.ten_loai, SUM(cthd.so_luong) AS so_luong_da_ban
            FROM loai_san_pham lsp
            JOIN san_pham sp ON sp.ma_loai = lsp.ma_loai
            JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
            WHERE lsp.ten_loai LIKE @searchValue
            GROUP BY lsp.ten_loai";
                Dictionary<string, object> parameters = new Dictionary<string, object>
        {
            { "@searchValue", "%" + searchValue + "%" }
        };
                dataTable = DataProvider.Instance.ExecuteQuery(searchQuery, parameters);
            }
            else
            {
                // Nếu textBox1 rỗng, tải lại tất cả dữ liệu
                string getAllQuery = @"
            SELECT lsp.ten_loai, SUM(cthd.so_luong) AS so_luong_da_ban
            FROM loai_san_pham lsp
            JOIN san_pham sp ON sp.ma_loai = lsp.ma_loai
            JOIN chi_tiet_hoa_don cthd ON sp.ma_san_pham = cthd.ma_san_pham
            GROUP BY lsp.ten_loai";
                dataTable = DataProvider.Instance.ExecuteQuery(getAllQuery);
            }

            dataGridView2.DataSource = dataTable;
            UpdateLabels1();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // Tải lại dữ liệu
            LoadData1();
            textBox1.Clear();
            // Cập nhật lại label tổng xuất và đã xuất
            UpdateLabels1();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Tạo hộp thoại lựa chọn tệp
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files|*.xlsx";
            saveFileDialog.Title = "Chọn nơi lưu tệp Excel";
            saveFileDialog.FileName = "exported_data.xlsx"; // Tên mặc định của tệp

            // Hiển thị hộp thoại lựa chọn tệp và kiểm tra xem người dùng đã chọn đường dẫn lưu chưa
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;

                // Tạo một tệp Excel mới
                IWorkbook workbook = new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Sheet1");

                // Lấy dữ liệu từ DataGridView
                DataTable dt = (DataTable)dataGridView1.DataSource;

                // Thêm dòng tiêu đề vào tệp Excel
                IRow headerRow = sheet.CreateRow(0);
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    headerRow.CreateCell(i).SetCellValue(dt.Columns[i].ColumnName);
                }

                // Ghi dữ liệu từ DataTable vào Excel (bắt đầu từ dòng 2)
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow excelRow = sheet.CreateRow(i + 1); // +1 để bỏ qua dòng tiêu đề

                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        ICell excelCell = excelRow.CreateCell(j);
                        excelCell.SetCellValue(dt.Rows[i][j].ToString());
                    }
                }

                // Lưu tệp Excel vào đĩa tại đường dẫn được chọn
                using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(stream);
                }

                MessageBox.Show("Dữ liệu đã được xuất ra tệp Excel và lưu tại đường dẫn: " + filePath);
            }
        }




    }


}
