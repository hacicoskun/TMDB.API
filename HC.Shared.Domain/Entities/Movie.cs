using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Shared.Domain.Entities
{
    
    public class Movie: BaseEntity
    {
       
        public int id { get; set; } 
        public int movie_id { get; set; } 
        public int page { get; set; }  
        public bool adult { get; set; }  
        public string original_language { get; set; }  
        public string original_title { get; set; } 
        public string overview { get; set; }  
        public double popularity { get; set; }
        public string poster_path { get; set; }  
        public string release_date { get; set; } 
        public string title { get; set; } 
        public bool video { get; set; }  
        public double vote_average { get; set; }  
        public int vote_count { get; set; } 
    }
}
