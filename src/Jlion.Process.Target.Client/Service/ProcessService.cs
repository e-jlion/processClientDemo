using Jlion.Process.Target.Client.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jlion.Process.Target.Client
{
    public class ProcessService
    {
        public string GetProcessInfo()
        {
            return "这是TargetClient 客户端（第三方程序）";
        }


        public ProcessResponse GetProcessInfo(ProcessRequest request)
        {
            return new ProcessResponse()
            {
                Name = "这是TargetClient 客户端（第三方程序）",
                Version = request.Version
            };
        }
    }
}
