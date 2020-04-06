using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace TplinkPowerlineFix
{
    internal class PingService
    {
        public async Task Start()
        {
            var defaultGateway = GetDefaultGateway();
            if (defaultGateway == null)
                throw new Exception("Default gateway not found");

            await PingForeverAsync(defaultGateway);
        }

        public async Task PingForeverAsync(IPAddress host)
        {
            Ping ping = new Ping();
            while (true)
            {
                await Task.Delay(5000);
                var pingReply = await ping.SendPingAsync(host, 4000);
            }
        }

        public static IPAddress GetDefaultGateway()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                // .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                // .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault(a => a != null);
        }

        public void Stop()
        {
        }
    }
}