// Admin.NET ��Ŀ�İ�Ȩ���̱ꡢר�����������Ȩ��������Ӧ���ɷ���ı�����ʹ�ñ���ĿӦ������ط��ɷ�������֤��Ҫ��
//
// ����Ŀ��Ҫ��ѭ MIT ���֤�� Apache ���֤���汾 2.0�����зַ���ʹ�á����֤λ��Դ��������Ŀ¼�е� LICENSE-MIT �� LICENSE-APACHE �ļ���
//
// �������ñ���Ŀ����Σ�����Ұ�ȫ��������������ַ����˺Ϸ�Ȩ��ȷ��ɷ����ֹ�Ļ���κλ��ڱ���Ŀ���ο�����������һ�з��ɾ��׺����Σ����ǲ��е��κ����Σ�



using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Admin.NET.Plugin.Pay.Alipay.Controllers
{
     
    [Route("api/plugins/[controller]")]
    [AllowAnonymous]
    public class AliWebApiController : ControllerBase
    {
        [DisplayName("֧������Ϣ")]
        [HttpGet]
        public IActionResult Get()
        {
            string str = $"֧������Ϣ Hello PluginCore !";
            return Content(str);
        }
    }

}
