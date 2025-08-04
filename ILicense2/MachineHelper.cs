using System;
using System.Management;

namespace ILicense2
{
    static class MachineHelper
    {
        /// <summary>
        /// 获取CPUID
        /// </summary>
        /// <returns></returns>
        public static string GetCpuID()
        {
            try
            {
                //获取CPU序列号代码 
                string cpuInfo = "";//cpu序列号 
                ManagementClass mc = new ManagementClass("Win32_Processor");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    cpuInfo += mo.Properties["ProcessorId"].Value.ToString() + ",";
                }
                moc = null;
                mc = null;
                if (cpuInfo.Length > 0)
                {
                    cpuInfo = cpuInfo.Remove(cpuInfo.Length - 1, 1);
                }
                return cpuInfo;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }


        public static string GetMotherBoardID()

        {
            ManagementClass mc = new ManagementClass("Win32_BaseBoard");
            ManagementObjectCollection moc = mc.GetInstances();
            string strID = "";
            foreach (ManagementObject mo in moc)
            {
                strID = mo.Properties["SerialNumber"].Value.ToString();
                break;
            }

            return strID;
        }
        /// <summary>
        /// 获取网卡MAC地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //if ((bool)mo["IPEnabled"] == true)
                    //{
                        mac += mo["MacAddress"].ToString() + ",";
                    //    break;
                    //}
                }
                moc = null;
                mc = null;
                if (mac.Length > 0)
                {
                    mac = mac.Remove(mac.Length - 1, 1);
                }
                return mac;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIPAddress()
        {
            try
            {
                //获取IP地址 
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        //st=mo["IpAddress"].ToString(); 
                        System.Array ar;
                        ar = (System.Array)(mo.Properties["IpAddress"].Value);
                        st += ar.GetValue(0).ToString() + ",";
                        break;
                    }
                }
                moc = null;
                mc = null;
                if (st.Length > 0)
                {
                    st = st.Remove(st.Length - 1, 1);
                }
                return st;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            }

        }
        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskID()
        {
            try
            {
                //获取硬盘ID 
                String HDid = "";
                ManagementClass mc = new ManagementClass("Win32_DiskDrive");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    HDid = (string)mo.Properties["Model"].Value + ",";
                }
                moc = null;
                mc = null;
                if (HDid.Length > 0)
                {
                    HDid = HDid.Remove(HDid.Length - 1, 1);
                }
                return HDid;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }

        /// <summary> 
        /// 操作系统的登录用户名 
        /// </summary> 
        /// <returns></returns> 
        public static string GetUserName()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st += mo["UserName"].ToString() + ",";

                }
                if (st.Length > 0)
                {
                    st = st.Remove(st.Length - 1, 1);
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }

        /// <summary> 
        /// PC类型 
        /// </summary> 
        /// <returns></returns> 
        public static string GetSystemType()
        {
            try
            {
                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st += mo["SystemType"].ToString() + ",";

                }
                moc = null;
                mc = null;
                if (st.Length > 0)
                {
                    st = st.Remove(st.Length - 1, 1);
                }
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }

        }

        /// <summary> 
        /// 物理内存 
        /// </summary> 
        /// <returns></returns> 
        public static string GetTotalPhysicalMemory()
        {
            try
            {

                string st = "";
                ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {

                    st += mo["TotalPhysicalMemory"].ToString() + ",";

                }
                if (st.Length > 0)
                {
                    st = st.Remove(st.Length - 1, 1);
                }
                moc = null;
                mc = null;
                return st;
            }
            catch
            {
                return "";
            }
            finally
            {
            }
        }
        /// <summary> 
        /// 获取计算机名称
        /// </summary> 
        /// <returns></returns> 
        public static string GetComputerName()
        {
            try
            {
                return System.Environment.GetEnvironmentVariable("ComputerName");
            }
            catch
            {
                return "";
            }
            finally
            {
            }
        }
    }
}

