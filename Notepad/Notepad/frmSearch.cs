using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Notepad
{
    public partial class frmSearch : Form
    {
        int j = 1,t=0,y;
        private int FindStartPosition = 0;//定义光标起始位置
        private bool stringFound = false;
        private bool stringFound1= false;
        public string s;
        public frmSearch(frmNotepad parent)//构造函数写主窗体的初始化
        {
            InitializeComponent();
            paf = parent;
        }
        private frmNotepad paf; //定义私有主窗体实例
        private void button2_Click(object sender, EventArgs e)//取消按钮函数
        {
            this.Close();//关闭当前窗口
        }
        private void button1_Click(object sender, EventArgs e)//查找函数
        {
            if (down.Checked == true)//判断向下按钮checked属性是否为true
                j = 1;
            else
                j = 0;
            if (j == 1) //如果是向下查找
            {
                t+= paf.rtxtNotepad.SelectionStart;//把光标当前位置赋值给t+字符串长度(初始为0)
                FindStartPosition = t;
                int foundIndex = paf.rtxtNotepad.Find(textBox1.Text.ToString(), FindStartPosition, RichTextBoxFinds.None);//从文本开头开始寻找返回第一个搜索到的下标
                if (foundIndex == -1)//如果达到文本末尾
                {
                    if (stringFound)//判断是否没有找到字符
                    {
                        MessageBox.Show("已达文件尾");//提示
                        FindStartPosition = 0;//查找位置重新从文本开头开始
                    }
                    else
                    {
                        MessageBox.Show("没有找到输入的字符串");//提示
                    }
                }
                else
                {
                    paf.rtxtNotepad.Select(foundIndex, textBox1.Text.ToString().Length);//选中搜索到的字符串
                    stringFound = true;

                }
                t = textBox1.Text.ToString().Length;
           }
           else//如果是向上查找
           {
               try//避免查找到文件开头的异常
              {
                    y = paf.rtxtNotepad.Text.ToString().LastIndexOf(textBox1.Text.ToString(), paf.rtxtNotepad.SelectionStart - textBox1.Text.ToString().Length);//查找当前光标位置前所有字符串中所要查找的最后一次出现的下标

               }
                catch(Exception ex)
               {
                   MessageBox.Show("已达文件头");
               }
                if (y == -1)
            {           
                if (stringFound1)//后面找不到字符串
                {
                    MessageBox.Show("已达文件头");
                    FindStartPosition = 0;
                }
                else
                {
                    MessageBox.Show("没有找到输入的字符串");
                }
            }
            else
            {
                paf.rtxtNotepad.Select(y, textBox1.Text.ToString().Length);//选中需要查找的字符串
                stringFound1 = true;
            }
           }
        }
    }
}
