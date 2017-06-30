using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Engine;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Web
{
    /// <summary>
    ///     Class HtmlExtensions.
    /// </summary>
    /// TODO Edit XML Comment Template for HtmlExtensions
    public static class HtmlExtensions
    {
        /// <summary>
        ///     MVCs the grid.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <returns>IHtmlString.</returns>
        /// TODO Edit XML Comment Template for MvcGrid
        public static IHtmlString MvcGrid(this HtmlHelper helper, string name)
        {
            var currentMapping =
                MvcGridDefinitionTable.GetDefinitionInterface(name);

            return MvcGrid(helper, name, currentMapping, null);
        }

        /// <summary>
        ///     MVCs the grid.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>IHtmlString.</returns>
        /// TODO Edit XML Comment Template for MvcGrid
        public static IHtmlString MvcGrid(
            this HtmlHelper helper,
            string name,
            object pageParameters)
        {
            var currentMapping =
                MvcGridDefinitionTable.GetDefinitionInterface(name);

            return MvcGrid(helper, name, currentMapping, pageParameters);
        }

        /// <summary>
        ///     MVCs the grid.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="name">The name.</param>
        /// <param name="grid">The grid.</param>
        /// <param name="pageParameters">The page parameters.</param>
        /// <returns>IHtmlString.</returns>
        /// TODO Edit XML Comment Template for MvcGrid
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