using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnInterestingWebSiteName.Models
{
    public class YoneticiTur
    {

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Ad { get; set; }

        public virtual IEnumerable<Yonetici> Yoneticis { get; set; }


    }
}