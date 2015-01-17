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


    }
}
