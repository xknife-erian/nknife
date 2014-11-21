package net.xknife.web.utilities;

import javax.servlet.http.HttpServletRequest;
import java.io.BufferedReader;

/**
 * Created by erianlu on 2014/3/27.
 */
public class RequestUtil
{
    public static String readParameterString(HttpServletRequest request)
    {
        StringBuffer parameterBuff = new StringBuffer();
        String line = null;
        try
        {
            BufferedReader reader = request.getReader();
            while ((line = reader.readLine()) != null)
            {
                parameterBuff.append(line);
            }
            return parameterBuff.toString();
        }
        catch (Exception e)
        {
            return parameterBuff.toString();
        }
    }
}
