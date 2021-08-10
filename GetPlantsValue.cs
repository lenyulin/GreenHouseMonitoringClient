using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class GetPlantsValue
    {
        public enum Planets
        {
            甜菜, 西瓜, 白菜, 地瓜
        };
        public ValueMember GetValue(string Name)
        {
            ValueMember valueMember = new ValueMember();
            int count = (int)Enum.Parse(typeof(Planets), Name);
            switch (count)
            {
                case 0:
                    { valueMember.二氧化碳浓度 = 17; valueMember.光线 = 3500; valueMember.土壤湿度 = 20; valueMember.温度 = 34; valueMember.湿度 = 5; }
                    break;
                case 1:
                    { valueMember.二氧化碳浓度 = 15; valueMember.光线 = 3000; valueMember.土壤湿度 = 23; valueMember.温度 = 30; valueMember.湿度 = 10; }
                    break;
                case 2:
                    { valueMember.二氧化碳浓度 = 20; valueMember.光线 = 4000; valueMember.土壤湿度 = 27; valueMember.温度 = 25; valueMember.湿度 = 19; }
                    break;
                case 3:
                    { valueMember.二氧化碳浓度 = 19; valueMember.光线 = 4700; valueMember.土壤湿度 = 21; valueMember.温度 = 31; valueMember.湿度 = 8; }
                    break;
            }
            return valueMember;
        }
    }
}
