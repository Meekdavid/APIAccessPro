﻿using APIAccessProDependencies.Helpers.ConfigurationSettings.ConfigManager;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIAccessProDependencies.Helpers.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddCustomMiddlewares(this IApplicationBuilder app)
        {
            /* TO DO: Add custom middlewares like response caching, requesting and response logging */
            return app;
        }
    }
}
