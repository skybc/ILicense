using ILicense2.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LicenseCheckWin
{
    /// <summary>
    /// WinRegisiter.xaml 的交互逻辑
    /// </summary>
    partial class WinRegisiter : Window
    {
        /// <summary>
        /// 
        /// </summary>
        public SecretKey Secretkey { get; internal set; }
        public string Key { get; internal set; }
        public string SoftCode { get; internal set; }
        public string FileName { get; set; }
        public WinRegisiter()
        {
            InitializeComponent();
            this.Loaded += WinRegisiter_Loaded;
        }

        private void WinRegisiter_Loaded(object sender, RoutedEventArgs e)
        {
            txtSerialCode.Text = ILicense2.LicenseManage.GetRegistration();
            this.labSoftcode.Content = SoftCode;

        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisiter_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var regisitor = txtRegisitor.Text;
                var re = ILicense2.LicenseManage.Verify(Key, SoftCode, ILicense2.LicenseManage.GetRegistration(), regisitor);
                if (re.result != "ok")
                {
                    MessageBox.Show(re.result);
                }
                else
                {
                    this.Secretkey = re.secretkey;
                    if (File.Exists(FileName))
                    {
                        File.Delete(FileName);
                    }
                    using (var stream = new StreamWriter(File.Open(FileName, FileMode.OpenOrCreate)))
                    {
                        stream.Write(regisitor);
                        stream.Flush();
                        stream.Close();
                        stream.Dispose();
                    }
                    this.DialogResult = true;
                    this.Close();

                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
