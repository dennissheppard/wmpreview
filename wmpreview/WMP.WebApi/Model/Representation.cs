using Newtonsoft.Json;

namespace WMP.WebApi.Model
{
    public abstract class Representation : IRepresentation
    {
        public Representation()
        {
            Links = new LinkCollection();            
        }
        

        [JsonProperty("_links")]
        public LinkCollection Links { get; set; }

        public void AddLinks(params Link[] links)
        {
            Links.AddRange(links);
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }
    }
}