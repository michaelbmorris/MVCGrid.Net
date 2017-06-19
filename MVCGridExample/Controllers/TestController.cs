using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MichaelBrandonMorris.MvcGrid.Models;
using MichaelBrandonMorris.MvcGrid.Web;
using MVCGrid.Web.Models;
using MVCGrid.Web.Models.Test;

namespace MVCGrid.Web.Controllers
{
    public class TestController : Controller
    {
        public ActionResult DisplayGrid(string id)
        {
            ViewBag.GridName = id;

            return View();
        }

        public ActionResult InitialDate()
        {
            return View();
        }

        public ActionResult Issue6()
        {
            return View();
        }
    }

    public class TestControllerGrids : GridRegistration
    {
        private const string CacheKey = "ReportInvoiceLines";
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly Random _rng = new Random();

        public override void RegisterGrids()
        {
            //Issue6Grid
            MvcGridDefinitionTable.Add(
                "InitialDateGrid",
                new MvcGridBuilder<IReportInvoiceLine>().WithSorting(false)
                    .WithPaging(false)
                    .WithAllowChangingPageSize(false)
                    .WithFiltering(true)
                    .WithNoResultsMessage(
                        "Please enter a year to search for. No results found.")
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Year")
                                .WithHeaderText("Year")
                                .WithFiltering(true)
                                .WithValueExpression(a => a.Year.ToString());

                            cols.Add("InvoiceNo")
                                .WithHeaderText("Invoice No.")
                                .WithVisibility(true, true)
                                .WithValueExpression(
                                    a => a.InvoiceNumber.ToString());

                            cols.Add("City")
                                .WithHeaderText("City")
                                .WithVisibility(true, true)
                                .WithValueExpression(a => a.City);
                        })
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            IList<IReportInvoiceLine> list =
                                new List<IReportInvoiceLine>();
                            var totalRecords = 0;

                            var syear =
                                context.QueryOptions.GetFilterString("Year");
                            int year;

                            if (int.TryParse(syear, out year))
                            {
                                list = ReportInvoiceLines(year);
                                totalRecords = list.Count;
                            }

