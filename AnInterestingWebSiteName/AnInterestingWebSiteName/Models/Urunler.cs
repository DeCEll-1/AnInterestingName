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

        [Display(Name = "Ürün Adı")]
        [Required(ErrorMessage ="Lütfen Bu Alanı Doldurunuz")]
        [StringLength(64,ErrorMessage ="Bu Alan En Fazla 64 Karakter Olabilir")]
        public string Ad { get; set; }

        [Display(Name ="Ürün Açıklaması")]
        [Required(ErrorMessage = "Lütfen Bu Alanı Doldurunuz")]
        public string Aciklama { get; set; }

        [Display(Name = "Ürünün Fiyatı")]
        [Required(ErrorMessage = "Lütfen Bu Alanı Doldurunuz")]
        public double Fiyat { get; set; }

        [Range(0,100)]
        [Display(Name = "İndirim")]
        public byte? Indirim { get; set; }

        [Display(Name ="İndirimli Fiyat")]
        public double? IndirimliFiyat { get; set; }

        [Required( ErrorMessage ="Lütfen Fotoğraf Giriniz")]
        [Display(Name ="İkon")]
        [StringLength(155)]
        public string IkonYolu { get; set; }

        [Required( ErrorMessage ="Lütfen Fotoğraf Giriniz")]
        [Display(Name = "Tam Resim")]
        [StringLength(155)]
        public string TamResimYolu { get; set; }

        public bool Aktifmi { get; set; }
    }
}