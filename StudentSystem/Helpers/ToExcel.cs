using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentSystem.Helpers
{
	using Entities;
    using Syncfusion.XlsIO;
    using Windows.Storage;
    using Windows.Storage.Pickers;
	using Helpers;

    public static class ToExcel
    {
		public static async Task Some(IEnumerable<Student> reports) {
			var allStudents = reports.Count();
			var allMen = reports.Where(o => o.Gender == Constants.GenderMale).Count();
			var allWomen = allStudents - allMen;
			var noParent = reports.Where(s => s.StudentParents == null || s.StudentParents.Count == 0).Count();
			var privilages = reports.Where(s => s.AdditionalInfo != "default").Count();

			var departments = reports.GroupBy(o => o.Group.Department.ShortDepartmentName)
				.Select(g => new {
					DepartmentName = g.Key,
					Count = g.Count(),
					CountMen = g.Where(s => s.Gender == Constants.GenderMale).Count(),
					CountWomen = g.Where(s => s.Gender == Constants.GenderFemale).Count(),
					NoParent = g.Where(s => s.StudentParents == null || s.StudentParents.Count == 0).Count(),
					Privilages = g.Where(s => s.AdditionalInfo != "default").Count(),
					Groups = g.GroupBy( o => o.Group.ShortName).Select(gr =>
					new {
						GroupName = gr.Key,
						Count = gr.Count(),
						CountMen = gr.Where(m => m.Gender == Constants.GenderMale).Count(),
						CountWomen = gr.Where(m => m.Gender == Constants.GenderFemale).Count(),
						NoParent = gr.Where(m => m.StudentParents == null || m.StudentParents.Count == 0).Count(),
						Privilages = gr.Where(m => m.AdditionalInfo != "default").Count(),
						Students = gr.OrderBy(o => o.FirstName)
					})
				});
			using (ExcelEngine excelEngine = new ExcelEngine()) {
				IApplication application = excelEngine.Excel;

				application.DefaultVersion = ExcelVersion.Excel2016;
				var countSheets = reports.Select(o => o.Group.ShortName).Distinct().Count() + 1;
				//Create a workbook with a worksheet
				IWorkbook workbook = application.Workbooks.Create(countSheets);

				//Access first worksheet from the workbook instance.
				IWorksheet worksheet = workbook.Worksheets[0];
				worksheet.Name = "Global Info";
				worksheet.Range[1, 1, 1, 5].Merge();
				worksheet.Range[1, 1, 1, 5].Text = "Global Info";
				int globalRow = 2;
				worksheet.Range[globalRow, 1].Text = "Count Students";
				worksheet.Range[globalRow++, 2].Number = allStudents;
				
				worksheet.Range[globalRow, 1].Text = "Men";
				worksheet.Range[globalRow++, 2].Number = allMen;

				worksheet.Range[globalRow, 1].Text = "Women";
				worksheet.Range[globalRow++, 2].Number = allWomen;

				worksheet.Range[globalRow, 1].Text = "No Parent";
				worksheet.Range[globalRow++, 2].Number = noParent;

				worksheet.Range[globalRow, 1].Text = "Privilages";
				worksheet.Range[globalRow++, 2].Number = privilages;

				globalRow++;
				foreach(var el in departments) {
					worksheet.Range[globalRow++, 1].Text = el.DepartmentName;
					worksheet.Range[globalRow, 1].Text = "Count Students";
					worksheet.Range[globalRow++, 2].Number = el.Count;

					worksheet.Range[globalRow, 1].Text = "Men";
					worksheet.Range[globalRow++, 2].Number = el.CountMen;

					worksheet.Range[globalRow, 1].Text = "Women";
					worksheet.Range[globalRow++, 2].Number = el.CountWomen;

					worksheet.Range[globalRow, 1].Text = "No Parent";
					worksheet.Range[globalRow++, 2].Number = el.NoParent;

					worksheet.Range[globalRow, 1].Text = "Privilages";
					worksheet.Range[globalRow++, 2].Number = el.Privilages;

					++globalRow;
					foreach (var gr in el.Groups) {
						worksheet.Range[globalRow++, 1].Text = gr.GroupName;
						worksheet.Range[globalRow, 1].Text = "Count Students";
						worksheet.Range[globalRow++, 2].Number = gr.Count;

						worksheet.Range[globalRow, 1].Text = "Men";
						worksheet.Range[globalRow++, 2].Number = gr.CountMen;

						worksheet.Range[globalRow, 1].Text = "Women";
						worksheet.Range[globalRow++, 2].Number = gr.CountWomen;

						worksheet.Range[globalRow, 1].Text = "No Parent";
						worksheet.Range[globalRow++, 2].Number = gr.NoParent;

						worksheet.Range[globalRow, 1].Text = "Privilages";
						worksheet.Range[globalRow++, 2].Number = gr.Privilages;
					}
					++globalRow;
				}
				
				int sheetIndex = 1;
				foreach (var d in departments) {
					foreach (var e in d.Groups) {
						worksheet = workbook.Worksheets[sheetIndex++];
						worksheet.Name = e.GroupName;
						worksheet[1, 1, 1, 5].Merge();
						worksheet[1, 1, 1, 5].Text = $"Info about {e.GroupName}";
						List<string> columnNames = new List<string>() {
							"StudentId",
							"FirstName",
							"SecondName",
							"ThirdName",
							"Birthday",
							"Address",
							"BirthdayCertificate",
							"School",
							"AverageMarkInSchool",
							"Gender",
							"IdentificationCode",
							"PassportCode",
							"PhoneNumber",
							"SchoolCertificateCode",
							"ArmyCerificate",
							"AdditionalInfo"
						};
						int row = 2;
						worksheet.Range[row, 1].Text = "Count Students";
						worksheet.Range[row++, 2].Number = e.Count;

						worksheet.Range[row, 1].Text = "Men";
						worksheet.Range[row++, 2].Number = e.CountMen;

						worksheet.Range[row, 1].Text = "Women";
						worksheet.Range[row++, 2].Number = e.CountWomen;

						worksheet.Range[row, 1].Text = "No Parent";
						worksheet.Range[row++, 2].Number = e.NoParent;

						worksheet.Range[row, 1].Text = "Privilages";
						worksheet.Range[row++, 2].Number = e.Privilages;
						
						for (int index = 0; index < columnNames.Count; ++index) {
							worksheet.Range[row, index + 1].Text = columnNames[index];
						}
						
						foreach (var report in e.Students) {
							++row;
							int col = 1;
							worksheet.Range[row, col++].Number = report.StudentId;
							worksheet.Range[row, col++].Text = $"{report.FirstName}";
							worksheet.Range[row, col++].Text = $"{report.SecondName}";
							worksheet.Range[row, col++].Text = $"{report.ThirdName}";
							worksheet.Range[row, col++].DateTime = report.Birthday;
							worksheet.Range[row, col++].Text = $"{report.Address}";
							worksheet.Range[row, col++].Text = $"{report.BirthdayCertificate}";
							worksheet.Range[row, col++].Text = $"{report.School}";
							worksheet.Range[row, col++].Number = report.AverageMarkInSchool;
							worksheet.Range[row, col++].Text = $"{report.Gender}";
							worksheet.Range[row, col++].Text = $"{report.IdentificationCode}";
							worksheet.Range[row, col++].Text = $"{report.PassportCode}";
							worksheet.Range[row, col++].Text = $"{report.PhoneNumber}";
							worksheet.Range[row, col++].Text = $"{report.SchoolCertificateCode}";
							worksheet.Range[row, col++].Text = $"{report.ArmyCerificate}";
							worksheet.Range[row, col++].Text = $"{report.AdditionalInfo}";
						}
					}
                }
				
				
				
				// Save the Workbook
				StorageFile storageFile;
				if (!(Windows.Foundation.Metadata.ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))) {
					FileSavePicker savePicker = new FileSavePicker();
					savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
					savePicker.SuggestedFileName = "Output";
					savePicker.FileTypeChoices.Add("Excel Files", new List<string>() { ".xlsx" });
					storageFile = await savePicker.PickSaveFileAsync();
				}
				else {
					StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;
					storageFile = await local.CreateFileAsync("Output.xlsx", CreationCollisionOption.ReplaceExisting);
				}

				await workbook.SaveAsAsync(storageFile);

				// Launch the saved file
				await Windows.System.Launcher.LaunchFileAsync(storageFile);
			}
		}
	}
}
