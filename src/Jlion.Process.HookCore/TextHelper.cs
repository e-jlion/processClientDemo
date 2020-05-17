using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Jlion.Process.HookCore
{
    public class TextHelper
    {
        private static int dayCount = 0;
        private static readonly double maxSize = 50;
        private static readonly object locker = new object();

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="toFiles"></param>
        /// <returns></returns>
        public static void Write(string message, Exception ex = null, bool toFiles = true, LogEnumsType logEnumsType = LogEnumsType.LogsOption)
        {
            try
            {
                if (string.IsNullOrEmpty(message))
                    return;

                message = $"===============================================\r\n" +
                          $"日志时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}\r\n" +
                          $"日志内容：{message}\r\n";
                if (ex != null)
                {
                    message += $"异常内容：{ex.Message}\r\n";
                    if (!string.IsNullOrEmpty(ex.StackTrace))
                        message += $"堆栈内容：{ex.StackTrace}\r\n";
                    if (!string.IsNullOrEmpty(ex.InnerException?.StackTrace ?? ""))
                        message += $"Inner 堆栈内容 内容:{ex.InnerException.StackTrace}";
                }
                message += $"===============================================\r\n";

                if (toFiles)
                {
                    var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"logs/aa/{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
                    message += "\r\n";
                    DoWrite(filePath, message);
                }
            }
            catch { }
        }

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        /// <param name="toFiles"></param>
        /// <returns></returns>
        public static Task WriteAsync(string message, Exception ex = null, bool toFiles = true, LogEnumsType logEnumsType = LogEnumsType.LogsOption)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (string.IsNullOrEmpty(message))
                        return;

                    message = $"===============================================\r\n" +
                              $"日志时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff")}\r\n" +
                              $"日志内容：{message}\r\n";
                    if (ex != null)
                    {
                        message += $"异常内容：{ex.Message}\r\n";
                        if (!string.IsNullOrEmpty(ex.StackTrace))
                            message += $"堆栈内容：{ex.StackTrace}\r\n";
                    }
                    message += $"===============================================\r\n";

                    //Console.WriteLine(message);

                    if (toFiles)
                    {
                        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"logs/aa/{DateTime.Now.ToString("yyyy-MM-dd")}.txt");
                        message += "\r\n";
                        DoWrite(filePath, message);
                    }
                }
                catch { }
            });
        }

        ///// <summary>
        ///// 写入日志
        ///// </summary>
        ///// <param name="message"></param>
        ///// <param name="ex"></param>
        ///// <param name="toFiles"></param>
        //public static void Write(string message, Exception ex = null, bool toFiles = true)
        //{
        //    WriteAsync(message, ex, toFiles);
        //}
        public static Task ErrorAsync(string message, Exception ex = null, bool toFiles = true)
        {
            return WriteAsync(message, ex, toFiles, LogEnumsType.ErrorLogs);
        }

        public static void Error(string message, Exception ex = null, bool toFiles = true)
        {
            Write(message, ex, toFiles, LogEnumsType.ErrorLogs);
        }

        public static Task LogInfoAsync(string message, bool toFiles = true, LogEnumsType logEnumsType = LogEnumsType.LogsDetail)
        {
            return WriteAsync(message, toFiles: toFiles, logEnumsType: logEnumsType);
        }

        public static void LogInfo(string message, bool toFiles = true, LogEnumsType logEnumsType = LogEnumsType.LogsDetail)
        {
            Write(message, toFiles: toFiles, logEnumsType: logEnumsType);
        }

        //最大 50M
        /// <summary>
        /// 写入文本文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="message"></param>
        private static void DoWrite(string filePath, string message)
        {
            var gb2312 = Encoding.GetEncoding("utf-8");//设置一下编码格式
            if (!File.Exists(filePath))
            {
                var dir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                var swCreate = new StreamWriter(filePath, false, gb2312);
                swCreate.Flush();
                swCreate.Close();
                swCreate.Dispose();
            }
            else
            {
                var fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > maxSize * 1024 * 1024)
                {
                    filePath = fileInfo.DirectoryName + $"/{DateTime.Now.ToString("yyyy-MM-dd")}_{dayCount}.txt";
                    if (!File.Exists(filePath))
                    {
                        DoWrite(filePath, message);
                        return;
                    }

                    fileInfo = new FileInfo(filePath);
                    if (fileInfo.Length > maxSize * 1024 * 1024)
                    {
                        filePath = fileInfo.DirectoryName + $"/{DateTime.Now.ToString("yyyy-MM-dd")}_{++dayCount}.txt";
                        DoWrite(filePath, message);
                        return;
                    }
                }
            }

            lock (locker)
            {
                using (var sw = File.AppendText(filePath))
                {
                    sw.WriteLine(message);
                }
            }
        }

        /// <summary>
        /// 日志类型
        /// </summary>
        public enum LogEnumsType
        {
            /// <summary>
            /// 读取金额日志
            /// </summary>
            [Description("logsRead")]
            ReadLogs = 0,

            /// <summary>
            /// 错误等相关日志
            /// </summary>
            [Description("logsError")]
            ErrorLogs = 1,

            /// <summary>
            /// 操作日志
            /// </summary>
            [Description("logsOption")]
            LogsOption = 2,

            /// <summary>
            /// 支付日志
            /// </summary>
            [Description("logsPay")]
            LogsPay = 3,

            /// <summary>
            /// Aop 拦截日志
            /// </summary>
            LogsDetail = 4,
        }
    }
}
