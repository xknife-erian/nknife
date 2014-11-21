package net.xknife.data.api;

import java.util.Iterator;
import java.util.List;

public interface IStore<TEntity, ID, Where, Update, Cursor extends Iterator<TEntity>>
{
	/**
	 * 添加一个实体到数据库
	 * 
	 * @param data
	 *            添加的实体
	 * @return 是否添加成功
	 */
	boolean add(TEntity data);

	/**
	 * 添加一组同类型实体到数据库
	 * 
	 * @param datas
	 *            同类型实体集合
	 * @return 成功添加到数据库的实体个数
	 */
	int add(List<TEntity> datas);

	/**
	 * 根据实体的唯一标识(一般为id)删除实体
	 * 
	 * @param id
	 *            实体的唯一标识
	 * @return 删除是否成功
	 */
	boolean delete(ID id);

	/**
	 * 删除所有符合条件的实体
	 * 
	 * @param where
	 *            删除条件
	 * @return 成功删除的实体个数
	 */
	int remove(Where where);

	/**
	 * 更新实体，依据实体唯一标识查找到该实体
	 * 
	 * @param id
	 *            需要更新的实体的唯一标识
	 * @param data
	 *            希望更新的数据
	 * @return 更新后该实体在数据库中的体现
	 */
	TEntity updateById(ID id, TEntity data);

	/**
	 * 查找到符合条件的所有实体进行预期数据更新
	 * 
	 * @param where
	 * @param data
	 * @return
	 */
	List<ID> update(Where where, TEntity data);

    int updateSet(Where where, Update builder);

	/**
	 * 根据唯一标识查找实体
	 * 
	 * @param id
	 *            实体的唯一标识
	 * @return
	 */
	TEntity find(ID id);

	/**
	 * 根据条件实体查询，返回数据库中符合条件的第一条记录，获取符合条件的所有记录请使用 @see {@link #query(Object)}
	 * 
	 * @param where
	 * @return
	 */
	TEntity findOne(Where where);

	/**
	 * 查询所有实体，此方法设计安全因素，不考虑对外公开
	 * 
	 * @return
	 */
	Cursor query();

	/**
	 * 查询符合条件的所有实体
	 * 
	 * @param where
	 *            期望得到的实体满足的条件
	 * @return
	 */
	Cursor query(Where where);

	/**
	 * 查询符合条件的实体数量
	 * 
	 * @param where
	 *            条件
	 * @return
	 */
	long count(Where where);

	/**
	 * 查询所有的实体数量
	 * 
	 * @return
	 */
	long count();
}
