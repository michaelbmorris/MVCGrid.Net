using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class TemplateModel
    {
        /// <summary>
        /// 
        /// </summary>
        public UrlHelper Url => GridContext.UrlHelper;

        /// <summary>
        /// 
        /// </summary>
        public IMvcGridColumn GridColumn
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public GridContext GridContext
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public dynamic Item
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Row Row
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get;
            set;
        }
    }
}