package net.xknife.web;

import com.fasterxml.jackson.core.JsonParseException;
import com.fasterxml.jackson.databind.JsonMappingException;
import com.google.inject.ConfigurationException;
import com.google.inject.Key;
import com.google.inject.ProvisionException;
import com.google.inject.name.Names;
import net.xknife.inject.DI;
import net.xknife.json.JsonKnife;
import net.xknife.web.interfaces.IController;
import net.xknife.web.utilities.RequestUtil;

import javax.servlet.http.HttpServletRequest;
import java.io.IOException;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

/**
 * 针对提交数据为Json时的Servlet的基类。
 * 例如请求为：http://localhost/query?{"name":"kevin"}
 * Created by erianlu on 14-2-28.
 */
public abstract class WebapiServlet<T> extends BaseWebapiServlet<T>
{
    private static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(WebapiServlet.class);

    /**
     * 本项目的API的基础运行函数。约定，绝大多数的API均以POST请求，参数以Json方式传递。
     */
    @Override
    protected void runPost(final HttpServletRequest request, final PrintWriter writer)
    {
        String servletPath = request.getServletPath();
        String[] paths = servletPath.split("/");
        String methodName = paths[paths.length - 1];

        String controllerFullName = getControllerName(servletPath);
        String queryInfo = "";
        T params = null;

        try
        {
            params = getParams(request);// 转换参数为pojo对象
        }
        catch (ConfigurationException e)
        {
            _Logger.warn(String.format("未通过Guice配置指定的IController:%s", controllerFullName), e);
        }
        catch (ProvisionException e)
        {
            _Logger.warn(String.format("IController:%s 实例失败", controllerFullName), e);
        }
        catch (JsonParseException e)
        {
            _Logger.warn(String.format("参数转换异常:%s", queryInfo), e);
        }
        catch (JsonMappingException e)
        {
            _Logger.warn(String.format("参数转换异常:%s", queryInfo), e);
        }
        catch (IOException e)
        {
            _Logger.warn(String.format("JSON转换器异常:%s", queryInfo), e);
        }
        try
        {
            IController controller = DI.getInstance(Key.get(IController.class, Names.named(controllerFullName)));
            // 根据找到的controller执行相应的方法
            controller.process(methodName, params, writer);
        }
        catch (Exception e)
        {
            _Logger.warn(String.format("IController(%s)异常:%s", controllerFullName, queryInfo), e);
        }
    }

    /**
     * 本项目WebApi仅支持post方式，较安全，并且数据量较大一些。
     */
    @Override
    protected void runGet(final HttpServletRequest request, final PrintWriter writer)
    {
        runPost(request, writer);
    }

    /**
     * 将URL信息中的参数信息(json格式)转换成当前servlet的参数pojo类型
     *
     * @param request
     * @return
     * @throws com.fasterxml.jackson.core.JsonParseException
     * @throws com.fasterxml.jackson.databind.JsonMappingException
     * @throws java.io.IOException
     */
    @Override
    protected T getParams(final HttpServletRequest request) throws JsonParseException, JsonMappingException, IOException
    {
        String queryInfo = null;
        try
        {
            String method = request.getMethod();
            switch (method.toUpperCase())
            {
                case "GET":
                    queryInfo = URLDecoder.decode(request.getQueryString(), "utf-8");
                    break;
                case "POST":
                    queryInfo = RequestUtil.readParameterString(request);
                    break;
            }
            logRequest(request,queryInfo);
        }
        catch (UnsupportedEncodingException e)
        {
            _Logger.warn(String.format("参数解码异常:%s", request.getQueryString()), e);
        }
        Class<T> paramsType = getParamsType();
        return JsonKnife.getMapper().readValue(queryInfo, getParamsType());
    }

    /**
     * 实现从默认规范的servlet路径获取controller的DI注入名称，
     * 默认规范的servlet路径为：/模块名称/实体名称/具体操作方法，例如/user/administrator/query。
     * @param servletPath
     * @return
     */
    @Override
    protected String getControllerName(String servletPath)
    {
        String[] paths = servletPath.split("/");
        String moduleName = paths[1];
        String controllerName = paths[2];
        return String.format("%s/%s", moduleName, controllerName);
    }

    /**
     * 获取参数pojo类型。在最后的实现类中实现。
     *
     * @return
     */
    protected abstract Class<T> getParamsType();
}
