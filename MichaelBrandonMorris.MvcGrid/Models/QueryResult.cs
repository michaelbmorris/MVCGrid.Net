using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    public class QueryResult<T1>
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<T1> Items
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? TotalRecords
        {
            get;
            set;
        }
    }
}