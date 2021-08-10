using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class GetTotalDevInfo
    {
        public static WSocketClient wSocketClient;
        public static string x { get; set; }

        public string getX()
        {
            return x;
        }
        public void GetData(string url)
        {
            wSocketClient = new WSocketClient(url);
            try
            {
                wSocketClient.Start();
                MessageReceived();
                Thread.Sleep(100);
                wSocketClient.SendMessage("Conneted");
                MessageReceived();
            }
            catch (Exception ex)
            { }
        }

        public void Dispose()
        {
            wSocketClient.Dispose();
        }

        private void MessageReceived()
        {
            //注册消息接收事件，接收服务端发送的数据
            wSocketClient.MessageReceived += (data) => {
                x = data;
            };
        }
    }
}
