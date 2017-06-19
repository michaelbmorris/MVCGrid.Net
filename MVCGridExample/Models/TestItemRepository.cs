﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCGridExample.Models
{
    public class TestItemRepository
    {
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private readonly Random _rng = new Random();

        public IEnumerable<TestItem> GetData(
            out int totalRecords,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            return GetData(
                out totalRecords,
                null,
                limitOffset,
                limitRowCount,
                orderBy,
                desc);
        }

        public IEnumerable<TestItem> GetData(
            out int totalRecords,
            string col3Filter,
            int? limitOffset,
            int? limitRowCount,
            string orderBy,
            bool desc)
        {
            if (HttpContext.Current.Cache["TestData"] == null)
            {
                var items = new List<TestItem>();
                for (var i = 1; i < 1087; i++)
                {
                    items.Add(
                        new TestItem
                        {
                            Col1 = "Row" + i,
                            Col2 = RandomString(8),
                            Col3 = RandomString(11),
                            Col4 = RandomBool()
                        });
                }

                HttpContext.Current.Cache.Insert("TestData", items);
            }

            var data = (List<TestItem>) HttpContext.Current.Cache["TestData"];


            var q = data.AsQueryable();

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                switch (orderBy.ToLower())
                {
                    case "col1":
                        if (!desc)
                        {
                            q = q.OrderBy(p => p.Col1);
                        }
                        else
                        {
                            q = q.OrderByDescending(p => p.Col1);
                        }
                        break;
                    case "col2":
                        if (!desc)
                        {
                            q = q.OrderBy(p => p.Col2);
                        }
                        else
                        {
                            q = q.OrderByDescending(p => p.Col2);
                        }
                        break;
                    case "col3":
                        if (!desc)
                        {
                            q = q.OrderBy(p => p.Col3);
                        }
                        else
                        {
                            q = q.OrderByDescending(p => p.Col3);
                        }
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(col3Filter))
            {
                q = q.Where(p => p.Col3.Contains(col3Filter));
            }

            totalRecords = q.Count();

            if (limitOffset.HasValue)
            {
                q = q.Skip(limitOffset.Value).Take(limitRowCount.Value);
            }

            return q.ToList();
        }

        private bool RandomBool()
        {
            return _rng.Next(100) % 2 == 0;
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
}