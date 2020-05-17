using FastWin32.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Timers;

namespace Jlion.Process.Client
{
    public class InjectService
    {
        public static string path = AppDomain.CurrentDomain.BaseDirectory + "Jlion.Process.HookCore.dll";
        public static string path2 = AppDomain.CurrentDomain.BaseDirectory + "DotNetDetour.dll";

        /// <summary>
        /// 进程id
        /// </summary>
        public static uint pid = 0;

        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            Inject();
        }


        #region 私有方法
        private static void Inject()
        {
            try
            {
                //Injector.InjectManaged(pid, path2, "Jlion.Process.HookCore", "Start", "ss", out int returnValue);
                Injector.InjectManaged(pid, path, "Jlion.Process.HookCore.HookService", "Start", "", out int returnValue2);
            }
            catch (Exception ex)
            {
            }
        }

        #endregion
    }
}
