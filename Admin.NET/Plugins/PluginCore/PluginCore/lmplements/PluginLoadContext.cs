// Admin.NET ��Ŀ�İ�Ȩ���̱ꡢר�����������Ȩ��������Ӧ���ɷ���ı�����ʹ�ñ���ĿӦ������ط��ɷ�������֤��Ҫ��
//
// ����Ŀ��Ҫ��ѭ MIT ���֤�� Apache ���֤���汾 2.0�����зַ���ʹ�á����֤λ��Դ��������Ŀ¼�е� LICENSE-MIT �� LICENSE-APACHE �ļ���
//
// �������ñ���Ŀ����Σ�����Ұ�ȫ��������������ַ����˺Ϸ�Ȩ��ȷ��ɷ����ֹ�Ļ���κλ��ڱ���Ŀ���ο�����������һ�з��ɾ��׺����Σ����ǲ��е��κ����Σ�



using PluginCore.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Linq;

namespace PluginCore.lmplements
{
    /// <summary>
    /// LazyPluginLoadContext
    /// </summary>
    public class PluginLoadContext : LazyPluginLoadContext, IPluginContext
    {
        public PluginLoadContext(string pluginId, string pluginMainDllFilePath) : base(pluginId, pluginMainDllFilePath)
        {
        }
    }
}


