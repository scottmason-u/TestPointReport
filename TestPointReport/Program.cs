using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using TestPointReport.Services;
using TestPointReport.Models;
using TestPointReport.ViewModels;
using Newtonsoft.Json;

namespace CommandLineApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string fullPathFileName = GetFullFilePath();

            if (fullPathFileName.StartsWith("Error"))
            {
                Console.WriteLine(fullPathFileName);
                return;
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"The full file path is: {fullPathFileName}");

                Console.WriteLine("If this is correct, press any key to continue...or Ctrl C to exit and start over.");
                Console.ReadKey();
            }

            string pat = Environment.GetEnvironmentVariable("PATTestPointReport");

            if (pat == null)
            {
                Console.WriteLine("This program requires a PAT token for authorization.");
                Console.WriteLine("");
                Console.WriteLine("First step is to get a valid PAT from Azure DevOps");
                Console.WriteLine("      PAT's expire after a certain number of days.");
                Console.WriteLine("");
                Console.WriteLine("Second Step: After you have a valid PAT you must store that PAT in an environmental variable");
                Console.WriteLine("             To store your PAT in an environmental variable on Windows do this: ");
                Console.WriteLine("In Windows type: setx PATTestPointReport YOUR_PAT_VALUE ");
                Console.WriteLine("");
                Console.WriteLine("             To store your PAT in an environmental variable on Linux do this: ");
                Console.WriteLine("");
                Console.WriteLine("In Linux do the following:");
                Console.WriteLine("");
                Console.WriteLine("Edit the .bashrc or .bash_profile file in your home directory using a text editor like nano or vi.For example:");
                Console.WriteLine("");
                Console.WriteLine("nano ~/.bashrc");
                Console.WriteLine("");
                Console.WriteLine("Type the following line at the end of the file:");
                Console.WriteLine("");
                Console.WriteLine("export PATTestPointReport=YOUR_PAT_VALUE");
                Console.WriteLine("");
                Console.WriteLine("Replace YOUR_PAT_VALUE with your actual PAT value.");
                Console.WriteLine("");
                Console.WriteLine("Save the file and exit the text editor.");
                Console.WriteLine("");
                Console.WriteLine("Run source ~/.bashrc to apply the changes to the current session.");
                Console.WriteLine("");

            }

            TestPoints(pat, fullPathFileName);

            Console.WriteLine($"\nThe file was created here: {fullPathFileName}\n");
            Console.WriteLine("Press any key to close this program.");
            Console.ReadKey();
        }

        public static void TestPoints(string pat, string fullPathFileName)
        {
            if (string.IsNullOrEmpty(pat))
            {
                Console.WriteLine("Your Personal Access Token (PAT) cannot be blank.");
                return;
            }

            string token = pat;
            string base64Pat = Convert.ToBase64String(Encoding.ASCII.GetBytes($":{token}"));
            string authorizationHeaderValue = $"Basic {base64Pat}";

            string organization = "etsesi";
            string project = "FMM-User%20Stories";
            string apiVersion = "7.1-preview.2";

            string urlTestPlans = $"https://dev.azure.com/etsesi/FMM-User%20Stories/_apis/test/plans?api-version=5.0";

            List<TestPlanViewModel> testPlanList = new List<TestPlanViewModel>();
            List<PointViewModel> allTestPointsList = new List<PointViewModel>();
            List<PointViewModel> testPointListForPlan = new List<PointViewModel>();
            int testPointCount = 0;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Pat);

                try
                {
                    HttpResponseMessage testPlanResponse = client.GetAsync(urlTestPlans).Result;

                    if (testPlanResponse.IsSuccessStatusCode)
                    {
                        string testPlanResponseBody = testPlanResponse.Content.ReadAsStringAsync().Result;
                        testPlanList = GetListOfPlans(testPlanResponseBody);
                    }
                    else
                    {
                        Console.WriteLine($"Failed to call Azure DevOps Test Plans API. Status code: {testPlanResponse.StatusCode}");
                        Console.WriteLine($"TestPlanError: Failed to call Azure DevOps Test Plans API. Status code: {testPlanResponse.StatusCode}");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while calling Azure DevOps Test Plans API: {ex.Message}");
                    Console.WriteLine($"TestPlanError: An error occurred while calling Azure DevOps Test Plans API: {ex.Message}");
                    return;
                }

                foreach (var testPlan in testPlanList)
                {
                    string planId = testPlan.Id.ToString();
                    string suiteId = testPlan.RootSuite?.Id ?? string.Empty;
                    string urlPoints = $"https://dev.azure.com/{organization}/{project}/_apis/test/Plans/{planId}/Suites/{suiteId}/points?api-version=7.1-preview.2";

                    HttpResponseMessage response = client.GetAsync(urlPoints).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string responseBody = response.Content.ReadAsStringAsync().Result;
                        string planName = testPlan.Name;

                        testPointListForPlan = FilterAndAppendToCsv(planName, planId, suiteId, responseBody, fullPathFileName);

                        allTestPointsList.AddRange(testPointListForPlan);
                        testPointCount += testPointListForPlan.Count;
                    }
                    else
                    {
                        Console.WriteLine($"Failed to call Azure DevOps API for Test Points. Status code: {response.StatusCode}");
                        Console.WriteLine($"TestPointError: Failed to call Azure DevOps API for Test Points. Status code: {response.StatusCode}");
                        return;
                    }
                }

                Console.WriteLine("\n\nTest Points were pulled for each of these Test Plans:\n");
                foreach (var item in testPlanList)
                {
                    Console.WriteLine($"{item.Name}");
                }
            }
        }

        private static List<PointViewModel> FilterAndAppendToCsv(string? testPlanName, string? planId, string? suiteId, string jsonData, string fullPathFileNameParam)
        {
            var pointsContainer = JsonConvert.DeserializeObject<PointsContainer>(jsonData);

            var testPointList = new List<PointViewModel>();

            using (StreamWriter writerTestPoint = new StreamWriter(fullPathFileNameParam, true))
            {
                var columnNames = new List<string>
                {
                    "Plan Name",
                    "Test Points Name",
                    "Outcome",
                    "Test Case Id",
                    "Date Completed",
                    "Tester Name",
                    "Link",
                };

                if (writerTestPoint.BaseStream.Length == 0)
                {
                    writerTestPoint.WriteLine(string.Join("|", columnNames.Select(c => $"\"{c}\"")));
                }

                if (pointsContainer.Value != null && pointsContainer.Value.Any())
                {
                    foreach (var testPoint in pointsContainer.Value)
                    {
                        string outcomeDisplay = SetOutcomeDisplay(testPoint?.Outcome, testPoint.State);
                        string dateCompletedString = (testPoint.LastResultDetails != null && testPoint.LastResultDetails.DateCompleted != null)
                            ? testPoint.LastResultDetails.DateCompleted.ToString("yyyy-MM-dd HH:mm:ss")
                            : "";

                        var extractedData = new List<string>
                        {
                            testPlanName ?? "",
                            testPoint.TestCase.Name ?? "None Found",
                            outcomeDisplay,
                            testPoint.TestCase.Id ?? "",
                            dateCompletedString,
                            testPoint.LastResultDetails?.RunBy?.DisplayName ?? "",
                            testPoint.TestCase.WebUrl ?? ""
                        };
                        Console.Write("."); /* show action to user that a process is going on. */

                        writerTestPoint.WriteLine(string.Join("|", extractedData.Select(e => e.Contains(",") ? $"\"{e}\"" : e)));

                        var testPointItem = new PointViewModel
                        {
                            PlanName = testPlanName ?? "",
                            TestCaseName = testPoint.TestCase.Name ?? "",
                            OutcomeDisplay = SetOutcomeDisplay(testPoint.Outcome, testPoint.State),
                            TestCaseId = testPoint.TestCase.Id ?? "",
                            ResultsDateCompletedDate = dateCompletedString,
                            TesterName = testPoint.LastResultDetails?.RunBy?.DisplayName ?? "",
                            webUrl = testPoint.TestCase.WebUrl ?? ""
                        };

                        testPointList.Add(testPointItem);
                    }
                }
                else
                {
                    var testPointItemMissing = new PointViewModel
                    {
                        PlanName = testPlanName ?? "",
                        TestCaseName = "No Point Data Found",
                        OutcomeDisplay = "No Point Data Found",
                        TestCaseId = "",
                        ResultsDateCompletedDate = "",
                        TesterName = "",
                        webUrl = ""
                    };

                    var extractedData = new List<string>
                    {
                        testPlanName ?? "", "No Point Data Found", "", "", "", "", ""
                    };

                    Console.Write("."); // Show action to the user that a process is going on.

                    writerTestPoint.WriteLine(string.Join("|", extractedData.Select(e => e.Contains(",") ? $"\"{e}\"" : e)));

                    testPointList.Add(testPointItemMissing);
                }
            }

            return testPointList;
        }

        static List<TestPlanViewModel> GetListOfPlans(string jsonData)
        {
            var testPlanContainer = JsonConvert.DeserializeObject<TestPlansContainerViewModel>(jsonData);

            if (testPlanContainer?.Value != null)
            {
                return testPlanContainer.Value.Select(tp => new TestPlanViewModel
                {
                    Id = tp.Id,
                    Name = tp.Name,
                    RootSuite = tp.RootSuite
                }).ToList();
            }

            return new List<TestPlanViewModel>();
        }

        #region SetOutcomeDisplay
        private static string SetOutcomeDisplay(string outcome, string state)
        {
            if (outcome != null && state != null)
            {
                if (outcome.ToLower() == "unspecified" && state.ToLower() == "ready")
                {
                    return "Active";
                }

                switch (outcome.ToLower())
                {
                    case "unspecified":
                        return "Active";
                    case "failed":
                        return "Failed";
                    case "paused":
                        return "Paused";
                    case "passed":
                        return "Passed";
                    default:
                        return "Nothing Found";
                }
            }
            else
            {
                return "Nothing Found";
            }
        }
        #endregion


        static string GetFullFilePath()
        {
            Console.Write("Enter the file path: ");
            string filePath = Console.ReadLine();

            Console.Write("Enter the file name: ");
            string fileName = Console.ReadLine();

            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("Both the path and the file name must be given.");
                return GetFullFilePath();
            }

            if (!fileName.EndsWith(".csv"))
            {
                Console.Write("The file name entered does not end with '.csv'\n");
                Console.Write("Are you sure you want to use this file name ? (Y/N): ");
                string userResponse = Console.ReadLine();
                if (userResponse.ToLower() != "y" && userResponse.ToLower() != "yes")
                {
                    return GetFullFilePath();
                }
            }

            if (!filePath.EndsWith("\\"))
            {
                filePath += "\\";
            }

            if (!Directory.Exists(filePath))
            {
                string errMsg = $"Error: The specified path '{filePath}' does not exist.";
                Console.WriteLine(errMsg);
                return errMsg;
            }

            string fullPathFileName = Path.Combine(filePath, fileName);

            if (fullPathFileName.ToLower().Contains("zzzzz"))
            {
                DateTime currentDateTime = DateTime.Now;
                string formattedDate = currentDateTime.ToString("yyyy_MM_dd_HHmm");

                fullPathFileName = $"{fullPathFileName}_{formattedDate}";
            }

            return fullPathFileName;
        }
    }
}
