using System;
using System.Collections.Generic;
using System.Configuration;
using MichaelBrandonMorris.MvcGrid.Interfaces;
using MichaelBrandonMorris.MvcGrid.Templating;

namespace MichaelBrandonMorris.MvcGrid.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class GridDefaults : IMvcGridDefinition
    {
        /// <summary>
        /// 
        /// </summary>
        public GridDefaults()
        {
            PreloadData = true;
            QueryOnPageLoad = true;
            Paging = false;
            ItemsPerPage = 20;
            Sorting = false;
            DefaultSortColumn = null;
            DefaultSortDirection = SortDirection.Unspecified;
            NoResultsMessage = "No results.";
            NextButtonCaption = "Next";
            PreviousButtonCaption = "Previous";
            SummaryMessage = "Showing {0} to {1} of {2} entries";
            ProcessingMessage = "Processing";
            ClientSideLoadingMessageFunctionName = null;
            ClientSideLoadingCompleteFunctionName = null;
            Filtering = false;
            //RenderingEngine = typeof(MVCGrid.Rendering.BootstrapRenderingEngine);
            TemplatingEngine = typeof(SimpleTemplatingEngine);
            AdditionalSettings = new Dictionary<string, object>();
            RenderingMode = RenderingMode.RenderingEngine;
            ViewPath = "~/Views/MVCGrid/_Grid.cshtml";
            ContainerViewPath = null;
            ErrorMessageHtml =
                @"<div class=""alert alert-warning"" role=""alert"">There was a problem loading the grid.</div>";
            AdditionalQueryOptionNames = new HashSet<string>();
            PageParameterNames = new HashSet<string>();
            AllowChangingPageSize = false;
            MaxItemsPerPage = null;
            AuthorizationType = AuthorizationType.AllowAnonymous;

            RenderingEngines = new ProviderSettingsCollection
            {
                new ProviderSettings(
                    "BootstrapRenderingEngine",
                    "MVCGrid.Rendering.BootstrapRenderingEngine, MVCGrid"),
                new ProviderSettings(
                    "Export",
                    "MVCGrid.Rendering.CsvRenderingEngine, MVCGrid")
            };

            DefaultRenderingEngineName = "BootstrapRenderingEngine";
        }

        /// <summary>
        /// 
        /// </summary>
        public HashSet<string> AdditionalQueryOptionNames
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, object> AdditionalSettings
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool AllowChangingPageSize
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public AuthorizationType AuthorizationType
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ClientSideLoadingCompleteFunctionName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ClientSideLoadingMessageFunctionName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ContainerViewPath
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultRenderingEngineName
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultSortColumn
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public SortDirection DefaultSortDirection
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessageHtml
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Filtering
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetAdditionalSetting<T>(string name, T defaultValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMvcGridColumn> GetColumns()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public int ItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int? MaxItemsPerPage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string NextButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string NoResultsMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public HashSet<string> PageParameterNames
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Paging
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool PreloadData
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PreviousButtonCaption
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ProcessingMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool QueryOnPageLoad
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string QueryStringPrefix
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public ProviderSettingsCollection RenderingEngines
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public RenderingMode RenderingMode
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool Sorting
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string SummaryMessage
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public Type TemplatingEngine
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public string ViewPath
        {
            get;
            set;
        }
    }
}