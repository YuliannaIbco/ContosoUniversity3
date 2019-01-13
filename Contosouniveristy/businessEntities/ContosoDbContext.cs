using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace businessEntities
{    
   [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))] 

    public class ContosoDbContext : DbContext
    {
        public ContosoDbContext() : base("ContosoDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }


    }
}
