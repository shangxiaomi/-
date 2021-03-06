# Program-Design
长春理工大学17级2018年上学期数据结构与算法课程设计
## 课程设计题目：以队列实现的仿真技术预测理发馆的经营状况
### 设计内容
#### 理发馆一天的工作过程如下：
1. 理发馆有N把理发椅，可同时为N位顾客进行理发。
2.	理发师分三个等级（一级、二级、三级），对应不同的服务收费。
3.	当顾客进门时，需选择某级别理发师，只要该级别的理发师有空椅，则可立即坐下理发，否则需排队等候。
4.	一旦该级别的理发师有顾客理发完离去，排在队头的顾客便可开始理发。
5.	若理发馆每天连续营业T分钟，求  
a)	      一天内顾客在理发馆内的平均逗留时间；  
b)	      顾客排队等候理发的队列长度平均值；  
c)	      营业时间到点后仍需完成服务的收尾工作时间；  
d)	      统计每天的营业额；  
e)	      统计每天不同级别理发师的创收。  

### 设计要求
1. 	模拟理发馆一天的工作过程：必须采用事件驱动的离散模型（参考教科书3.5节离散事件模拟p65）；
2.	每个顾客到达和下一顾客到达时间的间隔应是随机的；
3.	理发师编号、理发师级别和每天的营业时间由用户输入；
4.	某顾客挑选某一个级别的理发师而不得时，选第一个队列排队等待 ；
5.	每个顾客进门时将生成三个随机数:  
a)	durtime:进门顾客理发所需服务时间（简称：理发时间）；   
b)	intertime:下一顾客将到达的时间间隔（简称：间隔时间）；  
c)	select:服务选项 ；  
6.	服务收费：应包含服务时间和理发师级别两个因素。  
7.	除了输出统计的数据外，还需要显示理发馆的状态，可以采用文本方式（横向显示每张椅编号、理发师级别。纵向表示等待该理发师理发的排队长度）。
	
---
#### 作者后台代码开发详解
1. 本程序使用的是“”离散事件模拟”来模拟一天内顾客的到达和离开，以此来模拟一天内理发馆的经营状况
2. 本程序全部完成了设计要求
3. 用一个事件链表来记录事件，然后用动态数量（根据用户输入）的队列表示不同理发师的当前的状态
4. 当一名顾客到达时，即生成下一名顾客到达的时间和逗留时间，然后按顺序插入到链表中，保证链表是按照顾客离开时间递增序列，这样就可以保证时间发生的先后顺序
5. 当顾客到达时，会先生成级别，先寻找该级别的空座位，若没有空座位，则寻找该级别队列最短的队排队
6. 收费是根据级别和服务时间共同确定的
7. 离散事件系统模拟的特点是：  
（1)离散事件系统仿真实验的目的是用大量抽样试验的统计结果来逼近总体分布的统计特征值，因而需要进行多次模拟运行。  
（2)离散事件系统模拟中，时间一般不按均匀步长推进，而是按“下一最早发生事件”的发生时间推进，称为“事件调度法”。  

7. 然后我就先不写了------------------------
