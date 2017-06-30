using System.IO;
using System.Web;
using MichaelBrandonMorris.MvcGrid.Models;

namespace MichaelBrandonMorris.MvcGrid.Interfaces
{
    /// <summary>
    ///     Interface IMvcGridRenderingEngine
    /// </summary>
    /// TODO Edit XML Comment Template for IMvcGridRenderingEngine
    public interface IMvcGridRenderingEngine
    {
        /// <summary>
        ///     Gets a value indicating whether [allows paging].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [allows paging]; otherwise,
        ///     <c>false</c>.
        /// </value>
        /// TODO Edit XML Comment Template for AllowsPaging
        bool AllowsPaging
        {
            get;
        }

        /// <summary>
        ///     Prepares the response.
        /// </summary>
        /// <param name="response">The response.</param>
        /// TODO Edit XML Comment Template for PrepareResponse
        void PrepareResponse(HttpResponse response);

        /// <summary>
        ///     Renders the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="gridContext">The grid context.</param>
        /// <param name="outputStream">The output stream.</param>
        /// TODO Edit XML Comment Template for Render
        void Render(
            RenderingModel model,
            GridContext gridContext,
            TextWriter outputStream);

        /// <summary>
        ///     Renders the container.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="outputStream">The output stream.</param>
        /// TODO Edit XML Comment Template for RenderContainer
        void RenderContainer(
            ContainerRenderingModel model,
            TextWriter outputStream);
    }
}