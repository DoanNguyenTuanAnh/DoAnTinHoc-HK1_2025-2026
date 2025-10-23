using System;
using System.IO;
using System.Collections.Generic;

class ReadCsv
{
    static List<string[]> ReadCsvFile(string filePath)
    {
        // Tạo list rỗng để lưu các dòng dữ liệu
        List<string[]> rows = new List<string[]>();

        try
        {
            // Đọc tất cả các dòng từ file CSV vào mảng lines
            string[] lines = File.ReadAllLines(filePath);

            // Duyệt qua từng dòng trong file
            foreach (string line in lines)
            {
                // Tách mỗi dòng thành mảng các giá trị, ngăn cách bởi dấu phẩy
                string[] values = line.Split(',');

                // Thêm mảng giá trị vào danh sách rows
                rows.Add(values);
            }
            // Thông báo đọc file thành công
            Console.WriteLine("CSV file read successfully!");
        }
        catch (Exception ex)
        {
            // Xử lý nếu có lỗi xảy ra
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        // Trả về danh sách các dòng đã đọc
        return rows;
    }


    static void Main()
    {
        // Khai báo đường dẫn đến file CSV
        string csvFilePath = "Dataset.csv";

        // Gọi method ReadCsvFile để đọc dữ liệu
        List<string[]> csvData = ReadCsvFile(csvFilePath);

        // Hiển thị dữ liệu đã đọc
        foreach (string[] row in csvData)
        {
            // Nối các phần tử trong mảng bằng dấu phẩy và khoảng trắng
            Console.WriteLine(string.Join(", ", row));
        }
    }
}
