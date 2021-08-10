using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;

namespace GreenHouseMonitoringClient
{
    class MQTTService
    {
        private static MqttClient mqttClient = null;
        private static ValueMember valueMember;
        private static RandomViewModel model;
        private static bool runState = false;
        private static bool running = false;
        private static CovertToJSON JSON;
        private static IMqttClientOptions options = null;
        private static string ServerUrl = "119.3.225.79";
        private static int Port = 1883;
        private static string Password = "  ";
        private static string UserId = "    ";
        private static string Topic = "0123/#";
        private static bool Retained = false;
        public  delegate void GetNewData(ValueMember valueMember);  //声明委托
        public static GetNewData getNewData;
        public static void Stop()
        {
            runState = false;
        }

        public static bool IsRun()
        {
            return (runState && running);
        }
        /// <summary>
        /// 启动客户端
        /// </summary>
        public static void Start()
        {
            try
            {
                JSON = new CovertToJSON();
                runState = true;
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(Work));
                thread.Start();
            }
            catch (Exception exp)
            {
                ;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private static void Work()
        {
            running = true;
            try
            {
                var factory = new MqttFactory();
                mqttClient = factory.CreateMqttClient() as MqttClient;

                options = new MqttClientOptionsBuilder()
                    .WithTcpServer(ServerUrl, Port)
                    .WithCredentials(UserId, Password)
                    .WithClientId(Guid.NewGuid().ToString().Substring(0, 5))
                    .Build();

                mqttClient.ConnectAsync(options);
                mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(new Func<MqttClientConnectedEventArgs, Task>(Connected));
                mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(new Func<MqttClientDisconnectedEventArgs, Task>(Disconnected));
                mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(new Action<MqttApplicationMessageReceivedEventArgs>(MqttApplicationMessageReceived));
                while (runState)
                {
                    System.Threading.Thread.Sleep(100);
                }
            }
            catch (Exception exp)
            {
                ;
            }
            running = false;
            runState = false;
        }

        public static void StartMain()
        {
            try
            {
                var factory = new MqttFactory();

                var mqttClient = factory.CreateMqttClient();

                var options = new MqttClientOptionsBuilder()
                    .WithTcpServer(ServerUrl, Port)
                    .WithCredentials(UserId, Password)
                    .WithClientId(Guid.NewGuid().ToString().Substring(0, 5))
                    .Build();

                mqttClient.ConnectAsync(options);

                mqttClient.UseConnectedHandler(async e =>
                {
                    // Subscribe to a topic
                    var topicFilterBulder = new TopicFilterBuilder().WithTopic(Topic).Build();
                    await mqttClient.SubscribeAsync(topicFilterBulder);
                });

                mqttClient.UseDisconnectedHandler(async e =>
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    try
                    {
                        await mqttClient.ConnectAsync(options);
                    }
                    catch (Exception exp)
                    {

                    }
                });

                mqttClient.UseApplicationMessageReceivedHandler(e =>
                {
                    // Console.WriteLine("MessageReceived >>" + Encoding.UTF8.GetString(e.ApplicationMessage.Payload));
                });
            }
            catch (Exception exp)
            {
                ;
            }
        }
        /// <summary>
        /// 发布
        /// <paramref name="QoS"/>
        /// <para>0 - 最多一次</para>
        /// <para>1 - 至少一次</para>
        /// <para>2 - 仅一次</para>
        /// </summary>
        /// <param name="Topic">发布主题</param>
        /// <param name="Message">发布内容</param>
        /// <returns></returns>
        public static void Publish(string Topic, string Message)
        {
            try
            {
                if (mqttClient == null) return;
                if (mqttClient.IsConnected == false)
                    mqttClient.ConnectAsync(options);

                if (mqttClient.IsConnected == false)
                {
                    ;
                }
                MqttApplicationMessageBuilder mamb = new MqttApplicationMessageBuilder()
                 .WithTopic(Topic)
                 .WithPayload(Message).WithRetainFlag(Retained);
                mamb = mamb.WithAtMostOnceQoS();
                mqttClient.PublishAsync(mamb.Build());
            }
            catch (Exception exp)
            {
                ;
            }
        }
        /// <summary>
        /// 连接服务器并按标题订阅内容
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static async Task Connected(MqttClientConnectedEventArgs e)
        {
            try
            {
                List<TopicFilter> listTopic = new List<TopicFilter>();
                if (listTopic.Count() <= 0)
                {
                    var topicFilterBulder = new TopicFilterBuilder().WithTopic(Topic).Build();
                    listTopic.Add(topicFilterBulder);
                }
                await mqttClient.SubscribeAsync(listTopic.ToArray());
            }
            catch (Exception exp)
            {
                ;
            }
        }
        /// <summary>
        /// 失去连接触发事件
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        private static async Task Disconnected(MqttClientDisconnectedEventArgs e)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                try
                {
                    await mqttClient.ConnectAsync(options);
                }
                catch (Exception exp)
                {
                    ;
                }
            }
            catch (Exception exp)
            {
                ;
            }
        }
        /// <summary>
        /// 接收消息触发事件
        /// </summary>
        /// <param name="e"></param>
        private static void MqttApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                string msg = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                valueMember = JSON.GetMember(msg);
                getNewData(valueMember);
            }
            catch (Exception exp)
            {
                ;
            }
        }
    }
}
