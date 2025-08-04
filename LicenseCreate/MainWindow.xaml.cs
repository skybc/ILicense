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
            
            // 不再强制最大化，让用户根据需要调整窗口大小
            // this.WindowState = WindowState.Maximized;
        }

        private void BCreateKey_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var (privatekey, publickey) = ILicense2.LicenseManage.CreateRsaKey();
                rtprivatekey.Text = privatekey;
                rtpublickey.Text = publickey;
                
                SweetAlertSharp.SweetAlert.Show("成功", "RSA密钥对生成成功！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
            }
            catch (Exception ex)
            {
                SweetAlertSharp.SweetAlert.Show("错误", $"密钥生成失败：{ex.Message}", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.ERROR);
            }
        }

        private void BCreate_Click(object sender, RoutedEventArgs e)
        {
            ILicense2.Entity.LicenseConfig lc = new ILicense2.Entity.LicenseConfig();
            if (string.IsNullOrWhiteSpace(txtsoftcode.Text))
            {
                SweetAlertSharp.SweetAlert.Show("提示", "软件代号必须填写", SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
                txtsoftcode.Focus();
                return;
            }
            lc.SoftCode = txtsoftcode.Text.Trim();
            if (string.IsNullOrWhiteSpace(rtpublickey.Text))
            {
                SweetAlertSharp.SweetAlert.Show("提示", "公钥必须填写", SweetAlertSharp.Enums.SweetAlertButton.OK, SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
                rtpublickey.Focus();
                return;
            }
            lc.PublicKey = rtpublickey.Text.Trim();
            lc.LicenseType = cbyj.IsChecked != null && cbyj.IsChecked.Value ? ILicense2.Entity.LicenseType.PermanentP : ILicense2.Entity.LicenseType.Limited;
            lc.CustomerName = txtUser.Text;
            lc.ClientType = ILicense2.Entity.ClientType.CS;
            
            lc.Due = Convert.ToDateTime(dp.Text);
            if (lc.Due < DateTime.Now && !cbyj.IsChecked.GetValueOrDefault())
            {
                SweetAlertSharp.SweetAlert.Show("提示", "授权到期时间不能小于当前时间");
                dp.Focus();
                return;
            }
            try
            {
                txtLicense.Text = ILicense2.LicenseManage.GetLicense(lc, txtRigisterCode.Text,txt_data.Text);
                SweetAlertSharp.SweetAlert.Show("成功", "授权密钥生成成功！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
            }
            catch (Exception ex)
            {
                SweetAlertSharp.SweetAlert.Show("错误", $"生成失败，请检查密钥信息：{ex.Message}",SweetAlertSharp.Enums.SweetAlertButton.OK,SweetAlertSharp.Enums.SweetAlertImage.ERROR);
            }
        }

        private void BLocalRigisterCode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtRigisterCode.Text = ILicense2.LicenseManage.GetRegistration();
                SweetAlertSharp.SweetAlert.Show("成功", "本机注册码获取成功！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
            }
            catch (Exception ex)
            {
                SweetAlertSharp.SweetAlert.Show("错误", $"获取本机注册码失败：{ex.Message}", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.ERROR);
            }
        }

        private void BCheck_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LicenseCheckWin.LicenseCheckMain.CheckLicense(rtprivatekey.Text, txtsoftcode.Text);
            }
            catch (Exception ex)
            {
                SweetAlertSharp.SweetAlert.Show("错误", $"验证失败：{ex.Message}", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.ERROR);
            }
        }

        private void Cbyj_Checked(object sender, RoutedEventArgs e)
        {
            dp.Text = "2199-12-31";
            dp.IsEnabled = false;
        }

        private void Cbyj_Unchecked(object sender, RoutedEventArgs e)
        {
            dp.IsEnabled = true;
            dp.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
        }

        // Copy methods for improved user experience
        private void CopyPublicKey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtpublickey.Text))
            {
                Clipboard.SetText(rtpublickey.Text);
                SweetAlertSharp.SweetAlert.Show("成功", "公钥已复制到剪贴板！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
            }
            else
            {
                SweetAlertSharp.SweetAlert.Show("提示", "公钥为空，请先生成密钥对！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
            }
        }

        private void CopyPrivateKey_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(rtprivatekey.Text))
            {
                Clipboard.SetText(rtprivatekey.Text);
                SweetAlertSharp.SweetAlert.Show("成功", "私钥已复制到剪贴板！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
            }
            else
            {
                SweetAlertSharp.SweetAlert.Show("提示", "私钥为空，请先生成密钥对！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
            }
        }

        private void CopyLicense_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLicense.Text))
            {
                Clipboard.SetText(txtLicense.Text);
                SweetAlertSharp.SweetAlert.Show("成功", "授权密钥已复制到剪贴板！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
            }
            else
            {
                SweetAlertSharp.SweetAlert.Show("提示", "授权密钥为空，请先生成授权密钥！", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.INFORMATION);
            }
        }

        private void SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var saveFileDialog = new Microsoft.Win32.SaveFileDialog
                {
                    Filter = "文本文件 (*.txt)|*.txt|JSON文件 (*.json)|*.json|所有文件 (*.*)|*.*",
                    DefaultExt = "txt",
                    FileName = $"LicenseConfig_{DateTime.Now:yyyyMMdd_HHmmss}"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    var config = new
                    {
                        SoftwareCode = txtsoftcode.Text,
                        CustomerName = txtUser.Text,
                        RegistrationCode = txtRigisterCode.Text,
                        ExpirationDate = dp.Text,
                        IsPermanent = cbyj.IsChecked.GetValueOrDefault(),
                        CustomData = txt_data.Text,
                        PublicKey = rtpublickey.Text,
                        GeneratedLicense = txtLicense.Text,
                        CreatedDate = DateTime.Now
                    };

                    string content;
                    if (saveFileDialog.FileName.EndsWith(".json"))
                    {
                        content = Newtonsoft.Json.JsonConvert.SerializeObject(config, Newtonsoft.Json.Formatting.Indented);
                    }
                    else
                    {
                        content = $@"密钥管理系统配置文件
===========================
软件代号: {config.SoftwareCode}
被授权人: {config.CustomerName}
注册码: {config.RegistrationCode}
到期时间: {config.ExpirationDate}
永久授权: {config.IsPermanent}
自定义数据: {config.CustomData}
创建时间: {config.CreatedDate}

公钥:
{config.PublicKey}

生成的授权密钥:
{config.GeneratedLicense}";
                    }

                    System.IO.File.WriteAllText(saveFileDialog.FileName, content, Encoding.UTF8);
                    SweetAlertSharp.SweetAlert.Show("成功", "配置已保存成功！", 
                        SweetAlertSharp.Enums.SweetAlertButton.OK, 
                        SweetAlertSharp.Enums.SweetAlertImage.SUCCESS);
                }
            }
            catch (Exception ex)
            {
                SweetAlertSharp.SweetAlert.Show("错误", $"保存配置失败：{ex.Message}", 
                    SweetAlertSharp.Enums.SweetAlertButton.OK, 
                    SweetAlertSharp.Enums.SweetAlertImage.ERROR);
            }
        }
    }
}
