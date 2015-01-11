using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMP.EFDalKit;

namespace WMPReview.DAL.Repositories
{
    public interface IReviewRepository : IRepository<Review>
    {
        Review FindById(int id);
        List<Review> GetAll();
        void Insert(Review review);
        

    }

     public class ReviewRepository : WMPFoodAppBaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(IUnitOfWork<WMPFoodAppEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public Review FindById(int id)
        {
            return this.EntitySet.Find(id);
            
        }

         public List<Review> GetAll()
         {
             return this.EntitySet.ToList();
         }

         public void Insert(Review review)
         {
             this.EntitySet.Add(review);
             UnitOfWork.SaveChanges();
         }

    }
}
