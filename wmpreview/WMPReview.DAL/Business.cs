namespace WMPReview.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class Business
    {
        public Business()
        {
            this.Reviews = new HashSet<Review>();
            this.Tags = new HashSet<Tag>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
     
        public Nullable<double> Lat { get; set; }
        public Nullable<double> Long { get; set; }
      
        public string YelpId { get; set; }
    
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
    }
}
