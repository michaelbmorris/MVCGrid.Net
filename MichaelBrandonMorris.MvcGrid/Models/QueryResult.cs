using System.Collections.Generic;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    ///     Class QueryResult.
    /// </summary>
    /// <typeparam name="T1">The type of the t1.</typeparam>
    /// TODO Edit XML Comment Template for QueryResult`1
    public class QueryResult<T1>
    {
        /// <summary>
        ///     Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        /// TODO Edit XML Comment Template for Items
        public IEnumerable<T1> Items
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the total records.
        /// </summary>
        /// <value>The total records.</value>
        /// TODO Edit XML Comment Template for TotalRecords
        public int? TotalRecords
        {
            get;
            set;
        }
    }
}