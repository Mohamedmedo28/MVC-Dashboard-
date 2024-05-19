using CompanyDAL.Models;

namespace CompanyPL.Helpers
{
    public interface IMailSettings
    {
        public void SendMail(Email email);
    }
}
