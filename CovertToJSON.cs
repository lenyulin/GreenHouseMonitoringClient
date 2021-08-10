using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class CovertToJSON
    {
        public ValueMember GetMember(string JSON)
        {
            ValueMember valueMember = new ValueMember();
            Value v = JsonConvert.DeserializeObject<Value>(JSON);
            valueMember.二氧化碳浓度 = int.Parse(v.二氧化碳浓度);
            valueMember.温度 = Convert.ToDouble(v.温度);
            valueMember.湿度 = Convert.ToDouble(v.湿度);
            valueMember.光线 = Convert.ToDouble(v.光线);
            return valueMember;
        }
    }
    public class Value
    {
        public string 光线 { get; set; }
        public string 二氧化碳浓度 { get; set; }
        public string 温度 { get; set; }
        public string 湿度 { get; set; }
    }
}
