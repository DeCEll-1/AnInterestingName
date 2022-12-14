using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnInterestingWebSiteName.Models
{
    public class Kategori
    {
        public int ID { get; set; }

        public int? UstKategori_ID { get; set; }

        [ForeignKey("UstKategori_ID")]
        public virtual Kategori UstKategori { get; set; }

        [Display(Name ="Ad")]
        [Required(ErrorMessage ="Bu Alan Boş Bırakılamaz")]
        [StringLength(63,ErrorMessage ="Bu Alan En Fazla 63 Karakter Olabilir")]
        public string Ad { get; set; }

        [Display(Name ="Açıklama")]
        [DataType(DataType.MultilineText)]
        public string Aciklama { get; set; }

        public virtual ICollection<Kategori> Kategoris { get; set; }

    }
}