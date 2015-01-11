using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace WMP.WebApi.Model
{
    public class Link
    {
        private static readonly Regex IsTemplatedRegex = new Regex(@"{.+}", RegexOptions.Compiled);

        public Link(string relation, string href, string title = null)
        {
            Rel = relation;
            Href = href;
            Title = title;
        }

        [JsonProperty("profile", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Profile { get; set; } // sec 5.6, RFC 6906

        [JsonProperty("type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Type { get; set; } // sec 5.3

        [JsonProperty("hreflang", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string HrefLang { get; set; } // sec 5.8

        [JsonProperty("deprecation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool? IsDeprecated { get; set; } // sec 5.4 "deprecated" property

        [JsonProperty("href", Order = 1)]
        public string Href { get; set; }

        [JsonProperty("rel", Order = 0)]
        public string Rel { get; set; }

        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("templated", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public bool IsTemplated
        {
            get { return !string.IsNullOrEmpty(Href) && IsTemplatedRegex.IsMatch(Href); }
        }
    }
}