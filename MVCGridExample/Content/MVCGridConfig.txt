using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using MvcGrid.Models;
using MvcGrid.Web.Data;
using MvcGrid.Web.Models;
using MVCGridExample.Models;

namespace MvcGrid.Web
{
    public class MvcGridConfig
    {
        public static void RegisterGrids()
        {
            var colDefauls = new ColumnDefaults
            {
                EnableSorting = true
            };

            MvcGridDefinitionTable.Add(
                "TestGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .WithSorting(true, "Id", SortDirection.Dsc)
                    .WithPaging(true, 10, true, 100)
                    .WithAdditionalQueryOptionNames("search")
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>",
                                    false)
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithVisibility(true, true)
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithVisibility(true, true)
                                .WithValueExpression(p => p.LastName);
                            cols.Add("FullName")
                                .WithHeaderText("Full Name")
                                .WithValueTemplate(
                                    "{Model.FirstName} {Model.LastName}")
                                .WithVisibility(false, true)
                                .WithSorting(false);
                            cols.Add("StartDate")
                                .WithHeaderText("Start Date")
                                .WithVisibility(true, true)
                                .WithValueExpression(
                                    p => p.StartDate?.ToShortDateString() ?? "");
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithVisibility(true, true)
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive")
                                .WithCellCssClassExpression(
                                    p => p.Active ? "success" : "danger");
                            cols.Add("Gender")
                                .WithValueExpression((p, c) => p.Gender)
                                .WithAllowChangeVisibility(true);
                            cols.Add("Email")
                                .WithVisibility(false, true)
                                .WithValueExpression(p => p.Email);
                            cols.Add("Url")
                                .WithVisibility(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }));
                        })
                    //.WithAdditionalSetting(MVCGrid.Rendering.BootstrapRenderingEngine.SettingNameTableClass, "notreal") // Example of changing table css class
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var globalSearch =
                                options.GetAdditionalQueryOptionString(
                                    "search");

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                globalSearch,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "EmployeeGrid",
                new MvcGridBuilder<Person>()
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithRetrieveDataMethod(
                        options =>
                        {
                            var result = new QueryResult<Person>();

                            using (var db = new SampleDatabaseEntities())
                            {
                                result.Items =
                                    db.People.Where(p => p.Employee).ToList();
                            }

                            return result;
                        }));

            MvcGridDefinitionTable.Add(
                "SortableGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;
                            var result = new QueryResult<Person>();

                            using (var db = new SampleDatabaseEntities())
                            {
                                var query = db.People.Where(p => p.Employee);

                                if (!string.IsNullOrWhiteSpace(
                                    options.SortColumnName))
                                {
                                    switch (options.SortColumnName.ToLower())
                                    {
                                        case "firstname":
                                            query = query.OrderBy(
                                                p => p.FirstName,
                                                options.SortDirection);
                                            break;
                                        case "lastname":
                                            query = query.OrderBy(
                                                p => p.LastName,
                                                options.SortDirection);
                                            break;
                                    }
                                }

                                result.Items = query.ToList();
                            }

                            return result;
                        }));

            MvcGridDefinitionTable.Add(
                "PagingGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var result = new QueryResult<Person>();

                            using (var db = new SampleDatabaseEntities())
                            {
                                var query = db.People.AsQueryable();

                                result.TotalRecords = query.Count();

                                if (!string.IsNullOrWhiteSpace(
                                    options.SortColumnName))
                                {
                                    switch (options.SortColumnName.ToLower())
                                    {
                                        case "firstname":
                                            query = query.OrderBy(
                                                p => p.FirstName,
                                                options.SortDirection);
                                            break;
                                        case "lastname":
                                            query = query.OrderBy(
                                                p => p.LastName,
                                                options.SortDirection);
                                            break;
                                    }
                                }

                                if (options.GetLimitOffset().HasValue)
                                {
                                    query = query
                                        .Skip(options.GetLimitOffset().Value)
                                        .Take(options.GetLimitRowcount().Value);
                                }

                                result.Items = query.ToList();
                            }

                            return result;
                        }));

            MvcGridDefinitionTable.Add(
                "DIGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.SortColumnName,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "FormattingGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("StartDate")
                                .WithHeaderText("Start Date")
                                .WithValueExpression(
                                    p => p.StartDate.HasValue
                                        ? p.StartDate.Value.ToShortDateString()
                                        : "");
                            cols.Add("ViewLink")
                                .WithSorting(false)
                                .WithHeaderText("")
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>View</a>");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.SortColumnName,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "StyledGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("StartDate")
                                .WithHeaderText("Start Date")
                                .WithValueExpression(
                                    p => p.StartDate.HasValue
                                        ? p.StartDate.Value.ToShortDateString()
                                        : "");
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                            cols.Add("Gender")
                                .WithValueExpression(p => p.Gender)
                                .WithCellCssClassExpression(
                                    p => p.Gender == "Female"
                                        ? "danger"
                                        : "warning");
                            cols.Add()
                                .WithColumnName("ViewLink")
                                .WithSorting(false)
                                .WithHeaderText("")
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>View</a>");
                        })
                    .WithRowCssClassExpression(p => p.Active ? "success" : "")
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "Preloading",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithPreloadData(false)
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "CustomLoading",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithClientSideLoadingMessageFunctionName("showLoading")
                    .WithClientSideLoadingCompleteFunctionName("hideLoading")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);

                            // pause to test loading message
                            Thread.Sleep(1000);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "Filtering",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName)
                                .WithFiltering(true);
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName)
                                .WithFiltering(true);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive")
                                .WithFiltering(true);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10, true, 100)
                    .WithFiltering(true)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            bool? active = null;
                            var fa = options.GetFilterString("Status");
                            if (!string.IsNullOrWhiteSpace(fa))
                            {
                                active =
                                    string.Compare(fa, "active", true) == 0;
                            }

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetFilterString("FirstName"),
                                options.GetFilterString("LastName"),
                                active,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "ExportGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add()
                                .WithColumnName("Id")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) =>
                                    {
                                        return string.Format(
                                            "<a href='{0}'>{1}</a>",
                                            c.UrlHelper.Action(
                                                "detail",
                                                "Demo",
                                                new
                                                {
                                                    id = p.Id
                                                }),
                                            p.Id);
                                    })
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithClientSideLoadingMessageFunctionName("showLoading")
                    .WithClientSideLoadingCompleteFunctionName("hideLoading")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "Multiple1",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>")
                                .WithPlainTextValueExpression(
                                    (p, c) => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithQueryStringPrefix("grid1")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "Multiple2",
                new MvcGridBuilder<TestItem>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Col1").WithValueExpression(p => p.Col1);
                            cols.Add("Col2").WithValueExpression(p => p.Col2);
                            cols.Add("Col3").WithValueExpression(p => p.Col3);
                        })
                    .WithSorting(true, "Col1")
                    .WithPaging(true, 10)
                    .WithQueryStringPrefix("grid2")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var repo = new TestItemRepository();
                            int totalRecords;
                            var items = repo.GetData(
                                out totalRecords,
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


            MvcGridDefinitionTable.Add(
                "CustomStyle",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>")
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 20)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "CustomRazorView",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .WithRenderingMode(RenderingMode.Controller)
                    .WithViewPath("~/Views/MVCGrid/_Custom.cshtml")
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>")
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 20)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "CustomRazorView2",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .WithRenderingMode(RenderingMode.Controller)
                    .WithViewPath("~/Views/MVCGrid/_Grid.cshtml")
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>")
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 20)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "ValueTemplate",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>",
                                    false)
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Edit")
                                .WithHtmlEncoding(false)
                                .WithSorting(false)
                                .WithHeaderText(" ")
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>");
                            cols.Add("Delete")
                                .WithHtmlEncoding(false)
                                .WithSorting(false)
                                .WithHeaderText(" ")
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}' class='btn btn-danger' role='button'>Delete</a>");
                            cols.Add("Example")
                                .WithHtmlEncoding(false)
                                .WithSorting(false)
                                .WithHeaderText("Example")
                                .WithValueExpression(
                                    (p, c) => p.Active
                                        ? "label-success"
                                        : "label-danger")
                                .WithValueTemplate(
                                    "You can access any of the item's properties: <strong>{Model.FirstName}</strong> <br />or the current column value: <span class='label {Value}'>{Model.Active}</span>");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 20)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "CustomErrorMessage",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithErrorMessageHtml(
                        @"<div class=""alert alert-danger"" role=""alert"">OH NO!!!</div>")
                    .WithSorting(true, "LastName")
                    .WithPaging(true)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var result = new QueryResult<Person>();

                            using (var db = new SampleDatabaseEntities())
                            {
                                var query = db.People.AsQueryable();

                                result.TotalRecords = query.Count();

                                if (!string.IsNullOrWhiteSpace(
                                    options.SortColumnName))
                                {
                                    switch (options.SortColumnName.ToLower())
                                    {
                                        case "firstname":
                                            throw new Exception(
                                                "Test exception");
                                        case "lastname":
                                            query = query.OrderBy(
                                                p => p.LastName,
                                                options.SortDirection);
                                            break;
                                    }
                                }

                                if (options.GetLimitOffset().HasValue)
                                {
                                    query = query
                                        .Skip(options.GetLimitOffset().Value)
                                        .Take(options.GetLimitRowcount().Value);
                                }

                                result.Items = query.ToList();
                            }

                            return result;
                        }));

            MvcGridDefinitionTable.Add(
                "UsageExample",
                new MvcGridBuilder<YourModelItem>()
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            // Add your columns here
                            cols.Add("UniqueColumnName")
                                .WithHeaderText("Any Header")
                                .WithValueExpression(
                                    i => i
                                        .YourProperty); // use the Value Expression to return the cell text for this column
                            cols.Add()
                                .WithColumnName("UrlExample")
                                .WithHeaderText("Edit")
                                .WithValueExpression(
                                    (i, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = i.YourProperty
                                        }));
                        })
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            // Query your data here. Obey Ordering, paging and filtering paramters given in the context.QueryOptions.
                            // Use Entity Framwork, a module from your IoC Container, or any other method.
                            // Return QueryResult object containing IEnumerable<YouModelItem>

                            return new QueryResult<YourModelItem>
                            {
                                Items = new List<YourModelItem>(),
                                TotalRecords =
                                    0 // if paging is enabled, return the total number of records of all pages
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "GlobalSearchGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithAdditionalQueryOptionNames("Search")
                    .WithAdditionalSetting("RenderLoadingDiv", false)
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10, true, 100)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var globalSearch =
                                options.GetAdditionalQueryOptionString(
                                    "Search");

                            var items = repo.GetData(
                                out totalRecords,
                                globalSearch,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.SortColumnName,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "PageSizeGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10, true, 100)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.SortColumnName,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "ColumnVisibilityGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("StartDate")
                                .WithHeaderText("Start Date")
                                .WithVisibility(false, true)
                                .WithValueExpression(
                                    p => p.StartDate.HasValue
                                        ? p.StartDate.Value.ToShortDateString()
                                        : "");
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithVisibility(false, true)
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive")
                                .WithCellCssClassExpression(
                                    (p, c) => p.Active ? "success" : "danger");
                            cols.Add("Gender")
                                .WithVisibility(false, true)
                                .WithValueExpression(p => p.Gender);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.SortColumnName,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "NestedObjectTest",
                new MvcGridBuilder<Job>()
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .WithPaging(true)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id", "Id", row => row.JobId.ToString());
                            cols.Add("Name", "Name", row => row.Name);

                            cols.Add("Contact")
                                .WithHeaderText("Contact")
                                .WithHtmlEncoding(false)
                                .WithSorting(true)
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
                        })
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;
                            var repo = new JobRepo();
                            int totalRecords;
                            var data = repo.GetData(
                                out totalRecords,
                                null,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                null,
                                false);

                            return new QueryResult<Job>
                            {
                                Items = data,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "PPGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .WithPageParameterNames("Active")
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithPreloadData(true)
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var ppactive =
                                options.GetPageParameterString("active");
                            var filterActive = bool.Parse(ppactive);

                            var items = repo.GetData(
                                out totalRecords,
                                null,
                                null,
                                filterActive,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "QPLGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .WithQueryOnPageLoad(false)
                    .WithPreloadData(false)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.GetSortColumnData<string>(),
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "CustomExport",
                new MvcGridBuilder<Person>(colDefauls)
                    .AddRenderingEngine(
                        "tabs",
                        typeof(TabDelimitedRenderingEngine))
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithHtmlEncoding(false)
                                .WithValueExpression(
                                    (p, c) => c.UrlHelper.Action(
                                        "detail",
                                        "Demo",
                                        new
                                        {
                                            id = p.Id
                                        }))
                                .WithValueTemplate(
                                    "<a href='{Value}'>{Model.Id}</a>")
                                .WithPlainTextValueExpression(
                                    p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                            cols.Add("Status")
                                .WithSortColumnData("Active")
                                .WithHeaderText("Status")
                                .WithValueExpression(
                                    p => p.Active ? "Active" : "Inactive");
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 20)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var sortColumn =
                                options.GetSortColumnData<string>();

                            var items = repo.GetData(
                                out totalRecords,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                sortColumn,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));

            MvcGridDefinitionTable.Add(
                "AQOGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithAdditionalQueryOptionNames(
                        "param1",
                        "param2",
                        "param3")
                    .WithAdditionalSetting("RenderLoadingDiv", false)
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10, true, 100)
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            int totalRecords;
                            var repo = DependencyResolver.Current
                                .GetService<IPersonRepository>();

                            var param1Value =
                                options.GetAdditionalQueryOptionString(
                                    "param1");
                            var param2Value =
                                options.GetAdditionalQueryOptionString(
                                    "param2");
                            var param3Value =
                                options.GetAdditionalQueryOptionString(
                                    "param3");

                            var items = repo.GetData(
                                out totalRecords,
                                null,
                                options.GetLimitOffset(),
                                options.GetLimitRowcount(),
                                options.SortColumnName,
                                options.SortDirection == SortDirection.Dsc);

                            return new QueryResult<Person>
                            {
                                Items = items,
                                TotalRecords = totalRecords
                            };
                        }));


            MvcGridDefinitionTable.Add(
                "LocalizationGrid",
                new MvcGridBuilder<Person>(colDefauls)
                    .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                    .AddColumns(
                        cols =>
                        {
                            cols.Add("Id")
                                .WithSorting(false)
                                .WithValueExpression(p => p.Id.ToString());
                            cols.Add("FirstName")
                                .WithHeaderText("First Name")
                                .WithValueExpression(p => p.FirstName);
                            cols.Add("LastName")
                                .WithHeaderText("Last Name")
                                .WithValueExpression(p => p.LastName);
                        })
                    .WithSorting(true, "LastName")
                    .WithPaging(true, 10)
                    .WithProcessingMessage("Cargando")
                    .WithNextButtonCaption("Siguiente")
                    .WithPreviousButtonCaption("Anterior")
                    .WithSummaryMessage("Mostrando {0} a {1} de {2} entradas")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            var options = context.QueryOptions;

                            var result = new QueryResult<Person>();

                            using (var db = new SampleDatabaseEntities())
                            {
                                var query = db.People.AsQueryable();

                                result.TotalRecords = query.Count();

                                if (!string.IsNullOrWhiteSpace(
                                    options.SortColumnName))
                                {
                                    switch (options.SortColumnName.ToLower())
                                    {
                                        case "firstname":
                                            query = query.OrderBy(
                                                p => p.FirstName,
                                                options.SortDirection);
                                            break;
                                        case "lastname":
                                            query = query.OrderBy(
                                                p => p.LastName,
                                                options.SortDirection);
                                            break;
                                    }
                                }

                                if (options.GetLimitOffset().HasValue)
                                {
                                    query = query
                                        .Skip(options.GetLimitOffset().Value)
                                        .Take(options.GetLimitRowcount().Value);
                                }

                                result.Items = query.ToList();
                            }

                            return result;
                        }));

            //MVCGridDefinitionTable.Add DO NOT DELETE - Needed for demo code parsing


            var def = new GridDefinition<YourModelItem>();

            var column = new GridColumn<YourModelItem>();
            column.ColumnName = "UniqueColumnName";
            column.HeaderText = "Any Header";
            column.ValueExpression = (i, c) => i.YourProperty;
            def.AddColumn(column);

            def.RetrieveData = options =>
            {
                return new QueryResult<YourModelItem>
                {
                    Items = new List<YourModelItem>(),
                    TotalRecords = 0
                };
            };
            MvcGridDefinitionTable.Add("NonFluentUsageExample", def);

            var defaultSet1 = new GridDefaults
            {
                Paging = true,
                ItemsPerPage = 20,
                Sorting = true,
                NoResultsMessage = "Sorry, no results were found"
            };

            MvcGridDefinitionTable.Add(
                "DefaultsExample",
                new MvcGridBuilder<YourModelItem>(
                        defaultSet1) // pass in defauls object to ctor
                    .AddColumns(
                        cols =>
                        {
                            // add columns
                        })
                    .WithDefaultSortColumn("Test")
                    .WithRetrieveDataMethod(
                        context =>
                        {
                            //get data
                            return new QueryResult<YourModelItem>();
                        }));


            var docsReturnTypeColumn = new GridColumn<MethodDocItem>
            {
                ColumnName = "ReturnType",
                HeaderText = "Return Type",
                HtmlEncode = false,
                ValueExpression = (p, c) => string.Format(
                    "<code>{0}</code>",
                    HttpUtility.HtmlEncode(p.Return))
            };
            var docsNameColumn = new GridColumn<MethodDocItem>
            {
                ColumnName = "Name",
                HtmlEncode = false,
                ValueExpression = (p, c) => string.Format(
                    "<code>{0}</code>",
                    HttpUtility.HtmlEncode(p.Name))
            };
            var docsDescriptionColumn = new GridColumn<MethodDocItem>
            {
                ColumnName = "Description",
                ValueExpression = (p, c) => p.Description
            };

            Func<GridContext, QueryResult<MethodDocItem>> docsLoadData =
                context =>
                {
                    var result = new QueryResult<MethodDocItem>();

                    var repo = new DocumentationRepository();
                    result.Items = repo.GetData(context.GridName);

                    return result;
                };

            MvcGridDefinitionTable.Add(
                "GridDefinition",
                new MvcGridBuilder<MethodDocItem>().AddColumn(docsNameColumn)
                    .AddColumn(docsReturnTypeColumn)
                    .AddColumn(docsDescriptionColumn)
                    .WithRetrieveDataMethod(docsLoadData));

            MvcGridDefinitionTable.Add(
                "GridColumn",
                new MvcGridBuilder<MethodDocItem>().AddColumn(docsNameColumn)
                    .AddColumn(docsReturnTypeColumn)
                    .AddColumn(docsDescriptionColumn)
                    .WithRetrieveDataMethod(docsLoadData));

            MvcGridDefinitionTable.Add(
                "QueryOptions",
                new MvcGridBuilder<MethodDocItem>().AddColumn(docsNameColumn)
                    .AddColumn(docsReturnTypeColumn)
                    .AddColumn(docsDescriptionColumn)
                    .WithRetrieveDataMethod(docsLoadData));

            MvcGridDefinitionTable.Add(
                "ClientSide",
                new MvcGridBuilder<MethodDocItem>().AddColumn(docsNameColumn)
                    .AddColumn(docsDescriptionColumn)
                    .WithRetrieveDataMethod(docsLoadData));
        }
    }
}