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
    /// DmXwlbWin.xaml 的交互逻辑
    /// </summary>
    public partial class DmXwlbWin : Window
    {
        public DmXwlbWin()
        {
            InitializeComponent();
            getDm();
        }

        /// <summary>
        /// 显示学位类别代码表
        /// </summary>
        private void getDm()
        {
            List<Xwlb> xlist = new List<Xwlb>();
            xlist = DB.getDmXwlb();
            this.dg_dmXwlb.DataContext = xlist;
        }
    }

    public class Xwlb
    {
        public string xwlb { get; set; }
        public string xwlbm { get; set; }
        public string xwlbold { get; set; }
    }
}
