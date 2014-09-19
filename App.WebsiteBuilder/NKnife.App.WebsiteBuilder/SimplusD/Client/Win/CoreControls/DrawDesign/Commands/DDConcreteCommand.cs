
/*
 * DrawDesigner�ľ��������ʵ��
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
	/// <summary>
	/// ����ֱ������
	/// </summary>
	public class AddLineCommand : BaseCommand
	{
		#region ���Գ�Ա����

		/// <summary>
		/// ��ȡ������ֱ�ߵ����
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		/// ��ȡ������ֱ�ߵ��յ�
		/// </summary>
		public int End{ get; set; }

		/// <summary>
		/// ��ȡ������ֱ�ߵ�λ��
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// ��ȡ������ֱ�ߵķ���
		/// </summary>
		public bool IsRow { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<PartitionLine> AddedLines { get; set; }

		/// <summary>
		/// ��ȡ�������Ƿ�Ϊ�����е����ֱ��,�������,��˵��Ϊ��һ���½�������ֱ��      
		/// </summary>
		public bool IsRedo { get; set; }

		public List<Rect> RemovedRects { get; set; }

		public List<Rect> AddedRects { get; set; }

		#endregion

		#region �ڲ�����

		/// <summary>
		/// ��ʼ�����Եĳ�ʼֵ
		/// </summary>
		private void Init()
		{
			AddedLines = new List<PartitionLine>();
			RemovedRects = new List<Rect>();
			AddedRects = new List<Rect>();
		}

		#endregion

		#region ���캯��

		public AddLineCommand(DesignPanel tdPanel, PartitionLine addedLine)
		{
			Init();
			AddedLines.Add(addedLine);

			Start = addedLine.Start;
			End = addedLine.End;
			Position = addedLine.Position;
			IsRow = addedLine.IsRow;

			TDPanel = tdPanel;
			TDPanel.Modified = true;
			CommandInfo = "����ֱ��";
		}

		public AddLineCommand(DesignPanel tdPanel, PartitionLine addedLine, List<PartitionLine> addLines)
		{
			Init();
			Start = addedLine.Start;
			End = addedLine.End;
			Position = addedLine.Position;
			IsRow = addedLine.IsRow;

			AddedLines = addLines;

			TDPanel = tdPanel;
			TDPanel.Modified = true;
			CommandInfo = "����ֱ��";
		}

		public AddLineCommand(DesignPanel tdPanel, int start, int end, int position, bool isRow)
		{
			Init();
			AddedLines.Add(new PartitionLine(start, end, position, isRow));

			Start = start;
			End = end;
			Position = position;
			IsRow = isRow;

			TDPanel = tdPanel;
			TDPanel.Modified = true;
			CommandInfo = "����ֱ��";
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���
		/// </summary>
		override public void Execute()
		{
			foreach (PartitionLine line in AddedLines)
			{
				TDPanel.DrawPanel.ListLine.AddLine(line.Start, line.End, line.Position, line.IsRow);
				TDPanel.DrawPanel.DrawLine(line);
			}            
			
			TDPanel.DrawPanel.ListRect.AddLine(this);
			foreach (Rect rect in RemovedRects)
			{
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);   
			}
		}

		override public void UnExecute()
		{
			foreach (PartitionLine line in AddedLines)
			{
				TDPanel.DrawPanel.ListLine.UnAddLine(line.Start, line.End, line.Position, line.IsRow);
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize * TDPanel.DrawPanel.BoldPenTimes,
					line,
					TDPanel.DrawPanel.CurZoom);
			}
			TDPanel.DrawPanel.ListRect.UnAddLine(this);
			foreach (Rect rect in AddedRects)
			{
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		#endregion

		#region ��Ϣ����

		#endregion
	}

	/// <summary>
	/// ɾ��ֱ������
	/// </summary>
	public class DeleteLineCommand : BaseCommand
	{
		#region ���Գ�Ա����

		/// <summary>
		/// ��ȡ������ֱ�ߵ����
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		/// ��ȡ������ֱ�ߵ��յ�
		/// </summary>
		public int End { get; set; }

		/// <summary>
		/// ��ȡ������ֱ�ߵ�λ��
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// ��ȡ������ֱ�ߵķ���
		/// </summary>
		public bool IsRow { get; set; }

		/// <summary>
		/// ��ȡ�������Ƿ�Ϊ�����е����ֱ��,�������,��˵��Ϊ��һ���½�������ֱ��      
		/// </summary>
		public bool IsRedo { get; set; }

		/// <summary>
		/// ��ɾ��֮����
		/// </summary>
		public List<Rect> RemovedRects { get; set; }

		/// <summary>
		/// ����֮���Ρ��ڴ˴��Ǳ�ѡ�����ľ���
		/// </summary>
		public List<Rect> AddedRects { get; set; }

		#endregion

		#region ���캯��

		public DeleteLineCommand(DesignPanel tdPanel, PartitionLine line, List<Rect> addedRects, List<Rect> removedRects)
		{
			Start = line.Start;
			End = line.End;
			Position = line.Position;
			IsRow = line.IsRow;


			AddedRects = addedRects;
			RemovedRects = removedRects;

			TDPanel = tdPanel;
			TDPanel.Modified = true;
			CommandInfo = "ɾ��ֱ��";
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���
		/// </summary>
		override public void Execute()
		{
			TDPanel.DrawPanel.ListLine.DeleteLine(Start, End, Position, IsRow);
			CommonFuns.Invalidate(TDPanel.DrawPanel,
				TDPanel.DrawPanel.PenSize * TDPanel.DrawPanel.BoldPenTimes,
				new PartitionLine(Start, End, Position, IsRow),
				TDPanel.DrawPanel.CurZoom);
			TDPanel.DrawPanel.ListRect.DeleteLine(this);

			foreach (Rect rect in AddedRects)
			{
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		override public void UnExecute()
		{
			TDPanel.DrawPanel.ListLine.UnDeleteLine(Start, End, Position, IsRow);
			TDPanel.DrawPanel.DrawLine(new PartitionLine(Start, End, Position, IsRow));

			TDPanel.DrawPanel.ListRect.UnDeleteLine(this);
			foreach (Rect rect in RemovedRects)
			{
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		#endregion

		#region ��Ϣ����

		#endregion
	}

	/// <summary>
	/// ������������
	/// </summary>
	public class LockRectCommand : BaseCommand
	{
		#region ���Գ�Ա����

		public List<Rect> LockedRects { get; set; }

		public List<PartitionLine> LockedLines { get; set; }

		#endregion

		#region ���캯��

		public LockRectCommand(DesignPanel tdPanel, List<Rect> lockedRects)
		{
			TDPanel = tdPanel;
			LockedRects = lockedRects;
			LockedLines = new List<PartitionLine>();
			CommandInfo = "��������";
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���
		/// </summary>
		override public void Execute()
		{
			foreach (Rect rect in LockedRects)
			{
				rect.LockRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
				
			}
			foreach (PartitionLine line in LockedLines)
			{
				line.LockLine();
			}
		}

		override public void UnExecute()
		{
			foreach (Rect rect in LockedRects)
			{
				rect.UnLockRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
			foreach (PartitionLine line in LockedLines)
			{
				line.UnLockLine();
			}
		}

		#endregion
	}

	/// <summary>
	/// �⿪������������
	/// </summary>
	public class DisLockRectCommand : BaseCommand
	{
		#region ���Գ�Ա����

		public List<Rect> UnLockedRects { get; set; }

		public List<PartitionLine> UnLockedLines { get; set; }

		#endregion

		#region ���캯��

		public DisLockRectCommand(DesignPanel tdPanel, List<Rect> unLockedRect)
		{
			TDPanel = tdPanel;
			UnLockedRects = unLockedRect;
			UnLockedLines = new List<PartitionLine>();
			CommandInfo = "�⿪��������";
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���
		/// </summary>
		override public void Execute()
		{
			foreach (Rect rect in UnLockedRects)
			{
				rect.UnLockRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
			foreach (PartitionLine line in UnLockedLines)
			{
				line.UnLockLine();
			}
		}

		override public void UnExecute()
		{
			foreach (Rect rect in UnLockedRects)
			{
				rect.LockRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
			foreach (PartitionLine line in UnLockedLines)
			{
				line.LockLine();
			}
		}

		#endregion
	}

	/// <summary>
	/// ѡ������
	/// </summary>
	public class SelectCommand : BaseCommand
	{
		#region �ֶγ�Ա����

		private List<PartitionLine> _selectedLines = new List<PartitionLine>();
		private List<PartitionLine> _disSelectedLines = new List<PartitionLine>();
		private List<Rect> _selectedRects = new List<Rect>();// ��ѡ��ľ���    
		private List<Rect> _disSelectedRects = new List<Rect>();

		#endregion

		#region ���Գ�Ա����

		/// <summary>
		/// ��ѡ���ֱ��
		/// </summary>
		public List<PartitionLine> SelectedLines
		{
			get { return _selectedLines; }
			set { _selectedLines = value; }
		}

		/// <summary>
		/// ��ѡ��ֱ�߶�ȡ��ѡ���ֱ��
		/// </summary>
		public List<PartitionLine> DisSelectedLines
		{
			get { return _disSelectedLines; }
			set { _disSelectedLines = value; }
		}

		public List<Rect> DisSelectedRects
		{
			get { return _disSelectedRects; }
			set { _disSelectedRects = value; }
		}

		/// <summary>
		/// ��ѡ��ľ���
		/// </summary>
		public List<Rect> SelectedRects
		{
			get { return _selectedRects; }
			set { _selectedRects = value; }
		}

		#endregion

		#region ���캯��

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="tdPanel"></param>
		/// <param name="selectedRects">��Ҫ��Ϊѡ��״̬֮����</param>
		/// <param name="disSelectedRects">��ȡ��ѡ��״̬֮����</param>
		/// <param name="selectedLines">����ѡ���ֱ��</param>
		/// <param name="disSelectedLines">����ȡ��ѡ���ֱ��</param>
		public SelectCommand(DesignPanel tdPanel, List<Rect> selectedRects, List<Rect> disSelectedRects, List<PartitionLine> selectedLines, List<PartitionLine> disSelectedLines)
		{
			TDPanel = tdPanel;

			SelectedRects.AddRange(selectedRects);
			DisSelectedRects.AddRange(disSelectedRects);
			SelectedLines.AddRange(selectedLines);
			DisSelectedLines.AddRange(disSelectedLines);

			CommandInfo = "����ѡ�����";
		}

		#endregion

		#region ����������Ա�ӿ�

		override public void Execute()
		{
			foreach (PartitionLine line in SelectedLines)
			{
				TDPanel.DrawPanel.ListLine.SelectLine(line);
				TDPanel.DrawPanel.DrawLine(line);
			}
			foreach (PartitionLine line in DisSelectedLines)
			{
				TDPanel.DrawPanel.ListLine.UnSelectLine(line);

				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize * TDPanel.DrawPanel.BoldPenTimes,
					line,
					TDPanel.DrawPanel.CurZoom);
			}
			foreach (Rect rect in SelectedRects)
			{
				rect.SelectRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
			foreach (Rect rect in DisSelectedRects)
			{
				rect.UnSelectRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize ,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		override public void UnExecute()
		{
			foreach (PartitionLine line in DisSelectedLines)
			{
				TDPanel.DrawPanel.ListLine.SelectLine(line);
				TDPanel.DrawPanel.DrawLine(line);
			}
			foreach (PartitionLine line in SelectedLines)
			{
				TDPanel.DrawPanel.ListLine.UnSelectLine(line);

				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize * TDPanel.DrawPanel.BoldPenTimes,
					line,
					TDPanel.DrawPanel.CurZoom);
			}

			foreach (Rect rect in DisSelectedRects)
			{
				rect.SelectRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
			foreach (Rect rect in SelectedRects)
			{
				rect.UnSelectRect();
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		#endregion
	}

	/// <summary>
	/// �ϲ�����
	/// </summary>
	public class MergeRectCommand : BaseCommand
	{
		#region �ֶγ�Ա����

		private List<Rect> _removedRects = new List<Rect>();
		private List<Rect> _addedRects = new List<Rect>();
		private Rectangle _leftRect = new Rectangle();///���µľ���
		private Rectangle _boundaryRect = new Rectangle();
		private List<PartitionLine> _inRectLines = new List<PartitionLine>();
		private bool _isRedo = false;///�Ƿ�Ϊ�����е����ֱ��,�������,��˵��Ϊ��һ���½�������ֱ��        

		#endregion

		#region ���Գ�Ա����
		public bool IsRedo
		{
			get { return _isRedo; }
			set { _isRedo = value; }
		}

		public List<Rect> RemovedRects
		{
			get { return _removedRects; }
			set { _removedRects = value; }
		}

		public List<Rect> AddedRects
		{
			get { return _addedRects; }
			set { _addedRects = value; }
		}

		/// <summary>
		/// ��ѡ������֮���δ�С
		/// </summary>
		public Rectangle LeftRect
		{
			get { return _leftRect; }
			set { _leftRect = value; }
		}

		/// <summary>
		/// �߽����
		/// </summary>
		public Rectangle BoundaryRect
		{
			get { return _boundaryRect; }
			set { _boundaryRect = value; }
		}

		public List<PartitionLine> InRectLines
		{
			get { return _inRectLines; }
			set { _inRectLines = value; }
		}
		#endregion

		#region ���캯��

		public MergeRectCommand(DesignPanel tdPanel)
		{
			TDPanel = tdPanel;

			List<Rect> selectedRects = tdPanel.DrawPanel.ListRect.GetSelectedRects();
			Rectangle boundaryRect = CommonFuns.FindRectsBorder(selectedRects);

			///����߽����
			BoundaryRect = new Rectangle(
				boundaryRect.X,
				boundaryRect.Y,
				boundaryRect.Width,
				boundaryRect.Height
				);

			CommandInfo = "�ϲ�����";
			
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���
		/// </summary>
		override public void Execute()
		{
			if (!TDPanel.DrawPanel.MergeSelectedRects(this))
			{ return; }
			TDPanel.DrawPanel.ListLine.DeleteRectLine(this);
			
			if (!IsRedo)
			{
				IsRedo = true;
			}
			CommonFuns.Invalidate(TDPanel.DrawPanel,
				TDPanel.DrawPanel.PenSize,
				BoundaryRect,
				TDPanel.DrawPanel.CurZoom);

			TDPanel.Modified = true;
		}

		override public void UnExecute()
		{
			TDPanel.DrawPanel.ListLine.UnDeleteRectLine(this);
			TDPanel.DrawPanel.UnMergeSelectedRects(this);
			CommonFuns.Invalidate(TDPanel.DrawPanel,
				TDPanel.DrawPanel.PenSize,
				BoundaryRect,
				TDPanel.DrawPanel.CurZoom);

			TDPanel.Modified = true;
		}

		#endregion
	}

	/// <summary>
	/// �ָ����
	/// </summary>
	public class PartRectCommand : BaseCommand
	{
		#region �ֶγ�Ա����

		private List<Rect> _removedRects = new List<Rect>();
		private List<Rect> _addedRects = new List<Rect>();
		private List<PartitionLine> _addedLines = new List<PartitionLine>();
		private bool _isRow = true;

		#endregion

		#region ���Գ�Ա����

		public List<Rect> RemovedRects
		{
			get { return _removedRects; }
			set { _removedRects = value; }
		}

		public List<Rect> AddedRects
		{
			get { return _addedRects; }
			set { _addedRects = value; }
		}

		public List<PartitionLine> AddedLines
		{
			get { return _addedLines; }
			set { _addedLines = value; }
		}

		public bool IsRow
		{
			get { return _isRow; }
			set { _isRow = value; }
		}
		#endregion

		#region ���캯��

		public PartRectCommand(DesignPanel tdPanel, bool isRow, List<Rect> addedRects, Rect removedRect, List<PartitionLine> newLines)
		{
			IsRow = isRow;
			AddedRects = addedRects;
			RemovedRects.Add(removedRect);
			AddedLines = newLines;
			TDPanel = tdPanel;
			TDPanel.Modified = true;
			CommandInfo = "�ϲ�����";
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���,��ִ������ǰ�Ѿ���_removedRects��_addedRects��_inRectLines��ʼ����
		/// </summary>
		override public void Execute()
		{
			foreach (Rect rect in AddedRects)
			{
				rect.IsDeleted = false;
			}
			foreach (PartitionLine line in AddedLines)
			{
				TDPanel.DrawPanel.ListLine.AddLine(line.Start, line.End, line.Position, line.IsRow);
			}
			foreach (Rect rect in RemovedRects)
			{
				rect.IsDeleted = true;
				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		override public void UnExecute()
		{
			foreach (Rect rect in AddedRects)
			{
				rect.IsDeleted = true;
			}

			foreach (PartitionLine line in AddedLines)
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(line.Start, line.End, line.Position, line.IsRow);
			}

			foreach (Rect rect in RemovedRects)
			{
				rect.IsDeleted = false;

				CommonFuns.Invalidate(TDPanel.DrawPanel,
					TDPanel.DrawPanel.PenSize,
					rect,
					TDPanel.DrawPanel.CurZoom);
			}
		}

		#endregion
	}

	/// <summary>
	/// �ƶ�ֱ��
	/// </summary>
	public class MoveLineCommand : BaseCommand
	{
		#region �ֶγ�Ա����

		PartitionLine _movedLine;//���ƶ�ֱ֮��
		int _movedToPos;//�ƶ�����λ��
		List<PartitionLine> _newLines = new List<PartitionLine> ();//�ƶ���֮��ֱ��
		PartitionLine _firstBorderLine;//����ϵĽ���
		PartitionLine _secondBorderLine;//�һ��µĽ���
	   // List<PartitionLine> _partedLines = new List<PartitionLine>();//�������ƶ���ֱ�߷ָ��ֱ��       
		List<PartitionLine> _endPointLines = new List<PartitionLine>();//�˵��ڱ��ƶ���ֱ���ϵ�ֱ��      
		List<PartitionLine> _startPointLines = new List<PartitionLine>();//�˵��ڱ��ƶ���ֱ���ϵ�ֱ��      

		List<Rect> _modefiedRects;//���޸�֮����       
		List<Rect> _oldRects = new List<Rect>();//�����ƶ�ǰ�Ĵ�С�ߴ�,�ʲ��蹤��ģʽ     
		List<Rect> _newRects = new List<Rect> ();//�����ƶ�ǰ�Ĵ�С�ߴ�,���蹤��ģʽ            

		#endregion

		#region ���Գ�Ա����

		/// <summary>
		/// ���ƶ�ֱ֮��
		/// </summary>
		public PartitionLine MovedLine
		{
			get { return _movedLine; }
			set { _movedLine = value; }
		}

		/// <summary>
		/// �ƶ�����λ��
		/// </summary>
		public int MovedToPos
		{
			get { return _movedToPos; }
			set { _movedToPos = value; }
		}

		/// <summary>
		/// �ƶ���֮��ֱ��
		/// </summary>
		public List<PartitionLine> NewLines
		{
			get { return _newLines; }
			set { _newLines = value; }
		}

		/// <summary>
		/// ����ϵĽ���
		/// </summary>
		public PartitionLine FirstBorderLine
		{
			get { return _firstBorderLine; }
			set { _firstBorderLine = value; }
		}

		/// <summary>
		/// �һ��µĽ���
		/// </summary>
		public PartitionLine SecondBorderLine
		{
			get { return _secondBorderLine; }
			set { _secondBorderLine = value; }
		}

		///// <summary>
		///// �������ƶ���ֱ�߷ָ��ֱ��
		///// </summary>
		//public List<PartitionLine> PartedLines
		//{
		//    get { return _partedLines; }
		//    set { _partedLines = value; }
		//}

		/// <summary>
		/// �˵��ڱ��ƶ���ֱ���ϵ�ֱ��
		/// </summary>
		public List<PartitionLine> EndPointLines
		{
			get { return _endPointLines; }
			set { _endPointLines = value; }
		}

		/// <summary>
		/// �˵��ڱ��ƶ���ֱ���ϵ�ֱ��      
		/// </summary>
		public List<PartitionLine> StartPointLines
		{
			get { return _startPointLines; }
			set { _startPointLines = value; }
		}

		/// <summary>
		/// ���޸�֮����
		/// </summary>
		public List<Rect> ModefiedRects
		{
			get { return _modefiedRects; }
			set { _modefiedRects = value; }
		}

		/// <summary>
		/// �����ƶ�ǰ�Ĵ�С�ߴ�,�ʲ��蹤��ģʽ
		/// </summary>
		public List<Rect> OldRects
		{
			get { return _oldRects; }
			set { _oldRects = value; }
		}

		/// <summary>
		/// �����ƶ�ǰ�Ĵ�С�ߴ�,���蹤��ģʽ
		/// </summary>
		public List<Rect> NewRects
		{
			get { return _newRects; }
			set { _newRects = value; }
		}

		#endregion

		#region ���캯��

		public MoveLineCommand(DesignPanel tdPanel,
			PartitionLine movedLine,
			int movedToPos,
			PartitionLine firstBorderLine,
			PartitionLine secondBorderLine)
		{
			TDPanel = tdPanel;

			MovedLine = movedLine;

			MovedToPos = movedToPos;
			FirstBorderLine = firstBorderLine;
			SecondBorderLine = secondBorderLine;

			///��ʼ��Rects
			InitRects();
			///��ʼ��ֱ������:NewLines,EndPointLines,StartPointLines
			InitLines();

			TDPanel.Modified = true;
			CommandInfo = "�ƶ�ֱ��";
		}

		/// <summary>
		/// ��ʼ��ֱ������:NewLines,EndPointLines,StartPointLines
		/// </summary>
		private void InitLines()
		{
			///�Ƿ������ֱ���غ�
			PartitionLine line = new PartitionLine(
				MovedLine.Start, MovedLine.End, MovedToPos, MovedLine.IsRow);
			FindLineByLine findByLine = new FindLineByLine(line);
			List<PartitionLine> allLines = new List<PartitionLine>(
				TDPanel.DrawPanel.ListLine.HPartionLines);
			allLines.AddRange(TDPanel.DrawPanel.ListLine.VPartionLines);

			List<PartitionLine> overLapLines = allLines.FindAll(findByLine.PredicateOverlap);
			List<PartitionLine> resultLines = new List<PartitionLine>();
			if (overLapLines.Count > 0)
			{
				resultLines = line.GetNotOverlapLine(overLapLines);
			}
			else///û���غ�
			{
				resultLines.Add(line);
			}
			NewLines = resultLines;

			///��ʼ��_endPointLines,_startPointLines
			FindLineByLine findByMovedLine = new FindLineByLine(MovedLine);
			EndPointLines = allLines.FindAll(findByMovedLine.PredicateEndTo);
			StartPointLines = allLines.FindAll(findByMovedLine.PredicateStartFrom);
		}

		/// <summary>
		/// ��ʼ��Rects:ModefiedRects,OldRects,NewRects
		/// </summary>
		private void InitRects()
		{
			///�ҵ�modifiedRects
			FindRectByLine findRect = new FindRectByLine(MovedLine);
			///�ҵ�������line���߽��ߵľ���
			ModefiedRects = TDPanel.DrawPanel.ListRect.SnipRectList.FindAll(findRect.BorderLinePredicate);

			///��ʼ��oldRects
			foreach (Rect rect in ModefiedRects)
			{
				OldRects.Add(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
				NewRects.Add(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
			}

			///�޸�newRects����
			if (MovedLine.IsRow)
			{
				foreach (Rect rect in NewRects)
				{
					if (rect.Y < MovedLine.Position)///rect��moveLine���ϱ�
					{
						rect.Height += (MovedToPos - MovedLine.Position);
					}
					else///rect��moveLine���±�
					{
						rect.Height += (MovedLine.Position - MovedToPos);
						rect.Y += (MovedToPos - MovedLine.Position);
					}
				}
			}
			else///���ƶ���ֱ��������ֱ��
			{
				foreach (Rect rect in NewRects)
				{
					if (rect.X < MovedLine.Position)///rect��moveLine�����
					{
						rect.Width += (MovedToPos - MovedLine.Position);
					}
					else///rect��moveLine���ұ�
					{
						rect.Width += (MovedLine.Position - MovedToPos);
						rect.X += (MovedToPos - MovedLine.Position);
					}
				}
			}
		}

		#endregion

		#region ����������Ա�ӿ�

		/// <summary>
		/// ʵ��ִ�к���,��ִ������ǰ�Ѿ���_removedRects��_addedRects��_inRectLines��ʼ����
		/// </summary>
		override public void Execute()
		{
			///����ֱ��
			HandleLine();
			///�������
			HandleRect();

			//ˢ������
			Rectangle invalRect ;
			if (MovedLine.IsRow)
			{
				invalRect = Utility.Draw.PointToRectangle(
					new Point(MovedLine.Start, FirstBorderLine.Position),
					new Point(MovedLine.End, SecondBorderLine.Position));
			}
			else
			{

				invalRect = Utility.Draw.PointToRectangle(
					new Point(FirstBorderLine.Position,MovedLine.Start ),
					new Point( SecondBorderLine.Position,MovedLine.End));
			}            
			CommonFuns.Invalidate(TDPanel.DrawPanel,
				TDPanel.DrawPanel.PenSize,
				invalRect,
				TDPanel.DrawPanel.CurZoom);
		}

		private void HandleLine()
		{
			///���ƶ��������ϱ߽��غ�
			if (MovedToPos==FirstBorderLine.Position)
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				///���û���ص������߶�
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine(line.Start, line.End, line.Position, line.IsRow);
				}
				
				///�޸����ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in StartPointLines)
				{
					line.Start = MovedToPos;
					if (line.ChildLines!=null)
					{
						line.ChildLines.First.Value.Start = MovedToPos;
					}
					if (line.IsRow)
					{
						Point startPoint = new Point(MovedToPos, line.Position);
						TDPanel.DrawPanel.ListLine.AddPoint(startPoint, !line.IsRow);                        
					}
					else
					{
						Point startPoint = new Point(line.Position, MovedToPos);
						TDPanel.DrawPanel.ListLine.AddPoint(startPoint, line.IsRow);   
					}

				}

				///�޸��յ�λ�ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in EndPointLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine
						(MovedToPos,
						line.End,
						line.Position,
						line.IsRow);
				}
			}
			else if (MovedToPos == SecondBorderLine.Position)///���ƶ�����һ��±߽��غ�
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				///���û���ص������߶�
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine(line.Start, line.End, line.Position, line.IsRow);
				}
				///�޸��յ�λ�ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in EndPointLines)
				{
					line.End = MovedToPos;//�޸��յ����꣬ͬʱ�޸����һ�����߶�
					if (line.ChildLines != null && line.ChildLines.Last!=null)
					{
						line.ChildLines.Last.Value.End = MovedToPos;
					}
					if (line.IsRow)
					{
						Point endPoint = new Point(MovedToPos, line.Position);
						TDPanel.DrawPanel.ListLine.AddPoint(endPoint, !line.IsRow);
					}
					else
					{
						Point endPoint = new Point(line.Position, MovedToPos);
						TDPanel.DrawPanel.ListLine.AddPoint(endPoint, line.IsRow);
					}

				}

				///�޸����ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in StartPointLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine
						(line.Start,
						MovedToPos,
						line.Position,
						line.IsRow);
				}
			}
			else///û�кͱ߽����غ�
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedToPos, MovedLine.IsRow);
				if (MovedToPos>MovedLine.Position)///�����ұ߻��±�
				{
					foreach (PartitionLine line in EndPointLines)
					{
						line.End = MovedToPos;
						if (line.ChildLines != null && line.ChildLines.Last!=null)
						{
							line.ChildLines.Last.Value.End = MovedToPos; 
						}
						if (line.IsRow)
						{
							Point endPoint = new Point(MovedToPos, line.Position);
							TDPanel.DrawPanel.ListLine.AddPoint(endPoint, !line.IsRow);
						}
						else
						{
							Point endPoint = new Point(line.Position, MovedToPos);
							TDPanel.DrawPanel.ListLine.AddPoint(endPoint, line.IsRow);
						}
						
					}
					foreach (PartitionLine line in StartPointLines)
					{
						TDPanel.DrawPanel.ListLine.DeleteLine
							(line.Start, 
							MovedToPos, 
							line.Position, 
							line.IsRow);
					}
				}
				else///���ƻ�����
				{
					foreach (PartitionLine line in StartPointLines)
					{
						line.Start = MovedToPos;
						if (line.ChildLines!=null && line.ChildLines.First !=null)
						{
							line.ChildLines.First.Value.Start = MovedToPos;
						}
						if (line.IsRow)
						{
							Point startPoint = new Point(MovedToPos, line.Position);
							TDPanel.DrawPanel.ListLine.AddPoint(startPoint, !line.IsRow);
						}
						else
						{
							Point startPoint = new Point(line.Position, MovedToPos);
							TDPanel.DrawPanel.ListLine.AddPoint(startPoint, line.IsRow);
						}
					}
					foreach (PartitionLine line in EndPointLines)
					{
						TDPanel.DrawPanel.ListLine.DeleteLine
							(MovedToPos,
							line.End,
							line.Position,
							line.IsRow);
					}
				}

			}
		}

		/// <summary>
		/// ������εı仯
		/// </summary>
		private void HandleRect()
		{
			for (int i = 0; i < ModefiedRects.Count; i++)
			{
				///����ͱ߽����غ�:����ؾ���Ϊ��ɾ��
				if (NewRects[i].Width<=0 || NewRects[i].Height<= 0)
				{
					ModefiedRects[i].IsDeleted = true;
				}
				else///����û�кͱ߽����غ�,���޸ľ��δ�С
				{
					ModefiedRects[i].X = NewRects[i].X;
					ModefiedRects[i].Y = NewRects[i].Y;
					ModefiedRects[i].Width = NewRects[i].Width;
					ModefiedRects[i].Height = NewRects[i].Height;
				}
			}
		}

		override public void UnExecute()
		{
			///����ֱ��
			HandleUnLine();
			///�������
			HandleUnRect();

			//ˢ������
			Rectangle invalRect;
			if (MovedLine.IsRow)
			{
				invalRect = Utility.Draw.PointToRectangle(
					new Point(MovedLine.Start, FirstBorderLine.Position),
					new Point(MovedLine.End, SecondBorderLine.Position));
			}
			else
			{

				invalRect = Utility.Draw.PointToRectangle(
					new Point(FirstBorderLine.Position, MovedLine.Start),
					new Point(SecondBorderLine.Position, MovedLine.End));
			}
			CommonFuns.Invalidate(TDPanel.DrawPanel,
				TDPanel.DrawPanel.PenSize,
				invalRect,
				TDPanel.DrawPanel.CurZoom);
		}

		private void HandleUnLine()
		{
			if (MovedToPos == FirstBorderLine.Position)
			{
				///ɾ��û���ص������߶�
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine(line.Start, line.End, line.Position, line.IsRow);
				}
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);


				///�޸����ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in StartPointLines)
				{
					line.Start = MovedLine.Position;
					if (line.ChildLines != null)
					{
						line.ChildLines.First.Value.Start = MovedLine.Position;
					}
					if (line.IsRow)
					{
						Point startPoint = new Point(MovedLine.Position, line.Position);
						TDPanel.DrawPanel.ListLine.DeletePoint(startPoint, !line.IsRow);
					}
					else
					{
						Point startPoint = new Point(line.Position, MovedLine.Position);
						TDPanel.DrawPanel.ListLine.DeletePoint(startPoint, line.IsRow);
					}
				}

				///�޸��յ�λ�ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in EndPointLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine
						(MovedToPos,
						MovedLine.Position,
						line.Position,
						line.IsRow);
				}                  
			}
			else if (MovedToPos == SecondBorderLine.Position)
			{
				///ɾ��û���ص������߶�
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine(line.Start, line.End, line.Position, line.IsRow);
				}
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);

				///�޸��յ�λ�ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in EndPointLines)
				{
					line.End = MovedLine.Position;
					if (line.ChildLines != null && line.ChildLines.Last!=null)
					{
						line.ChildLines.Last.Value.End = MovedLine.Position;
					}
					if (line.IsRow)
					{
						Point endPoint = new Point(MovedToPos, line.Position);
						TDPanel.DrawPanel.ListLine.DeletePoint(endPoint, !line.IsRow);
					}
					else
					{
						Point endPoint = new Point(line.Position, MovedToPos);
						TDPanel.DrawPanel.ListLine.DeletePoint(endPoint, line.IsRow);
					}

				}

				///�޸����ڱ��ƶ�ֱ֮��
				foreach (PartitionLine line in StartPointLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine
						(MovedLine.Position,
						MovedToPos,
						line.Position,
						line.IsRow);
				}           
			}
			else///û�кͱ߽����غ�
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedToPos, MovedLine.IsRow);
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				if (MovedToPos > MovedLine.Position)///�����ұ߻��±�
				{
					foreach (PartitionLine line in StartPointLines)
					{
						line.Start = MovedLine.Position;
						if (line.ChildLines != null && line.ChildLines.Last != null)
						{
							line.ChildLines.Last.Value.Start = MovedLine.Position;
						}
						if (line.IsRow)
						{
							Point endPoint = new Point(MovedLine.Position, line.Position);
							TDPanel.DrawPanel.ListLine.AddPoint(endPoint, !line.IsRow);
						}
						else
						{
							Point endPoint = new Point(line.Position, MovedLine.Position);
							TDPanel.DrawPanel.ListLine.AddPoint(endPoint, line.IsRow);
						}
					}
					foreach (PartitionLine line in EndPointLines)
					{
						TDPanel.DrawPanel.ListLine.DeleteLine
							(MovedLine.Position,
							MovedToPos,
							line.Position,
							line.IsRow);
					}
				}
				else///���ƻ�����
				{
					foreach (PartitionLine line in EndPointLines)
					{
						line.End = MovedLine.Position;
						if (line.ChildLines != null && line.ChildLines.First != null)
						{
							line.ChildLines.First.Value.End = MovedLine.Position;
						}
						if (line.IsRow)
						{
							Point endPoint = new Point(MovedLine.Position, line.Position);
							TDPanel.DrawPanel.ListLine.AddPoint(endPoint, !line.IsRow);
						}
						else
						{
							Point endPoint = new Point(line.Position, MovedLine.Position);
							TDPanel.DrawPanel.ListLine.AddPoint(endPoint, line.IsRow);
						}
					}
					foreach (PartitionLine line in StartPointLines)
					{
						TDPanel.DrawPanel.ListLine.DeleteLine
							(line.Start,
							MovedLine.Position,
							line.Position,
							line.IsRow);
					}               
				}
			}
		}

		private void HandleUnRect()
		{
			for (int i = 0; i < ModefiedRects.Count; i++)
			{
				///����ͱ߽����غ�:����ؾ���Ϊ��ɾ��
				if (ModefiedRects[i].IsDeleted)
				{
					ModefiedRects[i].IsDeleted = false;
				}
				else///����û�кͱ߽����غ�,���޸ľ��δ�С
				{
					ModefiedRects[i].X = OldRects[i].X;
					ModefiedRects[i].Y = OldRects[i].Y;
					ModefiedRects[i].Width = OldRects[i].Width;
					ModefiedRects[i].Height = OldRects[i].Height;
				}
			}
		}

		#endregion
	}

	/// <summary>
	/// �и��
	/// </summary>
	public class CilpPanelCommand : BaseCommand
	{
		#region �ֶγ�Ա����

		int _x;
		int _y;
		int _width;
		int _height;
		Rect _rect;
		LineList _listLine;
		Image _backImg;

		private SDList<PartitionLine> _hPartionLines = new SDList<PartitionLine>();/// ����ָ�������
		private SDList<PartitionLine> _vPartionLines = new SDList<PartitionLine>();/// ����ָ�������
		#endregion

		#region ���Գ�Ա����

		public int X
		{
			get { return _x; }
			set { _x = value; }
		}

		public int Y
		{
			get { return _y; }
			set { _y = value; }
		}

		public int Width
		{
			get { return _width; }
			set { _width = value; }
		}

		public int Height
		{
			get { return _height; }
			set { _height = value; }
		}

		#endregion

		#region ���캯��

		public CilpPanelCommand(DesignPanel tdPanel,Rect rect,Image img)
		{
			_width = rect.Width;
			_height = rect.Height;
			_x = rect.X;
			_y = rect.Y;
			_rect = rect;
			TDPanel = tdPanel;
			_listLine = tdPanel.DrawPanel.ListLine;
			CommandInfo = "�и��";
			_backImg = img;
			
		}

		#endregion

		#region �ڲ�����
		private void ExtractLines()
		{
			_hPartionLines.Clear();
			_vPartionLines.Clear();
			//���˴�ֱ��
			foreach (PartitionLine line in TDPanel.DrawPanel.VPartionLines)
			{
				if (line.Position >= X + Width && line.Position <= X)
					continue;
				if (line.Start < Y + Height && line.End > Y)
				{ 
					if (line.End >= Y + Height)
						line.End = Height;
					else
						line.End -= Y;

					if (line.Start <= Y)
						line.Start = 0;
					else
						line.Start -= Y;
					_vPartionLines.Add(line);
				}
				else
					continue;
			}
			//
			//����ˮƽ��
			//
			foreach (PartitionLine line in TDPanel.DrawPanel.HPartionLines)
			{
				if (line.Position >= Y + Height && line.Position <= Y)
					continue;
				if (line.Start < X + Width && line.End > X)
				{
					if (line.End >= X + Width)
						line.End = Width;
					else
						line.End -= X;

					if (line.Start <= X)
						line.Start = 0;
					else
						line.Start -= X;
					_hPartionLines.Add(line);
				}
			}
			TDPanel.DrawPanel.VPartionLines = _vPartionLines;
			TDPanel.DrawPanel.HPartionLines = _hPartionLines;
			
		}

		void DrawPanel_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
		   
		}
		#endregion

		#region ����������Ա�ӿ�

		public override void Execute()
		{
			//ExtractLines();
			//Bitmap newimg = new Bitmap(Width, Height);
			//Graphics g = Graphics.FromImage(newimg);
			//g.ScaleTransform(TDPanel.DrawPanel.CurZoom, TDPanel.DrawPanel.CurZoom);
			//Rectangle rect1 = new Rectangle(0, 0, Width, Height);
			//Rectangle rect = new Rectangle(X, Y, Width, Height);

			//g.DrawImage(_backImg,rect1, rect,GraphicsUnit.Pixel);
			
			//g.Flush();
			//TDPanel.DrawPanel.BackImage = newimg;

			//TDPanel.DrawPanel.Width = _width;
			//TDPanel.DrawPanel.Height = _height;
		
		}

		public override void UnExecute()
		{
		}

		#endregion
	}

}