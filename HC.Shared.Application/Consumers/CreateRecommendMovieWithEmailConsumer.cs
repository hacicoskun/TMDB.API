using MassTransit;
using System.Net.Mail;
using System.Net;

namespace HC.Shared
{
    public class CreateRecommendMovieWithEmailConsumer : IConsumer<CreateRecommendMovieWithEmail>
    {
        public CreateRecommendMovieWithEmailConsumer()
        {
            
        }
        public Task Consume(ConsumeContext<CreateRecommendMovieWithEmail> context)
        {
             
            var title = context.Message.Title;
            var body = context.Message.Body; 

            //using (MailMessage mail = new MailMessage())
            //{
            //    mail.From = new MailAddress("hc.smtp.test1@gmail.com");
            //    mail.To.Add("hacicoskun07@gmail.com");
            //    mail.Subject = title;
            //    mail.Body = body;
            //    mail.IsBodyHtml = true; 

            //    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
            //    {
            //        smtp.Credentials = new NetworkCredential("hc.smtp.test1@gmail.com", "Haci123!?");
            //        smtp.EnableSsl = true;
            //        smtp.Send(mail);
            //    }
            //}
            return Task.CompletedTask;
             
        }
    }
}
