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
    public partial class frmReplace : Form
    {
        int j = 1,t=0,y;
        private int FindStartPosition = 0;
        private bool stringFound = false;
        private bool stringFound1 = false; 
        public frmReplace(frmNotepad parent)
        {
            InitializeComponent();
            paf = parent;
        }
        private frmNotepad paf; 
        private void button1_Click(object sender, EventArgs e)
        {
            if (down.Checked == true)
                j = 1;
            else
                j = 0;
            if (j == 1)
            {
                t += paf.rtxtNotepad.SelectionStart;
                FindStartPosition = t;
                int foundIndex = paf.rtxtNotepad.Find(textBox1.Text.ToString(), FindStartPosition, RichTextBoxFinds.None);
                if (foundIndex == -1)
                {
                    if (stringFound)
                        MessageBox.Show("已达文件尾");
                    else
                        MessageBox.Show("没有找到输入的字符串");
                }
                else
                {
                    paf.rtxtNotepad.Select(foundIndex, textBox1.Text.ToString().Length);
                    stringFound = true;

                }
                t = textBox1.Text.ToString().Length;
            }
            else
            {
                try//避免查找到文件开头的异常
                {
                    y = paf.rtxtNotepad.Text.ToString().LastIndexOf(textBox1.Text.ToString(), paf.rtxtNotepad.SelectionStart - textBox1.Text.ToString().Length);//查找当前光标位置前所有字符串中所要查找的最后一次出现的下标

                }
                catch (Exception ex)//捕获异常
                {
                    MessageBox.Show("已达文件头");
                }
                if (y == -1)
                {
                    if (stringFound1)
                        MessageBox.Show("已达文件头");
                    else
                        MessageBox.Show("没有找到输入的字符串");
                }
                else
                {
                    paf.rtxtNotepad.Select(y, textBox1.Text.ToString().Length);
                    stringFound1 = true;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {    
            paf.rtxtNotepad.SelectedText =textBox2.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int FindStartPosition1 = 0;
            while (!stringFound) { 
            int foundIndex = paf.rtxtNotepad.Find(textBox1.Text.ToString(), FindStartPosition1, RichTextBoxFinds.None);
                 if (foundIndex == -1)
                {
                    stringFound = true;
                    MessageBox.Show("没有找到输入的字符串");
                }
                 else
                 {
                     paf.rtxtNotepad.Select(foundIndex, textBox1.Text.ToString().Length);
                     
                     paf.rtxtNotepad.SelectedText = textBox2.Text;

                 }
                 FindStartPosition1= foundIndex + 1;
            }
        }
    }
}
