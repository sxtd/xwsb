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
    /// DmZzmmWin.xaml 的交互逻辑
    /// </summary>
    public partial class DmZzmmWin : Window
    {
        public DmZzmmWin()
        {
            InitializeComponent();
            getDm();
        }

        /// <summary>
        /// 显示政治面貌代码表
        /// </summary>
        private void getDm()
        {
            List<Zzmm> zlist = new List<Zzmm>();
            zlist = DB.getDmZzmm();
            this.dg_dmZzmm.DataContext = zlist;
        }
    }

    public class Zzmm
    {
        public string zzmm { get; set; }
        public string zzmmm { get; set; }
    }
}
