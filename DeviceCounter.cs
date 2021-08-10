using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class DeviceCounter
    {
        public static DeviceCounterMember DeviceCounterMember= new DeviceCounterMember();
        public DeviceCounterMember GetOnlineDevice()
        {
            CreateGetHttpResponse createGetHttpResponse = new CreateGetHttpResponse();
            var result = JObject.Parse(createGetHttpResponse.Get("http://119.3.225.79:8081/api/v4/nodes/emqx@127.0.0.1/stats"));
            Console.WriteLine(result);
            foreach (JToken child in result.Children())
            {
                foreach (JToken grandChild in child)
                {
                    foreach (JToken grandGrandChild in grandChild)
                    {
                        var property = grandGrandChild as JProperty;
                        if (property != null)
                        {
                            if (property.Name == "connections.count")
                                DeviceCounterMember.onlineDevice = (float)property.Value;
                            else if (property.Name == "connections.max")
                                DeviceCounterMember.TotalDevice = (float)property.Value;
                        }
                    }
                }
            }
            DeviceCounterMember.offineDevice = DeviceCounterMember.TotalDevice - DeviceCounterMember.onlineDevice;
            if (DeviceCounterMember.TotalDevice != 0)
            {
                DeviceCounterMember.processbar = DeviceCounterMember.onlineDevice*100 / DeviceCounterMember.TotalDevice;
                DeviceCounterMember.pb = ((int)(DeviceCounterMember.processbar * 100)).ToString() + "%";
            }
            return DeviceCounterMember;
        }
    }
}
