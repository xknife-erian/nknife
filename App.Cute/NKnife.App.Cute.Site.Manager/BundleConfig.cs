using System.Web.Optimization;

namespace Didaku.Engine.Timeaxis.Site.Manager
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //传统浏览器目前不会被完全取代，令你难以将最新的 CSS3 或 HTML5 功能嵌入你的网站。 
            //Modernizr 正是为解决这一难题应运而生，作为一个开源的 JavaScript 库，
            //Modernizr 检测浏览器对 CSS3 或 HTML5 功能支持情况。 
            //Modernizr 并非试图添加老版本浏览器不支持的功能，而是令你通过创建可选风格配置修改页面设计。 
            //它也可以通过加载定制的脚本来模拟老版本浏览器不支持的功能。
            //http://www.adobe.com/cn/devnet/dreamweaver/articles/using-modernizr.html
            bundles.Add(new ScriptBundle("~/modernizr-js").Include("~/Scripts/modernizr-*"));

            //http://www.cnblogs.com/kyo-yo/archive/2010/07/05/use-jquery-validate-to-being-client-validate-high-1.html
            //使用jQuery.Validate进行客户端验证（高级篇-上）——不使用微软验证控件的理由
            bundles.Add(new ScriptBundle("~/jqueryval-js").Include("~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/jquery-js").Include("~/Scripts/jquery-{version}.js"));
            bundles.Add(new ScriptBundle("~/jqueryui-js").Include("~/Scripts/jquery-ui-{version}.js"));
            bundles.Add(new ScriptBundle("~/site-js").Include("~/Site/site*"));

            bundles.Add(new StyleBundle("~/jquery-ui-base-css").Include(
                "~/Content/themes/base/jquery-ui.css",
                "~/Content/themes/base/jquery.ui.core.css",
                "~/Content/themes/base/jquery.ui.theme.css"));
            bundles.Add(new StyleBundle("~/site-css").Include("~/Content/Site.*"));
        }
    }
}