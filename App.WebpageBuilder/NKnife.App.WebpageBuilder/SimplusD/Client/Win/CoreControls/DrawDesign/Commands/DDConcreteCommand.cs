
/*
 * DrawDesigner的具体命令的实现
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Jeelu.SimplusD.Client.Win
{
	/// <summary>
	/// 插入直线命令
	/// </summary>
	public class AddLineCommand : BaseCommand
	{
		#region 属性成员定义

		/// <summary>
		/// 获取或设置直线的起点
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		/// 获取或设置直线的终点
		/// </summary>
		public int End{ get; set; }

		/// <summary>
		/// 获取或设置直线的位置
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// 获取或设置直线的方向
		/// </summary>
		public bool IsRow { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public List<PartitionLine> AddedLines { get; set; }

		/// <summary>
		/// 获取或设置是否为撤销中的添加直线,如果不是,则说明为第一次新建并增加直线      
		/// </summary>
		public bool IsRedo { get; set; }

		public List<Rect> RemovedRects { get; set; }

		public List<Rect> AddedRects { get; set; }

		#endregion

		#region 内部方法

		/// <summary>
		/// 初始化属性的初始值
		/// </summary>
		private void Init()
		{
			AddedLines = new List<PartitionLine>();
			RemovedRects = new List<Rect>();
			AddedRects = new List<Rect>();
		}

		#endregion

		#region 构造函数

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
			CommandInfo = "插入直线";
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
			CommandInfo = "插入直线";
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
			CommandInfo = "插入直线";
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数
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

		#region 消息处理

		#endregion
	}

	/// <summary>
	/// 删除直线命令
	/// </summary>
	public class DeleteLineCommand : BaseCommand
	{
		#region 属性成员定义

		/// <summary>
		/// 获取或设置直线的起点
		/// </summary>
		public int Start { get; set; }

		/// <summary>
		/// 获取或设置直线的终点
		/// </summary>
		public int End { get; set; }

		/// <summary>
		/// 获取或设置直线的位置
		/// </summary>
		public int Position { get; set; }

		/// <summary>
		/// 获取或设置直线的方向
		/// </summary>
		public bool IsRow { get; set; }

		/// <summary>
		/// 获取或设置是否为撤销中的添加直线,如果不是,则说明为第一次新建并增加直线      
		/// </summary>
		public bool IsRedo { get; set; }

		/// <summary>
		/// 被删除之矩形
		/// </summary>
		public List<Rect> RemovedRects { get; set; }

		/// <summary>
		/// 增加之矩形。在此处是被选择保留的矩形
		/// </summary>
		public List<Rect> AddedRects { get; set; }

		#endregion

		#region 构造函数

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
			CommandInfo = "删除直线";
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数
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

		#region 消息处理

		#endregion
	}

	/// <summary>
	/// 锁定矩形命令
	/// </summary>
	public class LockRectCommand : BaseCommand
	{
		#region 属性成员定义

		public List<Rect> LockedRects { get; set; }

		public List<PartitionLine> LockedLines { get; set; }

		#endregion

		#region 构造函数

		public LockRectCommand(DesignPanel tdPanel, List<Rect> lockedRects)
		{
			TDPanel = tdPanel;
			LockedRects = lockedRects;
			LockedLines = new List<PartitionLine>();
			CommandInfo = "锁定矩形";
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数
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
	/// 解开锁定矩形命令
	/// </summary>
	public class DisLockRectCommand : BaseCommand
	{
		#region 属性成员定义

		public List<Rect> UnLockedRects { get; set; }

		public List<PartitionLine> UnLockedLines { get; set; }

		#endregion

		#region 构造函数

		public DisLockRectCommand(DesignPanel tdPanel, List<Rect> unLockedRect)
		{
			TDPanel = tdPanel;
			UnLockedRects = unLockedRect;
			UnLockedLines = new List<PartitionLine>();
			CommandInfo = "解开锁定矩形";
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数
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
	/// 选择命令
	/// </summary>
	public class SelectCommand : BaseCommand
	{
		#region 字段成员定义

		private List<PartitionLine> _selectedLines = new List<PartitionLine>();
		private List<PartitionLine> _disSelectedLines = new List<PartitionLine>();
		private List<Rect> _selectedRects = new List<Rect>();// 被选择的矩形    
		private List<Rect> _disSelectedRects = new List<Rect>();

		#endregion

		#region 属性成员定义

		/// <summary>
		/// 被选择的直线
		/// </summary>
		public List<PartitionLine> SelectedLines
		{
			get { return _selectedLines; }
			set { _selectedLines = value; }
		}

		/// <summary>
		/// 因选择直线而取消选择的直线
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
		/// 被选择的矩形
		/// </summary>
		public List<Rect> SelectedRects
		{
			get { return _selectedRects; }
			set { _selectedRects = value; }
		}

		#endregion

		#region 构造函数

		/// <summary>
		/// 构造函数
		/// </summary>
		/// <param name="tdPanel"></param>
		/// <param name="selectedRects">将要置为选择状态之矩形</param>
		/// <param name="disSelectedRects">将取消选择状态之矩形</param>
		/// <param name="selectedLines">将被选择的直线</param>
		/// <param name="disSelectedLines">将被取消选择的直线</param>
		public SelectCommand(DesignPanel tdPanel, List<Rect> selectedRects, List<Rect> disSelectedRects, List<PartitionLine> selectedLines, List<PartitionLine> disSelectedLines)
		{
			TDPanel = tdPanel;

			SelectedRects.AddRange(selectedRects);
			DisSelectedRects.AddRange(disSelectedRects);
			SelectedLines.AddRange(selectedLines);
			DisSelectedLines.AddRange(disSelectedLines);

			CommandInfo = "更改选择对象";
		}

		#endregion

		#region 公共函数成员接口

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
	/// 合并矩形
	/// </summary>
	public class MergeRectCommand : BaseCommand
	{
		#region 字段成员定义

		private List<Rect> _removedRects = new List<Rect>();
		private List<Rect> _addedRects = new List<Rect>();
		private Rectangle _leftRect = new Rectangle();///留下的矩形
		private Rectangle _boundaryRect = new Rectangle();
		private List<PartitionLine> _inRectLines = new List<PartitionLine>();
		private bool _isRedo = false;///是否为撤销中的添加直线,如果不是,则说明为第一次新建并增加直线        

		#endregion

		#region 属性成员定义
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
		/// 被选择留下之矩形大小
		/// </summary>
		public Rectangle LeftRect
		{
			get { return _leftRect; }
			set { _leftRect = value; }
		}

		/// <summary>
		/// 边界矩形
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

		#region 构造函数

		public MergeRectCommand(DesignPanel tdPanel)
		{
			TDPanel = tdPanel;

			List<Rect> selectedRects = tdPanel.DrawPanel.ListRect.GetSelectedRects();
			Rectangle boundaryRect = CommonFuns.FindRectsBorder(selectedRects);

			///保存边界矩形
			BoundaryRect = new Rectangle(
				boundaryRect.X,
				boundaryRect.Y,
				boundaryRect.Width,
				boundaryRect.Height
				);

			CommandInfo = "合并矩形";
			
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数
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
	/// 分割矩形
	/// </summary>
	public class PartRectCommand : BaseCommand
	{
		#region 字段成员定义

		private List<Rect> _removedRects = new List<Rect>();
		private List<Rect> _addedRects = new List<Rect>();
		private List<PartitionLine> _addedLines = new List<PartitionLine>();
		private bool _isRow = true;

		#endregion

		#region 属性成员定义

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

		#region 构造函数

		public PartRectCommand(DesignPanel tdPanel, bool isRow, List<Rect> addedRects, Rect removedRect, List<PartitionLine> newLines)
		{
			IsRow = isRow;
			AddedRects = addedRects;
			RemovedRects.Add(removedRect);
			AddedLines = newLines;
			TDPanel = tdPanel;
			TDPanel.Modified = true;
			CommandInfo = "合并矩形";
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数,在执行命令前已经把_removedRects和_addedRects、_inRectLines初始化。
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
	/// 移动直线
	/// </summary>
	public class MoveLineCommand : BaseCommand
	{
		#region 字段成员定义

		PartitionLine _movedLine;//被移动之直线
		int _movedToPos;//移动到的位置
		List<PartitionLine> _newLines = new List<PartitionLine> ();//移动后之新直线
		PartitionLine _firstBorderLine;//左或上的界线
		PartitionLine _secondBorderLine;//右或下的界线
	   // List<PartitionLine> _partedLines = new List<PartitionLine>();//被将被移动的直线分割的直线       
		List<PartitionLine> _endPointLines = new List<PartitionLine>();//端点在被移动的直线上的直线      
		List<PartitionLine> _startPointLines = new List<PartitionLine>();//端点在被移动的直线上的直线      

		List<Rect> _modefiedRects;//被修改之矩形       
		List<Rect> _oldRects = new List<Rect>();//保存移动前的大小尺寸,故不需工厂模式     
		List<Rect> _newRects = new List<Rect> ();//保存移动前的大小尺寸,不需工厂模式            

		#endregion

		#region 属性成员定义

		/// <summary>
		/// 被移动之直线
		/// </summary>
		public PartitionLine MovedLine
		{
			get { return _movedLine; }
			set { _movedLine = value; }
		}

		/// <summary>
		/// 移动到的位置
		/// </summary>
		public int MovedToPos
		{
			get { return _movedToPos; }
			set { _movedToPos = value; }
		}

		/// <summary>
		/// 移动后之新直线
		/// </summary>
		public List<PartitionLine> NewLines
		{
			get { return _newLines; }
			set { _newLines = value; }
		}

		/// <summary>
		/// 左或上的界线
		/// </summary>
		public PartitionLine FirstBorderLine
		{
			get { return _firstBorderLine; }
			set { _firstBorderLine = value; }
		}

		/// <summary>
		/// 右或下的界线
		/// </summary>
		public PartitionLine SecondBorderLine
		{
			get { return _secondBorderLine; }
			set { _secondBorderLine = value; }
		}

		///// <summary>
		///// 被将被移动的直线分割的直线
		///// </summary>
		//public List<PartitionLine> PartedLines
		//{
		//    get { return _partedLines; }
		//    set { _partedLines = value; }
		//}

		/// <summary>
		/// 端点在被移动的直线上的直线
		/// </summary>
		public List<PartitionLine> EndPointLines
		{
			get { return _endPointLines; }
			set { _endPointLines = value; }
		}

		/// <summary>
		/// 端点在被移动的直线上的直线      
		/// </summary>
		public List<PartitionLine> StartPointLines
		{
			get { return _startPointLines; }
			set { _startPointLines = value; }
		}

		/// <summary>
		/// 被修改之矩形
		/// </summary>
		public List<Rect> ModefiedRects
		{
			get { return _modefiedRects; }
			set { _modefiedRects = value; }
		}

		/// <summary>
		/// 保存移动前的大小尺寸,故不需工厂模式
		/// </summary>
		public List<Rect> OldRects
		{
			get { return _oldRects; }
			set { _oldRects = value; }
		}

		/// <summary>
		/// 保存移动前的大小尺寸,不需工厂模式
		/// </summary>
		public List<Rect> NewRects
		{
			get { return _newRects; }
			set { _newRects = value; }
		}

		#endregion

		#region 构造函数

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

			///初始化Rects
			InitRects();
			///初始化直线数据:NewLines,EndPointLines,StartPointLines
			InitLines();

			TDPanel.Modified = true;
			CommandInfo = "移动直线";
		}

		/// <summary>
		/// 初始化直线数据:NewLines,EndPointLines,StartPointLines
		/// </summary>
		private void InitLines()
		{
			///是否和已有直线重合
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
			else///没有重合
			{
				resultLines.Add(line);
			}
			NewLines = resultLines;

			///初始化_endPointLines,_startPointLines
			FindLineByLine findByMovedLine = new FindLineByLine(MovedLine);
			EndPointLines = allLines.FindAll(findByMovedLine.PredicateEndTo);
			StartPointLines = allLines.FindAll(findByMovedLine.PredicateStartFrom);
		}

		/// <summary>
		/// 初始化Rects:ModefiedRects,OldRects,NewRects
		/// </summary>
		private void InitRects()
		{
			///找到modifiedRects
			FindRectByLine findRect = new FindRectByLine(MovedLine);
			///找到所有以line做边界线的矩形
			ModefiedRects = TDPanel.DrawPanel.ListRect.SnipRectList.FindAll(findRect.BorderLinePredicate);

			///初始化oldRects
			foreach (Rect rect in ModefiedRects)
			{
				OldRects.Add(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
				NewRects.Add(new Rect(rect.X, rect.Y, rect.Width, rect.Height));
			}

			///修改newRects数据
			if (MovedLine.IsRow)
			{
				foreach (Rect rect in NewRects)
				{
					if (rect.Y < MovedLine.Position)///rect在moveLine的上边
					{
						rect.Height += (MovedToPos - MovedLine.Position);
					}
					else///rect在moveLine的下边
					{
						rect.Height += (MovedLine.Position - MovedToPos);
						rect.Y += (MovedToPos - MovedLine.Position);
					}
				}
			}
			else///待移动的直线是纵向直线
			{
				foreach (Rect rect in NewRects)
				{
					if (rect.X < MovedLine.Position)///rect在moveLine的左边
					{
						rect.Width += (MovedToPos - MovedLine.Position);
					}
					else///rect在moveLine的右边
					{
						rect.Width += (MovedLine.Position - MovedToPos);
						rect.X += (MovedToPos - MovedLine.Position);
					}
				}
			}
		}

		#endregion

		#region 公共函数成员接口

		/// <summary>
		/// 实现执行函数,在执行命令前已经把_removedRects和_addedRects、_inRectLines初始化。
		/// </summary>
		override public void Execute()
		{
			///处理直线
			HandleLine();
			///处理矩形
			HandleRect();

			//刷新区域
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
			///当移动后和左或上边界重合
			if (MovedToPos==FirstBorderLine.Position)
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				///添加没有重叠的新线段
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine(line.Start, line.End, line.Position, line.IsRow);
				}
				
				///修改起于被移动之直线
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

				///修改终点位于被移动之直线
				foreach (PartitionLine line in EndPointLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine
						(MovedToPos,
						line.End,
						line.Position,
						line.IsRow);
				}
			}
			else if (MovedToPos == SecondBorderLine.Position)///当移动后和右或下边界重合
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				///添加没有重叠的新线段
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine(line.Start, line.End, line.Position, line.IsRow);
				}
				///修改终点位于被移动之直线
				foreach (PartitionLine line in EndPointLines)
				{
					line.End = MovedToPos;//修改终点坐标，同时修改最后一条子线段
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

				///修改起于被移动之直线
				foreach (PartitionLine line in StartPointLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine
						(line.Start,
						MovedToPos,
						line.Position,
						line.IsRow);
				}
			}
			else///没有和边界线重合
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedToPos, MovedLine.IsRow);
				if (MovedToPos>MovedLine.Position)///移向右边或下边
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
				else///上移或左移
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
		/// 处理矩形的变化
		/// </summary>
		private void HandleRect()
		{
			for (int i = 0; i < ModefiedRects.Count; i++)
			{
				///如果和边界线重合:置相关矩形为被删除
				if (NewRects[i].Width<=0 || NewRects[i].Height<= 0)
				{
					ModefiedRects[i].IsDeleted = true;
				}
				else///否则没有和边界线重合,则修改矩形大小
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
			///处理直线
			HandleUnLine();
			///处理矩形
			HandleUnRect();

			//刷新区域
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
				///删除没有重叠的新线段
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine(line.Start, line.End, line.Position, line.IsRow);
				}
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);


				///修改起于被移动之直线
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

				///修改终点位于被移动之直线
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
				///删除没有重叠的新线段
				foreach (PartitionLine line in NewLines)
				{
					TDPanel.DrawPanel.ListLine.DeleteLine(line.Start, line.End, line.Position, line.IsRow);
				}
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);

				///修改终点位于被移动之直线
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

				///修改起于被移动之直线
				foreach (PartitionLine line in StartPointLines)
				{
					TDPanel.DrawPanel.ListLine.AddLine
						(MovedLine.Position,
						MovedToPos,
						line.Position,
						line.IsRow);
				}           
			}
			else///没有和边界线重合
			{
				TDPanel.DrawPanel.ListLine.DeleteLine(MovedLine.Start, MovedLine.End, MovedToPos, MovedLine.IsRow);
				TDPanel.DrawPanel.ListLine.AddLine(MovedLine.Start, MovedLine.End, MovedLine.Position, MovedLine.IsRow);
				if (MovedToPos > MovedLine.Position)///移向右边或下边
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
				else///上移或左移
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
				///如果和边界线重合:置相关矩形为被删除
				if (ModefiedRects[i].IsDeleted)
				{
					ModefiedRects[i].IsDeleted = false;
				}
				else///否则没有和边界线重合,则修改矩形大小
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
	/// 切割画板
	/// </summary>
	public class CilpPanelCommand : BaseCommand
	{
		#region 字段成员定义

		int _x;
		int _y;
		int _width;
		int _height;
		Rect _rect;
		LineList _listLine;
		Image _backImg;

		private SDList<PartitionLine> _hPartionLines = new SDList<PartitionLine>();/// 横向分割线容器
		private SDList<PartitionLine> _vPartionLines = new SDList<PartitionLine>();/// 纵向分割线容器
		#endregion

		#region 属性成员定义

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

		#region 构造函数

		public CilpPanelCommand(DesignPanel tdPanel,Rect rect,Image img)
		{
			_width = rect.Width;
			_height = rect.Height;
			_x = rect.X;
			_y = rect.Y;
			_rect = rect;
			TDPanel = tdPanel;
			_listLine = tdPanel.DrawPanel.ListLine;
			CommandInfo = "切割画板";
			_backImg = img;
			
		}

		#endregion

		#region 内部方法
		private void ExtractLines()
		{
			_hPartionLines.Clear();
			_vPartionLines.Clear();
			//过滤垂直线
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
			//过滤水平线
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

		#region 公共函数成员接口

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