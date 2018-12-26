using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Queue;
using LinkedList;
using System.Threading;
namespace 离散事件模拟
{
    public partial class Form1 : Form
    {
        Simulation op = new Simulation();//生成理发店模拟对象
        DateTime End;//关店时间
        DateTime Start;//开店时间
        public Form1()
        {
            InitializeComponent();
            TextBox.CheckForIllegalCrossThreadCalls = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToAddRows = false;//不允许用户添加行
            button2.Hide();
            button4.Hide();
            button5.Hide();
        }
        bool finish=false;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if(finish==false)//判断理发师人数和工资是否设定完成
            {
                MessageBox.Show("请先设置理发师的信息，然后再点击“设置理发师信息”按钮");
                return;
            }
            try//判断理发师编号是否填写完成
            {
                Exception ex = new Exception();
                int cnt = dataGridView1.RowCount;
                for(int i=0;i<cnt;i++)
                {
                    if (dataGridView1.Rows[i].Cells[1].Value.ToString() == String.Empty)
                        throw ex;
                }
            }
            catch
            {
                MessageBox.Show("请设置理发师编号设置"); 
                return;
            }
            try//判断开关店时间是否合法
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
            Start = new DateTime(11, 11, 11, int.Parse(comboBox1.Text), int.Parse(comboBox3.Text), int.Parse(comboBox5.Text));
            End = new DateTime(11, 11, 11, int.Parse(comboBox2.Text), int.Parse(comboBox4.Text), int.Parse(comboBox6.Text));
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
            button5.Hide(); button4.Hide();
            dataGridView1.ReadOnly = true;
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
            finish = false;
            textBox8.ReadOnly = false;
            textBox9.ReadOnly = false;
            textBox10.ReadOnly = false;
            textBox11.ReadOnly = false;
            textBox12.ReadOnly = false;
            textBox13.ReadOnly = false;
            dataGridView1.ReadOnly = false;
            for (int i = 0; i < 5; i++)
            {
                if (i == 1) continue;
                dataGridView1.Columns[i].ReadOnly = false;
            }
            this.Text = "理发馆仿真模拟系统";
            button1.Show();
            button3.Show();
            button4.Hide();
            
        }

