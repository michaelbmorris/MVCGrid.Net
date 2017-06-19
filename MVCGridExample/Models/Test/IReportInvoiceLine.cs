namespace MvcGrid.Web.Models.Test
{
    public class ReportInvoiceLine : IReportInvoiceLine
    {
        public string City
        {
            get;
            set;
        }

        public int InvoiceNumber
        {
            get;
            set;
        }

        public int Year
        {
            get;
            set;
        }
    }

    public interface IReportInvoiceLine
    {
        string City
        {
            get;
            set;
        }

        int InvoiceNumber
        {
            get;
            set;
        }

        int Year
        {
            get;
            set;
        }
    }
}