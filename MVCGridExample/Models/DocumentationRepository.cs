using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FileHelpers;

namespace MvcGrid.Web.Models
{
    public class DocumentationRepository
    {
        private static readonly Lazy<List<MethodDocItem>> Documentation =
            new Lazy<List<MethodDocItem>>(
                () =>
                {
                    var engine = new FileHelperEngine(typeof(MethodDocItem));

                    var filename =
                        HttpContext.Current.Server.MapPath(
                            "~/App_Data/documentation.csv");

                    var res = engine.ReadFile(filename) as MethodDocItem[];

                    if (res == null)
                    {
                        throw new Exception();
                    }

                    return new List<MethodDocItem>(res);
                });

        public List<MethodDocItem> GetData(string className)
        {
            return Documentation.Value.Where(p => p.Class == className)
                .OrderBy(p => p.Order)
                .ToList();
        }
    }

    [DelimitedRecord(",")]
    public class MethodDocItem
    {
        public string Class;

        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.NotAllow)]
        public string Description;

        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.NotAllow)]
        public string Name;

        public int Order;

        [FieldQuoted('"', QuoteMode.OptionalForRead, MultilineMode.NotAllow)]
        public string Return;
    }
}