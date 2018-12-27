using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Queue
{
    /// <summary>
    /// 基于链表的队列节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }

        public Node(T item)
        {
            this.Item = item;
        }

        public Node()
        { }
    }

    /// <summary>
    /// 基于链表的队列实现
    /// </summary>
    /// <typeparam name="T">类型</typeparam>
    public class MyLinkQueue<T>
    {
        private Node<T> head;
        private Node<T> tail;
        private int size;

        public MyLinkQueue()
        {
            this.head = null;
            this.tail = null;
            this.size = 0;
        }

        /// <summary>
        /// 入队操作
        /// </summary>
        /// <param name="node">首元素</param>
        public T First()
        {
            if (IsEmpty())
            {
                return default(T);
            }
            return head.Item;
        }
       
        /// <summary>
        /// 入队操作
        /// </summary>
        /// <param name="node">节点元素</param>

        public void EnQueue(T item)
        {
            Node<T> oldLastNode = tail;
            tail = new Node<T>();
            tail.Item = item;

            if(IsEmpty())
            {
                head = tail;
            }
            else
            {
                oldLastNode.Next = tail;
            }

            size++;
        }

        /// <summary>
        /// 出队操作
        /// </summary>
        /// <returns>出队元素</returns>
        public T DeQueue()
        {
            T result = head.Item;
            head = head.Next;
            size--;

            if(IsEmpty())
            {
                tail = null;
            }
            return result;
        }

        /// <summary>
        /// 是否为空队列
        /// </summary>
        /// <returns>true/false</returns>
        public bool IsEmpty()
        {
            return this.size == 0;
        }

        /// <summary>
        /// 队列中节点个数
        /// </summary>
        public int Size
        {
            get
            {
                return this.size;
            }
        }

        // 索引器
        public T this[int index]
        {
            get
            {
                return this.GetNodeByIndex(index).Item;
            }
            set
            {
                this.GetNodeByIndex(index).Item = value;
            }
        }

        private Node<T> GetNodeByIndex(int index)
        {
            if (index < 0 || index >= this.size)
            {
                throw new ArgumentOutOfRangeException("index", "索引超出范围");
            }

            Node<T> tempNode = this.head;
            for (int i = 0; i < index; i++)
            {
                tempNode = tempNode.Next;
            }
            return tempNode;
        }
    }
}
