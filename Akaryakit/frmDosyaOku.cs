using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;
using System.Text.RegularExpressions;
using CsvHelper;
using System.Data.Entity.Migrations;
using System.Text;

namespace Akaryakit
{
    public partial class frmDosyaOku : Form
    {
        AkaryakitModel context = new AkaryakitModel();

        List<Dosya> liste;
        public frmDosyaOku()
        {
            InitializeComponent();
        }
                

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            

            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = true;
            dlg.Filter = "Excel Dosyası |*.xls;*.xlsx";
            liste= new List<Dosya>();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in dlg.FileNames)
                {
                    FileInfo f = new FileInfo(file);
                    liste.Add(new Dosya() { DosyaAdi = f.Name, DosyaTamAdi = f.FullName });
                }
                dosyaBindingSource.DataSource = liste;
            }
            
           
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

            for (int rowHandle = 0; rowHandle < gridView1.RowCount; rowHandle++)
            {
                Dosya d = gridView1.GetRow(rowHandle) as Dosya;
                gridView1.SetRowCellValue(rowHandle, "Resim", Images1.Images[0]);

                using (OleDbConnection conn = new OleDbConnection())
                {
                    
                    string Import_FileName = d.DosyaTamAdi;
                    string fileExtension = Path.GetExtension(Import_FileName);
                    if (fileExtension == ".xls")
                    {
                        conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                        ReadXLS(conn);
                    }

                    if (fileExtension == ".xlsx")
                    {
                        conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                        ReadXLSX(conn);
                    }
                        

                    
                    

                }
            }


        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            liste.Clear();
            dosyaBindingSource.Clear();
            dosyaBindingSource.ResetBindings(false);
        }

        private void ReadXLS(OleDbConnection conn)
        {
            conn.Open();
            DataTable dt = new DataTable();
            DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheetName = dtSchema.Rows[0].Field<string>("TABLE_NAME");

            using (OleDbCommand comm = new OleDbCommand())
            {
                comm.CommandText = "Select * from [" + sheetName + "]";
                comm.Connection = conn;
                using (OleDbDataAdapter da = new OleDbDataAdapter())
                {
                    da.SelectCommand = comm;
                    da.Fill(dt);
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                Tuketim t = new Tuketim();

                foreach (PropertyInfo pinfo in t.GetType().GetProperties())
                {
                    pinfo.SetValue(t, row.Field<string>(pinfo.Name));
                }
            }
        }
        private void ReadXLSX(OleDbConnection conn)
        {
            conn.Open();
            DataTable dt = new DataTable();
            DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            string sheetName = dtSchema.Rows[0].Field<string>("TABLE_NAME");

            using (OleDbCommand comm = new OleDbCommand())
            {
                comm.CommandText = "Select * from [" + sheetName + "]";
                comm.Connection = conn;
                using (OleDbDataAdapter da = new OleDbDataAdapter())
                {
                    da.SelectCommand = comm;
                    da.Fill(dt);
                }
            }

            foreach (DataRow row in dt.Rows)
            {
                Tuketim t = new Tuketim();

                var plaka = row.Field<string>("Plaka");
                var s = plaka.Substring(0,2)+" "+plaka.Substring(2, 3) + " "+plaka.Substring(5, 2);



                foreach (PropertyInfo pinfo in t.GetType().GetProperties())
                {
                    pinfo.SetValue(t, row.Field<string>(pinfo.Name));
                }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Arac.csv";
            if (System.IO.File.Exists(appPath))
            {

                using (FileStream stream = new FileStream(appPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        CsvReader csvReader = new CsvReader(reader);
                        csvReader.Configuration.RegisterClassMap<AracMap>();
                        csvReader.Configuration.Delimiter = ";";
                        csvReader.Configuration.WillThrowOnMissingField = false;
                        var araclar = csvReader.GetRecords<Arac>().ToArray();
                        context.Araclar.AddOrUpdate(c => c.AracNo, araclar);
                    }
                }
                context.SaveChanges();
            }
        }
    }
}
