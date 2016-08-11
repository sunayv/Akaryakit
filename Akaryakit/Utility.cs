using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akaryakit
{
    class Utility
    {
        public static Form IsFormOpen(Type formType)
        {
            foreach (Form form in Application.OpenForms)
                if (form.GetType().Name == formType.Name)
                    return form;
            return null;
        }

        public static void OpenForm<T, T1>(T myForm, T1 mainForm)
            where T : Form
            where T1 : Form
        {
            if (!Application.OpenForms.OfType<T>().Any<T>())
            {
                myForm.MdiParent = mainForm;
                myForm.Show();
            }
            else
            {
                Application.OpenForms[myForm.Name].Activate();
            }
        }


        public static OleDbConnection returnConnection(string filename)
        {
            OleDbConnection conn = null;
            string ext = null;


            if (System.IO.Path.GetExtension(filename) == ".xls")
                ext = "Excel 8.0";
            if (System.IO.Path.GetExtension(filename) == ".xlsx")
                ext = "Excel 12.0";

            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filename + ";Extended Properties='" + ext + ";IMEX=1;'");


            return conn;
        }

    }
}
