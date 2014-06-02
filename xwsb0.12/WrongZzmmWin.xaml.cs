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
    /// WrongZzmmWin.xaml 的交互逻辑
    /// 错误的政治面貌处理窗口
    /// </summary>
    public partial class WrongZzmmWin : Window
    {
        /// <summary>
        /// 窗口初始化，显示错误名称的政治面貌和政治面貌代码表
        /// </summary>
        public WrongZzmmWin()
        {
            InitializeComponent();
            getZzmmFailList();
            getDm();
        }

        /// <summary>
        /// 获得不在政治面貌代码表中的字段,显示出来
        /// </summary>
        private void getZzmmFailList()
        {
            List<FailZzmm> unmatchedZzmmList = new List<FailZzmm>();

            unmatchedZzmmList = DB.getUnmatchedZzmmList();
            dg_zzmmFail.DataContext = unmatchedZzmmList;
        }

        /// <summary>
        /// 获得政治面貌代码表，显示出来
        /// </summary>
        private void getDm()
        {
            List<Zzmm> zlist = new List<Zzmm>();

            zlist = DB.getDmZzmm();

            this.dg_dmZzmm.DataContext = zlist;
        }

        /// <summary>
        /// 为不匹配的政治面貌设定正确的名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_setNewZzmm_Click(object sender, RoutedEventArgs e)
        {
            List<FailZzmm> unmatchedMzList = dg_zzmmFail.DataContext as List<FailZzmm>;

            if (dg_zzmmFail.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要更改的政治面貌");
                return;
            }
            else
            {
                    if (this.dg_dmZzmm.SelectedIndex == -1)
                    {
                        MessageBox.Show("请在列表中选择正确写法的政治面貌");
                    }
                    else
                    {
                        Zzmm z = this.dg_dmZzmm.SelectedItem as Zzmm;

                        unmatchedMzList[this.dg_zzmmFail.SelectedIndex].newZzmm = z.zzmm ;

                        dg_zzmmFail.DataContext = null;
                        dg_zzmmFail.DataContext = unmatchedMzList;
                    }
            }            
        }

        /// <summary>
        /// 提交本次操作，将上报名单中的政治面貌改为新的名称
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ZzmmConfim_Click(object sender, RoutedEventArgs e)
        {
            bool testZzmmSuccess = true;

            List<FailZzmm> unmatchedZzmmList = dg_zzmmFail.DataContext as List<FailZzmm>;

            for (int i = 0; i < unmatchedZzmmList.Count; i++)
            {
                FailZzmm fz = unmatchedZzmmList[i];
                if (fz.newZzmm == "")
                {
                    System.Windows.MessageBox.Show("请确定正确的政治面貌，再点完成");
                    testZzmmSuccess = false;
                    break;
                }

            }

            if (testZzmmSuccess)
            {
                DB.refreshXwzsZzmm(unmatchedZzmmList);

                MessageBox.Show("已更新上报名单中的政治面貌字段");

                this.Close();
            }

        }
        
        /// <summary>
        /// 如果中途窗口关闭，不需要做任何操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    /// <summary>
    /// 旧政治面貌和新政治面貌的类
    /// </summary>
    public class FailZzmm
    {
        public string newZzmm { get; set; }
        public string oldZzmm { get; set; }
    }
}
