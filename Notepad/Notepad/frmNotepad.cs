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
    public partial class frmNotepad : Form
    {
        bool b = false;//全局变量判断文件是否已存在(计算机上建立过了)
        bool s = true;//全局变量判断richtextbox里的文本是否有变动
        public frmNotepad()
        {
            InitializeComponent();//初始化
        }
        private void tsmiNew_Click(object sender, EventArgs e)//新建(文件下菜单项)
        {
            if(b==true||rtxtNotepad.Text.Trim()!="")//如果文本没保存过或者richtextbox里的内容不为空
            {
                if (s == false)//判断richtextbox里的文本是不是变动了
                {
                    string result;//定义字符串反馈用户的响应
                    result = MessageBox.Show("文件尚未保存,是否保存?",
                        "记事本",MessageBoxButtons.YesNoCancel).ToString();//消息框提示
                    switch(result)//选择
                    {
                        case "Yes":
                            if(b==true)//文件已存在,此时的保存为直接保存，不显示保存位置/文件名(因为此文件已在计算机上建立过了)
                            {
                                int odlg = sdlgNotepad.FilterIndex;//定义变量实时更新用户保存文件时选择格式的索引下标
                                switch (odlg)
                                {
                                    case 1: rtxtNotepad.SaveFile(odlgNotepad.FileName);//rtf格式文件保存方式
                                        break;
                                    case 2: rtxtNotepad.SaveFile(odlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt格式文件方式
                                        break;
                                }
                            }
                            else if(sdlgNotepad.ShowDialog()==DialogResult.OK)//计算机上还没有建立该文件
                            {
                                int h = sdlgNotepad.FilterIndex;//定义变量实时更新用户保存文件时选择格式的索引下标
                                switch (h)
                                {
                                    case 1: rtxtNotepad.SaveFile(sdlgNotepad.FileName);//rtf格式文件保存方式
                                        break;
                                    case 2: rtxtNotepad.SaveFile(sdlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt格式文件保存方式
                                        break;
                                }
                            }
                            s = true;
                            rtxtNotepad.Text = "";//文件保存完清空现在richtextbox里的内容(因为是新建项)
                            break;
                        case  "No":
                            b = false;//文件未建立
                            rtxtNotepad.Text = "";//清空内容
                            break;
                    }
                }
            }
        }
        private void tsmiOpen_Click(object sender, EventArgs e)//打开(文件下菜单项)
        {
            if (b == true || rtxtNotepad.Text.Trim() != "")//如果文本没保存过或者richtextbox里的内容不为空
            {
                if (s == false)//判断richtextbox里的文本是不是变动了
                {
                    string result;//定义字符串反馈用户的响应
                    result = MessageBox.Show("文件尚未保存,是否保存?",
                        "保存文件", MessageBoxButtons.YesNoCancel).ToString();//消息框提示
                    switch (result)
                    {
                        case "Yes":
                            if (b == true)//文件已存在,此时的保存为直接保存，不显示保存位置/文件名(因为此文件已在计算机上建立过了)
                            {
                                int odlg = sdlgNotepad.FilterIndex;//定义变量实时更新用户保存文件时选择格式的索引下标
                                switch (odlg)
                                {
                                    case 1: rtxtNotepad.SaveFile(odlgNotepad.FileName);//rtf格式文件保存方式
                                        break;
                                    case 2: rtxtNotepad.SaveFile(odlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt格式文件保存方式
                                        break;
                                }
                            }
                            else if (sdlgNotepad.ShowDialog() == DialogResult.OK)//计算机上还没有建立该文件
                            {
                                int h = sdlgNotepad.FilterIndex;//定义变量实时更新用户保存文件时选择格式的索引下标
                                switch (h)
                                {
                                    case 1: rtxtNotepad.SaveFile(sdlgNotepad.FileName);//rtf格式文件保存方式
                                        break;
                                    case 2: rtxtNotepad.SaveFile(sdlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt格式文件保存方式
                                        break;
                                }
                            }
                            s = true;
                            break;
                        case "No":
                            b = false;
                            rtxtNotepad.Text = "";//清空内容
                            break;
                    }
                }
            }
            odlgNotepad.RestoreDirectory = true;
            if(odlgNotepad.ShowDialog()==DialogResult.OK&&odlgNotepad.FileName!="")
            {
                int index = odlgNotepad.FilterIndex;
                switch (index)
                {
                    case 1: rtxtNotepad.LoadFile(odlgNotepad.FileName);
                        b = true;
                        break;
                    case 2: rtxtNotepad.LoadFile(odlgNotepad.FileName, RichTextBoxStreamType.PlainText);
                        b = true;
                        break;
                }
            }
            s = true;
        }
        private void tsmiSave_Click(object sender, EventArgs e)//保存(文件下菜单项)
        {
            if(b==true&&rtxtNotepad.Modified==true)//文件在计算机上已建立且内容被修改
            {
                string tp = odlgNotepad.FileName.Substring(odlgNotepad.FileName.Length-3,3);//定义字符串用substring来返回文件的后缀名
                switch(tp)//通过文件的后缀名来判断原来建立的文件时rtf类型还是txt类型
                {
                    case "txt": rtxtNotepad.SaveFile(odlgNotepad.FileName,RichTextBoxStreamType.TextTextOleObjs);//txt保存方式
                               break;
                    case "rtf": rtxtNotepad.SaveFile(odlgNotepad.FileName);//rtf保存方式
                               break;
                };
                s = true;
            }
            else if (b==false&&rtxtNotepad.Text.Trim()!=""&&sdlgNotepad.ShowDialog()==DialogResult.OK)
            {
                string r = odlgNotepad.FileName.Substring(odlgNotepad.FileName.Length - 3, 3);
                switch (r)
                {
                    case "txt": rtxtNotepad.SaveFile(sdlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);
                        break;
                    case "rtf": rtxtNotepad.SaveFile(sdlgNotepad.FileName);
                        break;
                }
               // rtxtNotepad.SaveFile(sdlgNotepad.FileName);
                s=true;
                b=true;;
                odlgNotepad.FileName=sdlgNotepad.FileName;
            }

        }
        private void tsmiSaveAs_Click(object sender, EventArgs e)//另存为(文件下菜单项)
        {
            if(sdlgNotepad.ShowDialog()==DialogResult.OK)
            {
                int h = sdlgNotepad.FilterIndex;//定义变量实时更新用户保存文件时选择格式的索引下标
                switch (h)
                {
                    case 1: rtxtNotepad.SaveFile(sdlgNotepad.FileName);//rtf保存方式
                        break;
                    case 2: rtxtNotepad.SaveFile(sdlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt保存方式
                        break;
                }
                s = true;
            }
        }
        private void tsmiClose_Click(object sender, EventArgs e)//退出(文件下菜单项)
        {
            Application.Exit();//调用系统自带函数
        }
        private void tsmiEdit_Click(object sender, EventArgs e)//撤销(编辑下菜单项)
        {
            rtxtNotepad.Undo();//调用richtextbox自带撤销函数
        }
        private void tsmiCopy_Click(object sender, EventArgs e)//复制(编辑下菜单项)
        {
            rtxtNotepad.Copy();//调用richtextbox自带的复制函数
        }
        private void tsmiCut_Click(object sender, EventArgs e)//剪切(编辑下菜单项)
        {
            rtxtNotepad.Cut();//调用richtextbox自带剪切函数
        }
        private void tsmiPaste_Click(object sender, EventArgs e)//粘贴(编辑下菜单项)
        {
            rtxtNotepad.Paste();//调用richtextbox自带的粘贴函数
        }
        private void tsmiSelectAll_Click(object sender, EventArgs e)//全选(编辑下菜单项)
        {
            rtxtNotepad.SelectAll();//调用richtextbox自带全选函数
        }
        private void tsmiDate_Click(object sender, EventArgs e)//显示时间日期(编辑下菜单项)
        {
            rtxtNotepad.AppendText(System.DateTime.Now.ToString());//向当前文本中追加文本显示系统时间
        }
        private void tsmiAuto_Click(object sender, EventArgs e)//自动换行
        {
            if(tsmiAuto.Checked=false)//启动时默认checked值为true
            {
                tsmiAuto.Checked = true;
            }
            else
            {
                tsmiAuto.Checked = false;
                rtxtNotepad.WordWrap = false;//指示多行文本控件子必要时是否自动换行到下一行
            }
        }
        private void tsmiFont_Click(object sender, EventArgs e)//字体(自动换行下菜单项)
        {
            fdlgNotepad.ShowColor = true;//显示选择颜色
            if(fdlgNotepad.ShowDialog()==DialogResult.OK)
            {
                rtxtNotepad.SelectionColor = fdlgNotepad.Color;//选择的颜色应用到文本颜色
                rtxtNotepad.SelectionFont = fdlgNotepad.Font;//选择的字体应用到文本字体
            }
        }
        private void tsmiToolStrip_Click(object sender, EventArgs e)
        {
            Point point;
            if(tsmiToolStrip.Checked==true)
            {
                point = new Point(0, 24);
                tsmiToolStrip.Checked = false;
                tlsNotepad.Visible = false;
                rtxtNotepad.Location = point;
                rtxtNotepad.Height += tlsNotepad.Height;
            }
            else
            {
                point = new Point(0,49);
                tsmiToolStrip.Checked = true;
                tlsNotepad.Visible = true;
                rtxtNotepad.Location = point;
                rtxtNotepad.Height -= tlsNotepad.Height;

            }
        }
        private void tsmiSToolStrip_Click(object sender, EventArgs e)
        {
            if(tsmiSToolStrip.Checked==true)
            {
                tsmiSToolStrip.Checked = false;
                stsNotepad.Visible = false;
                rtxtNotepad.Height += stsNotepad.Height;
            }
            else
            {
                tsmiSToolStrip.Checked = true;
                stsNotepad.Visible = true;
                rtxtNotepad.Height -= stsNotepad.Height;
            }
        }
        private void tsmiAbout_Click(object sender, EventArgs e)//关于记事本(帮助下菜单项)
        {
            frmAbout ob_FrmAbout = new frmAbout();//实例化另一个窗体
            ob_FrmAbout.Show();//显示窗体
        }
        private void tlsNotepad_ItemClicked(object sender, ToolStripItemClickedEventArgs e)//工具栏
        {
            int n;
            n = tlsNotepad.Items.IndexOf(e.ClickedItem);//获取鼠标单击工具栏中图标的下标位置
            switch(n)//通过下标来具体实现调用哪个函数
            {
                case 0:
                    tsmiNew_Click(sender,e);
                    break;
                case 1:
                    tsmiOpen_Click(sender,e);
                    break;
                case 2:
                    tsmiSave_Click(sender, e);
                    break;
                case 3:
                    tsmiCut_Click(sender, e);
                    break;
                case 4:
                    tsmiCopy_Click(sender, e);
                    break;
                case 5:
                    tsmiPaste_Click(sender, e);
                    break;
                case 6:
                    tsmiAbout_Click(sender, e);
                    break;

            }
        }
        private void tmrNotepad_Tick(object sender, EventArgs e)//状态栏中显示时间函数
        {
            tssLbl2.Text = System.DateTime.Now.ToString();//调用系统时间
        }
        private void frmNotepad_SizeChanged(object sender, EventArgs e)//记事本窗体大小变化函数
        {
            frmNotepad ob_frmNotepad = new frmNotepad();//实例化
            tssLbl1.Width = this.Width / 2 - 12;//状态栏中就绪toolstriplabel大小变化
            tssLbl2.Width = tssLbl1.Width;//状态栏中就绪toolstriplabel大小变化
            tssLbl3.Width = tssLbl1.Width;//状态栏中就绪toolstriplabel大小变化

        }
        private void tsmiSet_Click(object sender, EventArgs e)//页面设置(文件下菜单项)
        {
            MessageBox.Show("在执行与打印有关的操作时需安装有打印机！");
        }
        private void tsmiPrint_Click(object sender, EventArgs e)//打印(文件下菜单项)
        {
            MessageBox.Show("在执行与打印有关的操作时需安装有打印机！");//显示消息框
        }
        private void tsmiDelete_Click(object sender, EventArgs e)//删除(编辑下菜单项)
        {
            SendKeys.Send("{BackSpace}");//调用键盘的删除键
        }
        private void rtxtNotepad_MouseMove(object sender, MouseEventArgs e)//光标位置(状态栏中显示行列)
        {
            int x = e.Location.X - rtxtNotepad.Location.X;//获取x轴坐标

            int y = e.Location.Y - rtxtNotepad.Location.Y;//获取y轴坐标

            int fontheight = this.rtxtNotepad.Font.Height;//字体高度

            int fontwidth = Convert.ToInt32(rtxtNotepad.Font.Size);//字体宽度

            int row = (y / fontheight) + 4;//计算行

            int line = (x / fontwidth) +1;//计算列
            tssLbl3.Text = "行  " + row.ToString() + "  列  " + line.ToString();//显示行列
        }
        private void 查找ToolStripMenuItem_Click(object sender, EventArgs e)//查找(编辑下菜单项)
        {
            frmSearch ob = new frmSearch(this);//新建窗口实例化
            ob.Show();//显示窗口
        }
        private void tsmiReplace_Click(object sender, EventArgs e)//替换(编辑下菜单项)
        {
            frmReplace a = new frmReplace(this);//新建窗口实例化
            a.Show();//显示窗口
        }
        private void frmNotepad_FormClosing(object sender, FormClosingEventArgs e)//点击窗体关闭按钮事件
        {
            if (b == true || rtxtNotepad.Text.Trim() != "")//如果文本没保存过或者richtextbox里的内容不为空
            {
                if (s == false)//判断richtextbox里的文本是不是变动了
                {
                    string result;//定义字符串反馈用户的响应
                    result = MessageBox.Show("文件尚未保存,是否保存?",
                        "保存文件", MessageBoxButtons.YesNoCancel).ToString();//消息框提示
                    switch (result)
                    {
                        case "Yes":
                            if (b == true)//文件已存在,此时的保存为直接保存，不显示保存位置/文件名(因为此文件已在计算机上建立过了)
                            {
                                string tp = odlgNotepad.FileName.Substring(odlgNotepad.FileName.Length - 3, 3);//定义字符串用substring来返回文件的后缀名
                                switch (tp)//通过文件的后缀名来判断原来建立的文件时rtf类型还是txt类型
                                {
                                        case "txt": rtxtNotepad.SaveFile(odlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt保存方式
                                            break;
                                        case "rtf": rtxtNotepad.SaveFile(odlgNotepad.FileName);//rtf保存方式
                                            break;
                                };
                                s = true;
                            }
                            else if (sdlgNotepad.ShowDialog() == DialogResult.OK)
                            {
                                int h = sdlgNotepad.FilterIndex;//定义变量实时更新用户保存文件时选择格式的索引下标
                                switch (h)
                                {
                                    case 1: rtxtNotepad.SaveFile(sdlgNotepad.FileName);//rtf格式文件保存方式
                                        break;
                                    case 2: rtxtNotepad.SaveFile(sdlgNotepad.FileName, RichTextBoxStreamType.TextTextOleObjs);//txt格式文件保存方式
                                        break;
                                }
                            }
                            e.Cancel = false;//退出程序
                            break;
                        case "No":
                            e.Cancel = false;//退出程序
                            break;
                        case "Cancel":
                            e.Cancel = true;//取消退出程序命令
                            break;
                    }
                }
            }
        }
        private void rtxtNotepad_TextChanged_1(object sender, EventArgs e)//判断richtextbox中的文本是否变动
        {
            s = false;
        }
        private void 背景图片ToolStripMenuItem_Click(object sender, EventArgs e)//背景颜色(格式下菜单项)
        {
            fdlgNotepad.ShowColor = true;//显示颜色可选项
            if(fdlgNotepad.ShowDialog()==DialogResult.OK)
            {
                rtxtNotepad.BackColor = fdlgNotepad.Color;//选择的颜色应用于richtextbox的背景颜色

            }
        }
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)//粘贴(工具栏中)
        {
            rtxtNotepad.Paste();//调用richtextbox自带的粘贴函数
        }
        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)//撤销(工具栏中)
        {
            rtxtNotepad.Undo();//调用richtextbox自带的撤销函数
        }
        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)//复制(工具栏中)
        {
            rtxtNotepad.Copy();//调用richtextbox自带的复制函数
        }
        private void 删除DToolStripMenuItem_Click(object sender, EventArgs e)//删除(工具栏中)
        {
            SendKeys.Send("{BackSpace}");//调用键盘删除键的功能
        }
        private void 全选AToolStripMenuItem_Click(object sender, EventArgs e)//全选(工具栏中)
        {
            rtxtNotepad.SelectAll();//调用tichtextbox中全选函数
        }
        private void tsbNew_Click(object sender, EventArgs e)//新建(工具栏中)
        {
            tsmiNew.PerformClick();//调用performclick()函数复制菜单中新建按钮的功能
        }
        private void tsbOpen_Click(object sender, EventArgs e)//打开(工具栏中)
        {
        }
        private void tsbSave_Click(object sender, EventArgs e)//保存(工具栏中)
        {
            tsmiSave.PerformClick();//调用performclick()函数复制菜单中保存按钮的功能
        }
    }
}
