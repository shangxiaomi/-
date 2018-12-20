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
                MessageBox.Show("请输入正确的开店、闭店时间(00:00:00---23:59:59)");
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
                MessageBox.Show("请输入正确的各级别理发师人数(1---100)");
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
                MessageBox.Show("请输入正确的理发师工资(工资大于0)");
                return;
            }
            
            DateTime Start = new DateTime(11, 11, 11, int.Parse(comboBox1.Text), int.Parse(comboBox3.Text), int.Parse(comboBox5.Text));
            End = new DateTime(11, 11, 11, int.Parse(comboBox2.Text), int.Parse(comboBox4.Text), int.Parse(comboBox6.Text));
            //MessageBox.Show(Start.ToLongTimeString().ToString() + "\n" + End.ToLongTimeString().ToString());
            int temp = (int)(End - Start).TotalSeconds;
            if (temp <= 0) {
                MessageBox.Show("时间设置的不对哦"); return;
            }
            textBox17.Text = string.Empty;
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
            textBox18.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
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
            op.Queue_length = textBox17;
			op.onecustumer = textBox14;
			op.twocustumer = textBox15;
			op.threecustumer =textBox16;
            op.guandian = button2;
            op.data = dataGridView1;
            op.CloseTime = temp;
            this.Text = "理发馆仿真模拟系统--营业中";
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
            textBox18.Text = op.income[1].ToString()+"元";
            textBox19.Text = op.income[2].ToString()+"元";
            textBox20.Text = op.income[3].ToString()+"元";
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
            this.Text = "理发馆仿真模拟系统";
            button1.Show();
            /*comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDown;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDown;*/
        }

		private void groupBox2_Enter(object sender, EventArgs e)
		{

		}

		private void textBox11_TextChanged(object sender, EventArgs e)
		{

		}

		private void label20_Click(object sender, EventArgs e)
		{

		}

		private void textBox14_TextChanged(object sender, EventArgs e)
		{

		}

        private void 软件信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 xinxi = new Form2();
            xinxi.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void 事件发生间隔设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 t = new Form3();
            t.label2.Text ="当前时间间隔为 "+op.Jiange.ToString() + "毫秒";
            t.ShowDialog();
            if (t.DialogResult == DialogResult.OK) op.Jiange=int.Parse(t.textBox1.Text);
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
    #region//时间节点（结构体）
    public struct Event
	{
		public int OccurTime;//事件发生时间
		public int NType;//第几位理发师
		public int dur;//逗留时间
		public int Grade;//理发师级别
		public int Number;//顾客编号
	}
	#endregion
	#region//顾客节点（结构体）
	struct CustomerNode
	{
		public int OccurTime;//顾客的离开时间，到达时间就是离开时间减去逗留时间
		public int Duration;//逗留时间
		public int Number;//顾客编号
	}
	#endregion
	#region//理发店模拟类
	public class Simulation
	{
        #region//相关字段
        public int Queuelength=0;
        public int Jiange = 100;
        public TextBox Queue_length = null;
		public TextBox onecustumer = null;
		public TextBox twocustumer = null;
		public TextBox threecustumer = null;
		public DateTime FactEnd;//实际关店时间（DateTime类）
		public DateTime StartTime;//开店时间（DateTIme类） 
		public TextBox text1 = null;//Form1中的textBox5文本框（在From中赋值）,用于显示平均逗留时间
		public TextBox text2 = null;//Form1中的textBox6文本框（在From中赋值）,用于显示当前收入
		public TextBox text = null;//Form1中的事件显示文本框（在From中赋值）
		public TextBox text3 = null;//用于读入各个级别理发师的人数
		public TextBox text4 = null;//用于读入各个级别理发师的人数
		public TextBox text5 = null;//用于读入各个级别理发师的人数
		public Button guandian = null;//Form1中的关店按钮（在Form1中赋值），用于在营业结束后显示关店按钮
		public DataGridView data = null;//Form1中的表格空间（在Form1中赋值）,用于显示各个理发师的工作状态
		Thread th = null;//线程
		int Number_event;//事件的编号
		Random rd = new Random();//用于生成随机数
		MySingleLinkedList<Event> ev;//事件链表
		int TotalTime;//顾客逗留总时间
		public int CustomerNum;//顾客总数目
		public int CloseTime;//营业时间（关店时间减去开店时间）（在Form1中设置）
		Event en = new Event();//事件节点
		CustomerNode cn = new CustomerNode();//顾客节点
		public int[] Number_Customer = new int[4];//三个级别的已经结账顾客各有多少
		int[] Number_Staff = new int[4];//三个级别的理发师各多少人
		public int[] Salsry_STaff = new int[4];//三个级别的价格（在Form1中设置）
        public int[] income = new int[4];//当前级别的收入
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
					if (len > 0) data.Rows[cnt].Cells[3].Value = (len - 1).ToString();
					else data.Rows[cnt].Cells[3].Value = "0";
					cnt++;
				}
                Queue_length.Text=((float)Queuelength / (float)Number_event).ToString();

            }

		}
        #endregion
        #region
        void Sum_Queue()
        {
            for(int i=0;i<Number_Staff[1];i++)
            {
                if(QStaff1[i].Size>1)Queuelength += QStaff1[i].Size;
            }
            for (int i = 0; i < Number_Staff[2]; i++)
            {
                if (QStaff2[i].Size > 1) Queuelength += QStaff2[i].Size;
            }
            for (int i = 0; i < Number_Staff[3]; i++)
            {
                if (QStaff3[i].Size > 1) Queuelength += QStaff3[i].Size;
            }
        }
        #endregion
        #region//初始化理发师工作状态表的相关操作
        private void Delete()//把表格全部删除
		{
			int cnt = data.RowCount;
			for (int i = cnt - 2; i >= 0; i--)
			{
				data.Rows.Remove(data.Rows[i]);
			}

		}
		private void Initial_Data()//初始化表格
		{
			Delete();
			int cnt = 0;
			for (int i = 1; i <= 3; i++)
			{
				for (int j = 0; j < Number_Staff[i]; j++)
				{
					data.Rows.Add();
					data.Rows[cnt].Height = 30;
					data.Rows[cnt].Cells[0].Value = (cnt + 1).ToString();
					data.Rows[cnt].Cells[1].Value = i.ToString();
					data.Rows[cnt].Cells[2].Value = "空";
					cnt++;
				}
			}
		}
		#endregion
		public int First(Event item)//用于找到在事件链表中应该插入的位置，保证链表按事件发生时间的正序
		{
			int cur = 0;
			for (int i = 0; i < ev.Count; i++)
			{
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
			en.Grade = rd.Next(1, 4);//生成顾客级别
			++CustomerNum; //增加客户数量
			en.Number = CustomerNum;//记录客户编号
			int durTime = rd.Next(600, 3601);     //产生600-3600秒（10分钟到一小时）间的随机整数，为到达客户等待的时间。
			int interTime = rd.Next(0, 601);      //产生0-600秒 间的随机整数，为与下一客户到达时间的时间间隔。
			int depT = en.OccurTime + durTime;      //计算当前顾客的离开时间
			int arrT = en.OccurTime + interTime;    //计算下一个顾客的到达时间
													//开始排队，找到最短队伍，并加入队伍。
			int i = MinQueue(en.Grade);             //找到该级别理发师队列最短的队伍
			cn.OccurTime = en.OccurTime;            //顾客的离开时间
			cn.Duration = durTime;                  //顾客的逗留时间
			cn.Number = en.Number;                  //顾客的编号
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
            Sum_Queue();
			en.OccurTime = depT;
			en.NType = i;
			en.dur = durTime;
			text.Text = "第" + (++Number_event).ToString() + "个事件 " + "有新客户！  时间" + StartTime.AddSeconds(en.OccurTime - en.dur).ToLongTimeString().ToString() + "第" + en.Number + "号顾客将由 " + en.Grade.ToString() + " 级别的第" + (en.NType + 1).ToString() + " 位理发师进行服务\r\n" + text.Text;
			//把当前客户离开事件，加入事件列表
			int cur = First(en);
			ev.Insert(cur, en);
			//下一客户到达事件，加入事件列表。
			en.OccurTime = arrT;
			en.NType = 0;
			en.dur = 0;
			en.Grade = 0;
			if (en.OccurTime < CloseTime)//顾客的到达时间小于关店时间就进店，否则不进店
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
                income[1] += (int)((double)en.dur / (double)3600 * (double)Salsry_STaff[1]);
            }
			else if (2 == grade)
			{
				temp = QStaff2[i].DeQueue();
                income[2] += (int)((double)en.dur / (double)3600 * (double)Salsry_STaff[2]);
            }
			else if (3 == grade)
			{
				temp = QStaff3[i].DeQueue();
                income[3] += (int)((double)en.dur/(double)3600 * (double)Salsry_STaff[3]);
            }
			Number_Customer[en.Grade]++;//记录各级别顾客的结账人数
			onecustumer.Text = (Number_Customer[1]).ToString();
			twocustumer.Text = (Number_Customer[2]).ToString();
			threecustumer.Text = (Number_Customer[3]).ToString();
			TotalTime += temp.Duration;//记录顾客的总逗留时间
			text1.Text = Convert.ToString(TotalTime / (Number_Customer[1] + Number_Customer[2] + Number_Customer[3])) + "秒";
			int money = income[1]+income[2]+income[3];
			text2.Text = money.ToString() + "元";
            Sum_Queue();
            Update();
        }
		#endregion

		public void OpenDay()
		{
            Queuelength = 0;
			Number_event = 0;
			for (int i = 1; i < 4; i++)
			{
				income[i]=Number_Customer[i] = Number_Staff[i] = 0;
			}
			ev = new MySingleLinkedList<Event>();
			en.Grade = 0;//顾客的理发师级别;
			TotalTime = 0;
			CustomerNum = 0;
			en.OccurTime = rd.Next(0,601);//设置第一个客户到达事件
            en.NType = 0;
			ev.Add(en);
            text.Text = "";
			onecustumer.Text = (Number_Customer[1]).ToString();
			twocustumer.Text = (Number_Customer[2]).ToString();
			threecustumer.Text = (Number_Customer[3]).ToString();
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
					Thread.Sleep(Jiange);
				}
				else
				{
					FactEnd = StartTime.AddSeconds(en.OccurTime);
					text.Text = "第" + (++Number_event).ToString() + "个事件 " + "客户离开  时间" + StartTime.AddSeconds(en.OccurTime).ToLongTimeString().ToString() + "第 " + en.Number + " 号顾客由 " + en.Grade.ToString() + " 级别的第 " + (en.NType + 1).ToString() + " 位理发师完成服务 用时"+en.dur/60+"分钟\r\n" + text.Text;
					//text.Text= "第" + (++Number_event).ToString() + "个事件 "+"客户离开  级别" +"编号" + en.Number + " " + en.Grade.ToString() + "  此级别的第几位" + en.NType.ToString() + " " + StartTime.AddSeconds(en.OccurTime).ToLongTimeString().ToString() + "\r\n"+text.Text;                   
					CustomerDepature();
					
					Thread.Sleep(Jiange);
				}
			}
			guandian.Show();
            MessageBox.Show("营业结束，点击关店按钮查看相关信息");
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