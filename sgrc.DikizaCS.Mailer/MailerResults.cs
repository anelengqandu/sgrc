namespace sgrc.DikizaCS.Mailer
{
   public class MailerResults
    {
        public bool Success = true;
        public bool ThrewError = false;
        public string TitleText;
        public string DescripText;
        public string Status;

        public MailerResults() { }

        public MailerResults(bool success)
        {
            Success = success;
        }

        public MailerResults(bool success, string titleText, string descripText)
        {
            Success = success;
            TitleText = titleText;
            DescripText = descripText;
        }
       
    }
}
