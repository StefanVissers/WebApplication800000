﻿using System.Web;
using System.Web.Mvc;

namespace WebApplication800000
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
