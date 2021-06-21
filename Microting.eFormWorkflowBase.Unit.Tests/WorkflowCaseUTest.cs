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

namespace Microting.eFormWorkflowBase.Unit.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using eForm.Infrastructure.Constants;
    using Infrastructure.Const;
    using Infrastructure.Data.Entities;
    using Microsoft.EntityFrameworkCore;
    using NUnit.Framework;

    [TestFixture]
    public class WorkflowCaseUTest : DbTestFixture
    {
        [Test]
        public async Task WorkflowCase_Save_DoesSave()
        {
            // Arrange
            var workflowCase = new WorkflowCase
            {
                ActionPlan = Guid.NewGuid().ToString(),
                CheckId = 0,
                CheckMicrotingUid = 0,
                DateOfIncident = DateTime.Now,
                Deadline = DateTime.Now.AddDays(1),
                Description = Guid.NewGuid().ToString(),
                IncidentPlace = Guid.NewGuid().ToString(),
                IncidentType = Guid.NewGuid().ToString(),
                PhotosExist = false,
                SolvedBy = Guid.NewGuid().ToString(),
                Status = "Closed",
                MicrotingId = 0,
                UpdatedByUserId = 1,
                CreatedByUserId = 1,
            };

            await workflowCase.Create(DbContext);
            
            // Act
            var workflowCases = DbContext.WorkflowCases.AsNoTracking().ToList();
            var workflowCaseVersions = DbContext.WorkflowCaseVersions.AsNoTracking().ToList();
            
            // Assert
            Assert.AreEqual(1, workflowCases.Count);
            Assert.AreEqual(1, workflowCaseVersions.Count);

            Assert.AreEqual(workflowCase.Id, workflowCases[0].Id);
            Assert.AreEqual(workflowCase.ActionPlan, workflowCases[0].ActionPlan);
            Assert.AreEqual(workflowCase.CheckId, workflowCases[0].CheckId);
            Assert.AreEqual(workflowCase.CheckMicrotingUid, workflowCases[0].CheckMicrotingUid);
            Assert.AreEqual(workflowCase.DateOfIncident.ToString("s"), workflowCases[0].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCase.Deadline.ToString("s"), workflowCases[0].Deadline.ToString("s"));
            Assert.AreEqual(workflowCase.Description, workflowCases[0].Description);
            Assert.AreEqual(workflowCase.IncidentPlace, workflowCases[0].IncidentPlace);
            Assert.AreEqual(workflowCase.IncidentType, workflowCases[0].IncidentType);
            Assert.AreEqual(workflowCase.MicrotingId, workflowCases[0].MicrotingId);
            Assert.AreEqual(workflowCase.PhotosExist, workflowCases[0].PhotosExist);
            Assert.AreEqual(workflowCase.SolvedBy, workflowCases[0].SolvedBy);
            Assert.AreEqual(workflowCase.Status, workflowCases[0].Status);
            Assert.AreEqual(workflowCase.CreatedByUserId, workflowCases[0].CreatedByUserId);
            Assert.AreEqual(workflowCase.UpdatedByUserId, workflowCases[0].UpdatedByUserId);
            Assert.AreEqual(workflowCase.CreatedAt.ToString("s"), workflowCases[0].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCase.UpdatedAt!.Value.ToString("s"), workflowCases[0].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(1, workflowCases[0].Version);
            Assert.AreEqual(workflowCase.Version, workflowCases[0].Version);
            Assert.AreEqual(Constants.WorkflowStates.Created, workflowCases[0].WorkflowState);
            Assert.AreEqual(workflowCase.WorkflowState, workflowCases[0].WorkflowState);
            
            // versions
            Assert.AreEqual(workflowCase.Id, workflowCaseVersions[0].WorkflowCaseId);
            Assert.AreEqual(workflowCase.ActionPlan, workflowCaseVersions[0].ActionPlan);
            Assert.AreEqual(workflowCase.CheckId, workflowCaseVersions[0].CheckId);
            Assert.AreEqual(workflowCase.CheckMicrotingUid, workflowCaseVersions[0].CheckMicrotingUid);
            Assert.AreEqual(workflowCase.DateOfIncident.ToString("s"), workflowCaseVersions[0].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCase.Deadline.ToString("s"), workflowCaseVersions[0].Deadline.ToString("s"));
            Assert.AreEqual(workflowCase.Description, workflowCaseVersions[0].Description);
            Assert.AreEqual(workflowCase.IncidentPlace, workflowCaseVersions[0].IncidentPlace);
            Assert.AreEqual(workflowCase.IncidentType, workflowCaseVersions[0].IncidentType);
            Assert.AreEqual(workflowCase.MicrotingId, workflowCaseVersions[0].MicrotingId);
            Assert.AreEqual(workflowCase.PhotosExist, workflowCaseVersions[0].PhotosExist);
            Assert.AreEqual(workflowCase.SolvedBy, workflowCaseVersions[0].SolvedBy);
            Assert.AreEqual(workflowCase.Status, workflowCaseVersions[0].Status);
            Assert.AreEqual(workflowCase.CreatedByUserId, workflowCaseVersions[0].CreatedByUserId);
            Assert.AreEqual(workflowCase.UpdatedByUserId, workflowCaseVersions[0].UpdatedByUserId);
            Assert.AreEqual(workflowCase.CreatedAt.ToString("s"), workflowCaseVersions[0].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCase.UpdatedAt!.Value.ToString("s"), workflowCaseVersions[0].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(1, workflowCaseVersions[0].Version);
            Assert.AreEqual(workflowCase.Version, workflowCaseVersions[0].Version);
            Assert.AreEqual(Constants.WorkflowStates.Created, workflowCaseVersions[0].WorkflowState);
            Assert.AreEqual(workflowCase.WorkflowState, workflowCaseVersions[0].WorkflowState);
        }

        [Test]
        public async Task WorkflowCase_Update_DoesUpdate()
        {
            // Arrange
            var workflowCase = new WorkflowCase
            {
                ActionPlan = Guid.NewGuid().ToString(),
                CheckId = 0,
                CheckMicrotingUid = 0,
                DateOfIncident = DateTime.Now,
                Deadline = DateTime.Now.AddDays(1),
                Description = Guid.NewGuid().ToString(),
                IncidentPlace = Guid.NewGuid().ToString(),
                IncidentType = Guid.NewGuid().ToString(),
                PhotosExist = false,
                SolvedBy = Guid.NewGuid().ToString(),
                Status = "Closed",
                MicrotingId = 0,
                UpdatedByUserId = 1,
                CreatedByUserId = 1,
            };

            await workflowCase.Create(DbContext);
            var workflowCaseAfterCreate = await DbContext.WorkflowCases.AsNoTracking().FirstAsync();
            // Act

            workflowCase.ActionPlan = Guid.NewGuid().ToString();
            workflowCase.CheckId = 1;
            workflowCase.CheckMicrotingUid = 1;
            workflowCase.DateOfIncident = DateTime.Now;
            workflowCase.Deadline = DateTime.Now.AddDays(1);
            workflowCase.Description = Guid.NewGuid().ToString();
            workflowCase.IncidentPlace = Guid.NewGuid().ToString();
            workflowCase.IncidentType = Guid.NewGuid().ToString();
            workflowCase.PhotosExist = false;
            workflowCase.SolvedBy = Guid.NewGuid().ToString();
            workflowCase.Status = "Closed";
            workflowCase.MicrotingId = 1;
            workflowCase.UpdatedByUserId = 2;
            workflowCase.CreatedByUserId = 2;

            await workflowCase.Update(DbContext);

            var workflowCases = DbContext.WorkflowCases.AsNoTracking().ToList();
            var workflowCaseVersions = DbContext.WorkflowCaseVersions.AsNoTracking().ToList();

            // Assert
            Assert.AreEqual(1, workflowCases.Count);
            Assert.AreEqual(2, workflowCaseVersions.Count);
            Assert.AreEqual(workflowCase.Id, workflowCases[0].Id);
            Assert.AreEqual(workflowCase.ActionPlan, workflowCases[0].ActionPlan);
            Assert.AreEqual(workflowCase.CheckId, workflowCases[0].CheckId);
            Assert.AreEqual(workflowCase.CheckMicrotingUid, workflowCases[0].CheckMicrotingUid);
            Assert.AreEqual(workflowCase.DateOfIncident.ToString("s"), workflowCases[0].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCase.Deadline.ToString("s"), workflowCases[0].Deadline.ToString("s"));
            Assert.AreEqual(workflowCase.Description, workflowCases[0].Description);
            Assert.AreEqual(workflowCase.IncidentPlace, workflowCases[0].IncidentPlace);
            Assert.AreEqual(workflowCase.IncidentType, workflowCases[0].IncidentType);
            Assert.AreEqual(workflowCase.MicrotingId, workflowCases[0].MicrotingId);
            Assert.AreEqual(workflowCase.PhotosExist, workflowCases[0].PhotosExist);
            Assert.AreEqual(workflowCase.SolvedBy, workflowCases[0].SolvedBy);
            Assert.AreEqual(workflowCase.Status, workflowCases[0].Status);
            Assert.AreEqual(workflowCase.CreatedByUserId, workflowCases[0].CreatedByUserId);
            Assert.AreEqual(workflowCase.UpdatedByUserId, workflowCases[0].UpdatedByUserId);
            Assert.AreEqual(workflowCase.CreatedAt.ToString("s"), workflowCases[0].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCase.UpdatedAt!.Value.ToString("s"), workflowCases[0].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(2, workflowCases[0].Version);
            Assert.AreEqual(workflowCase.Version, workflowCases[0].Version);
            Assert.AreEqual(Constants.WorkflowStates.Created, workflowCases[0].WorkflowState);
            Assert.AreEqual(workflowCase.WorkflowState, workflowCases[0].WorkflowState);

            // versions [0]
            Assert.AreEqual(workflowCaseAfterCreate.Id, workflowCaseVersions[0].WorkflowCaseId);
            Assert.AreEqual(workflowCaseAfterCreate.ActionPlan, workflowCaseVersions[0].ActionPlan);
            Assert.AreEqual(workflowCaseAfterCreate.CheckId, workflowCaseVersions[0].CheckId);
            Assert.AreEqual(workflowCaseAfterCreate.CheckMicrotingUid, workflowCaseVersions[0].CheckMicrotingUid);
            Assert.AreEqual(workflowCaseAfterCreate.DateOfIncident.ToString("s"), workflowCaseVersions[0].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCaseAfterCreate.Deadline.ToString("s"), workflowCaseVersions[0].Deadline.ToString("s"));
            Assert.AreEqual(workflowCaseAfterCreate.Description, workflowCaseVersions[0].Description);
            Assert.AreEqual(workflowCaseAfterCreate.IncidentPlace, workflowCaseVersions[0].IncidentPlace);
            Assert.AreEqual(workflowCaseAfterCreate.IncidentType, workflowCaseVersions[0].IncidentType);
            Assert.AreEqual(workflowCaseAfterCreate.MicrotingId, workflowCaseVersions[0].MicrotingId);
            Assert.AreEqual(workflowCaseAfterCreate.PhotosExist, workflowCaseVersions[0].PhotosExist);
            Assert.AreEqual(workflowCaseAfterCreate.SolvedBy, workflowCaseVersions[0].SolvedBy);
            Assert.AreEqual(workflowCaseAfterCreate.Status, workflowCaseVersions[0].Status);
            Assert.AreEqual(workflowCaseAfterCreate.CreatedByUserId, workflowCaseVersions[0].CreatedByUserId);
            Assert.AreEqual(workflowCaseAfterCreate.UpdatedByUserId, workflowCaseVersions[0].UpdatedByUserId);
            Assert.AreEqual(workflowCaseAfterCreate.CreatedAt.ToString("s"), workflowCaseVersions[0].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCaseAfterCreate.UpdatedAt!.Value.ToString("s"), workflowCaseVersions[0].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(1, workflowCaseVersions[0].Version);
            Assert.AreEqual(workflowCaseAfterCreate.Version, workflowCaseVersions[0].Version);
            Assert.AreEqual(Constants.WorkflowStates.Created, workflowCaseVersions[0].WorkflowState);
            Assert.AreEqual(workflowCaseAfterCreate.WorkflowState, workflowCaseVersions[0].WorkflowState);

            // versions [1]
            Assert.AreEqual(workflowCase.Id, workflowCaseVersions[1].WorkflowCaseId);
            Assert.AreEqual(workflowCase.ActionPlan, workflowCaseVersions[1].ActionPlan);
            Assert.AreEqual(workflowCase.CheckId, workflowCaseVersions[1].CheckId);
            Assert.AreEqual(workflowCase.CheckMicrotingUid, workflowCaseVersions[1].CheckMicrotingUid);
            Assert.AreEqual(workflowCase.DateOfIncident.ToString("s"), workflowCaseVersions[1].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCase.Deadline.ToString("s"), workflowCaseVersions[1].Deadline.ToString("s"));
            Assert.AreEqual(workflowCase.Description, workflowCaseVersions[1].Description);
            Assert.AreEqual(workflowCase.IncidentPlace, workflowCaseVersions[1].IncidentPlace);
            Assert.AreEqual(workflowCase.IncidentType, workflowCaseVersions[1].IncidentType);
            Assert.AreEqual(workflowCase.MicrotingId, workflowCaseVersions[1].MicrotingId);
            Assert.AreEqual(workflowCase.PhotosExist, workflowCaseVersions[1].PhotosExist);
            Assert.AreEqual(workflowCase.SolvedBy, workflowCaseVersions[1].SolvedBy);
            Assert.AreEqual(workflowCase.Status, workflowCaseVersions[1].Status);
            Assert.AreEqual(workflowCase.CreatedByUserId, workflowCaseVersions[1].CreatedByUserId);
            Assert.AreEqual(workflowCase.UpdatedByUserId, workflowCaseVersions[1].UpdatedByUserId);
            Assert.AreEqual(workflowCase.CreatedAt.ToString("s"), workflowCaseVersions[1].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCase.UpdatedAt!.Value.ToString("s"), workflowCaseVersions[1].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(2, workflowCaseVersions[1].Version);
            Assert.AreEqual(workflowCase.Version, workflowCaseVersions[1].Version);
            Assert.AreEqual(Constants.WorkflowStates.Created, workflowCaseVersions[1].WorkflowState);
            Assert.AreEqual(workflowCase.WorkflowState, workflowCaseVersions[1].WorkflowState);
        }

        [Test]
        public async Task WorkflowCase_Delete_DoesDelete()
        {
            // Arrange
            var workflowCase = new WorkflowCase
            {
                ActionPlan = Guid.NewGuid().ToString(),
                CheckId = 0,
                CheckMicrotingUid = 0,
                DateOfIncident = DateTime.Now,
                Deadline = DateTime.Now.AddDays(1),
                Description = Guid.NewGuid().ToString(),
                IncidentPlace = Guid.NewGuid().ToString(),
                IncidentType = Guid.NewGuid().ToString(),
                PhotosExist = false,
                SolvedBy = Guid.NewGuid().ToString(),
                Status = "Closed",
                MicrotingId = 0,
                UpdatedByUserId = 1,
                CreatedByUserId = 1,
            };

            await workflowCase.Create(DbContext);
            // Act

            await workflowCase.Delete(DbContext);

            var workflowCases = DbContext.WorkflowCases.AsNoTracking().ToList();
            var workflowCaseVersions = DbContext.WorkflowCaseVersions.AsNoTracking().ToList();

            // Assert
            Assert.AreEqual(1, workflowCases.Count);
            Assert.AreEqual(2, workflowCaseVersions.Count);

            Assert.AreEqual(workflowCase.Id, workflowCases[0].Id);
            Assert.AreEqual(workflowCase.ActionPlan, workflowCases[0].ActionPlan);
            Assert.AreEqual(workflowCase.CheckId, workflowCases[0].CheckId);
            Assert.AreEqual(workflowCase.CheckMicrotingUid, workflowCases[0].CheckMicrotingUid);
            Assert.AreEqual(workflowCase.DateOfIncident.ToString("s"), workflowCases[0].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCase.Deadline.ToString("s"), workflowCases[0].Deadline.ToString("s"));
            Assert.AreEqual(workflowCase.Description, workflowCases[0].Description);
            Assert.AreEqual(workflowCase.IncidentPlace, workflowCases[0].IncidentPlace);
            Assert.AreEqual(workflowCase.IncidentType, workflowCases[0].IncidentType);
            Assert.AreEqual(workflowCase.MicrotingId, workflowCases[0].MicrotingId);
            Assert.AreEqual(workflowCase.PhotosExist, workflowCases[0].PhotosExist);
            Assert.AreEqual(workflowCase.SolvedBy, workflowCases[0].SolvedBy);
            Assert.AreEqual(workflowCase.Status, workflowCases[0].Status);
            Assert.AreEqual(workflowCase.CreatedByUserId, workflowCases[0].CreatedByUserId);
            Assert.AreEqual(workflowCase.UpdatedByUserId, workflowCases[0].UpdatedByUserId);
            Assert.AreEqual(workflowCase.CreatedAt.ToString("s"), workflowCases[0].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCase.UpdatedAt!.Value.ToString("s"), workflowCases[0].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(2, workflowCases[0].Version);
            Assert.AreEqual(workflowCase.Version, workflowCases[0].Version);
            Assert.AreEqual(Constants.WorkflowStates.Removed, workflowCases[0].WorkflowState);
            Assert.AreEqual(workflowCase.WorkflowState, workflowCases[0].WorkflowState);

            // versions [1]
            Assert.AreEqual(workflowCase.Id, workflowCaseVersions[1].WorkflowCaseId);
            Assert.AreEqual(workflowCase.ActionPlan, workflowCaseVersions[1].ActionPlan);
            Assert.AreEqual(workflowCase.CheckId, workflowCaseVersions[1].CheckId);
            Assert.AreEqual(workflowCase.CheckMicrotingUid, workflowCaseVersions[1].CheckMicrotingUid);
            Assert.AreEqual(workflowCase.DateOfIncident.ToString("s"), workflowCaseVersions[1].DateOfIncident.ToString("s"));
            Assert.AreEqual(workflowCase.Deadline.ToString("s"), workflowCaseVersions[1].Deadline.ToString("s"));
            Assert.AreEqual(workflowCase.Description, workflowCaseVersions[1].Description);
            Assert.AreEqual(workflowCase.IncidentPlace, workflowCaseVersions[1].IncidentPlace);
            Assert.AreEqual(workflowCase.IncidentType, workflowCaseVersions[1].IncidentType);
            Assert.AreEqual(workflowCase.MicrotingId, workflowCaseVersions[1].MicrotingId);
            Assert.AreEqual(workflowCase.PhotosExist, workflowCaseVersions[1].PhotosExist);
            Assert.AreEqual(workflowCase.SolvedBy, workflowCaseVersions[1].SolvedBy);
            Assert.AreEqual(workflowCase.Status, workflowCaseVersions[1].Status);
            Assert.AreEqual(workflowCase.CreatedByUserId, workflowCaseVersions[1].CreatedByUserId);
            Assert.AreEqual(workflowCase.UpdatedByUserId, workflowCaseVersions[1].UpdatedByUserId);
            Assert.AreEqual(workflowCase.CreatedAt.ToString("s"), workflowCaseVersions[1].CreatedAt.ToString("s"));
            Assert.AreEqual(workflowCase.UpdatedAt!.Value.ToString("s"), workflowCaseVersions[1].UpdatedAt!.Value.ToString("s"));
            Assert.AreEqual(2, workflowCaseVersions[1].Version);
            Assert.AreEqual(workflowCase.Version, workflowCaseVersions[1].Version);
            Assert.AreEqual(Constants.WorkflowStates.Removed, workflowCaseVersions[1].WorkflowState);
            Assert.AreEqual(workflowCase.WorkflowState, workflowCaseVersions[1].WorkflowState);
        }
    }
}