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
    public partial class WrongZymcWin : Window
    {
        /// <summary>
        /// 是否成功更新代码表字段
        /// </summary>
        bool testZymcSuccess = true;

        public WrongZymcWin()
        {
            InitializeComponent();
            getZymcFailList();
        }

        /// <summary>
        /// 获得专业名称在代码表中无对应代码的名称列表，显示在窗口中
        /// </summary>
        private void getZymcFailList()
        {
            List<Zymc> unmatchedZymcList = new List<Zymc>();

            unmatchedZymcList = DB.getUnmatchedZymcList();

            dg_zymcFail.DataContext = unmatchedZymcList;
        }

        /// <summary>
        /// tabitem初始化，显示专业代码表。。。我也不知道是个什么意思
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ti_zydm_Initialized(object sender, EventArgs e)
        {
            getDm();
        }

        /// <summary>
        /// 另一个tabitem初始化，显示原始专业代码表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ti_zydmInit_Initialized(object sender, EventArgs e)
        {
            getDmInit();
        }

        /// <summary>
        /// 获得专业代码表
        /// </summary>
        private void getDm()
        {
            List<Zymc> zlist = new List<Zymc>();

            zlist = DB.getDmZymc();

            this.dg_dmZymc.DataContext = zlist;
        }

        /// <summary>
        /// 获得原始专业代码表,来源于DB.mdb
        /// </summary>
        private void getDmInit()
        {
            List<ZymcInit> zilist = new List<ZymcInit>();

            zilist = DB.getDmZymcInit();

            this.dg_dmZymcInit.DataContext = zilist;
        }
        
        /// <summary>
        /// 为当前专业名称设定专业代码，从专业代码表或原始专业代码表中选择。注意：上报名单中的学生专业不能修改，但民族可以修改，民族有可能出现错别字或为空的情况
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_setZymcDm_Click(object sender, RoutedEventArgs e)
        {
            List<Zymc> unmatchedZymcList = dg_zymcFail.DataContext as List<Zymc>;

            if (dg_zymcFail.SelectedIndex == -1)
            {
                MessageBox.Show("请选择要设定代码的专业！");
                return;
            }
            else
            {
                if (this.ti_zydm.IsSelected) //如果从本校的专业代码表中查找专业码
                {
                    if (this.dg_dmZymc.SelectedIndex == -1)
                    {
                        MessageBox.Show("请在代码列表中选择该专业的代码");
                    }
                    else
                    {
                        Zymc z = dg_dmZymc.SelectedItem as Zymc;

                        unmatchedZymcList[dg_zymcFail.SelectedIndex].zydm = z.zydm;
                        unmatchedZymcList[dg_zymcFail.SelectedIndex].zymc = z.zymc;

                        dg_zymcFail.DataContext = null;
                        dg_zymcFail.DataContext = unmatchedZymcList;
                    }
                   
                }
                if (ti_zydmInit.IsSelected) //如果从原始专业代码表中查找专业码
                {
                    if (this.dg_dmZymcInit.SelectedIndex == -1)
                    {
                        MessageBox.Show("请在原始代码列表中选择该专业的代码");
                    }
                    else
                    {
                        ZymcInit z = this.dg_dmZymcInit.SelectedItem as ZymcInit;

                        unmatchedZymcList[dg_zymcFail.SelectedIndex].zydm = z.zydm;
                        unmatchedZymcList[dg_zymcFail.SelectedIndex].zymc = z.zymc;

                        dg_zymcFail.DataContext = null;
                        dg_zymcFail.DataContext = unmatchedZymcList;
                    }
                }
                return;
            }            
        }

        /// <summary>
        /// 提交本次操作，将确定专业代码的专业名称、专业自设名称、专业代码，写入代码表中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_zymcConfim_Click(object sender, RoutedEventArgs e)
        {
            List<Zymc> unmatchedZymcList = dg_zymcFail.DataContext as List<Zymc>;

            for (int i = 0; i < unmatchedZymcList.Count; i++)
            {
                Zymc z = unmatchedZymcList[i];
                if (z.zydm == null || z.zymc == null)
                {
                    System.Windows.MessageBox.Show("专业代码不允许为空，请确定所有专业代码");
                    testZymcSuccess = false;
                    break;
                }
                else
                {
                    testZymcSuccess = true;
                }

            }

            if (testZymcSuccess)
            {
                DB.setDmZymc(unmatchedZymcList);

                MessageBox.Show("已更新代码表");

                this.Close();
            }
            
        }

        /// <summary>
        /// 关闭窗口，如果没有成功更新代码表，需要删除专业代码表中没有自设专业名称和专业代码的专业名称。
        /// 因为刚刚找出这些没有对应代码的专业名称时就插入到了专业代码表中。程序猿bug,sorry
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!testZymcSuccess)
            {
                List<Zymc> unmatchedZymcList = dg_zymcFail.DataContext as List<Zymc>;

                DB.deleteUnmatchedZymc(unmatchedZymcList);                
            }
           
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
