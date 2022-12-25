using System;
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


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
