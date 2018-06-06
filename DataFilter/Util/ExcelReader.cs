using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;

namespace DataFilter.Util
{
    public class ExcelReader
    {
        public List<string> Load(string fileName)
        {
            List<string> result = new List<string>();
            string fileExt = Path.GetExtension(fileName);
            IWorkbook workbook;
            ISheet sheet;

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                workbook = WorkbookFactory.Create(fs);
                if (workbook == null)
                    return result;
                sheet = workbook.GetSheetAt(0);
                if (sheet == null)
                    return result;

                int rowCount = sheet.LastRowNum;

                for (int i = 0; i <= rowCount; i++)
                {
                    IRow row = sheet.GetRow(i);
                    foreach (ICell cell in row.Cells)
                    {
                        result.Add(cell.ToString());
                    }
                }
            }

            return result;
        }
    }
}
