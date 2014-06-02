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
using System.ComponentModel;

namespace xwsb0._11
{
    /// <summary>
    /// DmZydmWin.xaml 的交互逻辑
    /// </summary>
    public partial class DmZydmWin : Window
    {
        public DmZydmWin()
        {
            InitializeComponent();
            getDm();
        }

        private void ti_zydm_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            getDm();
        }

        /// <summary>
        /// 显示专业名称代码表
        /// </summary>
        private void getDm()
        {
            List<Zymc> zlist = new List<Zymc>();

            zlist = DB.getDmZymc();

            this.dg_dmZymc.DataContext = zlist;
        }

        /// <summary>
        /// 显示原始专业代码表
        /// </summary>
        private void getDmInit()
        {
            List<ZymcInit> zilist = new List<ZymcInit>();

            zilist = DB.getDmZymcInit();

            this.dg_dmZymcInit.DataContext = zilist;
        }

        private void ti_zydmInit_Initialized(object sender, EventArgs e)
        {
            getDmInit();
        }
    }

    public class Zymc 
    {
        public string zydm { get; set; }
        public string zymc { get; set; }
        public string zszymc { get; set; }
    }

    public class ZymcInit
    {
        public string zydm { get; set; }
        public string zymc { get; set; }
    }
}
