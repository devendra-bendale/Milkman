using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Milkman2.Data
{
    public class DataContextFactory
    : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            //var options = new DbContextOptionsBuilder<DataContext>()
            //    .UseSqlite("Data Source=Milkman.db")
            //    .Options;

            var options = new DbContextOptionsBuilder<DataContext>();
            options.UseNpgsql("User Id=postgres.jmksayjmurtaejyyqjhf;Password=$cjGd*MFj5sQLM@;Server=aws-1-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres");

            return new DataContext(options.Options);
        }
    }
}
