using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace xwsb0._11
{
    /// <summary>
    /// DmXbWin.xaml 的交互逻辑
    /// </summary>
    public partial class DmXbWin : Window
    {
        public DmXbWin()
        {
            InitializeComponent();
            getDm();
        }

        /// <summary>
        /// 显示性别代码表
        /// </summary>
        private void getDm()
        {
            List<Xb> xblist = new List<Xb>();

            xblist = DB.getDmXb();

            this.dg_dmXb.DataContext = xblist;
        }

        private void bt_xbdmModify_Click(object sender, RoutedEventArgs e)
        {
            this.dg_dmXb.IsEnabled = true;
        }

        private void bt_xbdmClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }


    public class Xb
    {
        public string xb { get; set; }
        public string xbm { get; set; }
    }
}
