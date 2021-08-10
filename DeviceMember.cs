using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class DeviceMember
    {
        public int UID { get; set; }
        public string CPU { get; set; }
        public DateTime Date { get; set; }
        public string SoftwareVer { get; set; }
    }
}
