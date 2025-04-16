﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronicGadgets.Util
{
    public class DBPropertyUtil
    {
        public static string GetConnectionString(string filePath)
        {
            // This will search the current working directory (debug folder)
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(filePath);

            var config = builder.Build();
            var connectionString = config.GetConnectionString("DefaultConnection");
            return connectionString ?? throw new InvalidOperationException("Connection String Not Found");
        }
    }
}
