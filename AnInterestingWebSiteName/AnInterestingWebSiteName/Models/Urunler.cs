using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AnInterestingWebSiteName.Models
{
    public class Urunler
    {

        public int ID { get; set; }

        public int Kategori_ID { get; set; }

        [ForeignKey("Kategori_ID")]
        public virtual Kategori Kategori { get; set; }

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage ="Lütfen Bu Alanı Doldurunuz")]
        [StringLength(64,ErrorMessage ="Bu Alan En Fazla 64 Karakter Olabilir")]
        public string Ad { get; set; }

        [Display(Name ="Ürün Açıklaması")]
        [Required(ErrorMessage = "Lütfen Bu Alanı Doldurunuz")]
        public string Aciklama { get; set; }

        [Display(Name = "Ürünün Fiyatı")]
        [Required(ErrorMessage = "Lütfen Bu Alanı Doldurunuz")]
        public int Fiyat { get; set; }

        [Display(Name ="İkon")]
        [StringLength(155)]
        public string IkonYolu { get; set; }

        [Display(Name = "Tam Resim")]
        [StringLength(155)]
        public string TamResimYolu { get; set; }

        public bool Aktifmi { get; set; }
    }
}