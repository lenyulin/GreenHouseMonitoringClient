using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.Core;
using Windows.UI.Xaml;

namespace GreenHouseMonitoringClient
{
    class RandomViewModel : ViewModelBase
    {
        public static int uid { get; set; }
        private DispatcherTimer timer;

        private static ValueMember valueMember;
        private CallsData currentActiveCalls;

        public CallsData CurrentActiveCalls
        {
            get { return currentActiveCalls; }
            set
            {
                currentActiveCalls = value;
                this.OnPropertyChanged("CurrentActiveCalls");
            }
        }

        public void setuid(int a)
        {
            uid = a;
        }
        private ObservableCollection<CallsData> callHistory;

        public ObservableCollection<CallsData> CallHistory
        {
            get { return callHistory; }
            private set
            {
                callHistory = value;
                this.OnPropertyChanged("CallHistory");
            }
        }
        /// <summary>
        /// shidu
        /// </summary>
        private ObservableCollection<CallsData> callShidu;

        public ObservableCollection<CallsData> CallShidu
        {
            get { return callShidu; }
            private set
            {
                callShidu = value;
                this.OnPropertyChanged("CallShidu");
            }
        }
        private CallsData currentActiveCallsShidu;

        public CallsData CurrentActiveCallsShidu
        {
            get { return currentActiveCallsShidu; }
            set
            {
                currentActiveCallsShidu = value;
                this.OnPropertyChanged("CurrentActiveCallsShidu");
            }
        }
        /// <summary>
        /// wendu
        /// </summary>
        private ObservableCollection<CallsData> callWendu;

        public ObservableCollection<CallsData> CallWendu
        {
            get { return callWendu; }
            private set
            {
                callWendu = value;
                this.OnPropertyChanged("CallWendu");
            }
        }
        private CallsData currentActiveCallsWendu;

        public CallsData CurrentActiveCallsWendu
        {
            get { return currentActiveCallsWendu; }
            set
            {
                currentActiveCallsWendu = value;
                this.OnPropertyChanged("CurrentActiveCallsWendu");
            }
        }
        /// <summary>
        /// unknow
        /// </summary>
        public RandomViewModel()
        {
            valueMember = new ValueMember();
            timer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += UpdateItemData;
            this.LoadData();
        }

        public void StopTimer()
        {
            this.timer.Stop();
        }
        public void StartTimer()
        {
            this.timer.Start();
        }

        public int GetWendu()
        {
            return (int)valueMember.温度;
        }

        public void UpdateValueMember(ValueMember vm)
        {
            valueMember = vm;
        }

        public void UpdateItemData(object sender, object e)
        {
            var lastRecorderData = this.CallHistory.Last();
            this.CurrentActiveCalls = new CallsData { Date = lastRecorderData.Date.AddMilliseconds(500), ActiveCalls = (int)valueMember.二氧化碳浓度 };
            this.CallHistory.Add(CurrentActiveCalls);
            this.CallHistory.RemoveAt(0);

            var lastRecorderDataShidu = this.CallShidu.Last();
            this.CurrentActiveCallsShidu = new CallsData { Date = lastRecorderDataShidu.Date.AddMilliseconds(500), ShiduCalls = (int)valueMember.湿度 };
            this.CallShidu.Add(CurrentActiveCallsShidu);
            this.CallShidu.RemoveAt(0);

            var lastRecorderDataWendu = CallWendu.Last();
            this.CurrentActiveCallsWendu = new CallsData { Date = lastRecorderDataWendu.Date.AddMilliseconds(500), WenduCalls = (int)valueMember.温度 };
            this.CallWendu.Add(CurrentActiveCallsWendu);
            this.CallWendu.RemoveAt(0);
        }

        private void LoadData()
        {
            var now = DateTime.Now;
            var historyData = from c in Enumerable.Range(0, 24)
                              select new CallsData { ActiveCalls = 0, Date = now.AddMilliseconds(-500 * c) };
            var shiduHistoryData = from c in Enumerable.Range(0, 24)
                                   select new CallsData { ShiduCalls = 0, Date = now.AddMilliseconds(-500 * c) };
            var wenduHistoryData = from c in Enumerable.Range(0, 24)
                                   select new CallsData { WenduCalls = 0, Date = now.AddMilliseconds(-500 * c) };

            this.CallWendu = new ObservableCollection<CallsData>(wenduHistoryData.OrderBy(c => c.Date));
            this.CallHistory = new ObservableCollection<CallsData>(historyData.OrderBy(c => c.Date));
            this.CallShidu = new ObservableCollection<CallsData>(shiduHistoryData.OrderBy(c => c.Date));
        }

    }
    public class CallsData : ViewModelBase
    {
        private int activeCalls;


        public int ActiveCalls
        {
            get { return activeCalls; }
            set
            {
                activeCalls = value;
                this.OnPropertyChanged("ActiveCalls");
            }
        }

        private int shiduCalls;

        public int ShiduCalls
        {
            get { return shiduCalls; }
            set
            {
                shiduCalls = value;
                this.OnPropertyChanged("ShiduCalls");
            }
        }

        private int wenduCalls;

        public int WenduCalls
        {
            get { return wenduCalls; }
            set
            {
                wenduCalls = value;
                this.OnPropertyChanged("WenduCalls");
            }
        }


        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                this.OnPropertyChanged("Date");
            }
        }

    }
}
