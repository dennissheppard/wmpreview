using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WMPReview.DAL.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string sql = "SET ANSI_NULLS ON "
                + Environment.NewLine
                + "GO "
                + Environment.NewLine
                + "SET QUOTED_IDENTIFIER ON "
                + Environment.NewLine
                + "GO "
                + Environment.NewLine
                + getFunctionString()
                + Environment.NewLine
                + "GO "
                + GetSqlString()
                + Environment.NewLine + "GO ";
            Debug.WriteLine(sql);
    
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
             "AS" +
             "Begin " +
             "Declare @EarthRadius float; " +
             "set @EarthRadius =6371; " +
             "SELECT Id, distance FROM" +
             " (SELECT * , dbo.GetDistance (@Lat, @Long, l.lat, l.long) distance FROM dbo.Business l WHERE(l.Lat >= @LatMin AND l.Lat <= @LatMax)" +
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
    }
}
