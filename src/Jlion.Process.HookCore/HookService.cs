using DotNetDetour;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jlion.Process.HookCore
{
    public class HookService
    {
        /// <summary>
        /// Hook 初始化
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static int Start(string msg)
        {
            try
            {
                TextHelper.LogInfo("开始"+msg);
                MethodHook.Install();
            }
            catch
            {
                return -1;
            }
            return 1;
        }
    }
}
