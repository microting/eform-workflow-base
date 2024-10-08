using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon.S3.Model;
using DocumentFormat.OpenXml.Packaging;
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

        public WorkflowReportHelper(eFormCore.Core sdkCore, WorkflowPnDbContext dbContext)
        {
            _dbContext = dbContext;
            _sdkCore = sdkCore;
        }

        public async Task<string> GenerateReportAnd(int languageId, WorkflowCase workflowCase, string fileType)
        {
            await using MicrotingDbContext sdkDbConetxt = _sdkCore.DbContextHelper.GetDbContext();
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyName = assembly.GetName().Name;

            List<KeyValuePair<string, List<string>>> pictures = new List<KeyValuePair<string, List<string>>>();

            string description = FixLineBreaksAndTabsForWord(workflowCase.Description);
            string actionPlan = FixLineBreaksAndTabsForWord(workflowCase.ActionPlan);

            SortedDictionary<string, string> valuePairs = new SortedDictionary<string, string>
            {
                {"{created_by}", workflowCase.CreatedBySiteName},
                {"{created_date}", workflowCase.DateOfIncident.ToString("dd-MM-yyyy")},
                {"{incident_type}", workflowCase.IncidentType},
                {"{incident_location}", workflowCase.IncidentPlace},
                {"{incident_description}", description},
                {"{incident_deadline}", workflowCase.Deadline?.ToString("dd-MM-yyyy")},
                {"{incident_action_plan}", actionPlan},
                {"{incident_solved_by}", workflowCase.SolvedBy},
                {"{incident_status}", GetStatusTranslated(workflowCase.Status)}
            };

            foreach (PicturesOfTask picturesOfTask in await _dbContext.PicturesOfTasks.Where(x => x.WorkflowCaseId == workflowCase.Id).ToListAsync())
            {
                try
                {
                    var fileName = picturesOfTask.FileName;
                    if (fileName.Length < 25)
                    {
                        var ud = await sdkDbConetxt.UploadedDatas.AsNoTracking()
                            .SingleAsync(x => x.Id == picturesOfTask.UploadedDataId);

                        if (ud.FileLocation.Contains("https"))
                        {
                            await _sdkCore.DownloadUploadedData(ud.Id);
                            ud = await sdkDbConetxt.UploadedDatas.AsNoTracking()
                                .SingleAsync(x => x.Id == picturesOfTask.UploadedDataId);
                        }

                        fileName = $"{picturesOfTask.UploadedDataId}_700_{ud.Checksum}{ud.Extension}";
                        picturesOfTask.FileName = fileName;
                        await picturesOfTask.Update(_dbContext);
                    }

                    FieldValue fieldValue =
                        await sdkDbConetxt.FieldValues.FirstOrDefaultAsync(x =>
                            x.UploadedDataId == picturesOfTask.UploadedDataId);

                    var list = new List<string>();

                    // string fileName =
                    //     $"{uploadedData.Id}_700_{uploadedData.Checksum}{uploadedData.Extension}";

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
                    list.Add("NoValue");

                    pictures.Add(new KeyValuePair<string, List<string>>("Billeder af hændelsen", list));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (PicturesOfTaskDone picturesOfTask in await _dbContext.PicturesOfTaskDone.Where(x => x.WorkflowCaseId == workflowCase.Id).ToListAsync())
            {
                try
                {
                    UploadedData uploadedData =
                        await sdkDbConetxt.UploadedDatas.SingleOrDefaultAsync(
                            x => x.Id == picturesOfTask.UploadedDataId);

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
                    list.Add("NoValue");

                    pictures.Add(new KeyValuePair<string, List<string>>("Billeder af løst opgave", list));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
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
            WordprocessingDocument wordDoc = WordprocessingDocument.Open(resultDocument, true);

            ReportHelper.SearchAndReplace(valuePairs, wordDoc);

            ReportHelper.InsertImages(wordDoc, pictures);
            string outputFolder = Path.Combine(Path.GetTempPath(), "results");

            wordDoc.Save();
            wordDoc.Dispose();

            if (fileType == "pdf")
            {
                ReportHelper.ConvertToPdf(resultDocument, outputFolder);

                string filePath = Path.Combine(Path.GetTempPath(), "results",
                    $"{timeStamp}_{workflowCase.Id}.pdf");
                return filePath;
            }
            else
            {
                string filePath = Path.Combine(Path.GetTempPath(), "results",
                    $"{timeStamp}_{workflowCase.Id}.docx");
                return filePath;
            }
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

        private string FixLineBreaksAndTabsForWord(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return "";
            }
            // input = input.Replace("<", "&lt;").Replace(">", "&gt;");
            input = input.Replace("<br>", "|||");
            input = Regex.Replace(input, "<.*?>",
                string.Empty);
            input = input.Replace("\t", @"</w:t><w:tab/><w:t>");
            input =
                input.Replace("|||", @"</w:t><w:br/><w:t>");
            input =
                input.Replace("\n", @"</w:t><w:br/><w:t>");
            return input;
        }
    }
}