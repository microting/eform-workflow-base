/*
The MIT License (MIT)

Copyright (c) 2007 - 2020 Microting A/S

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

using System.Linq;
using System.Text.RegularExpressions;
using Microting.eFormWorkflowBase.Infrastructure.Data;

namespace Microting.WorkOrderBase.Infrastructure.Data.Base
{
    using System;
    using System.Reflection;
    using System.Threading.Tasks;
    using eForm.Infrastructure.Constants;
    using eFormApi.BasePn.Infrastructure.Database.Base;

    public class PnBase : BaseEntity
    {
        public async Task Create(WorkflowPnDbContext dbContext)
        {
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Version = 1;
            WorkflowState = Constants.WorkflowStates.Created;

            await dbContext.AddAsync(this).ConfigureAwait(false);
            await dbContext.SaveChangesAsync().ConfigureAwait(false);

            var res = MapVersion(this);
            if (res != null)
            {
                await dbContext.AddAsync(res).ConfigureAwait(false);
                await dbContext.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task Update(WorkflowPnDbContext dbContext)
        {
            await UpdateInternal(dbContext).ConfigureAwait(false);
        }

        public async Task Delete(WorkflowPnDbContext dbContext)
        {
            await UpdateInternal(dbContext, Constants.WorkflowStates.Removed).ConfigureAwait(false);
        }

        private async Task UpdateInternal(WorkflowPnDbContext dbContext, string state = null)
        {
            if (state != null)
            {
                WorkflowState = state;
            }

            if (dbContext.ChangeTracker.HasChanges())
            {
                Version += 1;
                UpdatedAt = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();

                var res = MapVersion(this);
                if (res != null)
                {
                    await dbContext.AddAsync(res).ConfigureAwait(false);
                    await dbContext.SaveChangesAsync().ConfigureAwait(false);
                }
            }
        }

        private object MapVersion(object obj)
        {
            Type type = obj.GetType().UnderlyingSystemType;
            String className = type.Name;
            var name = obj.GetType().FullName + "Version";
            var resultType = Assembly.GetExecutingAssembly().GetType(name);
            if (resultType == null)
                return null;

            var returnObj = Activator.CreateInstance(resultType);

            var curreList = obj.GetType().GetProperties();
            foreach (var prop in curreList)
            {
                if (!prop.PropertyType.FullName.Contains("Microting.WorkOrderBase.Infrastructure.Data.Entities"))
                {
                    try
                    {
                        var propName = prop.Name;
                        if (propName != "Id")
                        {
                            var propValue = prop.GetValue(obj);
                            Type targetType = returnObj.GetType();
                            PropertyInfo targetProp = targetType.GetProperty(propName);

                            targetProp.SetValue(returnObj, propValue, null);
                        } else {
                            var propValue = prop.GetValue(obj);
                            Type targetType = returnObj.GetType();
                            PropertyInfo targetProp = targetType.GetProperty($"{className}Id");

                            targetProp.SetValue(returnObj, propValue, null);
                        }
                    }
                    catch (Exception)
                    {
                        // Console.WriteLine($"{ex.Message} - Property:{prop.Name} probably not found on Class {returnObj.GetType().Name}");
                    }
                }
            }

            return returnObj;
        }
    }
}