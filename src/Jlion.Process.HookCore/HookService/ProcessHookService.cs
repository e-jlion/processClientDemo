using DotNetDetour;
using Jlion.Process.HookCore.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jlion.Process.HookCore
{
    public class ProcessHookService : IMethodHook
    {
        [HookMethod("Jlion.Process.Target.Client.ProcessService", null, null)]
        public string GetProcessInfo()
        {
            TextHelper.LogInfo($"这是Jlion.Process.HookCore.HookService dll. 改写TargetClient 客户端 的GetProcessInfo 方法后得到的结果");
            return "这是Jlion.Process.HookCore.HookService dll. 改写TargetClient 客户端 的GetProcessInfo 方法后得到的结果";
        }

        [OriginalMethod]
        public string GetProcessInfo_Original()
        {
            return null;
        }

        [HookMethod("Jlion.Process.Target.Client.ProcessService", null, null)]
        public object GetProcessInfo([RememberType("Jlion.Process.Target.Client.Model.ProcessRequest", false)] object request)
        {
            var json = JsonConvert.SerializeObject(request);
            TextHelper.LogInfo($"json:{json}");

            var name = "这是Jlion.Process.HookCore.HookService dll. 改写TargetClient 客户端的GetProcessInfo(obj)后得到的结果";
            return new ProcessResponse()
            {
                Name = name,
                Version = "改写的dll 版本"
            };
        }

        [OriginalMethod]
        public object GetProcessInfo_Original([RememberType("Jlion.Process.Target.Client.Model.ProcessRequest", false)] object request)
        {
            return null;
        }
    }
}
