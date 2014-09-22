using System;
using System.Collections.Generic;
using System.Text;

namespace Jeelu.SimplusD.Client.Win
{
	/// <summary>
	/// �̳���������,֧����������
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class SDLinkedList<T> : LinkedList<T>
	{
		/// <summary>
		/// �����ⲿί����ʵ�ֲ���
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		public LinkedListNode<T> Find(Predicate<T> match)
		{
			LinkedListNode<T> node = this.First;

			for (int i = 0; i < this.Count; i++)
			{
				if (match(node.Value))
				{
					return node;
				}
				node = node.Next;
			}
			return default(LinkedListNode<T>);
		}

		/// <summary>
		/// �Ƴ�node֮��֮���,������node
		/// </summary>
		/// <param name="node"></param>
		public void RemoveAfter(LinkedListNode<T> node)
		{
			if (node==null)
			{
				 return;
			}
			LinkedListNode<T> curNode = node.Next;
			LinkedListNode<T> tmpNode;
			while (curNode!=null)
			{
				tmpNode = curNode.Next;
				Remove(curNode);
				curNode = tmpNode;    
			}
		}

		/// <summary>
		/// �Ƴ�node֮ǰ֮���,������node����
		/// </summary>
		/// <param name="node"></param>
		public void RemoveBefore(LinkedListNode<T> node)
		{
			if (node==null)
			{
				 return;
			}
			while (this.First != node)
			{
				RemoveFirst();
			}
		}

		/// <summary>
		/// ���Ҳ����ط��Ϸ���match֮���ֵ����
		/// </summary>
		/// <param name="match"></param>
		/// <returns></returns>
		public List<T> FindAll(Predicate<T> match)
		{
			List<T> reslut = new List<T>();

			foreach (T nodeValue in this)
			{
				if (match(nodeValue))
				{
					reslut.Add(nodeValue);
				}    
			}

			return reslut;
		}
	}
}
