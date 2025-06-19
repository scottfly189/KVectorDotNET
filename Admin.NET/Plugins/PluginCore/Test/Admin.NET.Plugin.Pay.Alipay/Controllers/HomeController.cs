

using Microsoft.AspNetCore.Mvc;
using PluginCore;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Admin.NET.Plugin.Pay.Yfzf.Controllers
{
    /// <summary>
    /// 其实也可以不写这个, 直接访问 Plugins/HelloWorldPlugin/index.html
    /// 
    /// 下面的方法, 是去掉 index.html
    /// 
    /// 若 wwwroot 下有其它需要访问的文件, 如何 css, js, 而你又不想每次新增 action 指定返回, 则 Route 必须 Plugins/{PluginId},
    /// 这样访问 Plugins/HelloWorldPlugin/css/main.css 就会访问到你插件下的 wwwroot/css/main.css
    /// </summary>
    [Route("Plugins/HelloWorldPlugin")]
    public class HomeController : Controller
    {

        /// <summary>
        /// 新大陆信息
        /// </summary>
        /// <returns></returns>
        [ApiDescriptionSettings(Name = "Index"), HttpGet]
        [DisplayName("新大陆信息")]
        public async Task<ActionResult> Index()
        {
            string indexFilePath = System.IO.Path.Combine(PluginPathProvider.PluginWwwRootDir("Admin.NET.Plugin.Pay.Yfzf"), "index.html");

            return PhysicalFile(indexFilePath, "text/html");
        }
    }
}
