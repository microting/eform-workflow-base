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
            Assert.That(workflowCases.Count, Is.EqualTo(1));
            Assert.That(workflowCaseVersions.Count, Is.EqualTo(1));

            Assert.That(workflowCases[0].Id, Is.EqualTo(workflowCase.Id));
            Assert.That(workflowCases[0].ActionPlan, Is.EqualTo(workflowCase.ActionPlan));
            Assert.That(workflowCases[0].CheckId, Is.EqualTo(workflowCase.CheckId));
            Assert.That(workflowCases[0].CheckMicrotingUid, Is.EqualTo(workflowCase.CheckMicrotingUid));
            Assert.That(workflowCases[0].DateOfIncident.ToString("s"), Is.EqualTo(workflowCase.DateOfIncident.ToString("s")));
            Assert.That(workflowCases[0].Deadline.ToString(), Is.EqualTo(workflowCase.Deadline.ToString()));
            Assert.That(workflowCases[0].Description, Is.EqualTo(workflowCase.Description));
            Assert.That(workflowCases[0].IncidentPlace, Is.EqualTo(workflowCase.IncidentPlace));
            Assert.That(workflowCases[0].IncidentType, Is.EqualTo(workflowCase.IncidentType));
            Assert.That(workflowCases[0].MicrotingId, Is.EqualTo(workflowCase.MicrotingId));
            Assert.That(workflowCases[0].PhotosExist, Is.EqualTo(workflowCase.PhotosExist));
            Assert.That(workflowCases[0].SolvedBy, Is.EqualTo(workflowCase.SolvedBy));
            Assert.That(workflowCases[0].Status, Is.EqualTo(workflowCase.Status));
            Assert.That(workflowCases[0].CreatedByUserId, Is.EqualTo(workflowCase.CreatedByUserId));
            Assert.That(workflowCases[0].UpdatedByUserId, Is.EqualTo(workflowCase.UpdatedByUserId));
            Assert.That(workflowCases[0].CreatedAt.ToString("s"), Is.EqualTo(workflowCase.CreatedAt.ToString("s")));
            Assert.That(workflowCases[0].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCase.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCases[0].Version, Is.EqualTo(1));
            Assert.That(workflowCases[0].Version, Is.EqualTo(workflowCase.Version));
            Assert.That(workflowCases[0].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Created));
            Assert.That(workflowCases[0].WorkflowState, Is.EqualTo(workflowCase.WorkflowState));

            // versions
            Assert.That(workflowCaseVersions[0].WorkflowCaseId, Is.EqualTo(workflowCase.Id));
            Assert.That(workflowCaseVersions[0].ActionPlan, Is.EqualTo(workflowCase.ActionPlan));
            Assert.That(workflowCaseVersions[0].CheckId, Is.EqualTo(workflowCase.CheckId));
            Assert.That(workflowCaseVersions[0].CheckMicrotingUid, Is.EqualTo(workflowCase.CheckMicrotingUid));
            Assert.That(workflowCaseVersions[0].DateOfIncident.ToString("s"), Is.EqualTo(workflowCase.DateOfIncident.ToString("s")));
            Assert.That(workflowCaseVersions[0].Deadline.ToString(), Is.EqualTo(workflowCase.Deadline.ToString()));
            Assert.That(workflowCaseVersions[0].Description, Is.EqualTo(workflowCase.Description));
            Assert.That(workflowCaseVersions[0].IncidentPlace, Is.EqualTo(workflowCase.IncidentPlace));
            Assert.That(workflowCaseVersions[0].IncidentType, Is.EqualTo(workflowCase.IncidentType));
            Assert.That(workflowCaseVersions[0].MicrotingId, Is.EqualTo(workflowCase.MicrotingId));
            Assert.That(workflowCaseVersions[0].PhotosExist, Is.EqualTo(workflowCase.PhotosExist));
            Assert.That(workflowCaseVersions[0].SolvedBy, Is.EqualTo(workflowCase.SolvedBy));
            Assert.That(workflowCaseVersions[0].Status, Is.EqualTo(workflowCase.Status));
            Assert.That(workflowCaseVersions[0].CreatedByUserId, Is.EqualTo(workflowCase.CreatedByUserId));
            Assert.That(workflowCaseVersions[0].UpdatedByUserId, Is.EqualTo(workflowCase.UpdatedByUserId));
            Assert.That(workflowCaseVersions[0].CreatedAt.ToString("s"), Is.EqualTo(workflowCase.CreatedAt.ToString("s")));
            Assert.That(workflowCaseVersions[0].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCase.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCaseVersions[0].Version, Is.EqualTo(1));
            Assert.That(workflowCaseVersions[0].Version, Is.EqualTo(workflowCase.Version));
            Assert.That(workflowCaseVersions[0].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Created));
            Assert.That(workflowCaseVersions[0].WorkflowState, Is.EqualTo(workflowCase.WorkflowState));
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
            Assert.That(workflowCases.Count, Is.EqualTo(1));
            Assert.That(workflowCaseVersions.Count, Is.EqualTo(2));
            Assert.That(workflowCases[0].Id, Is.EqualTo(workflowCase.Id));
            Assert.That(workflowCases[0].ActionPlan, Is.EqualTo(workflowCase.ActionPlan));
            Assert.That(workflowCases[0].CheckId, Is.EqualTo(workflowCase.CheckId));
            Assert.That(workflowCases[0].CheckMicrotingUid, Is.EqualTo(workflowCase.CheckMicrotingUid));
            Assert.That(workflowCases[0].DateOfIncident.ToString("s"), Is.EqualTo(workflowCase.DateOfIncident.ToString("s")));
            Assert.That(workflowCases[0].Deadline.ToString(), Is.EqualTo(workflowCase.Deadline.ToString()));
            Assert.That(workflowCases[0].Description, Is.EqualTo(workflowCase.Description));
            Assert.That(workflowCases[0].IncidentPlace, Is.EqualTo(workflowCase.IncidentPlace));
            Assert.That(workflowCases[0].IncidentType, Is.EqualTo(workflowCase.IncidentType));
            Assert.That(workflowCases[0].MicrotingId, Is.EqualTo(workflowCase.MicrotingId));
            Assert.That(workflowCases[0].PhotosExist, Is.EqualTo(workflowCase.PhotosExist));
            Assert.That(workflowCases[0].SolvedBy, Is.EqualTo(workflowCase.SolvedBy));
            Assert.That(workflowCases[0].Status, Is.EqualTo(workflowCase.Status));
            Assert.That(workflowCases[0].CreatedByUserId, Is.EqualTo(workflowCase.CreatedByUserId));
            Assert.That(workflowCases[0].UpdatedByUserId, Is.EqualTo(workflowCase.UpdatedByUserId));
            Assert.That(workflowCases[0].CreatedAt.ToString("s"), Is.EqualTo(workflowCase.CreatedAt.ToString("s")));
            Assert.That(workflowCases[0].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCase.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCases[0].Version, Is.EqualTo(2));
            Assert.That(workflowCases[0].Version, Is.EqualTo(workflowCase.Version));
            Assert.That(workflowCases[0].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Created));
            Assert.That(workflowCases[0].WorkflowState, Is.EqualTo(workflowCase.WorkflowState));

            // versions [0]
            Assert.That(workflowCaseVersions[0].WorkflowCaseId, Is.EqualTo(workflowCaseAfterCreate.Id));
            Assert.That(workflowCaseVersions[0].ActionPlan, Is.EqualTo(workflowCaseAfterCreate.ActionPlan));
            Assert.That(workflowCaseVersions[0].CheckId, Is.EqualTo(workflowCaseAfterCreate.CheckId));
            Assert.That(workflowCaseVersions[0].CheckMicrotingUid, Is.EqualTo(workflowCaseAfterCreate.CheckMicrotingUid));
            Assert.That(workflowCaseVersions[0].DateOfIncident.ToString("s"), Is.EqualTo(workflowCaseAfterCreate.DateOfIncident.ToString("s")));
            Assert.That(workflowCaseVersions[0].Deadline.ToString(), Is.EqualTo(workflowCaseAfterCreate.Deadline.ToString()));
            Assert.That(workflowCaseVersions[0].Description, Is.EqualTo(workflowCaseAfterCreate.Description));
            Assert.That(workflowCaseVersions[0].IncidentPlace, Is.EqualTo(workflowCaseAfterCreate.IncidentPlace));
            Assert.That(workflowCaseVersions[0].IncidentType, Is.EqualTo(workflowCaseAfterCreate.IncidentType));
            Assert.That(workflowCaseVersions[0].MicrotingId, Is.EqualTo(workflowCaseAfterCreate.MicrotingId));
            Assert.That(workflowCaseVersions[0].PhotosExist, Is.EqualTo(workflowCaseAfterCreate.PhotosExist));
            Assert.That(workflowCaseVersions[0].SolvedBy, Is.EqualTo(workflowCaseAfterCreate.SolvedBy));
            Assert.That(workflowCaseVersions[0].Status, Is.EqualTo(workflowCaseAfterCreate.Status));
            Assert.That(workflowCaseVersions[0].CreatedByUserId, Is.EqualTo(workflowCaseAfterCreate.CreatedByUserId));
            Assert.That(workflowCaseVersions[0].UpdatedByUserId, Is.EqualTo(workflowCaseAfterCreate.UpdatedByUserId));
            Assert.That(workflowCaseVersions[0].CreatedAt.ToString("s"), Is.EqualTo(workflowCaseAfterCreate.CreatedAt.ToString("s")));
            Assert.That(workflowCaseVersions[0].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCaseAfterCreate.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCaseVersions[0].Version, Is.EqualTo(1));
            Assert.That(workflowCaseVersions[0].Version, Is.EqualTo(workflowCaseAfterCreate.Version));
            Assert.That(workflowCaseVersions[0].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Created));
            Assert.That(workflowCaseVersions[0].WorkflowState, Is.EqualTo(workflowCaseAfterCreate.WorkflowState));

            // versions [1]
            Assert.That(workflowCaseVersions[1].WorkflowCaseId, Is.EqualTo(workflowCase.Id));
            Assert.That(workflowCaseVersions[1].ActionPlan, Is.EqualTo(workflowCase.ActionPlan));
            Assert.That(workflowCaseVersions[1].CheckId, Is.EqualTo(workflowCase.CheckId));
            Assert.That(workflowCaseVersions[1].CheckMicrotingUid, Is.EqualTo(workflowCase.CheckMicrotingUid));
            Assert.That(workflowCaseVersions[1].DateOfIncident.ToString("s"), Is.EqualTo(workflowCase.DateOfIncident.ToString("s")));
            Assert.That(workflowCaseVersions[1].Deadline.ToString(), Is.EqualTo(workflowCase.Deadline.ToString()));
            Assert.That(workflowCaseVersions[1].Description, Is.EqualTo(workflowCase.Description));
            Assert.That(workflowCaseVersions[1].IncidentPlace, Is.EqualTo(workflowCase.IncidentPlace));
            Assert.That(workflowCaseVersions[1].IncidentType, Is.EqualTo(workflowCase.IncidentType));
            Assert.That(workflowCaseVersions[1].MicrotingId, Is.EqualTo(workflowCase.MicrotingId));
            Assert.That(workflowCaseVersions[1].PhotosExist, Is.EqualTo(workflowCase.PhotosExist));
            Assert.That(workflowCaseVersions[1].SolvedBy, Is.EqualTo(workflowCase.SolvedBy));
            Assert.That(workflowCaseVersions[1].Status, Is.EqualTo(workflowCase.Status));
            Assert.That(workflowCaseVersions[1].CreatedByUserId, Is.EqualTo(workflowCase.CreatedByUserId));
            Assert.That(workflowCaseVersions[1].UpdatedByUserId, Is.EqualTo(workflowCase.UpdatedByUserId));
            Assert.That(workflowCaseVersions[1].CreatedAt.ToString("s"), Is.EqualTo(workflowCase.CreatedAt.ToString("s")));
            Assert.That(workflowCaseVersions[1].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCase.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCaseVersions[1].Version, Is.EqualTo(2));
            Assert.That(workflowCaseVersions[1].Version, Is.EqualTo(workflowCase.Version));
            Assert.That(workflowCaseVersions[1].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Created));
            Assert.That(workflowCaseVersions[1].WorkflowState, Is.EqualTo(workflowCase.WorkflowState));
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
            Assert.That(workflowCases.Count, Is.EqualTo(1));
            Assert.That(workflowCaseVersions.Count, Is.EqualTo(2));

            Assert.That(workflowCases[0].Id, Is.EqualTo(workflowCase.Id));
            Assert.That(workflowCases[0].ActionPlan, Is.EqualTo(workflowCase.ActionPlan));
            Assert.That(workflowCases[0].CheckId, Is.EqualTo(workflowCase.CheckId));
            Assert.That(workflowCases[0].CheckMicrotingUid, Is.EqualTo(workflowCase.CheckMicrotingUid));
            Assert.That(workflowCases[0].DateOfIncident.ToString("s"), Is.EqualTo(workflowCase.DateOfIncident.ToString("s")));
            Assert.That(workflowCases[0].Deadline.ToString(), Is.EqualTo(workflowCase.Deadline.ToString()));
            Assert.That(workflowCases[0].Description, Is.EqualTo(workflowCase.Description));
            Assert.That(workflowCases[0].IncidentPlace, Is.EqualTo(workflowCase.IncidentPlace));
            Assert.That(workflowCases[0].IncidentType, Is.EqualTo(workflowCase.IncidentType));
            Assert.That(workflowCases[0].MicrotingId, Is.EqualTo(workflowCase.MicrotingId));
            Assert.That(workflowCases[0].PhotosExist, Is.EqualTo(workflowCase.PhotosExist));
            Assert.That(workflowCases[0].SolvedBy, Is.EqualTo(workflowCase.SolvedBy));
            Assert.That(workflowCases[0].Status, Is.EqualTo(workflowCase.Status));
            Assert.That(workflowCases[0].CreatedByUserId, Is.EqualTo(workflowCase.CreatedByUserId));
            Assert.That(workflowCases[0].UpdatedByUserId, Is.EqualTo(workflowCase.UpdatedByUserId));
            Assert.That(workflowCases[0].CreatedAt.ToString("s"), Is.EqualTo(workflowCase.CreatedAt.ToString("s")));
            Assert.That(workflowCases[0].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCase.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCases[0].Version, Is.EqualTo(2));
            Assert.That(workflowCases[0].Version, Is.EqualTo(workflowCase.Version));
            Assert.That(workflowCases[0].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Removed));
            Assert.That(workflowCases[0].WorkflowState, Is.EqualTo(workflowCase.WorkflowState));

            // versions [1]
            Assert.That(workflowCaseVersions[1].WorkflowCaseId, Is.EqualTo(workflowCase.Id));
            Assert.That(workflowCaseVersions[1].ActionPlan, Is.EqualTo(workflowCase.ActionPlan));
            Assert.That(workflowCaseVersions[1].CheckId, Is.EqualTo(workflowCase.CheckId));
            Assert.That(workflowCaseVersions[1].CheckMicrotingUid, Is.EqualTo(workflowCase.CheckMicrotingUid));
            Assert.That(workflowCaseVersions[1].DateOfIncident.ToString("s"), Is.EqualTo(workflowCase.DateOfIncident.ToString("s")));
            Assert.That(workflowCaseVersions[1].Deadline.ToString(), Is.EqualTo(workflowCase.Deadline.ToString()));
            Assert.That(workflowCaseVersions[1].Description, Is.EqualTo(workflowCase.Description));
            Assert.That(workflowCaseVersions[1].IncidentPlace, Is.EqualTo(workflowCase.IncidentPlace));
            Assert.That(workflowCaseVersions[1].IncidentType, Is.EqualTo(workflowCase.IncidentType));
            Assert.That(workflowCaseVersions[1].MicrotingId, Is.EqualTo(workflowCase.MicrotingId));
            Assert.That(workflowCaseVersions[1].PhotosExist, Is.EqualTo(workflowCase.PhotosExist));
            Assert.That(workflowCaseVersions[1].SolvedBy, Is.EqualTo(workflowCase.SolvedBy));
            Assert.That(workflowCaseVersions[1].Status, Is.EqualTo(workflowCase.Status));
            Assert.That(workflowCaseVersions[1].CreatedByUserId, Is.EqualTo(workflowCase.CreatedByUserId));
            Assert.That(workflowCaseVersions[1].UpdatedByUserId, Is.EqualTo(workflowCase.UpdatedByUserId));
            Assert.That(workflowCaseVersions[1].CreatedAt.ToString("s"), Is.EqualTo(workflowCase.CreatedAt.ToString("s")));
            Assert.That(workflowCaseVersions[1].UpdatedAt!.Value.ToString("s"), Is.EqualTo(workflowCase.UpdatedAt!.Value.ToString("s")));
            Assert.That(workflowCaseVersions[1].Version, Is.EqualTo(2));
            Assert.That(workflowCaseVersions[1].Version, Is.EqualTo(workflowCase.Version));
            Assert.That(workflowCaseVersions[1].WorkflowState, Is.EqualTo(Constants.WorkflowStates.Removed));
            Assert.That(workflowCaseVersions[1].WorkflowState, Is.EqualTo(workflowCase.WorkflowState));
        }
    }
}