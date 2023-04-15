using HC.Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HC.Presentation.API.Application.DTOs
{
    public class MovieDTO
    {
        public bool adult { get; set; }
        public int id { get; set; }
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
        public string note { get; set; }
        public int score { get; set; }
    }
    public class MovieAndCommentsDTO
    {
        public MovieDTO movie { get; set; }
        public List<NoteAndScores> note_and_scores { get; set; } = new List<NoteAndScores>();
        public double average_score { get; set; }
        public string response { get; set; }
        public string response_code { get; set; }
    }
    public class NoteAndScores
    {
        public string note { get; set; }
        public int score { get; set; }
    }
}
