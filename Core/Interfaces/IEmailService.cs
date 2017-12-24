using Fyo.Models;

namespace Fyo.Interfaces {
    public interface IEmailService
    {
        void Send(string[] to, string subject, string body, string text = null);
    }
}