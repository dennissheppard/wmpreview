using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WmpReview.Api.Models.DTO
{
    public class Business
    {
        public Business()
        {
            this.Reviews = new HashSet<Review>();
            this.Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
        public int YelpId { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}