namespace HC.Presentation.API.Models
{
    public class AddMovieCommentRequestModel
    {
        public int MovieId { get; set; }
        public int Score { get; set; }
        public string Note { get; set; }
    }
}
