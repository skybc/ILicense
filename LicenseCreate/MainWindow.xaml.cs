using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LicenseCreate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.dp.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
        }

        private void BCreateKey_Click(object sender, RoutedEventArgs e)
        {
            var (privatekey, publickey) = ILicense2.LicenseManage.CreateRsaKey();
            rtprivatekey.Text = privatekey;
            rtpublickey.Text = publickey;
        }

        private void BCreate_Click(object sender, RoutedEventArgs e)
        {
            ILicense2.Entity.LicenseConfig lc = new ILicense2.Entity.LicenseConfig();
            if (string.IsNullOrWhiteSpace(txtsoftcode.Text))
            {

                SweetAlertSharp.SweetAlert.Show("提示", "软件代号必须填写", SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
                return;
            }
            lc.SoftCode = txtsoftcode.Text.Trim();
            if (string.IsNullOrWhiteSpace(rtpublickey.Text))
            {
                SweetAlertSharp.SweetAlert.Show("提示", "公钥必须填写", SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
                return;
            }
            lc.PublicKey = rtpublickey.Text.Trim();
            lc.LicenseType = cbyj.IsChecked != null && cbyj.IsChecked.Value ? ILicense2.Entity.LicenseType.PermanentP : ILicense2.Entity.LicenseType.Limited;
            lc.CustomerName = txtUser.Text;
            lc.ClientType = ILicense2.Entity.ClientType.CS;
            
            lc.Due = Convert.ToDateTime(dp.Text);
            if (lc.Due < DateTime.Now)
            {
                SweetAlertSharp.SweetAlert.Show("提示", "授权到期时间不能小于当前时间");
                return;
            }
            try
            {
                txtLicense.Text = ILicense2.LicenseManage.GetLicense(lc, txtRigisterCode.Text,txt_data.Text);
            }
            catch (Exception ex)
            {
                SweetAlertSharp.SweetAlert.Show("错误", "生产失败，请检查秘钥信息.",SweetAlertSharp.Enums.SweetAlertButton.OK,SweetAlertSharp.Enums.SweetAlertImage.ERROR);

            }
        }

        private void BLocalRigisterCode_Click(object sender, RoutedEventArgs e)
        {
            txtRigisterCode.Text = ILicense2.LicenseManage.GetRegistration();
        }

        private void BCheck_Click(object sender, RoutedEventArgs e)
        {
            LicenseCheckWin.LicenseCheckMain.CheckLicense(rtprivatekey.Text, txtsoftcode.Text);
            //var (result, sercret) = ILicense2.LicenseManage.Verify(rtprivatekey.Text, txtsoftcode.Text, txtRigisterCode.Text, txtLicense.Text);
            //if (result == "ok")
            //{
            //    SweetAlertSharp.SweetAlert.Show("消息", $"验证成功,"+sercret.Data, SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
            //}
        }

        private void Cbyj_Checked(object sender, RoutedEventArgs e)
        {
            dp.Text = "2199-12-31";
            dp.IsEnabled = false;
        }

        private void Cbyj_Unchecked(object sender, RoutedEventArgs e)
        {
            dp.IsEnabled = true;
        }
    }
}
