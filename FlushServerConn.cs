using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace GreenHouseMonitoringClient
{
    class FlushServerConn
    {
        private static byte[] result = new byte[1024];
        public async Task<bool> FlushAsync(string ip)
        {
            Windows.Networking.HostName host = new Windows.Networking.HostName(ip);
            var eps = await DatagramSocket.GetEndpointPairsAsync(host, "80");
            if (eps.Count >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
