using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Engine;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    /// <summary>
    /// 
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IHtmlString MvcGrid(this HtmlHelper helper, string name)
        {
            var currentMapping =
                MvcGridDefinitionTable.GetDefinitionInterface(name);

            return MvcGrid(helper, name, currentMapping, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="name"></param>
        /// <param name="pageParameters"></param>
        /// <returns></returns>
        public static IHtmlString MvcGrid(
            this HtmlHelper helper,
            string name,
            object pageParameters)
        {
            var currentMapping =
                MvcGridDefinitionTable.GetDefinitionInterface(name);

            return MvcGrid(helper, name, currentMapping, pageParameters);
        }

        internal static IHtmlString MvcGrid(
            this HtmlHelper helper,
            string name,
            IMvcGridDefinition grid,
            object pageParameters)
        {
            var ge = new GridEngine();

            var html = ge.GetBasePageHtml(helper, name, grid, pageParameters);

            return MvcHtmlString.Create(html);
        }
    }
}