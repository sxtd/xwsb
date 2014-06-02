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
    /// DmMzWin.xaml 的交互逻辑
    /// </summary>
    public partial class DmMzWin : Window
    {
        public DmMzWin()
        {
            InitializeComponent();
            getDm();
        }

        /// <summary>
        /// 显示民族代码表
        /// </summary>
        private void getDm()
        {
            List<Mz> mzlist = new List<Mz>();

            mzlist = DB.getDmMz();

            this.dg_dmXb.DataContext = mzlist;
        }

    }

    public class Mz
    {
        public string mz { get; set; }
        public string mzm { get; set; }
    }
}
