using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenHouseMonitoringClient
{
    class GetDeviceBasicInfo
    {
        public DeviceMember GetMember(int id)
        {
            DeviceMember member = new DeviceMember();
            string connstr = "data source=119.3.225.79;database=dataMirror;user id=root;password=*{Lyl19970203..}*;pooling=false;charset=utf8";
            MySqlConnection conn= new MySqlConnection(connstr);
            string sql = "select * from DeviceInfo";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            try
            {
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();//执行ExecuteReader()返回一个MySqlDataReader对象
                while (reader.Read())//初始索引是-1，执行读取下一行数据，返回值是bool
                {
                    if (int.Parse(reader.GetString("Uid"))== id){
                        member.UID = int.Parse(reader.GetString("Uid"));
                        member.SoftwareVer = reader.GetString("SoftwareVer");
                        member.Date = reader.GetDateTime("Registration");
                        member.CPU = reader.GetString("CPU");
                    }
                }
            }
            catch (Exception e)
            {
                ;
            }
            return member;
        }
    }
}
