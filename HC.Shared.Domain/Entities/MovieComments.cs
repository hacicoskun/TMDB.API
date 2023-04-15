using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Shared.Domain.Entities
{
    public class MovieComments
    {
        public int id { get; set; }
        public int movie_id { get; set; } 
        public string note { get; set; }
        public int score { get; set; }
        public string user_id { get; set; }
    }
}
