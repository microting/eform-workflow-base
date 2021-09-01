using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using ImageMagick;
using Microsoft.EntityFrameworkCore;
using Microting.eForm.Helpers;
using Microting.eForm.Infrastructure;
using Microting.eForm.Infrastructure.Data.Entities;
using Microting.eFormWorkflowBase.Infrastructure.Data;
using Microting.eFormWorkflowBase.Infrastructure.Data.Entities;

namespace Microting.eFormWorkflowBase.Helpers
{
    public class WorkflowReportHelper
    {
        private readonly eFormCore.Core _sdkCore;
        private readonly WorkflowPnDbContext _dbContext;

        public WorkflowReportHelper(eFormCore.Core sdkCore, DbContextHelper dbContextHelper)
        {
            _dbContext = dbContextHelper.GetDbContext();
            _sdkCore = sdkCore;
        }

        public async Task<string> GenerateReportAnd(int languageId, WorkflowCase workflowCase)
        {
            await using MicrotingDbContext sdkDbConetxt = _sdkCore.DbContextHelper.GetDbContext();
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;

            List<KeyValuePair<string, List<string>>> pictures = new List<KeyValuePair<string, List<string>>>();

            SortedDictionary<string, string> valuePairs = new SortedDictionary<string, string>
            {
                {"{created_by}", workflowCase.CreatedBySiteName},
                {"{created_date}", workflowCase.CreatedAt.ToString("dd-MM-yyyy")},
                {"{incident_type}", workflowCase.IncidentType},
                {"{incident_location}", workflowCase.IncidentPlace},
                {"{incident_description}", workflowCase.Description.Replace("&", "&amp;")},
                {"{incident_deadline}", workflowCase.Deadline?.ToString("dd-MM-yyyy")},
                {"{incident_action_plan}", workflowCase.ActionPlan?.Replace("&", "&amp;")},
                {"{incident_solved_by}", workflowCase.SolvedBy},
                {"{incident_status}", GetStatusTranslated(workflowCase.Status)}
            };

            foreach (PicturesOfTask picturesOfTask in await _dbContext.PicturesOfTasks.Where(x => x.WorkflowCaseId == workflowCase.Id).ToListAsync())
            {
                UploadedData uploadedData =
                    await sdkDbConetxt.UploadedDatas.SingleOrDefaultAsync(x => x.Id == picturesOfTask.UploadedDataId);

                FieldValue fieldValue =
                    await sdkDbConetxt.FieldValues.SingleOrDefaultAsync(x =>
                        x.UploadedDataId == picturesOfTask.UploadedDataId);

                var list = new List<string>();

                string fileName =
                    $"{uploadedData.Id}_700_{uploadedData.Checksum}{uploadedData.Extension}";

                string fileContent = "";
                using GetObjectResponse response =
                    await _sdkCore.GetFileFromS3Storage(fileName);
                using var image = new MagickImage(response.ResponseStream);
                fileContent = image.ToBase64();

                string geoTag = "";
                if (fieldValue.Latitude != null)
                {
                    geoTag =
                        $"https://www.google.com/maps/place/{fieldValue.Latitude},{fieldValue.Longitude}";
                }

                list.Add(fileContent);
                list.Add(geoTag);

                pictures.Add(new KeyValuePair<string, List<string>>("Billeder af hændelsen", list));
            }

            foreach (PicturesOfTaskDone picturesOfTask in await _dbContext.PicturesOfTaskDone.Where(x => x.WorkflowCaseId == workflowCase.Id).ToListAsync())
            {
                UploadedData uploadedData =
                    await sdkDbConetxt.UploadedDatas.SingleOrDefaultAsync(x => x.Id == picturesOfTask.UploadedDataId);

                FieldValue fieldValue =
                    await sdkDbConetxt.FieldValues.SingleOrDefaultAsync(x =>
                        x.UploadedDataId == picturesOfTask.UploadedDataId);

                var list = new List<string>();

                string fileName =
                    $"{uploadedData.Id}_700_{uploadedData.Checksum}{uploadedData.Extension}";

                string fileContent = "";
                using GetObjectResponse response =
                    await _sdkCore.GetFileFromS3Storage(fileName);
                using var image = new MagickImage(response.ResponseStream);
                fileContent = image.ToBase64();

                string geoTag = "";
                if (fieldValue.Latitude != null)
                {
                    geoTag =
                        $"https://www.google.com/maps/place/{fieldValue.Latitude},{fieldValue.Longitude}";
                }

                list.Add(fileContent);
                list.Add(geoTag);

                pictures.Add(new KeyValuePair<string, List<string>>("Billeder behandlet hændelse", list));
            }

            var stream = assembly.GetManifestResourceStream($"{assemblyName}.Resources.report.docx");

            Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), "results"));

            string timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            string resultDocument = Path.Combine(Path.GetTempPath(), "results",
                $"{timeStamp}_{workflowCase.Id}.docx");


            await using (var fileStream = File.Create(resultDocument))
            {
                if (stream != null) await stream.CopyToAsync(fileStream);
            }

            ReportHelper.SearchAndReplace(valuePairs, resultDocument);

            ReportHelper.InsertImages(resultDocument, pictures);
            string outputFolder = Path.Combine(Path.GetTempPath(), "results");

            ReportHelper.ConvertToPdf(resultDocument, outputFolder);

            string filePath = Path.Combine(Path.GetTempPath(), "results",
                $"{timeStamp}_{workflowCase.Id}.pdf");
            return filePath;
        }

        private string GetStatusTranslated(string constant)
        {
            switch (constant)
            {
                case "Not initiated":
                    return "Ikke igangsat";
                case "Ongoing":
                    return "Igangværende";
                case "No status":
                    return "Vælg status";
                case "Closed":
                    return "Afsluttet";
                case "Canceled":
                    return "Annulleret";
                default:
                    return "Ikke igangsat";
            }
        }
    }
}