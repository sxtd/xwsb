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
    /// 在当前上报学生名单xwzs_init_temp基础上，再从原始名单xwzs_init中添加部分学生
    /// </summary>
    public partial class LoadXsmdWinAdd: Window
    {
        LoadXsmdWin lxWin;
        public LoadXsmdWinAdd(LoadXsmdWin lxw)
        {
            InitializeComponent();
            lxWin = lxw;
        }
        
        /// <summary>
        /// 从原始名单中xwzs_init中，将符合筛选条件的学生添加到本次需要上报的学生名单xwzs_init_temp中
        /// 筛选条件：打印时间，打印时间+学号开头
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_addxsmdfilter_Click(object sender, RoutedEventArgs e)
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
                //采用打印时间作为筛选条件
                else if (ck_printtime.IsChecked == true && ck_xh.IsChecked == false)
                {

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
                    MessageBoxResult result = System.Windows.MessageBox.Show("本次增加的学生条件：打印时间：起始时间：" + startTime + "。结束时间：" + endTime, "确认窗口", System.Windows.MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    this.lxWin.lb_filterList.Items.Add("增加：起始时间：" + startTime + "。结束时间：" + endTime);

                    //开始连接数据库，从xwzs_init中筛选符合打印时间范围的学生
                    DB.FilterXwzsByPrinttime(startTime, endTime, false);

                    List<StuInit> stuInitList = DB.getStuInitTempList();
                    int xsmd_count = DB.getStuInitTempCount();
                    this.lxWin.dg_xwzs_init.DataContext = stuInitList;
                    this.lxWin.tb_StuListCount.Text = xsmd_count.ToString();

                    MessageBox.Show("已完成");

                    this.Hide();

                }//end:采用打印时间筛选
                //采用学号筛选，暂不支持
                else if (ck_printtime.IsChecked == false && ck_xh.IsChecked == true)
                {
                    System.Windows.MessageBox.Show("目前版本不支持学号筛选，请输入打印时间");
                }
                //采用打印时间+学号开头作为筛选条件
                else if (ck_printtime.IsChecked == true && ck_xh.IsChecked == true)
                {
                    if (dp_starttime.SelectedDate == null || dp_endtime.SelectedDate == null || tb_xhstart.Text == null)
                    {
                        System.Windows.MessageBox.Show("请输入筛选的 起始打印时间、结束打印时间、学号开头。");
                        return;
                    }
                    //读取用户输入的打印时间 和学号开头
                    string startTime = dp_starttime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string endTime = dp_endtime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string xhStart = tb_xhstart.Text;
                    startTime += " 00:00:01";
                    endTime += " 23:59:59";
                    string xhstart = tb_xhstart.Text.ToString();
                    MessageBoxResult result = System.Windows.MessageBox.Show("本次增加的学生条件：打印时间：起始时间：" + startTime + "。结束时间：" + endTime + "。学号开头：" + xhstart, "确认窗口", System.Windows.MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    this.lxWin.lb_filterList.Items.Add("增加：起始时间：" + startTime + "。结束时间：" + endTime + "。学号开头：" + xhstart);

                    //从xwzs_init中筛选符合打印时间范围的学生
                    DB.FilterXwzsByPtXh(startTime, endTime, xhstart, false);

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
