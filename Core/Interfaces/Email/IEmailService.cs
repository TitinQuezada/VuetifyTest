using Core.Entities.Email;
using System.Threading.Tasks;

namespace Core.Interfaces.Email
{
    public interface IEmailService
    {
        Task SendActivationEmail(SystemUserActivationRequest systemUserActivationRequest);
    }
}
