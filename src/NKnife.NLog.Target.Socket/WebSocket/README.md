# NLog.Targets.WebSocket

这是一个NLog的目标包，基于WebSocket服务

### 使用方法

使用时安装包

```bash
dotnet add package NLog.Targets.WebSocket
```

在nlog配置文件中载入包，并进行配置

示例：

```xml
<?xml version="1.0" encoding="utf-8"?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" autoReload="true">
	<extensions>
		<add assembly="NLog.Targets.WebSocket" />
	</extensions>
    <targets async="true">
        <!-- 日志输出到控制台 -->
        <target name="console" 
				xsi:type="ColoredConsole" 
				layout="${longdate} [${level}] ${message} ${exception:format=tostring}" />
		<!-- 日志输出到WebSocket -->
		<target name="websocket" 
				xsi:type="WebSocket" 
				layout="${longdate} [${level}] ${message} ${exception}"
                port="5000"
				wspath="log" />
    </targets>

    <rules>
        <logger name="*" minlevel="Trace" writeTo="console" />
		<logger name="*" minlevel="Trace" writeTo="websocket" />
    </rules>
</nlog>
```

### 参数

- port -> websocket服务监听的端口
- wspath -> websocket服务的监听路径

#### 注意事项

如果程序运行在Windows下，默认监听的地址是`127.0.0.1`，此时只有本地可以访问，

要想任何地址都能访问，需要程序运行在管理员权限下。

或者使用cmd执行命令：

```bash
netsh http add urlacl url=http://*:{port}/{wspath}/ user=Everyone
```

再以普通权限运行程序也可以达到监听所有地址的效果。



Linux下则没有上述影响。