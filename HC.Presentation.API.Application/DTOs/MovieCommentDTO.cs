using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Presentation.API.Application.DTOs
{
    public class MovieCommentDTO
    {
        public int id { get; set; }
        public int movie_id { get; set; }
        public string note { get; set; }
        public int score { get; set; }
        public string user_id { get; set; }
        public int response_code { get; set; }
        public string response { get; set; }
    }
}
