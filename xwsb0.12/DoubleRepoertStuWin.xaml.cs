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
    /// doubleRepoertStuWin.xaml 的交互逻辑
    /// </summary>
    public partial class DoubleRepoertStuWin : Window
    {
        public DoubleRepoertStuWin(List<StuFinal> stuList)
        {
            InitializeComponent();
            dg_doubleReportStu.DataContext = stuList;
        }

        /// <summary>
        /// 从学位上报名单中将重复上报的学生删去
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_deleteDoubleReport_Click(object sender, RoutedEventArgs e)
        {
            if (DB.deleteDoubleReportStu())
            {
                MessageBox.Show("已将上报过的学生从本表中删除！");
                this.Close();
            }
            else
            {
                MessageBox.Show("删除失败");
            }
        }

    }
}
