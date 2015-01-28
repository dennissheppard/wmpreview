using System.Runtime.Remoting.Contexts;

namespace WMPReview.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WMPReview.DAL.WMPFoodAppEntities>
    {

        protected override void Seed(WMPReview.DAL.WMPFoodAppEntities context)
        {


            context.Database.ExecuteSqlCommand(GetSqlString());
            context.Database.ExecuteSqlCommand(getFunctionString());
            seedBusinesses(context);
        }

        private string GetSqlString()
        {
            return "CREATE PROCEDURE [dbo].[LocationsWithInRadius]" +
                   " @Lat float, " +
                   "@Long float," +
                   " @Distance float,  " +
                   "@LatMin float, " +
                   "@LatMax float," +
                   "@LongMin float," +
                   "@LongMax float " +
                   "AS " +
                   Environment.NewLine +
                   "Begin " +
                   "Declare @EarthRadius float; " +
                   "set @EarthRadius =6371; " +
                   "SELECT Id, distance FROM" +
                   " (SELECT * , dbo.GetDistance (@Lat, @Long, l.lat, l.long) distance FROM dbo.Businesses l WHERE(l.Lat >= @LatMin AND l.Lat <= @LatMax)" +
                   " AND (l.Long >= @LongMin AND l.Long <= @LongMax)) as filtered WHERE distance <= @Distance end ";
        }

        private string getFunctionString()
        {
            return
                "CREATE FUNCTION [dbo].[GetDistance] (@Lat float,@Long float,@LatDest float,@LongDest float ) " +
                "RETURNS float AS BEGIN  DECLARE @Distance as float, @EarthRadius float set @EarthRadius =6371;" +
                " SELECT @Distance = acos(sin(@Lat) * sin(@LatDest) + cos(@Lat) * cos(@LatDest) * cos(@LongDest - (@Long))) * @EarthRadius " +
                "RETURN @Distance END";
        }

        private void seedBusinesses(WMPFoodAppEntities context)
        {
            context.Businesses.AddOrUpdate(new Business()
            {
                Name = "This",
                Long = 123,
                Lat = 123,
                YelpId = "the-gage-chicago"
            });
        }
            
        
    }
}
