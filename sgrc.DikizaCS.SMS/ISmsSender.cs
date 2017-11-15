using sgrc.DikizaCS.SMS.Model;

namespace sgrc.DikizaCS.SMS
{
   public  interface ISmsSender
   {
        SmsResults Send(MessageModel message);
   }
}
