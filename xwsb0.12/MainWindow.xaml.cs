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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using System.IO;
using System.Windows.Forms;

namespace xwsb0._11
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public string wjc = "未检测";

        public bool mzStatus = false;
        public bool zzmmStatus = false;
        public bool zymcStatus = false;
        public bool fisrtReportStatus = false;

        public static MainWindow mw;

        /// <summary>
        /// 学位上报学生名单主窗口
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mw = this; //持有自身变量

            DB.TempTableInit();  //检测所需的临时表是否在数据库中

        }
        
        /// <summary>
        /// 打开导入学生名单窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openLoadWin_Click(object sender, RoutedEventArgs e)
        {
            LoadXsmdWin win = new LoadXsmdWin(this);
            win.ShowDialog();
        }

        /// <summary>
        /// 打开性别代码显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_dmXbMrg_Click(object sender, RoutedEventArgs e)
        {
            DmXbWin dmXbWin = new DmXbWin();
            dmXbWin.Show();
        }

        /// <summary>
        /// 打开民族代码显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_dmMzMrg_Click(object sender, RoutedEventArgs e)
        {
            DmMzWin dmw = new DmMzWin();
            dmw.Show();
        }

        /// <summary>
        /// 打开政治面貌代码显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_dmZzmmMrg_Click(object sender, RoutedEventArgs e)
        {
            DmZzmmWin dzw = new DmZzmmWin();
            dzw.Show();
        }

        /// <summary>
        /// 打开学位类型代码显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_dmXwlbMrg_Click(object sender, RoutedEventArgs e)
        {
            DmXwlbWin dxw = new DmXwlbWin();
            dxw.Show();
        }

        /// <summary>
        /// 打开专业名称代码显示窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_dmZymcMrg_Click(object sender, RoutedEventArgs e)
        {
            DmZydmWin dzw = new DmZydmWin();
            dzw.Show();
        }

        /// <summary>
        /// 检测表中的民族字段内容是否都在民族代码表中
        /// 如果不在代码表中，显示不正确的民族
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_testMz_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_stuFinalCount.Text == "0")
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }

            else 
            {
                if (DB.testMz())
                {
                    this.lb_mzstatus.Content = "已检测。正确！";
                    this.mzStatus = true;
                }
                else
                {
                    WrongMzWin wmw = new WrongMzWin();

                    wmw.ShowDialog();
                    //处理异常 打开一个异常窗口，显示民族不正确的学生列表

                    //处理异常后刷新表格
                    List<StuFinal> stuFinalList = new List<StuFinal>();
                    stuFinalList = DB.getStuFinalList();
                    dg_xwsbFinal.DataContext = stuFinalList;
                }
            }
        }

        /// <summary>
        /// 检测表中的政治面貌字段内容是否都在代码表中
        /// 如果不在代码表中，显示不正确的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_testZzmm_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_stuFinalCount.Text == "0")
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                if (DB.testZzmm())
                {
                    this.lb_zzmmstatus.Content = "已检测。正确！";
                    this.zzmmStatus = true;
                }
                else
                {
                    WrongZzmmWin wzw = new WrongZzmmWin();
                    wzw.ShowDialog();
                    //处理异常 打开一个异常窗口，显示政治面貌不正确的学生列表
                    List<StuFinal> stuFinalList = new List<StuFinal>();
                    stuFinalList = DB.getStuFinalList();
                    dg_xwsbFinal.DataContext = stuFinalList;
                }
            }
            
        }
        
        /// <summary>
        /// 检测表中的专业名称字段内容是否都在代码表中
        /// 如果不在代码表中，显示不正确的内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_testZymc_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_stuFinalCount.Text == "0")
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                if (DB.testZymc())
                {
                    this.lb_zymcstatus.Content = "已检测。正确！";
                    this.zymcStatus = true;
                }
                else
                {
                    //处理异常 打开一个异常窗口，显示专业名称不正确的学生列表
                    WrongZymcWin wzw = new WrongZymcWin();

                    wzw.ShowDialog();
                    List<StuFinal> stuFinalList = new List<StuFinal>();
                    stuFinalList = DB.getStuFinalList();
                    dg_xwsbFinal.DataContext = stuFinalList;
                }
            }
        }

        /// <summary>
        /// 检测本次上报名单中的学生是否都是首次上报，即不在历次上报表xwzs_pre中
        /// 如果已上报过，显示这些学生的名单，并将其从本表中删除。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ifFristReport_Click(object sender, RoutedEventArgs e)
        {
            if (tb_stuFinalCount.Text == "0")
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                List<StuFinal> doubleReportStuList = new List<StuFinal>();
                doubleReportStuList = DB.testFirstReport();
                if (doubleReportStuList.Count == 0)
                {
                    this.lb_testFirstReport.Content = "不含已经上报的学生。";
                    this.fisrtReportStatus = true;
                }
                else
                {
                    DoubleRepoertStuWin drsw = new DoubleRepoertStuWin(doubleReportStuList);
                    drsw.ShowDialog();
                    dg_xwsbFinal.DataContext = DB.getStuFinalList();
                    tb_stuFinalCount.Text = DB.getStuFinalCount().ToString();
                }
            }
        }

        /// <summary>
        /// 显示单个字段管理窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_singleField_Click(object sender, RoutedEventArgs e)
        {
            if (dg_xwsbFinal.DataContext == null)
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                FieldManagerWin fmw = new FieldManagerWin(this);
                fmw.ShowDialog();
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult rs = System.Windows.MessageBox.Show("是否关闭窗口", "提示", MessageBoxButton.YesNo);
            if (rs == MessageBoxResult.Yes)
            {
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 显示获学位证书日期设定窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_hxwrqField_Click(object sender, RoutedEventArgs e)
        {
            if (dg_xwsbFinal.DataContext == null)
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                FieldHxwrWin fhw = new FieldHxwrWin(this);

                fhw.Show();
            }
        }

        /// <summary>
        /// 各个字段都检测正确、并且设置了获学位证书的日期后，填充表格。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_fillTable_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_stuFinalCount.Text == "0")
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                bool hxwzsIsNull = true;

                if (!mzStatus || !zzmmStatus || !zymcStatus || !fisrtReportStatus)
                {
                    System.Windows.MessageBox.Show("请先检测名族、政治面貌、专业名称字段的正确性,是否包含已上报过的学生。");
                }
                else
                {
                    //检测获学位证书日期是否填写
                    List<StuFinal> sflist = dg_xwsbFinal.DataContext as List<StuFinal>;
                    for (int i = 0; i < sflist.Count; i++)
                    {
                        if (sflist[i].hxwrq == "")
                        {
                            hxwzsIsNull = true;
                            break;
                        }
                        else
                        {
                            hxwzsIsNull = false;
                        }
                    }
                    if (hxwzsIsNull)
                    {
                        System.Windows.MessageBox.Show("请先设定获学位日期");
                        return;
                    }
                    else
                    {
                        if (DB.fillXwzs(FieldManagerWin.zjlxm, FieldManagerWin.zjlx, FieldManagerWin.pydwm, FieldManagerWin.pydw, FieldManagerWin.xzxm, FieldManagerWin.zxxm))
                        {
                            System.Windows.MessageBox.Show("学位上报表格填充完成!");

                            List<StuFinal> stuFinalList = new List<StuFinal>();
                            stuFinalList = DB.getStuFinalList();
                            dg_xwsbFinal.DataContext = stuFinalList;
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("表格填充失败！");
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 将最终上报名单(xwzs_final)的内容导出成dbf格式文件，
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_TableToDBF_Click(object sender, RoutedEventArgs e)
        {
            if (this.tb_stuFinalCount.Text == "0")
            {
                System.Windows.MessageBox.Show("请先导入学生名单");
                return;
            }
            else
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();

                saveFileDialog.Title = "选择目录";
                saveFileDialog.Filter = "dbf文件|*.dbf|所有文件|*.*";
                saveFileDialog.RestoreDirectory = true;

                DialogResult result = saveFileDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }

                string fullpath = saveFileDialog.FileName;
                string filename = fullpath.Substring(fullpath.LastIndexOf("\\") + 1);
                string path = fullpath.Substring(0, fullpath.LastIndexOf("\\"));

                List<StuFinal> sflist = dg_xwsbFinal.DataContext as List<StuFinal>;
                bool suc = DB.importDataIntoDBF(path, filename, sflist);

                if (suc)
                {
                    System.Windows.MessageBox.Show("已导出文件！");
                }
            }
        }

        /// <summary>
        /// 显示导入历次上报信息窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_loadPastStuInfor_Click(object sender, RoutedEventArgs e)
        {
            LoadPastStuInforWin lpsiwin = new LoadPastStuInforWin();
            lpsiwin.Show();
        }

        /// <summary>
        /// 显示所有历次上报信息窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mi_ViewPastStuInfor_Click(object sender, RoutedEventArgs e)
        {
            ViewPastStuInforWin vpsi = new ViewPastStuInforWin();
            vpsi.Show();
        }

    }
}
