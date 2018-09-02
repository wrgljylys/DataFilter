using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Text;

namespace Register
{
    public class NetworkAdapter
    {
        public NetworkAdapter() 
        {
        }

        public NetworkAdapter(ManagementObject mo)
        {
            Mac = mo["MacAddress"] == null ? "" : mo["MacAddress"].ToString().ToUpper();
            ServiceName = mo["ServiceName"] == null ? "" : mo["ServiceName"].ToString();
            Caption = mo["Caption"] == null ? "" : mo["Caption"].ToString();
            Name = mo["Name"] == null ? "" : mo["Name"].ToString();
            NetConnectionStatus = mo["NetConnectionStatus"] == null ? "" : mo["NetConnectionStatus"].ToString();
        }

        public string Ip
        { get; set; }

        public string Mac
        { get; set; }

        public string Caption
        { get; set; }

        public string Name
        { get; set; }

        public bool IPEnabled
        { get; set; }

        public string ServiceName
        { get; set; }

        public string NetConnectionStatus
        { get; set; }
      
        public bool IsDefault
        {
            get { return "2" == NetConnectionStatus; }
        }

        public override string ToString()
        {
            return "" + Name + ": " + Mac;
        }

        #region 获取网卡配置的静态量
        private static NetworkAdapter defaultNa = null;
        private static ObservableCollection<NetworkAdapter> listNa = null;

        public static NetworkAdapter DefaultNa
        {
            get
            {
                if (listNa == null)
                    getNetworkAdapterConfig();
                return defaultNa;
            }
        }

        public static ObservableCollection<NetworkAdapter> ListNa
        {
            get
            {
                if (listNa == null)
                    getNetworkAdapterConfig();
                return listNa;
            }
        }

        private static void getNetworkAdapterConfig()
        {
            listNa = new ObservableCollection<NetworkAdapter>();
            ManagementClass mc;
            //mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            mc = new ManagementClass("Win32_NetworkAdapter");

            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                NetworkAdapter na = new NetworkAdapter(mo);
                listNa.Add(na);
                if (na.IsDefault)
                {
                    defaultNa = na;
                }
            }
        }
        #endregion

    }
}
