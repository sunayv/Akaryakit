namespace Akaryakit.Migrations
{
    using CsvHelper;
    using System.Data.Entity.Migrations;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;

    internal sealed class Configuration : DbMigrationsConfiguration<AkaryakitModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AkaryakitModel context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //


            //string appPath = Path.GetDirectoryName(Application.ExecutablePath) + "\\Arac.csv";
            //if (System.IO.File.Exists(appPath))
            //{

            //    using (FileStream stream = new FileStream(appPath, FileMode.Open))
            //    {
            //        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            //        {
            //            CsvReader csvReader = new CsvReader(reader);
            //            csvReader.Configuration.RegisterClassMap<AracMap>();
            //            csvReader.Configuration.Delimiter = ";";
            //            csvReader.Configuration.WillThrowOnMissingField = false;
            //            var araclar = csvReader.GetRecords<Arac>().ToArray();
            //            context.Araclar.AddOrUpdate(c => c.AracNo, araclar);
            //        }
            //    }
            //    context.SaveChanges();
            //}
        }
    }
}
