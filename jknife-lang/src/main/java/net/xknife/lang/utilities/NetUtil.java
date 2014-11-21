package net.xknife.lang.utilities;

import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.SocketException;
import java.util.Enumeration;

/**
 * Created by lukan@xknife.net on 13-12-27.
 */
public class NetUtil
{

    /**
     * 获取本机的IP地址
     *
     * @return
     * @throws SocketException
     */
    public static String getLocalIpAddress() throws SocketException
    {
        String ipAddress = "";

        for (Enumeration<NetworkInterface> en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements(); )
        {
            NetworkInterface net = en.nextElement();
            for (Enumeration<InetAddress> ip = net.getInetAddresses(); ip.hasMoreElements(); )
            {
                InetAddress inetAddress = ip.nextElement();
                if (!inetAddress.isLoopbackAddress())
                {
                    ipAddress = inetAddress.getHostAddress();
                }
            }
        }
        return ipAddress;
    }
}
