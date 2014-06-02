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
    /// ViewPastStuInforWin.xaml 的交互逻辑
    /// 查看历次上报的学生信息窗口
    /// </summary>
    public partial class ViewPastStuInforWin : Window
    {
        public ViewPastStuInforWin()
        {
            InitializeComponent();

            InitialComboBox();
        }

        /// <summary>
        /// 初始化下拉列表。显示历次上报时间
        /// </summary>
        public void InitialComboBox()
        {
            List<string> pastXwsbTableName = DB.getPastXwsbTableName();

            this.cb_pastXwzzTableName.ItemsSource = pastXwsbTableName;
        }

        /// <summary>
        /// 将用户所选的数据库中历次上报的学生名单显示在本窗口中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_TableNameConfirm_Click(object sender, RoutedEventArgs e)
        {
            string tableName = cb_pastXwzzTableName.Text;

            this.dg_pastXwsb.DataContext = DB.getPastXwsb(tableName);

        }

        /// <summary>
        /// 用窗口中显示的历次上报的学生名单中的专业名称、专业代码刷新专业代码表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_refreshZydm_Click(object sender, RoutedEventArgs e)
        {
            if (dg_pastXwsb.DataContext == null)
            {
                MessageBox.Show("请先选择一个历年学位上报表");
            }
            else
            {
                MessageBoxResult rs = MessageBox.Show("确定要用本表中的专业代码更新专业代码表吗？","提示", MessageBoxButton.YesNo);
                if (rs == MessageBoxResult.No)
                {
                    return;
                }
                else
                {
                   int refreshCount = DB.refreshDmZymc(cb_pastXwzzTableName.Text);
                   MessageBox.Show("已更新专业代码表，更新条数为：" + refreshCount);
                }
            }
            
        }

    }
}
