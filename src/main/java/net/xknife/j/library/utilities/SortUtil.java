package net.xknife.j.library.utilities;

import java.util.Random;

/**
 * 排序测试类
 * 
 * 排序算法的分类如下： <BR>
 * 1.插入排序（直接插入排序、折半插入排序、希尔排序）；<BR>
 * 2.交换排序（冒泡泡排序、快速排序）； <BR>
 * 3.选择排序（直接选择排序、堆排序）； <BR>
 * 4.归并排序； <BR>
 * 5.基数排序。
 * 
 * 关于排序方法的选择： <BR>
 * (1)若n较小(如n≤50)，可采用直接插入或直接选择排序。 当记录规模较小时，直接插入排序较好；否则因为直接选择移动的记录数少于直接插人，应选直接选择排序为宜。 <BR>
 * (2)若文件初始状态基本有序(指正序)，则应选用直接插人、冒泡或随机的快速排序为宜； <BR>
 * (3)若n较大，则应采用时间复杂度为O(nlgn)的排序方法：快速排序、堆排序或归并排序。
 * 
 * @description JAVA排序汇总
 */
public class SortUtil
{
	/**
	 * 生成随机数
	 */
	public int[] createArray()
	{
		Random random = new Random();
		int[] array = new int[10];
		for (int i = 0; i < 10; i++)
		{
			array[i] = random.nextInt(100) - random.nextInt(100);// 生成两个随机数相减，保证生成的数中有负数
		}
		return array;
	}

	/**
	 * @description 交换相邻两个数
	 * @date Nov 19, 2009
	 * @author HDS
	 * @param data
	 * @param x
	 * @param y
	 */
	public void swap(final int[] data, final int x, final int y)
	{
		int temp = data[x];
		data[x] = data[y];
		data[y] = temp;
	}

	/**
	 * 冒泡排序<BR>
	 * 交换排序的一种<BR>
	 * 方法：相邻两元素进行比较，如有需要则进行交换，每完成一次循环就将最大元素排在最后（如从小到大排序），下一次循环是将其他的数进行类似操作。 <BR>
	 * 性能：比较次数O(n^2),n^2/2；交换次数O(n^2),n^2/4
	 * 
	 * @param data
	 *            要排序的数组
	 * @param sortType
	 *            排序类型
	 * @return
	 */
	public int[] bubbleSort(final int[] data, final String sortType)
	{
		if (sortType.equals("asc"))
		{ // 正排序，从小排到大
			// 比较的轮数
			for (int i = 1; i < data.length; i++)
			{
				// 将相邻两个数进行比较，较大的数往后冒泡
				for (int j = 0; j < (data.length - i); j++)
				{
					if (data[j] > data[j + 1])
					{
						// 交换相邻两个数
						swap(data, j, j + 1);
					}
				}
			}
		}
		else if (sortType.equals("desc"))
		{ // 倒排序，从大排到小
			// 比较的轮数
			for (int i = 1; i < data.length; i++)
			{
				// 将相邻两个数进行比较，较大的数往后冒泡
				for (int j = 0; j < (data.length - i); j++)
				{
					if (data[j] < data[j + 1])
					{
						// 交换相邻两个数
						swap(data, j, j + 1);
					}
				}
			}
		}
		else
		{
			System.out.println("您输入的排序类型错误！");
		}
		return data;
	}

	/**
	 * 直接选择排序法<BR>
	 * 选择排序的一种 <BR>
	 * 方法：每一趟从待排序的数据元素中选出最小（或最大）的一个元素，顺序放在已排好序的数列的最后，直到全部待排序的数据元素排完。 <BR>
	 * 性能：比较次数O(n^2),n^2/2 交换次数O(n),n 交换次数比冒泡排序少多了，由于交换所需CPU时间比比较所需的CUP时间多，<BR>
	 * 所以选择排序比冒泡排序快。 <BR>
	 * 但是N比较大时，比较所需的CPU时间占主要地位，所以这时的性能和冒泡排序差不太多，但毫无疑问肯定要快些。
	 * 
	 * @param data
	 *            要排序的数组
	 * @param sortType
	 *            排序类型
	 * @return
	 */
	public int[] selectSort(final int[] data, final String sortType)
	{
		if (sortType.endsWith("asc"))
		{// 正排序，从小排到大
			int index;
			for (int i = 1; i < data.length; i++)
			{
				index = 0;
				for (int j = 1; j <= (data.length - i); j++)
				{
					if (data[j] > data[index])
					{
						index = j;
					}
				}
				// 交换在位置data.length-i和index(最大值)两个数
				swap(data, data.length - i, index);
			}
		}
		else if (sortType.equals("desc"))
		{ // 倒排序，从大排到小
			int index;
			for (int i = 1; i < data.length; i++)
			{
				index = 0;
				for (int j = 1; j <= (data.length - i); j++)
				{
					if (data[j] < data[index])
					{
						index = j;
					}
				}
				// 交换在位置data.length-i和index(最大值)两个数
				swap(data, data.length - i, index);
			}
		}
		else
		{
			System.out.println("您输入的排序类型错误！");
		}
		return data;
	}

