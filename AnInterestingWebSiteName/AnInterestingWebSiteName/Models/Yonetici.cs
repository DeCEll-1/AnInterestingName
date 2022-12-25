using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnInterestingWebSiteName.Models
{
    public class Yonetici
    {

        public int ID { get; set; }


        public int YoneticiTur_ID { get; set; }

        [ForeignKey("YoneticiTur_ID")]

        public virtual YoneticiTur YoneticiTur { get; set; }


        public string Isim { get; set; }

        public string SoyIsim { get; set; }

        public string KullaniciAdi { get; set; }

        public string Sifre { get; set; }

        public string Mail { get; set; }

        public bool Aktif { get; set; }
    }
}