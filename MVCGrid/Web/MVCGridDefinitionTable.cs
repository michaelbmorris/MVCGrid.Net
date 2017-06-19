using System;
using System.Collections.Generic;
using System.Linq;
using MvcGrid.Interfaces;
using MvcGrid.Models;

namespace MvcGrid.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class MvcGridDefinitionTable
    {
        private static readonly Dictionary<string, object> Table =
            new Dictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="name"></param>
        /// <param name="builder"></param>
        public static void Add<T1>(string name, MvcGridBuilder<T1> builder)
        {
            Add(name, builder.GridDefinition);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="name"></param>
        /// <param name="mapping"></param>
        public static void Add<T1>(string name, GridDefinition<T1> mapping)
        {
            if (Table.ContainsKey(name))
            {
                throw new ArgumentException(
                    $"There is already a grid definition with the name '{name}'.",
                    nameof(name));
            }

            if (mapping.RetrieveData == null)
            {
                throw new ArgumentException(
                    $"There is no RetrieveData expression defined for grid '{name}'.",
                    nameof(name));
            }

            if (mapping.Sorting
                && string.IsNullOrWhiteSpace(mapping.DefaultSortColumn))
            {
                throw new Exception(
                    $"Grid '{name}': When sorting is enabled, a default sort column must be specified");
            }

            if (mapping.AdditionalQueryOptionNames.Count > 0)
            {
                // TODO: dynamically get names
                var forbiddenNames = new HashSet<string>
                {
                    QueryStringParser.QueryStringSuffixPage,
                    QueryStringParser.QueryStringSuffixSort,
                    QueryStringParser.QueryStringSuffixSortDir,
                    QueryStringParser.QueryStringSuffixEngine,
                    QueryStringParser.QueryStringSuffixItemsPerPage,
                    QueryStringParser.QueryStringSuffixColumns
                };

                mapping.GetColumns()
                    .ToList()
                    .ForEach(col => forbiddenNames.Add(col.ColumnName));

                foreach (var forbiddenName in forbiddenNames)
                {
                    if (mapping.AdditionalQueryOptionNames.Contains(
                        forbiddenName,
                        StringComparer.InvariantCultureIgnoreCase))
                    {
                        throw new Exception(
                            $"Grid '{name}': Invalid additional query option name: '{forbiddenName}'. Cannot be column name or reserved keyword.");
                    }
                }
            }

            Table.Add(name, mapping);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GridDefinition<T1> GetDefinition<T1>(string name)
        {
            return (GridDefinition<T1>) GetDefinitionInterface(name);
        }

        internal static IMvcGridDefinition GetDefinitionInterface(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Table.ContainsKey(name))
            {
                throw new Exception(
                    $"There is no grid defined with the name '{name}'");
            }

            return (IMvcGridDefinition) Table[name];
        }
    }
}