using System;
using System.Data;
using System.IO;
using System.Collections.Generic;

namespace DoAnTinHoc
{
    public static class CsvReader
    {
        // Đọc CSV và trả về DataTable (dùng để bind vào DataGridView)
        // Dòng đầu sẽ được dùng làm header (cột)
        public static DataTable ReadCsvToDataTable(string filePath, char separator = ',')
        {
            var table = new DataTable();

            if (!File.Exists(filePath))
                throw new FileNotFoundException("CSV file not found", filePath);

            using (var sr = new StreamReader(filePath))
            {
                bool firstLine = true;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var values = line.Split(separator);

                    if (firstLine)
                    {
                        foreach (var header in values)
                            table.Columns.Add(header.Trim());
                        firstLine = false;
                    }
                    else
                    {
                        var newRow = table.NewRow();
                        int count = Math.Min(values.Length, table.Columns.Count);
                        for (int i = 0; i < count; i++)
                            newRow[i] = values[i].Trim();
                        table.Rows.Add(newRow);
                    }
                }
            }

            return table;
        }

        // (Tùy chọn) giữ lại phương thức trả về List<string[]> giống ví dụ ban đầu
        public static List<string[]> ReadCsvFile(string filePath, char separator = ',')
        {
            var rows = new List<string[]>();
            if (!File.Exists(filePath))
                return rows;

            try
            {
                foreach (var line in File.ReadAllLines(filePath))
                {
                    rows.Add(line.Split(separator));
                }
            }
            catch
            {
                // lỗi đọc file: trả về rỗng (bạn có thể throw hoặc log tuỳ ý)
            }

            return rows;
        }
    }
}
