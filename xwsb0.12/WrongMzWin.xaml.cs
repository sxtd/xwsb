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
    /// WrongZymcWin.xaml 的交互逻辑
    /// 本次上报的学生名单中，学生的民族在民族表中，找不到对应的代码，需要人工确定学生的民族代码时，打开本窗口
    /// </summary>
    public partial class WrongMzWin : Window
    {
        /// <summary>
        /// 窗口初始化，获得民族不在民族表中的民族名称，同时显示民族代码表
        /// </summary>
        public WrongMzWin()
        {
            InitializeComponent();
            getMzFailList();
            getDm();
        }

        /// <summary>
        /// 获得当前上报学生名单中，学生民族在民族代码中，找不到相同民族名称的民族，显示出来
        /// </summary>
        private void getMzFailList()
        {
            List<FailMz> unmatchedMzList = new List<FailMz>();

            unmatchedMzList = DB.getUnmatchedMzList();
            dg_mzFail.DataContext = unmatchedMzList;
        }

        /// <summary>
        /// 显示名族代码表
        /// </summary>
        private void getDm()
        {
            List<Mz> zlist = new List<Mz>();

            zlist = DB.getDmMz();

            this.dg_dmMz.DataContext = zlist;
        }

        /// <summary>
        /// 为错误的民族设定民族代码表中的一个民族名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_setNewMz_Click(object sender, RoutedEventArgs e)
        {
            List<FailMz> unmatchedMzList = dg_mzFail.DataContext as List<FailMz>;

            if (dg_mzFail.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要更改的民族");
                return;
            }
            else
            {
                    if (this.dg_dmMz.SelectedIndex == -1)
                    {
                        MessageBox.Show("请在列表中选择该正确的民族");
                    }
                    else
                    {
                        Mz m = this.dg_dmMz.SelectedItem as Mz;

                        unmatchedMzList[this.dg_mzFail.SelectedIndex].newMz = m.mz ;

                        dg_mzFail.DataContext = null;
                        dg_mzFail.DataContext = unmatchedMzList;
                    }
            }            
        }

        /// <summary>
        /// 为每个无对应代码的民族确定正确名称后，将当前上报名单中这些书写方式的民族进行更新。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_MzConfim_Click(object sender, RoutedEventArgs e)
        {
            bool testMzSuccess = true;

            List<FailMz> unmatchedMzList = dg_mzFail.DataContext as List<FailMz>;

            for (int i = 0; i < unmatchedMzList.Count; i++)
            {
                FailMz fz = unmatchedMzList[i];
                if (fz.newMz == "")
                {
                    System.Windows.MessageBox.Show("请确定正确的民族，再点完成");
                    testMzSuccess = false;
                    break;
                }

            }

            if (testMzSuccess)
            {
                DB.refreshXwzsMz(unmatchedMzList);

                MessageBox.Show("已更新上报名单中的民族字段");

                this.Close();
            }

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    /// <summary>
    /// 旧的民族表达和新的民族表达字段的类
    /// </summary>
    public class FailMz
    {
        public string newMz { get; set; }
        public string oldMz { get; set; }
    }
}
