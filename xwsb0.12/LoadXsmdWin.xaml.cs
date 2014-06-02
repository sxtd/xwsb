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
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace xwsb0._11
{
    /// <summary>
    /// LoadXsmd.xaml 的交互逻辑
    /// 从DB.mdb中导入原始学生名单到数据库中，从而筛选出本次上报的学生
    /// </summary>
    public partial class LoadXsmdWin : Window
    {
        public static LoadXsmdWin lxWin;
        MainWindow mw;

        public LoadXsmdWin(MainWindow mw)
        {
            InitializeComponent();
            lxWin = this;
            this.mw = mw;
        }

        /// <summary>
        /// 导入DB.mdb中用户选择的表的名单到数据库中，（表名为xwzs20XX），显示在窗口中，并显示人数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_load_Click(object sender, RoutedEventArgs e)
        {
            if (cb_tbName.Text == null)
            {
                System.Windows.MessageBox.Show("请选择要加载的数据表！");
            } if (cb_tbName.Text.Substring(0, 4) != "XWZS")
            {
                System.Windows.MessageBox.Show("请选择XWZS开头的数据表！");
            }
            else
            {
                List<StuInit> xwzsList = new List<StuInit>();
                int xsmd_count = 0; //导入总人数记录

                xsmd_count = DB.loadXwzsFile(tb_dbPath.Text, cb_tbName.Text);
                if (xsmd_count == -1)
                {
                    return;
                }
                else
                {
                    xwzsList = DB.getStuInitList();

                    //dg_xwzs_init.ItemsSource = xwzsList;
                    dg_xwzs_init.DataContext = xwzsList;
                    //显示本次加载的学生数目
                    tb_StuListCount.Text = xsmd_count.ToString();
                    lb_filterList.Items.Clear();
                }
            }
        }

        /// <summary>
        /// 对学生进行首次筛选。
        /// 可以按打印时间，打印时间+学号开头 筛选，将筛选结果显示在窗口中，并显示人数
        /// 注意首次筛选和二次筛选的不同，首次过滤需要先清空数据库中的临时表xwzs_init_temp中的数据；
        /// 二次筛选直接在xwzs_init_temp表基础上进行增减
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_xsmdfilter_Click(object sender, RoutedEventArgs e)
        {
            if (!dg_xwzs_init.HasItems)
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
            }
            else
            {
                lb_filterList.Items.Clear();//如果多次进行‘首次筛选’，每次都是从xwzs_init中进行筛选，效果不会累加

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
                    MessageBoxResult result = System.Windows.MessageBox.Show("本次筛选的打印时间：起始时间：" + startTime + "。结束时间：" + endTime, "确认窗口", System.Windows.MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }

                    lb_filterList.Items.Add("起始时间：" + startTime + "。结束时间：" + endTime);

                    //开始连接数据库，从xwzs_init中筛选符合打印时间范围的学生
                    DB.FilterXwzsByPrinttime(startTime, endTime, true);
                    int xsmd_count = DB.getStuInitTempCount();

                    //System.Windows.MessageBox.Show("已完成");

                    List<StuInit> stuInitList = DB.getStuInitTempList();
                    dg_xwzs_init.DataContext = stuInitList;
                    tb_StuListCount.Clear();
                    tb_StuListCount.Text = xsmd_count.ToString();

                }//end:采用打印时间筛选
                //采用学号筛选，暂不支持
                else if (ck_printtime.IsChecked == false && ck_xh.IsChecked == true)
                {
                    System.Windows.MessageBox.Show("目前版本不支持学号筛选，请输入打印时间");
                }
                //采用打印时间+学号开头作为筛选条件
                else if (ck_printtime.IsChecked == true && ck_xh.IsChecked == true)
                {
                    if (dp_starttime.SelectedDate == null || dp_endtime.SelectedDate == null || tb_xhstart.Text == "")
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

                    MessageBoxResult result = System.Windows.MessageBox.Show("本次筛选的打印时间：起始时间：" + startTime + "。结束时间：" + endTime + "。学号开头：" + xhstart, "确认窗口", System.Windows.MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.Cancel)
                    {
                        return;
                    }
                    lb_filterList.Items.Add("起始时间：" + startTime + "。结束时间：" + endTime + "。学号开头：" + xhstart);

                    //从xwzs_init中筛选符合打印时间范围的学生
                    DB.FilterXwzsByPtXh(startTime, endTime, xhstart, true);
                    int xsmd_count = DB.getStuInitTempCount();
                    //System.Windows.MessageBox.Show("已完成");

                    List<StuInit> stuInitList = DB.getStuInitTempList();
                    dg_xwzs_init.DataContext = stuInitList;

                    tb_StuListCount.Clear();
                    tb_StuListCount.Text = xsmd_count.ToString();


                }//end:采用打印时间+学号开头作为筛选条件
            }   
        }

        /// <summary>
        /// 在首次筛选的基础上，进行二次筛选，打开‘在xwzs_init_temp中增加xwzs_init中的部分学生’窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_addXsmdFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadXsmdWinAdd axw = new LoadXsmdWinAdd(this);
            axw.ShowDialog();
        }

        /// <summary>
        /// 在首次筛选的基础上，进行二次筛选，打开‘在xwzs_init_temp中减少部分学生’窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_minusXsmdFilter_Click(object sender, RoutedEventArgs e)
        {
            LoadXsmdWinMinus mxw = new LoadXsmdWinMinus(this);
            mxw.ShowDialog();
        }

        /// <summary>
        /// 确定本次上报的学生人数，将临时表名单存入含最后字段的名单中，显示在主窗口中，并显示人数。筛选窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_xwzsInitConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!lb_filterList.HasItems)
            {
                System.Windows.MessageBox.Show("请先对导入的学生名单进行筛选");
            }
            else 
            {
                DB.insertInitToFinal();

                List<StuFinal> stuFinalList = new List<StuFinal>();

                stuFinalList = DB.getStuFinalList();

                mw.dg_xwsbFinal.DataContext = stuFinalList;
                mw.tb_stuFinalCount.Text = DB.getStuFinalCount().ToString();
                
                mw.lb_mzstatus.Content = mw.wjc;
                mw.lb_zymcstatus.Content = mw.wjc;
                mw.lb_zzmmstatus.Content = mw.wjc;
                mw.lb_testFirstReport.Content = mw.wjc;
                mw.mzStatus = false;
                mw.zzmmStatus = false;
                mw.zymcStatus = false;
                mw.fisrtReportStatus = false;

                this.Close();
            }
        }

        /// <summary>
        /// 加载DB.mdb中的所有表名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_loadAccess_Click(object sender, RoutedEventArgs e)
        {
            if (tb_dbPath.Text == "")
            {
                System.Windows.MessageBox.Show("请选择Access文件的存放位置！");
                return;
            }
            else
            {
                List<string> dbTableList = DB.loadDbTable(tb_dbPath.Text.ToString());

                cb_tbName.ItemsSource = dbTableList;
            }
        }

        /// <summary>
        /// 选择DB.mdb文件的位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_dbPath_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择数据库位置";
            ofd.Filter = "access文件|*.mdb";
            DialogResult result = ofd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            else
            {
                tb_dbPath.Text = ofd.FileName;
            }
        }

       
    }
}
