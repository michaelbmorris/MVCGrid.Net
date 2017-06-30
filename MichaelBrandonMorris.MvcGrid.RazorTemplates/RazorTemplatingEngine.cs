using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;
using RazorEngine;
using RazorEngine.Templating;

namespace MVCGrid.RazorTemplates
{
    /// <summary>
    ///     Class RazorTemplatingEngine.
    /// </summary>
    /// <seealso
    ///     cref="IMvcGridTemplatingEngine" />
    /// TODO Edit XML Comment Template for RazorTemplatingEngine
    public class RazorTemplatingEngine : IMvcGridTemplatingEngine
    {
        /// <summary>
        ///     Processes the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for Process
        public string Process(string template, TemplateModel model)
        {
            var templateKey =
                $"{model.GridContext.GridName}_{model.GridColumn.ColumnName}";

            var result = Engine.Razor.RunCompile(
                template,
                templateKey,
                typeof(TemplateModel),
                model);

            return result;
        }
    }
}