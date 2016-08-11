using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akaryakit
{
    public partial class frmAkaryakitMain : Form
    {
        public frmAkaryakitMain()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Utility.OpenForm<frmDosyaOku,frmAkaryakitMain>(new frmDosyaOku(), this as frmAkaryakitMain);


            //DataTable sheetData = new DataTable();
            //using (OleDbConnection conn = this.returnConnection())
            //{
            //    conn.Open();
            //    // retrieve the data using data adapter
            //    DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //    string Sheet1 = dtSchema.Rows[0].Field<string>("TABLE_NAME");

            //    DataTable dtColumns = conn.GetSchema("Columns", new string[] { null, null, Sheet1, null });
            //    List<string> columns = new List<string>();

            //}
        }


        private OleDbConnection returnConnection()
        {
            OleDbConnection conn=null;
            string ext = null;

            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (System.IO.Path.GetExtension(dlg.FileName) == ".xls")
                    ext = "Excel 8.0";
                if (System.IO.Path.GetExtension(dlg.FileName) == ".xlsx")
                    ext = "Excel 12.0";


                //conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + dlg.FileName + "; Extended Properties='" + ext + ";'");

                //conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dlg.FileName + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";");

                conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dlg.FileName + ";Extended Properties='" + ext + ";IMEX=1;'");

            }

            return conn;
        }

        private void frmAkaryakitMain_Load(object sender, EventArgs e)
        {
            

        }
    }
}
