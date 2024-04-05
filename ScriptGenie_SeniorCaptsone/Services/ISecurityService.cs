using ScriptGenie_SeniorCaptsone.Models;

namespace ScriptGenie_SeniorCaptsone.Services
{
    public interface ISecurityService
    {
        bool ProcessLogin(UserModel user);
        bool ProcessRegister(UserModel user);
        string ProcessForgotPassword(UserModel user);
    }
}
