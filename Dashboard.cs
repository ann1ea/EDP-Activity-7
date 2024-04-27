using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using MySql.Data.MySqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;

namespace AZUL_Bookstore
{
    public partial class Dashboard : Form
    {

        // Connection string for MySQL database
        private string connectionString = "Server=127.0.0.1;Port=3306;Database=bookstore;Uid=root;Pwd=BicolU4851;";

        public Dashboard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Close the current Dashboard form
            this.Close();

            // Show the Login form
            Login loginForm = new Login();
            loginForm.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Retrieve data from the 'stocks' table
            DataTable stocksData = RetrieveStocksData();

            // Generate report in Excel format
            GenerateExcelReport(stocksData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Retrieve data from the 'customers' table
            DataTable customersData = RetrieveCustomersData();

            // Generate report in Excel format
            GenerateExcelReport(customersData);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Retrieve data from the 'employees' table
            DataTable employeesData = RetrieveEmployeesData();

            // Generate report in Excel format
            GenerateExcelReport(employeesData);
        }

        private DataTable RetrieveCustomersData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM customers";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        private DataTable RetrieveEmployeesData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM employees";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }

        private DataTable RetrieveStocksData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM stocks";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }
            }

            return dataTable;
        }


        private void GenerateExcelReport(DataTable data)
        {
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = true;
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];

            // Add header (company name and logo)
            worksheet.Cells[1, 1] = "A.A. Bookstore";
            worksheet.Shapes.AddPicture(@"C:\Users\EMBR5-AZUL\Desktop\Annie\AA College\YEAR 3\2nd Sem\IT 120 - Event Driven Programming\database_imgs\booklogo.jpg", MsoTriState.msoFalse, MsoTriState.msoCTrue, 0, 15, 30, 30);

            // Add data from DataTable to Excel worksheet
            for (int i = 0; i < data.Rows.Count; i++)
            {
                for (int j = 0; j < data.Columns.Count; j++)
                {
                    worksheet.Cells[i + 4, j + 1] = data.Rows[i][j].ToString();
                }
            }

            // Get the range of the cell containing the text "Signature:"
            Excel.Range signatureTextCell = worksheet.Cells[data.Rows.Count + 6, 1];

            // Add the text "Signature:"
            signatureTextCell.Value = "Signature:";

            // Get the position of the text cell
            float textLeft = (float)signatureTextCell.Left;
            float textTop = (float)signatureTextCell.Top;

            // Get the range for the signature picture based on the position of the text cell
            Excel.Range signaturePictureRange = worksheet.Range[worksheet.Cells[data.Rows.Count + 6, 1], worksheet.Cells[data.Rows.Count + 6, 1]];

            // Add the picture to the worksheet and align it with the cell
            Excel.Shape picture = worksheet.Shapes.AddPicture(@"C:\Users\EMBR5-AZUL\Desktop\Annie\AA College\YEAR 3\2nd Sem\IT 120 - Event Driven Programming\database_imgs\ANNIE_A_SIGN.png", MsoTriState.msoFalse, MsoTriState.msoCTrue, textLeft + 100, textTop, 100, 50);


            // Create Sheet 2 with a graph of the data
            Excel.Worksheet worksheet2 = (Excel.Worksheet)workbook.Worksheets.Add();
            Excel.ChartObjects chartObjects = (Excel.ChartObjects)worksheet2.ChartObjects(Type.Missing);
            Excel.ChartObject chartObject = chartObjects.Add(100, 100, 300, 300);
            Excel.Chart chart = chartObject.Chart;
            Excel.Range range = worksheet.Range[worksheet.Cells[4, 1], worksheet.Cells[data.Rows.Count + 3, data.Columns.Count]];
            chart.SetSourceData(range, Type.Missing);
            chart.ChartType = Excel.XlChartType.xlColumnClustered;

        
        }
    }


}
