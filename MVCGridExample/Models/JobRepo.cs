﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcGrid.Web.Models
{
    public class JobRepo
    {
        private const string CacheKey = "JobRepo";
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly Random _rng = new Random();

        public IEnumerable<Job> GetData(
            out int totalRecords,
            string globalSearch,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            if (HttpContext.Current.Cache[CacheKey] == null)
            {
                var items = new List<Job>();
                var contactId = 0;
                for (var i = 1; i < 1087; i++)
                {
                    var j = new Job
                    {
                        JobId = i,
                        Name = RandomString(10)
                    };

                    var addContact = _rng.NextDouble() > 0.5;

                    if (addContact)
                    {
                        var c = new Contact();

                        contactId++;
                        j.Contact = c;
                        c.FullName = RandomString(5);
                        c.Id = contactId;
                    }

                    items.Add(j);
                }

                HttpContext.Current.Cache.Insert(CacheKey, items);
            }

            var data = (List<Job>) HttpContext.Current.Cache[CacheKey];


            var q = data.AsQueryable();

            if (!string.IsNullOrWhiteSpace(globalSearch))
            {
                q = q.Where(p => p.Name.Contains(globalSearch));
            }

            totalRecords = q.Count();

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "id":
                        if (!desc)
                        {
                            q = q.OrderBy(p => p.JobId);
                        }
                        else
                        {
                            q = q.OrderByDescending(p => p.JobId);
                        }
                        break;
                    case "name":
                        if (!desc)
                        {
                            q = q.OrderBy(p => p.Name);
                        }
                        else
                        {
                            q = q.OrderByDescending(p => p.Name);
                        }
                        break;
                    case "contact":
                        if (!desc)
                        {
                            q = q.OrderBy(p => p.Contact.FullName);
                        }
                        else
                        {
                            q = q.OrderByDescending(p => p.Contact.FullName);
                        }
                        break;
                }
            }

            if (limitOffset.HasValue)
            {
                q = q.Skip(limitOffset.Value).Take(limitRowCount.Value);
            }

            return q.ToList();
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
    }

    public class Job
    {
        public Contact Contact
        {
            get;
            set;
        }

        public int JobId
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public class Contact
    {
        public string FullName
        {
            get;
            set;
        }

        public int Id
        {
            get;
            set;
        }
    }
}