	/**
	 * 插入排序 <BR>
	 * 方法：将一个记录插入到已排好序的有序表（有可能是空表）中,从而得到一个新的记录数增1的有序表。 <BR>
	 * 性能：比较次数O(n^2),n^2/4 复制次数O(n),n^2/4 比较次数是前两者的一般，而复制所需的CPU时间较交换少，<BR>
	 * 所以性能上比冒泡排序提高一倍多，而比选择排序也要快。
	 * 
	 * @param data
	 *            要排序的数组
	 * @param sortType
	 *            排序类型
	 */
	public int[] insertSort(final int[] data, final String sortType)
	{
		if (sortType.equals("asc"))
		{ // 正排序，从小排到大
			// 比较的轮数
			for (int i = 1; i < data.length; i++)
			{
				// 保证前i+1个数排好序
				for (int j = 0; j < i; j++)
				{
					if (data[j] > data[i])
					{
						// 交换在位置j和i两个数
						swap(data, i, j);
					}
				}
			}
		}
		else if (sortType.equals("desc"))
		{ // 倒排序，从大排到小
			// 比较的轮数
			for (int i = 1; i < data.length; i++)
			{
				// 保证前i+1个数排好序
				for (int j = 0; j < i; j++)
				{
					if (data[j] < data[i])
					{
						// 交换在位置j和i两个数
						swap(data, i, j);
					}
				}
			}
		}
		else
		{
			System.out.println("您输入的排序类型错误！");
		}
		return data;
	}

	/**
	 * 反转数组的方法
	 * 
	 * @param data
	 *            源数组
	 */
	public int[] reverse(final int[] data)
	{
		int length = data.length;
		int temp = 0;// 临时变量
		for (int i = 0; i < (length / 2); i++)
		{
			temp = data[i];
			data[i] = data[length - 1 - i];
			data[length - 1 - i] = temp;
		}
		return data;
	}

	/**
	 * 快速排序 <BR>
	 * 快速排序使用分治法（Divide and conquer）策略来把一个序列（list）分为两个子序列（sub-lists）。 <BR>
	 * 步骤为： <BR>
	 * 1. 从数列中挑出一个元素，称为 "基准"（pivot）， <BR>
	 * 2. 重新排序数列，所有元素比基准值小的摆放在基准前面， 所有元素比基准值大的摆在基准的后面（相同的数可以到任一边）。 在这个分割之后，该基准是它的最后位置。这个称为分割（partition）操作。 <BR>
	 * 3. 递归地（recursive）把小于基准值元素的子数列和大于基准值元素的子数列排序。 <BR>
	 * 递回的最底部情形，是数列的大小是零或一，也就是永远都已经被排序好了。<BR>
	 * 虽然一直递回下去，但是这个算法总会结束，因为在每次的迭代（iteration）中，它至少会把一个元素摆到它最后的位置去。
	 * 
	 * @param data
	 *            待排序的数组
	 * @param low
	 * @param high
	 * @see SortTest#qsort(int[], int, int)
	 * @see SortTest#qsort_desc(int[], int, int)
	 */
	public void quickSort(final int[] data, final String sortType)
	{
		if (sortType.equals("asc"))
		{
			qsort_asc(data, 0, data.length - 1);
		}
		else if (sortType.equals("desc"))
		{
			qsort_desc(data, 0, data.length - 1);
		}
		else
		{
			System.out.println("您输入的排序类型错误！");
		}
	}

