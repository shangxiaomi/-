using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataStructure.Queue;
using DataStructure.LinkedList;
using System.Threading;
namespace 离散事件模拟
{
    public partial class Form1 : Form
    {
        public int number_staff = 0;
        Simulation op = new Simulation();
        DateTime End;
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Hide();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Exception t = new Exception();
                int hour = int.Parse(comboBox1.Text);
                int minute = int.Parse(comboBox3.Text);
                int second = int.Parse(comboBox5.Text);
                if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59) throw t;
                hour = int.Parse(comboBox2.Text);
                minute = int.Parse(comboBox4.Text);
                second = int.Parse(comboBox6.Text);
                if (hour < 0 || hour > 23 || minute < 0 || minute > 59 || second < 0 || second > 59) throw t;
            }
            catch
            {
                MessageBox.Show("请输入正确的开店、闭店时间");
                return;
            }
            try
            {
                Exception t = new Exception();
                int a = int.Parse(textBox8.Text);
                int b = int.Parse(textBox10.Text);
                int c = int.Parse(textBox12.Text);
                if (a <= 0 || c <= 0 || b <= 0 || a > 100 || b > 100 || c > 100) throw t;
            }
            catch
            {
                MessageBox.Show("请输入正确的各级别理发师人数");
                return;
            }
            try
            {
                Exception t = new Exception();
                int a = int.Parse(textBox13.Text);
                int b = int.Parse(textBox9.Text);
                int c = int.Parse(textBox11.Text);
                if (a <= 0 || c <= 0 || b <= 0 ) throw t;
            }
            catch
            {
                MessageBox.Show("请输入正确的理发师工资");
                return;
            }
            
            DateTime Start = new DateTime(11, 11, 11, int.Parse(comboBox1.Text), int.Parse(comboBox3.Text), int.Parse(comboBox5.Text));
            End = new DateTime(11, 11, 11, int.Parse(comboBox2.Text), int.Parse(comboBox4.Text), int.Parse(comboBox6.Text));
            //MessageBox.Show(Start.ToLongTimeString().ToString() + "\n" + End.ToLongTimeString().ToString());
            int temp = (int)(End - Start).TotalSeconds;
            if (temp <= 0) {
                MessageBox.Show("时间设置的不对哦"); return;
            }
            textBox8.ReadOnly = true;
            textBox9.ReadOnly = true;
            textBox10.ReadOnly = true;
            textBox11.ReadOnly = true;
            textBox12.ReadOnly = true;
            textBox13.ReadOnly = true;
            button1.Hide();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            /*comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;*/
            op.StartTime = Start;
            op.Salsry_STaff[1] = int.Parse(textBox9.Text);
            op.Salsry_STaff[2] = int.Parse(textBox11.Text);
            op.Salsry_STaff[3] = int.Parse(textBox13.Text);
            op.text1 = textBox5;
            op.text = textBox4;
			op.text2 = textBox6;
			op.text3 = textBox8;
			op.text4 = textBox10;
            op.text5 = textBox12;
            op.guandian = button2;
            op.data = dataGridView1;
            op.CloseTime = temp;
            op.kaishi();
            
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
		public int time;
		private void button2_Click_1(object sender, EventArgs e)
		{
            CloseDay();
            textBox1.Text = op.FactEnd.Hour.ToString();
            textBox2.Text = op.FactEnd.Minute.ToString();
            textBox3.Text = op.FactEnd.Second.ToString();
            textBox7.Text = (op.FactEnd - End).TotalSeconds.ToString()+"秒";
            button2.Hide();
        }
        private void CloseDay()
        {
            textBox8.ReadOnly = false;
            textBox9.ReadOnly = false;
            textBox10.ReadOnly = false;
            textBox11.ReadOnly = false;
            textBox12.ReadOnly = false;
            textBox13.ReadOnly = false;
            button1.Show();
            /*comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDown;*/
        }

	}
    #region//时间节点（结构体）
    public struct Event
    {
        public int OccurTime;
        public int NType;
        public int dur;
        public int Grade;
        public int Number;
    }
    #endregion
    #region//顾客节点（结构体）
    struct CustomerNode
    {
        public int OccurTime;
        public int Duration;
        public int Number;
    }
    #endregion
    #region//理发店模拟类
    public class Simulation
    {
        #region//相关字段
        public DateTime FactEnd;
        public TextBox text1 = null;
        public TextBox text2 = null;
        public TextBox text = null;
        public TextBox text3 = null;
        public TextBox text4 = null;
        public TextBox text5 = null;
        public Button guandian = null;
        Thread th = null;
        public DateTime StartTime;
        public DataGridView data = null;
        int Number_event;
        Random rd = new Random();
        MySingleLinkedList<Event> ev;//事件链表
        int TotalTime;
        public int CustomerNum;
        public int CloseTime;
        Event en = new Event();//事件节点
        CustomerNode cn = new CustomerNode();
        int[] Number_Customer = new int[4];//三个级别的顾客各有多少
        int[] Number_Staff = new int[4];//三个级别的理发师各多少人
        public int[] Salsry_STaff = new int[4];//三个级别的价格
        public int Total_Staff;//理发师总人数
        MyLinkQueue<CustomerNode>[] QStaff1 = null;//三个级别的理发师队列
        MyLinkQueue<CustomerNode>[] QStaff2 = null;
        MyLinkQueue<CustomerNode>[] QStaff3 = null;
        #endregion
        #region//更新理发师工作状态函数
        private void Update()
        {
            int cnt = 0;
            int len = 0;
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < Number_Staff[i]; j++)
                {
                    if (i == 1)
                    {
                        len = QStaff1[j].Size;
                        if (len > 0) data.Rows[cnt].Cells[2].Value = QStaff1[j].First().Number.ToString();
                        else data.Rows[cnt].Cells[2].Value = "空";
                    }
                    else if (i == 2)
                    {
                        len = QStaff2[j].Size;
                        if (len > 0) data.Rows[cnt].Cells[2].Value = QStaff2[j].First().Number.ToString();
                        else data.Rows[cnt].Cells[2].Value = "空";
                    }
                    else if (i == 3)
                    {
                        len = QStaff3[j].Size;
                        if (len > 0) data.Rows[cnt].Cells[2].Value = QStaff3[j].First().Number.ToString();
                        else data.Rows[cnt].Cells[2].Value = "空";
                    }
                    if (len > 0) data.Rows[cnt].Cells[3].Value = (len-1).ToString();
                    else data.Rows[cnt].Cells[3].Value = "0";
                    cnt++;
                }


            }

        }
        #endregion
        #region//初始化理发师工作状态表的相关操作
        private void Delete()
        {
            int cnt = data.RowCount;
            for (int i = cnt - 2; i >= 0; i--)
            {
                data.Rows.Remove(data.Rows[i]);
            }

        }
        private void Initial_Data()
        {
            Delete();
            int cnt = 0;
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < Number_Staff[i]; j++)
                {
                    data.Rows.Add();
                    data.Rows[cnt].Height = 20;
                    data.Rows[cnt].Cells[0].Value = (cnt + 1).ToString();
                    data.Rows[cnt].Cells[1].Value = i.ToString();
                    data.Rows[cnt].Cells[2].Value = "空";
                    cnt++;
                }
            }
        }
        #endregion
        public int First(Event item)
        {
            int cur = 0;
            for (int i = 0; i < ev.Count; i++)
            {
                //Console.WriteLine(ev[i].OccurTime + " "+item.OccurTime);
                if (ev[i].OccurTime <= item.OccurTime)
                {
                    cur++;
                }
                else break;
            }
            return cur;
        }
        #region//顾客到达和离开
        public void CustomerArrived()
        {
            en.Grade = rd.Next(1, 4);
            ++CustomerNum; //增加客户数量
            en.Number = CustomerNum;

            int durTime = rd.Next(600, 3600);                      //产生0-30 间的随机整数，为到达客户等待的时间。
            int interTime = rd.Next(0, 600);                      //产生0-5 间的随机整数，为与下一客户到达时间的时间间隔。
            int depT = en.OccurTime + durTime;
            int arrT = en.OccurTime + interTime;
            //开始排队，找到最短队伍，并加入队伍。
            int i = MinQueue(en.Grade);
            cn.OccurTime = en.OccurTime;
            cn.Duration = durTime;
            cn.Number = en.Number;
            if (en.Grade == 1)
            {
                QStaff1[i].EnQueue(cn);
            }
            else if (en.Grade == 2)
            {
                QStaff2[i].EnQueue(cn);
            }
            else if (en.Grade == 3)
            {
                QStaff3[i].EnQueue(cn);
            }
            //
            en.OccurTime = depT;
            en.NType = i;
            en.dur = durTime;
            text.Text = "第" + (++Number_event).ToString() + "个事件 " + "有新客户！  时间" + StartTime.AddSeconds(en.OccurTime-en.dur).ToLongTimeString().ToString() + "第" + en.Number + "号顾客将由 " + en.Grade.ToString() + " 级别的第" + (en.NType + 1).ToString() + " 位理发师进行服务\r\n" + text.Text;
            //text.Text= "第" + (++Number_event).ToString() + "个事件 "+"有新客户！" + " 编号"+en.Number+" "+StartTime.AddSeconds(en.OccurTime).ToLongTimeString() + " 逗留时间 " + durTime.ToString() + " 级别" + en.Grade.ToString() + " 第几位" + i.ToString()+"\r\n"+text.Text;
            //把当前客户离开事件，加入事件列表
            

            int cur = First(en);
            ev.Insert(cur, en);//还没有实现自动按顺序插入！！！！！

            //下一客户到达事件，加入事件列表。
            en.OccurTime = arrT;
            en.NType = 0;
            en.dur = 0;
            en.Grade = 0;
            //Console.WriteLine(en.OccurTime.ToString() + " " + CloseTime.ToString() + "  !!!!");
            if (en.OccurTime < CloseTime)
            {
                cur = First(en);
                ev.Insert(cur, en);
            }
        }
        public void CustomerDepature()
        {
            CustomerNode temp = new CustomerNode();
            int i = en.NType;
            int grade = en.Grade;
            if (1 == grade)
            {
                temp = QStaff1[i].DeQueue();
            }
            else if (2 == grade)
            {
                temp = QStaff2[i].DeQueue();
            }
            else if (3 == grade)
            {
                temp = QStaff3[i].DeQueue();
            }
            Number_Customer[en.Grade]++;
            TotalTime += temp.Duration;
            text1.Text = Convert.ToString(TotalTime / CustomerNum)+"秒";
            int money = Number_Customer[1] * Salsry_STaff[1]+ Number_Customer[2] * Salsry_STaff[2]+ Number_Customer[3] * Salsry_STaff[3];
            //text2.Text = Number_Customer[1].ToString()+ Salsry_STaff[1].ToString()+Number_Customer[2].ToString()+Salsry_STaff[2].ToString() + Number_Customer[3].ToString() + Salsry_STaff[3];
            text2.Text = money.ToString()+"元";

        }
        #endregion
        public void OpenDay()
        {

            Number_event = 0;
            for (int i = 1; i < 4; i++)
            {
                Number_Customer[i] = Number_Staff[i] = 0;
            }
            ev = new MySingleLinkedList<Event>();
            en.Grade = 0;//顾客的理发师级别;
            TotalTime = 0;
            CustomerNum = 0;
            en.OccurTime = 0;  //设置第一个客户到达事件
            en.NType = 0;
            ev.Add(en);
            Number_Staff[1] = Convert.ToInt32(text3.Text);
            Number_Staff[2] = Convert.ToInt32(text4.Text);
            Number_Staff[3] = Convert.ToInt32(text5.Text);
            Initial_Data();
            QStaff1 = new MyLinkQueue<CustomerNode>[Number_Staff[1]];
            for (int i = 0; i < Number_Staff[1]; i++)
            {
                QStaff1[i] = new MyLinkQueue<CustomerNode>();
            }
            QStaff2 = new MyLinkQueue<CustomerNode>[Number_Staff[2]];
            for (int i = 0; i < Number_Staff[2]; i++)
            {
                QStaff2[i] = new MyLinkQueue<CustomerNode>();
            }
            QStaff3 = new MyLinkQueue<CustomerNode>[Number_Staff[3]];
            for (int i = 0; i < Number_Staff[3]; i++)
            {
                QStaff3[i] = new MyLinkQueue<CustomerNode>();
            }
        }
        public int MinQueue(int Type)
        {
            int cur = 0;
            if (Type == 1)
            {
                int len = QStaff1[0].Size;
                for (int i = 1; i < Number_Staff[1]; i++)
                {
                    if (len > QStaff1[i].Size)
                    {
                        len = QStaff1[i].Size;
                        cur = i;
                    }
                }
            }
            if (Type == 2)
            {
                int len = QStaff2[0].Size;
                for (int i = 1; i < Number_Staff[2]; i++)
                {
                    if (len > QStaff2[i].Size)
                    {
                        len = QStaff2[i].Size;
                        cur = i;
                    }
                }
            }
            if (Type == 3)
            {
                int len = QStaff3[0].Size;
                for (int i = 1; i < Number_Staff[3]; i++)
                {
                    if (len > QStaff3[i].Size)
                    {
                        len = QStaff3[i].Size;
                        cur = i;
                    }
                }
            }
            return cur;
        }
        public void barber_simulation()
        {

            OpenDay();
            while (ev.Count != 0)
            {
                en = ev[0];
                ev.RemoveAt(0);
                if (en.Grade == 0)
                {
                    CustomerArrived();
                    Update();
                    Thread.Sleep(100);
                }
                else
                {
                    FactEnd = StartTime.AddSeconds(en.OccurTime);
                    text.Text = "第" + (++Number_event).ToString() + "个事件 " + "客户离开  时间" + StartTime.AddSeconds(en.OccurTime).ToLongTimeString().ToString() + "第 " + en.Number + " 号顾客由 " + en.Grade.ToString() + " 级别的第 " + (en.NType + 1).ToString() + " 位理发师完成服务\r\n" + text.Text;
                    //text.Text= "第" + (++Number_event).ToString() + "个事件 "+"客户离开  级别" +"编号" + en.Number + " " + en.Grade.ToString() + "  此级别的第几位" + en.NType.ToString() + " " + StartTime.AddSeconds(en.OccurTime).ToLongTimeString().ToString() + "\r\n"+text.Text;                   
                    CustomerDepature();
                    Update();
                    Thread.Sleep(100);
                }
            }
            guandian.Show();
            th.Abort();

        }
        public void kaishi()
        {
            th = new Thread(new ThreadStart(barber_simulation));//创建线程对象
            th.Start();//启动线程
        }
    }
    #endregion
}



	