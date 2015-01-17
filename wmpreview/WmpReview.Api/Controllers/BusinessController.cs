using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using WMP.EFDalKit;
using WMPReview.DAL;
using WMPReview.DAL.Repositories;
using Business = WmpReview.Api.Models.DTO.Business;

namespace WmpReview.Api.Controllers
{

    public class BusinessController : ApiController
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly ITagRepository _tagRepository;
        private IUnitOfWork<WMPFoodAppEntities> _unitOfWork; 

        public BusinessController(IBusinessRepository businessRepository, ITagRepository tagRepository, IUnitOfWork<WMPFoodAppEntities> unitOfWork )
        {
            _businessRepository = businessRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;

        }

        // GET: api/Business
        public IEnumerable<Business> Get()
        {
            var dbBusinesses =_businessRepository.GetAll();
            var businesses =Mapper.Map<List<Business>>(dbBusinesses);
            return businesses;
        }

        public IEnumerable<Business> Get(int count, int offset)
        {
            

            var dbBusinesses = _businessRepository.GetAll(count, offset);
            var businesses = Mapper.Map<List<Business>>(dbBusinesses);
            return businesses;

        }
        // GET: api/Business
        [HttpGet]
        public IEnumerable<Business> Popular(double lat, double lon, int count, int offset)
        {
           var tag = _tagRepository.Query(x=>x.Name == "Popular");
          //  Tag t = new Tag();
            throw new NotImplementedException();
            //return _businessRepository.Query(x => x.Tags.Contains(tag));
        } 

        // GET: api/Business/5
        public Business Get(int id)
        {
            var dbBusiness = _businessRepository.FindById(id);
            return Mapper.Map<Business>(dbBusiness);
        }

        // POST: api/Business
        public void Post(Business business)
        {
            var dbBusiness = Mapper.Map<WMPReview.DAL.Business>(business);
            _businessRepository.Add(dbBusiness);
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
