using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    [Serializable]
    public class TruyCap
    {
        private static TruyCap instance = null;
        private List<AVLTree> dsCay;
     
        private TruyCap()
        {
            dsCay = new List<AVLTree>();
        
        }
        public static TruyCap khoiTao()
        {
            if (instance == null)
                instance = new TruyCap();
            return instance;
        }
        public List<AVLTree> getDanhSachCay()
        {
            return dsCay;
        }

        public static bool docFile(string tenFile)
        {
            try
            {
                // Khởi tạo/lấy instance
                if (instance == null)
                {
                    instance = new TruyCap();
                }

                // Đảm bảo có cây AVL để chèn dữ liệu
                AVLTree targetTree;
                if (!instance.dsCay.Any())
                {
                    targetTree = new AVLTree();
                    instance.dsCay.Add(targetTree);
                }
                else
                {
                    // Sử dụng cây đầu tiên và xóa dữ liệu cũ
                    targetTree = instance.dsCay.First();
                    // THAY ĐỔI: Dùng hàm Clear() thay vì gán Root = null trực tiếp
                    targetTree.Clear();
                }

                // Đọc tất cả các dòng từ file
                if (!File.Exists(tenFile))
                {
                    // Nếu file không tồn tại, coi như đọc thành công (không có dữ liệu)
                    return true;
                }
                string[] lines = File.ReadAllLines(tenFile);

                // Dòng đầu tiên là header, bỏ qua
                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;

                    // Phân tích dòng thành CustomerRecord
                    CustomerRecord record = CustomerRecord.ParseFromCsv(lines[i]);

                    if (record != null)
                    {
                        // Chèn vào cây AVL (giả định hàm Insert có sẵn)
                        targetTree.Insert(record);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                // In lỗi ra Console hoặc Log nếu cần
                // Console.WriteLine($"Lỗi khi đọc file CSV: {ex.Message}");
                return false;
            }
        }
        public static bool ghiFile(string tenFile)
        {
            try
            {
                if (instance == null || !instance.dsCay.Any())
                {
                    // Không có dữ liệu để ghi
                    return false;
                }

                // Lấy cây AVL đầu tiên
                AVLTree sourceTree = instance.dsCay.First();

                // Nếu cây rỗng, vẫn tạo file chỉ có header
                if (sourceTree.Root == null)
                {
                    File.WriteAllText(tenFile, GetCsvHeader(), Encoding.UTF8);
                    return true;
                }

                // Sử dụng phương thức duyệt cây để lấy danh sách CustomerRecord
                List<CustomerRecord> records = new List<CustomerRecord>();
                // Cần một hàm duyệt In-order để lấy dữ liệu (Hàm này cần được định nghĩa trong AVLTree hoặc QLAVL)
                // Tái sử dụng logic duyệt In-order từ QLAVL.cs
                InOrderTraversal(sourceTree.Root, records);

                // Bắt đầu ghi file
                using (StreamWriter sw = new StreamWriter(tenFile, false, Encoding.UTF8))
                {
                    // 1. Ghi Header
                    sw.WriteLine(GetCsvHeader());

                    // 2. Ghi từng CustomerRecord
                    foreach (var record in records)
                    {
                        sw.WriteLine(record.ToString());
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Console.WriteLine($"Lỗi khi ghi file CSV: {ex.Message}");
                return false;
            }
        }
        private static string GetCsvHeader()
        {
            return "Customer ID,Age,Gender,Item Purchased,Category,Purchase Amount (USD),Location,Size,Color,Season,Review Rating,Subscription Status,Shipping Type,Discount Applied,Promo Code Used,Previous Purchases,Payment Method,Frequency of Purchases";
        }

        private static void InOrderTraversal(AVLNode node, List<CustomerRecord> list)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left, list);
                SinglyNode current = node.DuplicatesHead;
                while (current != null)
                {
                    list.Add(current.Data);
                    current = current.Next;
                }
                InOrderTraversal(node.Right, list);
            }
        }
    }
}
