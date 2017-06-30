using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Interfaces
{
    /// <summary>
    ///     Interface IMvcGridTemplatingEngine
    /// </summary>
    /// TODO Edit XML Comment Template for IMvcGridTemplatingEngine
    public interface IMvcGridTemplatingEngine
    {
        /// <summary>
        ///     Processes the specified template.
        /// </summary>
        /// <param name="template">The template.</param>
        /// <param name="model">The model.</param>
        /// <returns>System.String.</returns>
        /// TODO Edit XML Comment Template for Process
        string Process(string template, TemplateModel model);
    }
}