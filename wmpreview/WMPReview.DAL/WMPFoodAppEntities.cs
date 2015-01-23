using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using WMP.EFDalKit;

namespace WMPReview.DAL
{
    public partial class WMPFoodAppEntities : DbContextBase
    {
        public WMPFoodAppEntities()
            : base("name=LocalDb")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public virtual List<QueryResult> SearchForBusinessInRadius(double lat, double lon, double distanceKm, double latMin,
            double lonMin, double latMax, double lonMax)
        {
            
            try
            {
                var latParam = new SqlParameter("Lat", (float)lat);
                var lonParam = new SqlParameter("Long", (float)lon);
                var distanceParam = new SqlParameter("Distance", (float)distanceKm);
                var latMinParam = new SqlParameter("LatMin", (float)latMin);
                var lonMinParam = new SqlParameter("LongMin", (float)lonMin);
                var latMaxParam = new SqlParameter("LatMax", (float)latMax);
                var lonMaxParam = new SqlParameter("LongMax", (float)lonMax);

                var BusinessIdResult = ((IObjectContextAdapter)this).ObjectContext.ExecuteStoreQuery<QueryResult>(
                    "Exec[dbo].[LocationsWithInRadius] @Lat," +
                    "@Long, " +
                    "@Distance, " +
                    "@LatMin, " +
                    "@LatMax, " +
                    "@LongMin, " +
                    "@LongMax", latParam, lonParam, distanceParam, latMinParam, lonMinParam, latMaxParam,
                    lonMaxParam);



                return BusinessIdResult.ToList();


            }

            catch (Exception e)
            {
                throw e;
            }

            
        }
    }

    public class QueryResult
    {
        public int Id { get; set; }
        public int Distance { get; set; }
    }
}