	/**
	 * 快速排序的具体实现，排正序
	 * 
	 * @param data
	 * @param low
	 * @param high
	 */
	private void qsort_asc(final int data[], final int low, final int high)
	{
		int i, j, x;
		if (low < high)
		{ // 这个条件用来结束递归
			i = low;
			j = high;
			x = data[i];
			while (i < j)
			{
				while ((i < j) && (data[j] > x))
				{
					j--; // 从右向左找第一个小于x的数
				}
				if (i < j)
				{
					data[i] = data[j];
					i++;
				}
				while ((i < j) && (data[i] < x))
				{
					i++; // 从左向右找第一个大于x的数
				}
				if (i < j)
				{
					data[j] = data[i];
					j--;
				}
			}
			data[i] = x;
			qsort_asc(data, low, i - 1);
			qsort_asc(data, i + 1, high);
		}
	}

	/**
	 * 快速排序的具体实现，排倒序
	 * 
	 * @param data
	 * @param low
	 * @param high
	 */
	private void qsort_desc(final int data[], final int low, final int high)
	{
		int i, j, x;
		if (low < high)
		{ // 这个条件用来结束递归
			i = low;
			j = high;
			x = data[i];
			while (i < j)
			{
				while ((i < j) && (data[j] < x))
				{
					j--; // 从右向左找第一个小于x的数
				}
				if (i < j)
				{
					data[i] = data[j];
					i++;
				}
				while ((i < j) && (data[i] > x))
				{
					i++; // 从左向右找第一个大于x的数
				}
				if (i < j)
				{
					data[j] = data[i];
					j--;
				}
			}
			data[i] = x;
			qsort_desc(data, low, i - 1);
			qsort_desc(data, i + 1, high);
		}
	}

	/**
	 * 二分查找特定整数在整型数组中的位置(递归) 查找线性表必须是有序列表
	 * 
	 * @paramdataset
	 * @paramdata
	 * @parambeginIndex
	 * @paramendIndex
	 * @returnindex
	 */
	public int binarySearch(final int[] dataset, final int data, final int beginIndex, final int endIndex)
	{
		int midIndex = (beginIndex + endIndex) >>> 1; // 相当于mid = (low + high)
		// / 2，但是效率会高些
		if ((data < dataset[beginIndex]) || (data > dataset[endIndex]) || (beginIndex > endIndex))
		{
			return -1;
		}
		if (data < dataset[midIndex])
		{
			return this.binarySearch(dataset, data, beginIndex, midIndex - 1);
		}
		else if (data > dataset[midIndex])
		{
			return this.binarySearch(dataset, data, midIndex + 1, endIndex);
		}
		else
		{
			return midIndex;
		}
	}

	/**
	 * 二分查找特定整数在整型数组中的位置(非递归) 查找线性表必须是有序列表
	 * 
	 * @paramdataset
	 * @paramdata
	 * @returnindex
	 */
	public int binarySearch(final int[] dataset, final int data)
	{
		int beginIndex = 0;
		int endIndex = dataset.length - 1;
		int midIndex = -1;
		if ((data < dataset[beginIndex]) || (data > dataset[endIndex]) || (beginIndex > endIndex))
		{
			return -1;
		}
		while (beginIndex <= endIndex)
		{
			midIndex = (beginIndex + endIndex) >>> 1; // 相当于midIndex =
			// (beginIndex +
			// endIndex) / 2，但是效率会高些
			if (data < dataset[midIndex])
			{
				endIndex = midIndex - 1;
			}
			else if (data > dataset[midIndex])
			{
				beginIndex = midIndex + 1;
			}
			else
			{
				return midIndex;
			}
		}
		return -1;
	}

	public static void main(final String[] args)
	{
		SortUtil ST = new SortUtil();
		int[] array = ST.createArray();
		System.out.println("==========冒泡排序后(正序)==========");
		ST.bubbleSort(array, "asc");
		System.out.println("==========冒泡排序后(倒序)==========");
		ST.bubbleSort(array, "desc");

		array = ST.createArray();
		System.out.println("==========选择排序后(正序)==========");
		ST.selectSort(array, "asc");
		System.out.println("==========选择排序后(倒序)==========");
		ST.selectSort(array, "desc");

		array = ST.createArray();
		System.out.println("==========插入排序后(正序)==========");
		ST.insertSort(array, "asc");
		System.out.println("==========插入排序后(倒序)==========");
		ST.insertSort(array, "desc");

		array = ST.createArray();
		System.out.println("==========快速排序后(正序)==========");
		ST.quickSort(array, "asc");
		System.out.println("==========快速排序后(倒序)==========");
		ST.quickSort(array, "desc");
		System.out.println("==========数组二分查找==========");
		System.out.println("您要找的数在第" + ST.binarySearch(array, 74) + "个位子。（下标从0计算）");

	}

}
