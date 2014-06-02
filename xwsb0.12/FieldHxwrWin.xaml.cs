using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;

namespace xwsb0._11
{
    /// <summary>
    /// FieldHxwrWin.xaml 的交互逻辑
    /// </summary>
    public partial class FieldHxwrWin : Window
    {
        MainWindow mw;

        bool loadMdSuccess = false;

        public FieldHxwrWin(MainWindow mw)
        {
            this.mw = mw;
            InitializeComponent();
        }
        
        /// <summary>
        /// 清空所有获学位日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_cleanAllHxwrq_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBoxResult rs = System.Windows.MessageBox.Show("是否要删除所有学生的获学位日期?", "提示", MessageBoxButton.YesNo);
            if (rs == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                DB.cleanAllHxwrq();

                List<StuFinal> stuFinalList = new List<StuFinal>();
                stuFinalList = DB.getStuFinalList();
                mw.dg_xwsbFinal.DataContext = stuFinalList;

                System.Windows.MessageBox.Show("已清空所有获学位日期");
            }        
        }

        /// <summary>
        /// 统一设置获学位日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_allHxwrqConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (tb_allHxwrq.Text == "")
            {
                System.Windows.MessageBox.Show("请输入获学位日期");
            }
            else
            {
                string hxwrq = tb_allHxwrq.Text.ToString();

                DB.setAllHxwrq(hxwrq);

                List<StuFinal> stuFinalList = new List<StuFinal>();
                stuFinalList = DB.getStuFinalList();
                mw.dg_xwsbFinal.DataContext = stuFinalList;

                System.Windows.MessageBox.Show("设定成功");
                this.Close();
            }
        }

        /// <summary>
        /// 分批设定获学位日期时，浏览excel文件位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_openFile_Click(object sender, RoutedEventArgs e)
        {
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Title = "选择文件";
            //openFileDialog.Filter = "txt文件|*.txt|cvs文件|*.csv";
            //DialogResult result = openFileDialog.ShowDialog();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择文件";
            openFileDialog.Filter = "excel文件|*.xls";
            DialogResult result = openFileDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string fileName = openFileDialog.FileName;
            this.tb_mdPath.Text = fileName;
        }

        /// <summary>
        /// 分批设定获学位日期，将学生名单excel导入数据库中xwzs_hxwrqmd
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_confirmMd_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBoxResult rs = System.Windows.MessageBox.Show("请保证导入的excel文件已删除标题行，并且前三列为序号、学号、姓名！","提示",MessageBoxButton.YesNo);

            if (rs == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                string filePath = tb_mdPath.Text;

                loadMdSuccess = DB.loadHxwrqMd(filePath);

                if (loadMdSuccess)
                {
                    System.Windows.MessageBox.Show("名单已经导入数据库，请设定名单的获学位日期，并刷新");
                }
                else
                {
                    System.Windows.MessageBox.Show("名单导入失败！");
                }
            }
        }

        /// <summary>
        /// 分批设定获学位日期，按上传名单设定获学位日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_refreshHxwrq_part1_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)rb_MdHxwrq.IsChecked)
            {
                System.Windows.MessageBox.Show("请选择按上传名单设置获学位日期的方式");
            }
            else
            {
                if (!loadMdSuccess || tb_mdHxwrq_part1.Text == "")
                {
                    System.Windows.MessageBox.Show("请上传名单并输入获学位日期方式");
                }
                else
                {
                    string hxwrq = tb_mdHxwrq_part1.Text.ToString();
                    DB.setHxwrqPart1(hxwrq);
                    
                    List<StuFinal> stuFinalList = new List<StuFinal>();
                    stuFinalList = DB.getStuFinalList();
                    mw.dg_xwsbFinal.DataContext = stuFinalList;

                    System.Windows.MessageBox.Show("设定成功");
                }
            }
           
        }

        /// <summary>
        ///  分批设定获学位日期，按打印时间和学号确定获学位日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_refreshHxwrq_part2_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)rb_ppXhHxwrq.IsChecked)
            {
                System.Windows.MessageBox.Show("请选择按照打印时间和学号开头设定获学位日期方式");
            }
            else
            {
                if (dp_starttime.SelectedDate == null || dp_endtime.SelectedDate == null || tb_xhstart.Text == "" || tb_ppXhHxwrq_part2.Text == "")
                {
                    System.Windows.MessageBox.Show("请输入打印起始时间、结束时间、学号开头和获学位日期时间");
                }
                else
                {
                    string startTime = dp_starttime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    string endTime = dp_endtime.SelectedDate.Value.ToString("yyyy-MM-dd");
                    startTime += " 00:00:01";
                    endTime += " 23:59:59";
                    string xhStart = tb_xhstart.Text.ToString();
                    string hxwrq = tb_ppXhHxwrq_part2.Text.ToString();

                    DB.setHxwrqPart2(startTime, endTime,xhStart, hxwrq);

                    List<StuFinal> stuFinalList = new List<StuFinal>();
                    stuFinalList = DB.getStuFinalList();
                    mw.dg_xwsbFinal.DataContext = stuFinalList;
                    System.Windows.MessageBox.Show("设定成功");
                }
                
            }
        }

        /// <summary>
        /// 分批设定获学位日期，剩余部分学生或学位日期设定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_refreshHxwrq_part3_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)rb_otherHxwrq.IsChecked)
            {
                System.Windows.MessageBox.Show("请选择剩余部分获学位时间");
            }
            else
            {
                string hxwrq = tb_otherHxwrq_part3.Text.ToString();
                DB.setHxwrqPart3(hxwrq);

                List<StuFinal> stuFinalList = new List<StuFinal>();
                stuFinalList = DB.getStuFinalList();
                mw.dg_xwsbFinal.DataContext = stuFinalList;
                System.Windows.MessageBox.Show("设定成功");
            }
        }

        /// <summary>
        /// 清空所有获学位日期字段内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_cleanHxwrq_Click(object sender, RoutedEventArgs e)
        {
             System.Windows.MessageBoxResult rs = System.Windows.MessageBox.Show("是否要删除所有学生的获学位日期?", "提示", MessageBoxButton.YesNo);
             if (rs == MessageBoxResult.No)
             {
                 return;
             }
             else
             {
                 DB.cleanAllHxwrq();

                 List<StuFinal> stuFinalList = new List<StuFinal>();
                 stuFinalList = DB.getStuFinalList();
                 mw.dg_xwsbFinal.DataContext = stuFinalList;

                 System.Windows.MessageBox.Show("已清空获学位日期");
             }
        }


    }
}
