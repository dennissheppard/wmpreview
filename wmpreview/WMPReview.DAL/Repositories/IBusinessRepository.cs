using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using WMP.EFDalKit;

namespace WMPReview.DAL.Repositories
{
    public interface IBusinessRepository : IRepository<Business>
    {
        Business FindById(int id);
        List<Business> GetAll();
        List<Business> GetAll(int count, int offset);
        Business FindByName(string name);
        List<Business> Query(Expression<Func<Business, bool>> filter);
        List<Business> GetByLocation(double lat, double lon, double distanceInMiles);
    }

    public class BusinessRepository : WMPFoodAppBaseRepository<Business>, IBusinessRepository
    {
        public BusinessRepository(IUnitOfWork<WMPFoodAppEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public Business FindById(int id)
        {
            return this.EntitySet.Find(id);
            
        }

        public List<Business> GetAll()
        {
            return this.EntitySet.ToList();
            
        }

        public Business FindByName(string name)
        {
            return EntitySet.FirstOrDefault(x => x.Name == name);
        }

        public List<Business> GetAll(int count, int offset)
        {
            var businesses = EntitySet.Skip(offset).Take(count).ToList();
            return businesses;
        }

        public List<Business> Query(Expression<Func<Business, bool>> filter)
        {
            var businesses = EntitySet.Where(filter).ToList();
            return businesses;
        }

        public List<Business> GetByLocation(double lat, double lon, double distanceInMiles)
        {
           
            GeoLocation geoLocation = GeoLocation.fromRadians(lat,lon);
            double radianLat = geoLocation.getLatitudeInRadians();
            double radianLon = geoLocation.getLongitudeInRadians();
            double distanceKm = GeoLocation.ConvertMilesToKilometers(distanceInMiles);
            
            var boundingCoordinates =  geoLocation.boundingCoordinates(distanceKm, GeoLocation.earthRadius);

            double latMin = boundingCoordinates[0].getLatitudeInRadians();
            double latMax = boundingCoordinates[1].getLatitudeInRadians();
            double lonMin = boundingCoordinates[0].getLongitudeInRadians();
            double lonMax = boundingCoordinates[1].getLongitudeInRadians();

            var businessesIds = UnitOfWork.DbContext.SearchForBusinessInRadius(radianLat,radianLon,distanceKm, latMin,lonMin,latMax,lonMax);

            throw new NotImplementedException();
            


        }
    }

   
}
