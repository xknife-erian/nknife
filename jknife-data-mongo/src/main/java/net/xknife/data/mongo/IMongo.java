package net.xknife.data.mongo;

import com.google.common.base.Strings;
import com.mongodb.DBCollection;
import com.mongodb.MongoClient;
import com.mongodb.MongoException;
import com.mongodb.ServerAddress;
import net.xknife.data.api.IStore;
import org.mongojack.*;
import org.mongojack.DBQuery.Query;

import java.net.UnknownHostException;
import java.util.List;

public interface IMongo<T> extends IStore<T, String, DBQuery.Query, DBUpdate.Builder, DBCursor<T>>
{
	public static abstract class MongoBase<T> implements IMongo<T>
	{
		static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(IMongo.MongoBase.class);

		protected String getIpAddres()
		{
			return "127.0.0.1";
		}

		protected int getPort()
		{
			return 27017;
		}

		protected abstract String getDbName();

		protected abstract String getCollectionName();

		protected abstract String getSubCollectionName();

		protected abstract Class<T> getEntityClass();

		protected abstract void ensureIndex();

		protected DBCollection getCollection(final ServerAddress address)
		{
			MongoClient client = new MongoClient(address);
			String collName = String.format("%s.%s", getCollectionName(), getSubCollectionName());
			return client.getDB(getDbName()).getCollection(collName);
		}

		protected ServerAddress getServer()
		{
			try
			{
				return new ServerAddress(getIpAddres(), getPort());
			}
			catch (UnknownHostException e)
			{
				return null;
			}
		}

		protected JacksonDBCollection<T, String> collection()
		{
			return JacksonDBCollection.wrap(getCollection(getServer()), getEntityClass(), String.class);
		}

		@Override
		public boolean add(final T data)
		{
			if (data == null)
			{
				_Logger.warn("添加实体失败，实体为null.");
				return false;
			}
			WriteResult<T, String> result;
			try
			{
				result = collection().insert(data);
				return !Strings.isNullOrEmpty(result.getSavedId());
			}
			catch (MongoException e)
			{
				e.printStackTrace();
				_Logger.warn("添加实体失败，有可能是重复记录或者实体Id处理问题：" + data.toString(), e);
				return false;
			}
		}

		@Override
		public int add(final List<T> datas)
		{
			WriteResult<T, String> result = collection().insert(datas);
			return result.getSavedIds().size();
		}

		@Override
		public boolean delete(final String id)
		{
			if (Strings.isNullOrEmpty(id))
			{
				return false;
			}
			WriteResult<T, String> result = collection().removeById(id);
			return result.getN() > 0;
		}

		@Override
		public int remove(final Query where)
		{
			WriteResult<T, String> result = collection().remove(where);
			return result.getN();
		}

		@Override
		public T updateById(final String id, final T data)
		{
			T findedT = find(id);

			boolean isDeleted = delete(id);
			if (!isDeleted)
			{
				_Logger.warn("实体不存在，更新失败。");
				return null;
			}
			boolean isAdded = add(data);
			if (isAdded)
			{
				T updatedT = find(id);
				return updatedT;
			}
			add(findedT);
			_Logger.warn("实体更新失败，可能是因为更新后数据记录不唯一。");
			return null;

			// WriteResult<T, String> result = collection().updateById(id, data);
			// System.out.println(result.getN());
			// return find(result.getSavedId());
		}

		@Override
		public List<String> update(final Query where, final T data)
		{
			WriteResult<T, String> result = collection().update(where, data, false, false);
			return result.getSavedIds();
		}

        @Override
        public int updateSet(final Query where, final DBUpdate.Builder builder)
        {
            WriteResult<T, String> result = collection().update(where, builder, false, true);
            return result.getN();
        }

		@Override
		public T find(final String id)
		{
			if (Strings.isNullOrEmpty(id))
			{
				_Logger.debug("查找失败，id不能为Null 或者 Empty。");
				return null;
			}

			try
			{
				return collection().findOneById(id);
			}
			catch (Exception e)
			{
				_Logger.debug("查找失败，有可能是id不合法原因" + e);
				return null;
			}

		}

		@Override
		public T findOne(final Query where)
		{
			if (where == null)
			{
				_Logger.debug("查找失败，查找条件不能为Null。");
				return null;
			}
			try
			{
				return collection().findOne(where);
			}
			catch (Exception e)
			{
				return null;
			}
		}

		@Override
		public DBCursor<T> query()
		{
			DBCursor<T> result = collection().find();
			return result;
		}

		@Override
		public DBCursor<T> query(final Query where)
		{
			DBCursor<T> result = collection().find(where);
			return result;
		}

		@Override
		public long count(final Query where)
		{
			return collection().find(where).count();
		}

		@Override
		public long count()
		{
			return collection().count();
		}

	}
}
