namespace sgrc.DikizaCS.SMS
{
   public class SmsResults
    {
        public bool Success = true;
        public bool ThrewError = false;
        public string TitleText;
        public string DescripText;
        public bool HttpResponse;
        public string Status;

        public SmsResults() { }

        public SmsResults(bool success)
        {
            Success = success;
        }

        public SmsResults(bool success, string titleText,string descripText, bool httpResponse)
        {
            Success = success;
            TitleText = titleText;
            DescripText = descripText;
            HttpResponse = httpResponse;
        }
    }
}
