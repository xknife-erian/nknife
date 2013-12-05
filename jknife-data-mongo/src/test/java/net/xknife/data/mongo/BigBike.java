package net.xknife.data.mongo;

import java.util.UUID;

import net.xknife.data.mongo.MongoUtil;

import org.mongojack.Id;
import org.mongojack.ObjectId;

public class BigBike
{
	private String id;
	private String name;

	public BigBike()
	{
	}

	/**
	 * 快速生成一个bigBike实体
	 * 
	 * @param isYongJiu
	 *            是否是永久牌
	 */
	public static BigBike newBigBike(final boolean isYongJiu)
	{
		BigBike bb = new BigBike();
		bb.id = MongoUtil.mongoId();
		bb.name = UUID.randomUUID().toString().substring(0, 4) + (isYongJiu ? "yongjiu" : "bike");
		return bb;
	}

	/**
	 * 快速生成一个固定名称vyyvyy的bigBike实体
	 */
	public static BigBike newBigBike()
	{
		BigBike bb = new BigBike();
		bb.id = MongoUtil.mongoId();
		bb.name = "vyyvyy";
		return bb;
	}

	@Id
	@ObjectId
	public String getId()
	{
		return id;
	}

	@Id
	@ObjectId
	public void setId(final String id)
	{
		this.id = id;
	}

	public String getName()
	{
		return name;
	}

	public void setName(final String name)
	{
		this.name = name;
	}

	@Override
	public int hashCode()
	{
		final int prime = 31;
		int result = 1;
		result = (prime * result) + ((id == null) ? 0 : id.hashCode());
		result = (prime * result) + ((name == null) ? 0 : name.hashCode());
		return result;
	}


	@Override
	public boolean equals(final Object obj)
	{
		if (this == obj)
		{
			return true;
		}
		if (obj == null)
		{
			return false;
		}
		if (getClass() != obj.getClass())
		{
			return false;
		}
		BigBike other = (BigBike) obj;
		if (id == null)
		{
			if (other.id != null)
			{
				return false;
			}
		}
		else if (!id.equals(other.id))
		{
			return false;
		}
		if (name == null)
		{
			if (other.name != null)
			{
				return false;
			}
		}
		else if (!name.equals(other.name))
		{
			return false;
		}
		return true;
	}

	@Override
	public String toString()
	{
		return "BigBike [id=" + id + ", name=" + name + "]";
	}

}
