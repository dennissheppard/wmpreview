using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WmpReview.Api.Models.DTO
{
    public class Tag
    {
        public int Id { get; set; }
        public Nullable<int> Name { get; set; }
        public string Description { get; set; }
    }
}
