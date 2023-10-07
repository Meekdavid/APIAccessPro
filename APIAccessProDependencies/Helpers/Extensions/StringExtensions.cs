using APIAccessProDependencies.Helpers.DTOs.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static APIAccessProDependencies.Helpers.Common.Utils;

namespace APIAccessProDependencies.Helpers.Extensions
{
    public static class StringExtensions
    {
        //Method used to convert model validation response to dictionary
        public static string ToDictionaryString<TKey, TValue>(this IDictionary<TKey, TValue> dictionary) where TKey : class where TValue : class
        {
            return $"{{ {string.Join(", ", dictionary.Select(kv => kv.Key + " = " + kv.Value).ToArray())} }}";
        }


        // This extension method is called to add a new string of logs to an the existing list of logs
        public static void AddToLogs(this string messageLog, ref List<Log> logs, LogType logType = LogType.LOG_DEBUG, Exception exceptionLog = null)
        {
            logs.Add(new Log()
            {
                MessageLog = messageLog,
                LogType = (int)logType,
                ExceptionLog = exceptionLog
            });
        }
    }
}
