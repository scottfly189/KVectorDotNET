// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
// Project: SimCaptcha
// https://github.com/yiyungent/SimCaptcha
// Author: yiyun <yiyungent@gmail.com>

/// <summary>
/// JavaScript时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总毫秒数。（13 位数字）
/// 
/// Unix时间戳：是指格林威治时间1970年01月01日00时00分00秒(北京时间1970年01月01日08时00分00秒)起至现在的总秒数。（10 位数字）
/// </summary>
namespace PluginCore.Utils
{
    public static class DateTimeUtil
    {
        public static DateTime DateTime1970 = new DateTime(1970, 1, 1).ToLocalTime();

        #region Unix 10位时间戳-总秒数
        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        public static long ToTimeStamp10(this DateTime dateTime)
        {
            // 相差秒数
            long timeStamp = (long)(dateTime.ToLocalTime() - DateTime1970).TotalSeconds;

            return timeStamp;
        }

        /// <summary>
        /// C# DateTime转换为Unix时间戳
        /// </summary>
        public static long ToTimeStamp10(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return 0;
            }
            // 相差秒数
            long timeStamp = ToTimeStamp10((DateTime)dateTime);

            return timeStamp;
        }

        /// <summary>
        /// Unix时间戳转换为C# DateTime 
        /// </summary>
        public static DateTime ToDateTime10(this long timeStamp10)
        {
            DateTime dateTime = DateTime1970.AddSeconds(timeStamp10).ToLocalTime();

            return dateTime;
        }
        #endregion

        #region JavaScript 13位时间戳-总毫秒数
        /// <summary>
        /// C# DateTime转换为JavaScript时间戳
        /// </summary>
        public static long ToTimeStamp13(this DateTime dateTime)
        {
            // 相差毫秒数
            long timeStamp = (long)(dateTime.ToLocalTime() - DateTime1970).TotalMilliseconds;

            return timeStamp;
        }

        /// <summary>
        /// C# DateTime转换为JavaScript时间戳
        /// </summary>
        public static long ToTimeStamp13(this DateTime? dateTime)
        {
            if (dateTime == null)
            {
                return 0;
            }
            // 相差秒数
            long timeStamp = ToTimeStamp13((DateTime)dateTime);

            return timeStamp;
        }

        /// <summary>
        /// JavaScript时间戳转换为C# DateTime
        /// </summary>
        public static DateTime ToDateTime13(this long timeStamp13)
        {
            DateTime dateTime = DateTime1970.AddMilliseconds(timeStamp13).ToLocalTime();

            return dateTime;
        }
        #endregion

        #region 获取当前Unix时间戳
        public static long NowTimeStamp10()
        {
            return ToTimeStamp10(DateTime.Now);
        }
        #endregion

        #region 获取当前JavaScript时间戳
        public static long NowTimeStamp13()
        {
            return ToTimeStamp13(DateTime.Now);
        }
        #endregion
    }
}


