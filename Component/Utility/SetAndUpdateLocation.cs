using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Component.Utility
{
    public static class SetAndUpdateLocation
    {


        //public static void UpdateGeographyLocationInDB(string table, double latitude, double longitude, int id)
        //{
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

        //    string query = String.Format(@"UPDATE [dbo].[{0}] SET Location = geography::
        //    STPointFromText('POINT(' + CAST({1} AS VARCHAR(20)) + ' ' + CAST({2} AS VARCHAR(20)) + ')', 4326) WHERE(ID = {3})"
        //    , table.ToLower(), longitude, latitude, id);
        //    await ctx.Database.ExecuteSqlCommandAsync(query);
        //}

    }
}
