namespace MvcGrid.Web.Models
{
    public class MvcGridToolbarModel
    {
        public MvcGridToolbarModel()
        {
        }

        public MvcGridToolbarModel(string gridName)
        {
            MvcGridName = gridName;
        }

        public bool ColumnVisibility
        {
            get;
            set;
        }

        public bool Export
        {
            get;
            set;
        }

        public bool GlobalSearch
        {
            get;
            set;
        }

        public string MvcGridName
        {
            get;
            set;
        }

        public bool PageSize
        {
            get;
            set;
        }
    }
}