using MichaelBrandonMorris.MvcGrid.Interfaces;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class ColumnDefaults : IMvcGridColumn
    {
        /// <summary>
        /// 
        /// </summary>
        public ColumnDefaults()
        {
            ColumnName = null;
            HeaderText = null;
            EnableSorting = false;
            HtmlEncode = true;
            EnableFiltering = false;
            Visible = true;
            SortColumnData = null;
            AllowChangeVisibility = false;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowChangeVisibility
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ColumnName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableFiltering
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool EnableSorting
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string HeaderText
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool HtmlEncode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public object SortColumnData
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Visible
        {
            get;
            set;
        }
    }
}