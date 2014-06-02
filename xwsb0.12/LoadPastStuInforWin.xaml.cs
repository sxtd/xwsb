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

namespace xwsb0._11
{
    /// <summary>
    /// LoadPastStuInforWin.xaml 的交互逻辑
    /// 导入历次上报的学生信息到数据库中
    /// </summary>
    public partial class LoadPastStuInforWin : Window
    {
        public LoadPastStuInforWin()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择历次上报数据的dbf的文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_browse_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "选择目录";
            ofd.Filter = "dbf文件|*.dbf";
            DialogResult rs = ofd.ShowDialog();
            if (rs == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            else
            {
                tb_filePath.Text = ofd.FileName;
            }
        }

        /// <summary>
        /// 导入dbf文件到数据库中，使用dbf文件名作为时间字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_load_Click(object sender, RoutedEventArgs e)
        {
            if (tb_filePath.Text == "")
            {
                System.Windows.MessageBox.Show("请先选择文件位置");
            }
            else
            {
                string filePath = tb_filePath.Text;
                if (DB.preXwzsToDB(filePath))
                {
                    string fileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                    string sbrq = fileName.Substring(0, fileName.IndexOf(".DBF"));

                    List<StuFinal> sfList = DB.getPastXwsb(sbrq);
                    dg_pastXwsb.DataContext = sfList;

                    System.Windows.MessageBox.Show("数据已导入数据库！");
                }
                else
                {
                    System.Windows.MessageBox.Show("数据导入失败！");
                }

               
            }
        }

    }
}
