/*
The MIT License (MIT)
Copyright (c) 2007 - 2021 Microting A/S
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/

namespace Microting.eFormWorkflowBase.Infrastructure.Data.Factories
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class WorkflowPnContextFactory : IDesignTimeDbContextFactory<WorkflowPnDbContext>
    {
        public WorkflowPnDbContext CreateDbContext(string[] args)
        {
            const string defaultCs = "Server=localhost;port=3306;Database=workflow-pn;user=root;password=secretpassword;Convert Zero Datetime=true;";
            var optionsBuilder = new DbContextOptionsBuilder<WorkflowPnDbContext>();

            optionsBuilder.UseMySql(args.Any() ? args[0] : defaultCs, new MariaDbServerVersion(
                ServerVersion.AutoDetect(args.Any() ? args[0] : defaultCs)), mySqlOptionsAction: builder =>
            {
                builder.EnableRetryOnFailure();
            });

            return new WorkflowPnDbContext(optionsBuilder.Options);
            // dotnet ef migrations add InitialCreate --project Microting.eFormWorkflowBase --startup-project DBMigrator
        }
    }
}