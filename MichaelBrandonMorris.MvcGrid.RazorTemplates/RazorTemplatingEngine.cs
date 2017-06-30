using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Models;
using RazorEngine.Templating;

namespace MVCGrid.RazorTemplates
{
    public class RazorTemplatingEngine : IMvcGridTemplatingEngine
    {
        public string Process(string template, TemplateModel model)
        {
            var templateKey = string.Format(
                "{0}_{1}",
                model.GridContext.GridName,
                model.GridColumn.ColumnName);

            var result = RazorEngine.Engine.Razor.RunCompile(
                template,
                templateKey,
                typeof(TemplateModel),
                model);

            return result;
        }
    }
}