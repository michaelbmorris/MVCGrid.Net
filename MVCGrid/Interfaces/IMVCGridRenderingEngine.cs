using System.IO;
using System.Web;
using MvcGrid.Models;

namespace MvcGrid.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMvcGridRenderingEngine
    {
        /// <summary>
        /// 
        /// </summary>
        bool AllowsPaging
        {
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="response"></param>
        void PrepareResponse(HttpResponse response);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="gridContext"></param>
        /// <param name="outputStream"></param>
        void Render(
            RenderingModel model,
            GridContext gridContext,
            TextWriter outputStream);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="outputStream"></param>
        void RenderContainer(
            ContainerRenderingModel model,
            TextWriter outputStream);
    }
}