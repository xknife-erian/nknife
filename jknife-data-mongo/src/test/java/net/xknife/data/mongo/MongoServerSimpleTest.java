package net.xknife.data.mongo;

import com.mongodb.*;
import de.bwaldvogel.mongo.MongoServer;
import de.bwaldvogel.mongo.backend.memory.MemoryBackend;
import org.junit.After;
import org.junit.Before;
import org.junit.Test;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.net.InetSocketAddress;

import static org.junit.Assert.assertEquals;

/**
 * mongo-java-server官方提供的一个简单测试，可以读懂基本的思想，与jknife框架无关。
 *
 * Created by yangjuntao@xknife.net on 2014-08-13.
 */
public class MongoServerSimpleTest
{
    private static final Logger _Logger = LoggerFactory.getLogger(MongoServerSimpleTest.class);

    private DBCollection collection;
    private MongoClient client;
    private MongoServer server;

    @Before
    public void setUp() {
        server = new MongoServer(new MemoryBackend());

        // bind on a random local port
        InetSocketAddress serverAddress = server.bind();

        client = new MongoClient(new ServerAddress(serverAddress));
        collection = client.getDB("testdb").getCollection("testcollection");
    }

    @After
    public void tearDown() {
        client.close();
        server.shutdownNow();
    }

    @Test
    public void testSimpleInsertQuery() throws Exception {
        assertEquals(0, collection.count());

        // creates the database and collection in memory and insert the object
        DBObject obj = new BasicDBObject("_id", 1).append("key", "value");
        collection.insert(obj);

        assertEquals(1, collection.count());
        assertEquals(obj, collection.findOne());
    }
}
