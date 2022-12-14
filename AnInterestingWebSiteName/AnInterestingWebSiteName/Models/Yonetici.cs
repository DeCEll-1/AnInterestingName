using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage ="Bu Alan Zorunludur")]
        [StringLength(63,ErrorMessage ="Bu Alan En Fazla 63 Karakter Olabilir")]
        public string Isim { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(63, ErrorMessage = "Bu Alan En Fazla 63 Karakter Olabilir")]
        public string SoyIsim { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(63, ErrorMessage = "Bu Alan En Fazla 63 Karakter Olabilir")]
        public string KullaniciAdi { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(20,MinimumLength =8, ErrorMessage = "Bu Alan En Az 8, En Fazla 20 Karakter Olabilir")]
        public string Sifre { get; set; }

        [Required(ErrorMessage = "Bu Alan Zorunludur")]
        [StringLength(255, ErrorMessage = "Bu Alan En Fazla 255 Karakter Olabilir")]
        public string Mail { get; set; }


        public bool Aktif { get; set; }
    }
}