using sgrc.DikizaCS.Mailer.Model;

namespace sgrc.DikizaCS.Mailer
{
    public interface IEmailBuilder
    {
        void OnRegisterClient(AccountRegistration input);
        void OnRegisterStudent(AccountRegistration input);
    }
}
