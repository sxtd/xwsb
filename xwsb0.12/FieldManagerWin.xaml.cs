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
    /// FieldManagerWin.xaml 的交互逻辑
    /// 设定单个字段内容
    /// </summary>
    public partial class FieldManagerWin : Window
    {
        MainWindow mw;

        public static string zjlxm = "01";
        public static string zjlx = "中华人民共和国居民身份证";
        public static string pydwm = "10677";
        public static string pydw = "西南林业大学";
        public static string xzxm = "刘惠民";
        public static string zxxm = "刘惠民";

        public FieldManagerWin(MainWindow mw)
        {
            InitializeComponent();
            this.mw = mw;

            getContent();
        }

        /// <summary>
        /// 显示预先设定的字段内容
        /// </summary>
        private void getContent()
        {
            this.tb_xzxm.Text = xzxm;
            this.tb_zjlx.Text = zjlx;
            this.tb_pydwm.Text = pydwm;
            this.tb_pydw.Text = pydw;
            this.tb_zjlxm.Text = zjlxm;
            this.tb_zxxm.Text = zxxm;
        }

        /// <summary>
        /// ‘修改’字段按钮，使编辑框变成可修改状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_modifField_Click(object sender, RoutedEventArgs e)
        {
            tb_pydwm.IsEnabled = true;
            tb_pydw.IsEnabled = true;
            tb_xzxm.IsEnabled = true;
            tb_zjlx.IsEnabled = true;
            tb_zjlxm.IsEnabled = true;
            tb_zxxm.IsEnabled = true;
        }

        /// <summary>
        /// 保存已修改的字段内容，编辑框又变为不可修改状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_saveField_Click(object sender, RoutedEventArgs e)
        {
            xzxm = tb_xzxm.Text.ToString();
            zjlx = tb_zjlx.Text.ToString();
            pydwm = tb_pydwm.Text.ToString();
            pydw = tb_pydw.Text.ToString();
            zjlxm = tb_zjlxm.Text.ToString();
            zxxm = tb_zxxm.Text.ToString();

            MessageBox.Show("已更新字段");

            tb_pydwm.IsEnabled = false;
            tb_pydw.IsEnabled = false;
            tb_xzxm.IsEnabled = false;
            tb_zjlx.IsEnabled = false;
            tb_zjlxm.IsEnabled = false;
            tb_zxxm.IsEnabled = false;
        }

    }
}
