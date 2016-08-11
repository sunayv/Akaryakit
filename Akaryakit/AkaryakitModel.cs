namespace Akaryakit
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Drawing;
    

    public class AkaryakitModel : DbContext
    {
        // Your context has been configured to use a 'AkaryakitModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Akaryakit.AkaryakitModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AkaryakitModel' 
        // connection string in the application configuration file.
        public AkaryakitModel()
            : base("name=AkaryakitConn")
        {
            Database.SetInitializer<AkaryakitModel>(new CreateDatabaseIfNotExists<AkaryakitModel>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AkaryakitModel, Akaryakit.Migrations.Configuration>("AkaryakitConn"));

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Arac> Araclar { get; set; }
        public virtual DbSet<Tuketim> Tuketimler { get; set; }
        public virtual DbSet<Fiyat> Fiyatlar { get; set; }
    }

    public class Arac
    {
        [Key]
        public int AracId { get; set; }
        public string Plaka { get; set; }
        public string GNo { get; set; }
        public string AracNo { get; set; }
        public string Model { get; set; }
        public string Marka { get; set; }
        public string Uzunluk { get; set; }

        public virtual ICollection<Tuketim> Tuketim { get; set; }
    }

    public class Tuketim
    {
        [Key]
        public int TuketimId { get; set; }
        
        public DateTime Tarih { get; set; }

        public int AracId { get; set; }
        public virtual Arac Arac { get; set; }
        
        public decimal Litre { get; set; }
        public int Kilometre { get; set; }

    }

    public class Fiyat
    {
        [Key]
        public int FiyatId { get; set; }
        public DateTime Tarih { get; set; }
        public decimal BirimFiyat { get; set; }
    }


    public class Dosya
    {
        [Key]
        public int DosyaId { get; set; }
        public Image Durum { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaTamAdi { get; set; }
    }
    

}