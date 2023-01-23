using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace AnInterestingWebSiteName.Models
{
    public partial class AnInterestingWebSiteName_Model : DbContext
    {
        public AnInterestingWebSiteName_Model()
            : base("name=AnInterestingWebSiteName_Model")
        {
        }

        public virtual DbSet<Urunler> Urunlers { get; set; }

        public virtual DbSet<Yonetici> Yoneticis { get; set; }

        public virtual DbSet<YoneticiTur> YoneticiTurs { get; set; }

        public virtual DbSet<OyunResimleri> OyunResimleris{ get; set; }

        public virtual DbSet<Tag> Tags{ get; set; }

        public virtual DbSet<TagsVeUrunAraClass> TagsVeUrunAraClasses{ get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
