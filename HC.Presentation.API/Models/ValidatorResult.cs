namespace HC.Presentation.API.Models
{
    public class ValidatorResult
    {
        public bool IsValid { get; set; } = false;
        public string Url { get; set; }
        public List<ValidatorErrors> ValidatorErrors { get; set; } = new List<ValidatorErrors>();
    }
    public class ValidatorErrors
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }

    }
}
