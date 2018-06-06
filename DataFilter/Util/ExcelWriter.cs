using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataFilter.Util
{
    public class ExcelWriter
    {
        public void Write(string fileName, List<string> dataList)
        {
            IWorkbook workbook;
            ISheet sheet;

            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                workbook = new XSSFWorkbook();
                if (workbook == null)
                    return;
                sheet = workbook.CreateSheet();
                if (sheet == null)
                    return;

                int i = 0;
                foreach (string data in dataList)
                {
                    IRow row = sheet.CreateRow(i++);
                    row.CreateCell(0).SetCellValue(data);
                }
            }
        }
    }
}
