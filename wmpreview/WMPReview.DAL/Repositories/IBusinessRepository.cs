using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
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
        Business FindByName(string name);
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

       
    }
}
