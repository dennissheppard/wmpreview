using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WMP.EFDalKit;
using WMPReview.DAL;
using WMPReview.DAL.Repositories;

namespace WmpReview.Api.Controllers
{

    public class BusinessController : ApiController
    {
        private readonly IBusinessRepository _businessRepository;
        private IUnitOfWork<WMPFoodAppEntities> _unitOfWork; 

        public BusinessController(IBusinessRepository businessRepository, IUnitOfWork<WMPFoodAppEntities> unitOfWork)
        {
            _businessRepository = businessRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Business
        public IEnumerable<Business> Get()
        {
            return _businessRepository.GetAll();
        }

        // GET: api/Business/5
        public Business Get(int id)
        {
            return _businessRepository.FindById(id);
        }

        // POST: api/Business
        public void Post(Business business)
        {
            _businessRepository.Add(business);
            _unitOfWork.SaveChanges();
        }

        // PUT: api/Business/5
        public void Put(Business business)
        {
            var dbBusiness = _businessRepository.FindById(business.Id);

            dbBusiness.Name = business.Name;
            //do

            _unitOfWork.SaveChanges();

        }

        // DELETE: api/Business/5
        public void Delete(int id)
        {
            var business = _businessRepository.FindById(id);
            _businessRepository.Delete(business);
            _unitOfWork.SaveChanges();
        }
    }
}
