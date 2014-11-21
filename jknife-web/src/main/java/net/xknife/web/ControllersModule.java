package net.xknife.web;

import java.util.List;

import com.google.common.base.Strings;
import com.google.inject.AbstractModule;
import com.google.inject.name.Names;
import net.xknife.lang.widgets.ClassFinder;
import net.xknife.web.interfaces.IController;
import net.xknife.web.interfaces.WebapiController;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

/**
 * Controller管理器
 */
public class ControllersModule extends AbstractModule
{
    static org.slf4j.Logger _Logger = org.slf4j.LoggerFactory.getLogger(ControllersModule.class);

    private static boolean _Enable = false;

    /**
     * 设置Controller管理器是否启用
     *
     * @param enable 为true时启用，反之不启用
     */
    public static void setEnable(boolean enable)
    {
        _Enable = enable;
    }

    @Override
    protected void configure()
    {
        if (!_Enable)
        {
            _Logger.info("基于jknife.web的Controller管理器未启用。");
            return;
        }
        _Logger.debug("基于jknife.web的Controller管理器启用，开始查找项目中的Controller");
        int i = 0;
        List<Class<IController>> controllerClassList = ClassFinder.find(IController.class, WebapiController.class, false, "Controller");
        for (Class<IController> clazz : controllerClassList)
        {
            WebapiController ann = clazz.getAnnotation(WebapiController.class);
            if (ann != null)
            {
                String key = ann.value();
                if (!Strings.isNullOrEmpty(key))
                {
                    i++;
                    binder().bind(IController.class).annotatedWith(Names.named(key)).to(clazz);
                }
            }
        }
        _Logger.info(String.format("共查找到 %s 个符合约定的Controller，加入到Guice中。", i));
    }
}
