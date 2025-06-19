// Admin.NET 项目的版权、商标、专利和其他相关权利均受相应法律法规的保护。使用本项目应遵守相关法律法规和许可证的要求。
//
// 本项目主要遵循 MIT 许可证和 Apache 许可证（版本 2.0）进行分发和使用。许可证位于源代码树根目录中的 LICENSE-MIT 和 LICENSE-APACHE 文件。
//
// 不得利用本项目从事危害国家安全、扰乱社会秩序、侵犯他人合法权益等法律法规禁止的活动！任何基于本项目二次开发而产生的一切法律纠纷和责任，我们不承担任何责任！



﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PluginCore.Utils
{
    public class Md5Helper
    {
        #region MD5加密为32位16进制字符串
        /// <summary>
        /// MD5加密为32位16进制字符串
        /// </summary>
        /// <param name="source">原输入字符串</param>
        /// <returns>返回加密后的字符串</returns>
        public static string MD5Encrypt32(string source)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                // "x2" 转换为 16进制
                sb.Append(buffer[i].ToString("x2"));
            }
            return sb.ToString();
        }
        #endregion
    }
}


