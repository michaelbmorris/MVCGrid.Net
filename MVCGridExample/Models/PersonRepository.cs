using System.Collections.Generic;
using System.Linq;
using MvcGrid.Web.Data;

namespace MvcGrid.Web.Models
{
    public interface IPersonRepository
    {
        IEnumerable<Person> GetData(
            out int totalRecords,
            string globalSearch,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc);

        IEnumerable<Person> GetData(
            out int totalRecords,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc);

        IEnumerable<Person> GetData(
            out int totalRecords,
            string filterFirstName,
            string filterLastName,
            bool? filterActive,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc);
    }

    public class PersonRepository : IPersonRepository
    {
        public IEnumerable<Person> GetData(
            out int totalRecords,
            string filterFirstName,
            string filterLastName,
            bool? filterActive,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            return GetData(
                out totalRecords,
                null,
                filterFirstName,
                filterLastName,
                filterActive,
                limitOffset,
                limitRowCount,
                orderBy,
                desc);
        }

        public IEnumerable<Person> GetData(
            out int totalRecords,
            string globalSearch,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            return GetData(
                out totalRecords,
                globalSearch,
                null,
                null,
                null,
                limitOffset,
                limitRowCount,
                orderBy,
                desc);
        }

        public IEnumerable<Person> GetData(
            out int totalRecords,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            return GetData(
                out totalRecords,
                null,
                null,
                null,
                limitOffset,
                limitRowCount,
                orderBy,
                desc);
        }

        public IEnumerable<Person> GetData(
            out int totalRecords,
            string globalSearch,
            string filterFirstName,
            string filterLastName,
            bool? filterActive,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            using (var db = new SampleDatabaseEntities())
            {
                var query = db.People.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filterFirstName))
                {
                    query = query.Where(
                        p => p.FirstName.Contains(filterFirstName));
                }
                if (!string.IsNullOrWhiteSpace(filterLastName))
                {
                    query = query.Where(
                        p => p.LastName.Contains(filterLastName));
                }
                if (filterActive.HasValue)
                {
                    query = query.Where(p => p.Active == filterActive.Value);
                }

                if (!string.IsNullOrWhiteSpace(globalSearch))
                {
                    query = query.Where(
                        p => (p.FirstName + " " + p.LastName).Contains(
                            globalSearch));
                }

                totalRecords = query.Count();

                if (!string.IsNullOrWhiteSpace(orderBy))
                {
                    switch (orderBy.ToLower())
                    {
                        case "firstname":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.FirstName);
                            }
                            else
                            {
                                query = query.OrderByDescending(
                                    p => p.FirstName);
                            }
                            break;
                        case "lastname":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.LastName);
                            }
                            else
                            {
                                query = query.OrderByDescending(
                                    p => p.LastName);
                            }
                            break;
                        case "active":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.Active);
                            }
                            else
                            {
                                query = query.OrderByDescending(p => p.Active);
                            }
                            break;
                        case "email":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.Email);
                            }
                            else
                            {
                                query = query.OrderByDescending(p => p.Email);
                            }
                            break;
                        case "gender":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.Gender);
                            }
                            else
                            {
                                query = query.OrderByDescending(p => p.Gender);
                            }
                            break;
                        case "id":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.Id);
                            }
                            else
                            {
                                query = query.OrderByDescending(p => p.Id);
                            }
                            break;
                        case "startdate":
                            if (!desc)
                            {
                                query = query.OrderBy(p => p.StartDate);
                            }
                            else
                            {
                                query = query.OrderByDescending(
                                    p => p.StartDate);
                            }
                            break;
                    }
                }


                if (limitOffset.HasValue)
                {
                    query = query.Skip(limitOffset.Value)
                        .Take(limitRowCount.Value);
                }

                return query.ToList();
            }
        }
    }
}