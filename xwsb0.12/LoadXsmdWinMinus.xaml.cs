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
    /// AddXsmdWin.xaml 的交互逻辑
    /// 从本次上报的学生名单中xwzs_init_temp中删去部分学生，删除条件：打印时间，打印时间+学号开头
    /// </summary>
    public partial class LoadXsmdWinMinus : Window
    {
        LoadXsmdWin lxWin;
        public LoadXsmdWinMinus(LoadXsmdWin lxw)
        {
            InitializeComponent();
            lxWin = lxw;
        }

        /// <summary>
        /// 从当前上报学生名单中删除一部分学生，删除条件：打印时间，打印时间+学号开头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_minusxsmdfilter_Click(object sender, RoutedEventArgs e)
        {
            if (lxWin.dg_xwzs_init.DataContext == null)
            {
                MessageBox.Show("请先导入学生名单");
            }
            else
            {
                if (ck_printtime.IsChecked == false && ck_xh.IsChecked == false)
                {
                    System.Windows.MessageBox.Show("请选择一种学生筛选方式");
                }
                else if (ck_printtime.IsChecked == true && ck_xh.IsChecked == false)
                {
                    //采用打印时间作为筛选条件
                    if (dp_starttime.SelectedDate == null || dp_endtime.SelectedDate == null)
                    {
                        System.Windows.MessageBox.Show("请输入筛选的起始打印时间和结束打印时间");
                        return;
                    }
                    //读取用户输入的打印时间
                    string startTime = dp_starttime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string endTime = dp_endtime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    startTime += " 00:00:01";
                    endTime += " 23:59:59";
                    MessageBoxResult result = System.Windows.MessageBox.Show("本次减少的学生条件：打印时间：起始时间：" + startTime + "。结束时间：" + endTime, "确认窗口", System.Windows.MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    lxWin.lb_filterList.Items.Add("减少：起始时间：" + startTime + "。结束时间：" + endTime);


                    DB.FilterXwzsByPtDel(startTime, endTime);

                    List<StuInit> stuInitList = DB.getStuInitTempList();
                    int xsmd_count = DB.getStuInitTempCount();
                    this.lxWin.dg_xwzs_init.DataContext = stuInitList;
                    this.lxWin.tb_StuListCount.Text = xsmd_count.ToString();

                    MessageBox.Show("已完成");

                    this.Hide();


                }//end:采用打印时间筛选
                else if (ck_printtime.IsChecked == false && ck_xh.IsChecked == true)
                {
                    System.Windows.MessageBox.Show("目前版本不支持学号筛选，请输入打印时间");
                }
                else if (ck_printtime.IsChecked == true && ck_xh.IsChecked == true)
                {
                    //采用打印时间+学号开头作为筛选条件
                    //采用打印时间作为筛选条件
                    if (dp_starttime.SelectedDate == null || dp_endtime.SelectedDate == null || tb_xhstart.Text == null)
                    {
                        System.Windows.MessageBox.Show("请输入筛选的 起始打印时间、结束打印时间、学号开头。");
                        return;
                    }
                    //读取用户输入的打印时间 和学号开头
                    string startTime = dp_starttime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string endTime = dp_endtime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    startTime += " 00:00:01";
                    endTime += " 23:59:59";
                    string xhstart = tb_xhstart.Text.ToString();
                    MessageBoxResult result = System.Windows.MessageBox.Show("本次减少的学生条件：打印时间：起始时间：" + startTime + "。结束时间：" + endTime + "学号开头：" + xhstart, "确认窗口", System.Windows.MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    lxWin.lb_filterList.Items.Add("减少：起始时间：" + startTime + "。结束时间：" + endTime + "。学号开头：" + xhstart);

                    DB.FilterXwzsByPtXhDel(startTime, endTime, xhstart);

                    List<StuInit> stuInitList = DB.getStuInitTempList();
                    int xsmd_count = DB.getStuInitTempCount();
                    this.lxWin.dg_xwzs_init.DataContext = stuInitList;
                    this.lxWin.tb_StuListCount.Text = xsmd_count.ToString();

                    MessageBox.Show("已完成");

                    this.Hide();

                }//end:采用打印时间+学号开头作为筛选条件 
            }
        }
    }
}
