using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ConsumeDiscogsAPI.Models
{
    public class Record
    {
        public int DiscogsId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Label { get; set; }
        [DisplayName("Cat No.")]
        public string CatNo { get; set; }
        public string Genre { get; set; }
        public string Style { get; set; }

        public string DiscogsUrl 
        {
            get { return $"https://www.discogs.com/release/{DiscogsId}/"; } 
        }
        
    }
}
