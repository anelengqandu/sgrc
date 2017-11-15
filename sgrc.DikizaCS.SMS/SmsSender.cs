using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using sgrc.DikizaCS.SMS.Model;

namespace sgrc.DikizaCS.SMS
{
    public class SmsSender : ISmsSender
    {
        public SmsResults Send(MessageModel message)
        {
            HttpResponseMessage httpResponse;
            try
            {
                var smsUserName = ConfigurationManager.AppSettings["smsUserName"];
                var smsPassword = ConfigurationManager.AppSettings["smsPassword"];
                var url = "http://www.mymobileapi.com/api5/http5.aspx?Type=sendparam&username="                                           //sms api uri
                    + smsUserName + "&password=" + smsPassword
                     + "&numto=" + message.Number + "&data1=" + message.Message;

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    httpResponse = client.PostAsync(url, null).Result;
                }

            }
            catch (HttpRequestException ex)
            {
                return new SmsResults
                {
                    Status = "Fail",
                    DescripText = ex.Message,
                    Success = false
                };
            }
            catch (Exception ex)
            {
                return new SmsResults
                {
                    Status = "Fail",
                    DescripText = ex.Message,
                    Success = false
                };
            }
            return new SmsResults { Status = "Success", DescripText = "",HttpResponse = httpResponse.IsSuccessStatusCode};

        }
    }
}
