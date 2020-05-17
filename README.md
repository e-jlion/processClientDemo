# processClientDemo
跨进程改写目标方法（注入dll 到第三方运行的程序进行第三方程序方法拦截改写）

### Hook
通过开源框架DotNetDotour  进行第三方方法Hook 重新第三方方法，前提是需要知道第三方程序中有那些方法（有些没加壳的比较好办，加壳的就得想办法反编译脱壳了）
DotNetDotour 开源框架我fork 的地址：https://github.com/a312586670/DotNetDetour 我针对开源框架做了降级处理，支持.net framework 4 版本，开源作者里面的支持4.5+
故我fork 了一份（项目环境需要）

### FastWin32
通过 FastWin32 把重新的Hook dll 模块动态注入到第三方运行的程序中，并且执行Hook初始化操作，这样即可覆盖掉原有的第三方程序指定的方法，做自己的业务
