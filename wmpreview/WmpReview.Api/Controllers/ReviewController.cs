using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WMP.EFDalKit;
using WMPReview.DAL;
using WMPReview.DAL.Repositories;

namespace WmpReview.Api.Controllers
{
    public class ReviewController : ApiController
    {
        private IReviewRepository _reviewRepository;
        private IUnitOfWork<WMPFoodAppEntities> _unitOfWork; 

        public ReviewController(IReviewRepository reviewRepository, IUnitOfWork<WMPFoodAppEntities> unitOfWork)
        {
            _reviewRepository = reviewRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Reviews
        public IEnumerable<Review> Get()
        {
            return _reviewRepository.GetAll();
        }

        // GET: api/Reviews/5
        public Review Get(int id)
        {
           return _reviewRepository.FindById(id);
        }

        // POST: api/Reviews
        public void Post(Review review)//these should be model 
        {

            _reviewRepository.Add(review);
            _unitOfWork.SaveChanges();
        }

        // PUT: api/Reviews/5
        public void Put(Review review)
        {
           var reviewDb = _reviewRepository.FindById(review.Id);
            
            //do 
            
           _unitOfWork.SaveChanges();
        }

        // DELETE: api/Reviews/5
        public void Delete(int id)
        {
          var review =  _reviewRepository.FindById(id);
            _reviewRepository.Delete(review);
            _unitOfWork.SaveChanges();
        }

    }
}
