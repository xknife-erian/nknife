namespace NKnife.App.Sudoku.Common
{

	/// <summary>
	/// �������
	/// </summary>
	public enum SuDoStrategy
	{

        /// <summary>
        /// ��Ԫ��Ψһֵ
        /// </summary>
		UniqueValueForCell,

        /// <summary>
        /// ��Ψһֵ
        /// </summary>
		UniqueValueForRow,

        /// <summary>
        /// ��Ψһֵ
        /// </summary>
		UniqueValueForColumn,

        /// <summary>
        /// 3��3�ľ���Ψһֵ
        /// </summary>
		UniqueValueForSquare,

        /// <summary>
        /// ���ѡ��
        /// </summary>
		RandomPick

	}

}