        private void 软件信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 xinxi = new Form2();
            xinxi.ShowDialog();
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
        private void Delete()//把表格全部删除
        {

            int cnt = dataGridView1.RowCount;
            for (; cnt!=0; cnt=dataGridView1.RowCount)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }
        }
        private void Initial_Data()//初始化表格
        {
            Delete();
            int cnt = 0;
            for (int i = 1; i <= 3; i++)
            {
                for (int j = 0; j < op.Number_Staff[i]; j++)
                {
                    dataGridView1.Rows.Add();
                    dataGridView1.Rows[cnt].Height = 30;
                    dataGridView1.Rows[cnt].Cells[0].Value = (cnt+1).ToString();
                    dataGridView1.Rows[cnt].Cells[2].Value = i.ToString();
                    dataGridView1.Rows[cnt].Cells[4].Value = "空";
                    cnt++;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)//理发师信息设定按钮
        {
            
            try//判断理发师人数是否输入正确
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
            try//判断理发师工资是否设定正确
            {
                Exception t = new Exception();
                int a = int.Parse(textBox13.Text);
                int b = int.Parse(textBox9.Text);
                int c = int.Parse(textBox11.Text);
                if (a <= 0 || c <= 0 || b <= 0) throw t;
            }
            catch
            {
                MessageBox.Show("请输入正确的理发师工资(工资大于0)");
                return;
            }
            op.Number_Staff[1] = int.Parse(textBox8.Text);
            op.Number_Staff[2] = int.Parse(textBox10.Text);
            op.Number_Staff[3] = int.Parse(textBox12.Text);
            Delete();
            Initial_Data();
            for (int i = 0; i < 5; i++)
            {
                if (i == 1) continue;
                dataGridView1.Columns[i].ReadOnly = true;
            }
            button3.Hide();
            button4.Show();
            button5.Show();
            finish = true;
        }

        private void button4_Click(object sender, EventArgs e)//理发师信息重置按钮
        {
            Delete();
            finish = false;
            button5.Hide();
            button4.Hide();
            button3.Show();
        }

        private void button5_Click(object sender, EventArgs e)//一键填写理发师编号
        {
            int cnt = dataGridView1.RowCount;
            for(int i=0;i<cnt;i++)
            {
                dataGridView1.Rows[i].Cells[1].Value = (i+1).ToString();
            }
        }
    }
    #region//时间节点（结构体）
    public struct Event
	{
		public int OccurTime;//事件发生时间（对于已经进店且开始理发的顾客来说是离开时间，对于未进店和已经进店但在排队的顾客来说是到达时间）
        public int ArriveTime;//此事件记录的顾客的到达时间
		public int NType;//第几位理发师
		public int dur;//服务时间
		public int Select;//理发师级别
		public int Number;//顾客编号
	}
	#endregion
	#region//顾客节点（结构体）
	struct CustomerNode
	{
        public int ArriveTime;//顾客的到达时间
		public int Duration;//服务时间
		public int Number;//顾客编号
	}
	#endregion
	#region//理发店模拟类
	public class Simulation
	{
        #region//相关字段
        public int Queuelength=0;//用于记录队列的长度累积
        public int Jiange = 100;//两个事件发生的间隔，在Form1中可以修改
        public TextBox Queue_length = null;//用于显示平均队列长度
		public TextBox onecustumer = null;//用于显示相应级别的已经结账顾客数
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
        int CurrentTime;//系统当前时间
		public int CustomerNum;//顾客总数目
		public int CloseTime;//营业时间（关店时间减去开店时间）（在Form1中设置）
		Event en = new Event();//事件节点
		CustomerNode cn = new CustomerNode();//顾客节点
		public int[] Number_Customer = new int[4];//三个级别的已经结账顾客各有多少
		public int[] Number_Staff = new int[4];//三个级别的理发师各多少人
		public int[] Salsry_STaff = new int[4];//三个级别的价格（在Form1中设置）
        public int[] income = new int[4];//当前级别的收入
		public int Total_Staff;//理发师总人数
		MyLinkQueue<CustomerNode>[] QStaff1 = null;//三个级别的理发师队列
		MyLinkQueue<CustomerNode>[] QStaff2 = null;
		MyLinkQueue<CustomerNode>[] QStaff3 = null;
		#endregion
		#region//更新Form1中的理发师工作状态函数
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
						if (len > 0) data.Rows[cnt].Cells[3].Value = QStaff1[j].First().Number.ToString();
						else data.Rows[cnt].Cells[3].Value = "空";
					}
					else if (i == 2)
					{
						len = QStaff2[j].Size;
						if (len > 0) data.Rows[cnt].Cells[3].Value = QStaff2[j].First().Number.ToString();
						else data.Rows[cnt].Cells[3].Value = "空";
					}
					else if (i == 3)
					{
						len = QStaff3[j].Size;
						if (len > 0) data.Rows[cnt].Cells[3].Value = QStaff3[j].First().Number.ToString();
						else data.Rows[cnt].Cells[3].Value = "空";
					}
					if (len > 0) data.Rows[cnt].Cells[4].Value = (len - 1).ToString();
					else data.Rows[cnt].Cells[4].Value = "0";
					cnt++;
				}
                Queue_length.Text=((float)Queuelength / (float)Number_event).ToString();//计算等待队列平均长度

            }

		}
        #endregion
        #region//用于更新等待队列长度（用于计算等待队列平均长度）
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
        #region//顾客到达和离开
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
		public void CustomerArrived()
		{
			en.Select = rd.Next(1, 4);//生成顾客级别
            CurrentTime = en.ArriveTime;//更改系统当前时间
			++CustomerNum; //增加客户数量
			en.Number = CustomerNum;//记录客户编号
			int durTime = rd.Next(600, 3601);     //产生600-3600秒（10分钟到一小时）间的随机整数，为到达客户进行理发需要的时间。
			int interTime = rd.Next(0, 601);      //产生0-600秒 间的随机整数，为与下一客户到达时间的时间间隔。
            int arrT = en.ArriveTime + interTime;    //计算下一个顾客的到达时间
													//开始排队，找到最短队伍，并加入队伍。
			int i = MinQueue(en.Select);             //找到该级别理发师队列最短的队伍
            en.NType = i;
            en.dur = durTime;
            cn.ArriveTime = en.ArriveTime;         //顾客的到达时间
            cn.Duration = durTime;                  //顾客的服务时间
			cn.Number = en.Number;                  //顾客的编号
			if (en.Select == 1)//选择相应的理发师队伍入队
			{
				QStaff1[i].EnQueue(cn);
			}
			else if (en.Select == 2)
			{
				QStaff2[i].EnQueue(cn);
			}
			else if (en.Select == 3)
			{
				QStaff3[i].EnQueue(cn);
			}
            Sum_Queue();//更新等待队列的长度
            
            int cur;
            text.Text = "第" + (++Number_event).ToString() + "个事件 " + "有新客户！  时间" + StartTime.AddSeconds(cn.ArriveTime).ToLongTimeString().ToString() + "第" + en.Number + "号顾客将由 " + en.Select.ToString() + " 级别的第" + (en.NType + 1).ToString() + " 位理发师进行服务\r\n" + text.Text;
            //如果当前顾客能立马被服务就将当前顾客离开的事件加入事件链表，否则不加入事件链表
            if ((en.Select == 1 && QStaff1[i].Size <= 1) || (en.Select == 2 && QStaff2[i].Size <= 1) || (en.Select == 3 && QStaff3[i].Size <= 1))
            {
                int depT = cn.ArriveTime + durTime;      //计算当前顾客的离开时间             
                en.OccurTime = depT;//记录离开事件的发生时间，即顾客的离开事件
                cur = First(en);
                ev.Insert(cur, en);//插入到事件链表中
            }

			//下一客户到达事件，加入事件列表。
			en.ArriveTime = arrT;
            en.OccurTime = en.ArriveTime;
			en.NType = 0;
			en.dur = 0;
			en.Select = 0;
			if (en.ArriveTime < CloseTime)//顾客的到达时间小于关店时间就进店，否则不进店
			{
				cur = First(en);
				ev.Insert(cur, en);
			}
		}
		public void CustomerDepature()
		{
			CustomerNode temp = new CustomerNode();
			int i = en.NType;
			int grade = en.Select;
			if (1 == grade)//有顾客结账离开，更新收入情况
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
            CurrentTime = en.OccurTime;//更新系统时间
			Number_Customer[en.Select]++;//记录各级别顾客的结账人数
			onecustumer.Text = (Number_Customer[1]).ToString();//更改Form1中已经结账顾客人数
			twocustumer.Text = (Number_Customer[2]).ToString();
			threecustumer.Text = (Number_Customer[3]).ToString();
            TotalTime += (CurrentTime-temp.ArriveTime);//记录顾客的总逗留时间（顾客的离开时间 减去 到达时间）
            text1.Text = Convert.ToString(TotalTime / (Number_Customer[1] + Number_Customer[2] + Number_Customer[3])) + "秒";
			int money = income[1]+income[2]+income[3];
			text2.Text = money.ToString() + "元";
            Sum_Queue();
            
            //如果当前已经出队的队列还有顾客等待，就开始服务等待顾客，将此顾客的离开事件加入时间链表
            if((1==grade&&QStaff1[i].Size!=0)|| (2 == grade && QStaff2[i].Size != 0)|| (3 == grade && QStaff3[i].Size != 0))
            {
                if(1==grade)
                {
                    temp = QStaff1[i].First();
                }
                else if(2==grade)
                {
                    temp = QStaff2[i].First();
                }
                else if(3==grade)
                {
                    temp = QStaff3[i].First();
                }
                int depT = CurrentTime + temp.Duration;  //计算当前顾客的离开时间
                en.OccurTime = depT;
                en.NType = i;//记录此顾客的理发师号码
                en.Select = grade;//记录此顾客的理发师级别
                en.dur = temp.Duration;//记录此顾客的服务时间
                en.ArriveTime = temp.ArriveTime;//记录此顾客的到达时间
                en.Number = temp.Number;//记录此顾客的号码
                int cur = First(en);//插入事件链表
                ev.Insert(cur, en);
            }
            Update();
        }
		#endregion
		public void OpenDay()
		{
            Queuelength = 0;//等待队列长度初始化为0
			Number_event = 0;//事件数设为0
			for (int i = 1; i < 4; i++)
			{
				income[i]=Number_Customer[i] = Number_Staff[i] = 0;
			}
			ev = new MySingleLinkedList<Event>();//初始化事件链表
			en.Select = 0;//顾客的理发师级别;
			TotalTime = 0;//顾客逗留时间初始化为0
			CustomerNum = 0;//顾客总数初始化为0
			en.OccurTime = rd.Next(0,601);//设置第一个客户到达事件
            en.ArriveTime = en.OccurTime;
            en.NType = 0;
            if (en.ArriveTime < CloseTime)//如果第一个顾客的到达时间小于关店时间则加入时间链表
            {
                ev.Add(en);
            }
            else
            {
                FactEnd = StartTime.AddSeconds(CloseTime);//如果没有第一个顾客则记录实际关店时间
            }
            text.Text = "";
			onecustumer.Text = (Number_Customer[1]).ToString();
			twocustumer.Text = (Number_Customer[2]).ToString();
			threecustumer.Text = (Number_Customer[3]).ToString();
			Number_Staff[1] = Convert.ToInt32(text3.Text);
			Number_Staff[2] = Convert.ToInt32(text4.Text);
			Number_Staff[3] = Convert.ToInt32(text5.Text);
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
		public int MinQueue(int Type)//找到相应级别理发师的长度最短的队列
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
				if (en.Select == 0)
				{
					CustomerArrived();
					Update();//更新理发师状态信息
					Thread.Sleep(Jiange);
				}
				else
				{
					FactEnd = StartTime.AddSeconds(en.OccurTime);//记录实际关店时间
					text.Text = "第" + (++Number_event).ToString() + "个事件 " + "客户离开  时间" + StartTime.AddSeconds(en.OccurTime).ToLongTimeString().ToString() + "第 " + en.Number + " 号顾客由 " + en.Select.ToString() + " 级别的第 " + (en.NType + 1).ToString() + " 位理发师完成服务 用时"+en.dur/60+"分钟\r\n" + text.Text;
					//text.Text= "第" + (++Number_event).ToString() + "个事件 "+"客户离开  级别" +"编号" + en.Number + " " + en.Select.ToString() + "  此级别的第几位" + en.NType.ToString() + " " + StartTime.AddSeconds(en.OccurTime).ToLongTimeString().ToString() + "\r\n"+text.Text;                   
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