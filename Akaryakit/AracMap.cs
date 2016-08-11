using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akaryakit
{
    public sealed class AracMap : CsvClassMap<Arac>
    {
        public AracMap()
        {
            Map(m => m.Plaka).Index(0);
            Map(m => m.GNo).Index(1);
            Map(m => m.AracNo).Index(2);
            Map(m => m.Model).Index(3);
            Map(m => m.Marka).Index(4);
            Map(m => m.Uzunluk).Index(5);
            Map(m => m.AracId).Ignore();
            Map(m => m.Tuketim).Ignore();
        }
    }
}
