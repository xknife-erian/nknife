package jknife.fakes;

import java.net.InetSocketAddress;

import com.mongodb.BasicDBObject;
import com.mongodb.DBCollection;
import com.mongodb.DBObject;
import com.mongodb.MongoClient;
import com.mongodb.ServerAddress;
import de.bwaldvogel.mongo.MongoServer;
import de.bwaldvogel.mongo.backend.memory.MemoryBackend;

public class MongoFake
{
	private MongoServer _Server;
	private MongoClient _Client;

	public MongoServer getServer()
	{
		return _Server;
	}

	public void stop()
	{
		_Client.close();
		_Server.shutdown();
	}

	protected DBCollection createCollection(final ServerAddress address)
	{
		_Client = new MongoClient(address);
		return _Client.getDB(getDbName()).getCollection(getCollectionName());
	}

	/**
	 * 使用桩级的MonogoDB进行测试
	 */
	protected ServerAddress createServerAddress()
	{
		_Server = new MongoServer(new MemoryBackend());
		InetSocketAddress address = _Server.bind();
		return new ServerAddress(address);
	}

	public DBObject translate(final BigBike obj)
	{
		DBObject dbObject = new BasicDBObject().append("id", obj.getId()).append("name", obj.getName());
		return dbObject;
	}

	public BigBike translate(final DBObject obj)
	{
		BigBike bike = new BigBike();
		bike.setId(obj.get("id").toString());
		bike.setName(obj.get("name").toString());
		return bike;
	}

	protected String getIpAddres()
	{
		return "1.0.0.1";
	}

	protected int getPort()
	{
		return -1234;
	}

	protected String getDbName()
	{
		return "testdb";
	}

	protected String getCollectionName()
	{
		return "testCollection";
	}

	protected String getSubCollectionName()
	{
		return "testSubCollection";
	}

}