                            return new QueryResult<IReportInvoiceLine>
                            {
                                Items = list,
                                TotalRecords = totalRecords
                            };
                        }));


            //Issue6Grid
            MvcGridDefinitionTable.Add(
                "Issue6Grid",
                new MvcGridBuilder<Job>().WithSorting(true)
                    .WithDefaultSortColumn("Id")
                    .WithPaging(true)
                    .WithAllowChangingPageSize(true)
                    .WithMaxItemsPerPage(100)
                    .WithItemsPerPage(10)
                    .WithAdditionalQueryOptionName("globalsearch")
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id", "Id", row => row.JobId.ToString())
                                .WithSorting(true);
                            cols.Add("Name", "Name", row => row.Name)
                                .WithSorting(true);

                            cols.Add("Contact")
                                .WithHeaderText("Contact")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => p.Contact != null
                                        ? c.UrlHelper.Action(
                                            "Edit",
                                            "Contact",
                                            new
                                            {
                                                id = p.Contact.Id
                                            })
                                        : "")
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Contact.FullName}</a>")
                                .WithPlainTextValueExpression(
                                    (p, c) => p.Contact != null
                                        ? p.Contact.FullName
                                        : "");

                            cols.Add("Delete")
                                .WithHtmlEncoding(false)
                                .WithSorting(false)
                                .WithFiltering(false)
                                .WithHeaderText(
                                    "<input type='checkbox' id='chkselectall'>")
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "Save",
                                        "Country",
                                        new
                                        {
                                            area = "General",
                                            id = p.JobId
                                        }))
                                .WithValueTemplate(
                                    "<input type='checkbox' class='select' value='{Model.JobId}'>")
                                .WithPlainTextValueExpression((p, c) => "");
                        })
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var globalsearch =
                                options.GetAdditionalQueryOptionString(
                                    "globalsearch");

                            var repo = new JobRepo();
                            int totalRecords;
                            var data = repo.GetData(
                                out totalRecords,
                                globalsearch,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Job>
                            {
                                Items = data,
                                TotalRecords = totalRecords
                            };
                        }));

            //Issue21Grid
            //ńłóźćłąę.
            MvcGridDefinitionTable.Add(
                "Issue21Grid",
                new MvcGridBuilder<TestItem>()
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Col1")
                                .WithValueExpression(p => "ńłóźćłąę.");
                            cols.Add("Col2").WithValueExpression(p => p.Col2);
                        })
                    .WithSorting(true, "Col1")
                    .WithPaging(true, 10)
                    .WithFiltering(true)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var col3Filter =
                                context.QueryOptions
                                    .GetFilterString("FromDate");

                            var repo = new TestItemRepository();
                            int totalRecords;
                            var items = repo.GetData(
                                out totalRecords,
                                col3Filter,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);


                            return new QueryResult<TestItem>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            //Issue17Grid
            MvcGridDefinitionTable.Add(
                "Issue17Grid",
                new MvcGridBuilder<TestItem>()
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Col1").WithValueExpression(p => p.Col1);
                            cols.Add("Col2").WithValueExpression(p => p.Col2);


                            cols.Add("FromDate")
                                .WithHeaderText("From Date")
                                .WithFiltering(true)
                                .WithValueExpression(x => x.Col3);
                        })
                    .WithSorting(true, "Col1")
                    .WithPaging(true, 10)
                    .WithFiltering(true)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var col3Filter =
                                context.QueryOptions
                                    .GetFilterString("FromDate");

                            var repo = new TestItemRepository();
                            int totalRecords;
                            var items = repo.GetData(
                                out totalRecords,
                                col3Filter,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);


                            return new QueryResult<TestItem>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            //Issue28Grid
            MvcGridDefinitionTable.Add(
                "Issue28Grid",
                new MvcGridBuilder<TestItem>()
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Col1").WithValueExpression(p => p.Col1);
                            cols.Add("Col2").WithValueExpression(p => p.Col2);
                            cols.Add("Active")
                                .WithValueExpression(p => p.Col4.ToString());

                            cols.Add("Actions")
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) =>
                                    {
                                        // here's how to get an action url
                                        var url = c.UrlHelper.Action(
                                            "action1",
                                            "Test");

                                        // build whatever html you want
                                        var sb = new StringBuilder();
                                        sb.Append(
                                            "<img src='action1.png' onclick='someFunction()'>");
                                        sb.Append(
                                            "<img src='action2.png' onclick='someFunction()'>");

                                        // conditional html
                                        if (p.Col4)
                                        {
                                            sb.Append(
                                                "<img src='action3.png' onclick='someFunction()'>");
                                        }

                                        return sb.ToString();
                                    });
                        })
                    .WithSorting(true, "Col1")
                    .WithPaging(true, 10)
                    .WithFiltering(true)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var repo = new TestItemRepository();
                            int totalRecords;
                            var items = repo.GetData(
                                out totalRecords,
                                null,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);


                            return new QueryResult<TestItem>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));
        }

        private string RandomString(int size)
        {
            var buffer = new char[size];

            for (var i = 0; i < size; i++)
            {
                buffer[i] = Chars[_rng.Next(Chars.Length)];
            }

            return new string(buffer);
        }

        private IList<IReportInvoiceLine> ReportInvoiceLines(int year)
        {
            if (HttpContext.Current.Cache[CacheKey] == null)
            {
                var items = new List<IReportInvoiceLine>();

                for (var i = 1; i < 88; i++)
                {
                    var thisyear = _rng.Next(2010, 2016);
                    var j = new ReportInvoiceLine
                    {
                        Year = thisyear,
                        InvoiceNumber = i,
                        City = RandomString(8)
                    };

                    items.Add(j);
                }

                HttpContext.Current.Cache.Insert(CacheKey, items);
            }

            var data =
                (List<IReportInvoiceLine>) HttpContext.Current.Cache[CacheKey];


            var q = data.AsQueryable();

            //if (!String.IsNullOrWhiteSpace(globalSearch))
            {
                q = q.Where(p => p.Year == year);
            }

            //totalRecords = q.Count();

            //if (!String.IsNullOrWhiteSpace(orderBy))
            //{
            //    switch (orderBy.ToLower())
            //    {
            //        case "id":
            //            if (!desc)
            //                q = q.OrderBy(p => p.JobId);
            //            else
            //                q = q.OrderByDescending(p => p.JobId);
            //            break;
            //        case "name":
            //            if (!desc)
            //                q = q.OrderBy(p => p.Name);
            //            else
            //                q = q.OrderByDescending(p => p.Name);
            //            break;
            //        case "contact":
            //            if (!desc)
            //                q = q.OrderBy(p => p.Contact.FullName);
            //            else
            //                q = q.OrderByDescending(p => p.Contact.FullName);
            //            break;
            //    }
            //}

            //if (limitOffset.HasValue)
            //{
            //    q = q.Skip(limitOffset.Value).Take(limitRowCount.Value);
            //}

            return q.ToList();
        }
    }
}