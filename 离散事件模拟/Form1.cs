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
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text.Equals(string.Empty) || comboBox2.Text.Equals(string.Empty) || comboBox3.Text.Equals(string.Empty) || comboBox4.Text.Equals(string.Empty) || comboBox5.Text.Equals(string.Empty) || comboBox6.Text.Equals(string.Empty))
            { 
                MessageBox.Show("请填写完整的时间");
                return;
            }
            DateTime Start = new DateTime(11, 11, 11, int.Parse(comboBox1.Text), int.Parse(comboBox3.Text), int.Parse(comboBox5.Text));
            DateTime End = new DateTime(11, 11, 11, int.Parse(comboBox2.Text), int.Parse(comboBox4.Text), int.Parse(comboBox6.Text));
            //MessageBox.Show(Start.ToLongTimeString().ToString() + "\n" + End.ToLongTimeString().ToString());
            int temp = (int)(End - Start).TotalSeconds;
            if (temp <= 0) {
                MessageBox.Show("时间设置的不对哦"); return;
            }
            Simulation op = new Simulation();
            op.text = textBox4;
            op.CloseTime = temp;


            op.kaishi();
            
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            number_staff = int.Parse(textBox1.Text);
            textBox1.Hide();
            button2.Hide();
            textBox2.Visible = true;
            textBox3.Visible = true;
            button3.Visible = true;
            label3.Visible = true;
            label3.Text = "第" + number_staff.ToString() + "个人";
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }
    }
    //自己写的相关操作
    public struct Event
    {
        public int OccurTime;
        public int NType;
        public int dur;
        public int Grade;
        public int Number;
    }
    struct CustomerNode
    {
        public int OccurTime;
        public int Duration;
        public int Number;
    }
    public class Simulation
    {
        int Number_event;
        public  TextBox text = null;
        Random rd = new Random();
        MySingleLinkedList<Event> ev;//事件链表
        int TotalTime;
        public int CustomerNum;
        public int CloseTime;
        Event en = new Event();//事件节点
        CustomerNode cn = new CustomerNode();
        int[] Number_Customer = new int[4];//三个级别的顾客各有多少
        int[] Number_Staff = new int[4];//三个级别的理发师各多少人
        int[] Salsry_STaff = new int[4];//三个级别的价格
        public int Total_Staff;//理发师总人数
        MyLinkQueue<CustomerNode>[] QStaff1 = null;//三个级别的理发师队列
        MyLinkQueue<CustomerNode>[] QStaff2 = null;
        MyLinkQueue<CustomerNode>[] QStaff3 = null;
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
        public void CustomerArrived()
        {
            en.Grade = rd.Next(1, 3);
            Number_Customer[en.Grade]++;
            ++CustomerNum; //增加客户数量
            en.Number = CustomerNum;
            
            int durTime = rd.Next(600,3600);                      //产生0-30 间的随机整数，为到达客户等待的时间。
            int interTime = rd.Next(0,600);                      //产生0-5 间的随机整数，为与下一客户到达时间的时间间隔。
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
            text.Text+="有新客户！！" + "编号"+en.Number+" "+en.OccurTime.ToString() + " 逗留时间 " + durTime.ToString() + " 级别" + en.Grade.ToString() + " 第几位" + i.ToString()+"\r\n";
            Thread.Sleep(100);
            //把当前客户离开事件，加入事件列表
            en.OccurTime = depT;
            en.NType = i;
            en.dur = durTime;

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
            TotalTime += temp.Duration;
        }
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
            Number_Staff[1] = Number_Staff[2] = Number_Staff[3] = 3;//测试用
            QStaff1 = new MyLinkQueue<CustomerNode>[Number_Staff[1]];
            for (int i = 0; i < Number_Staff[1]; i++)
            {
                QStaff1[i] = new MyLinkQueue<CustomerNode>();
            }
            QStaff2 = new MyLinkQueue<CustomerNode>[Number_Staff[2]];
            for (int i = 0; i < Number_Staff[1]; i++)
            {
                QStaff2[i] = new MyLinkQueue<CustomerNode>();
            }
            QStaff3 = new MyLinkQueue<CustomerNode>[Number_Staff[3]];
            for (int i = 0; i < Number_Staff[1]; i++)
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
                text.Text += "第" + (++Number_event).ToString() + "个事件 ";
                ev.RemoveAt(0);
                if (en.Grade == 0)
                {
                    CustomerArrived();
                }
                else
                {
                    
                    text.Text+="客户离开 级别" +"编号" + en.Number + " " + en.Grade.ToString() + "  此级别的第几位" + en.NType.ToString() + " " + en.OccurTime.ToString()+"\r\n";
                    Thread.Sleep(300);
                    CustomerDepature();
                }
            }

        }
        public void kaishi()
        {
            Thread th = new Thread(new ThreadStart(barber_simulation));//创建线程对象
            th.Start();//启动线程
            th.Abort();

        }
    }
}
