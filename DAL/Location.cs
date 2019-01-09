using DAL.DomainModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.DomainModels
{
    public class Loc
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
    public class Location
    {

        private DataContext _dbContext { get; set; }

        public Location()
        {
        }

        public Location(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Distance(Loc destination, int srid = 4326)
        {
            var source = this;

            var Distance = string.Empty;
            var query = @"DECLARE @target geography =  geography::Point(" + destination.Latitude + @"," + destination.Longitude + @"," + srid + @")
                    DECLARE @orig geography = geography::Point(" + source.Latitude + @"," + source.Longitude + @"," + srid + @")
                    SELECT @orig.STDistance(@target) as Distance";
            try
            {
                var dbConn = _dbContext.Database.GetDbConnection();

                if (dbConn.State == System.Data.ConnectionState.Closed)
                    dbConn.Open();

                var command = dbConn.CreateCommand();
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = query;
                return Convert.ToDouble(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
