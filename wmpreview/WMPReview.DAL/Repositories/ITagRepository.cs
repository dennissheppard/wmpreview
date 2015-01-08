using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMP.EFDalKit;

namespace WMPReview.DAL.Repositories
{

    public interface ITagRepository : IRepository<Tag>
    {
        Tag FindById(int id);
        List<Tag> GetAll();

    }

    public class TagRepository : WMPFoodAppBaseRepository<Tag>, ITagRepository 
    {
        public TagRepository(IUnitOfWork<WMPFoodAppEntities> unitOfWork) : base(unitOfWork)
        {
        }

        public Tag FindById(int id)
        {
            return this.EntitySet.Find(id);
        }

        public List<Tag> GetAll()
        {
            return this.EntitySet.ToList();
        }
    }


}
