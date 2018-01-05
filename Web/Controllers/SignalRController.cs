using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Fyo
{
    public class SignalR : Hub
    {
        public Task Screenshot()
        {
            Console.WriteLine("Screenshot from SignalR");
            return Clients.All.InvokeAsync("Screenshot");
        }
    }
}