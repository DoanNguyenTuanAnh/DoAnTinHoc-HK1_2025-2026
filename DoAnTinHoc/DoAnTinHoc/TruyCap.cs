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
                if (instance == null)
                {
                    instance = new TruyCap();
                }
                AVLTree targetTree;
                if (!instance.dsCay.Any())
                {
                    targetTree = new AVLTree();
                    instance.dsCay.Add(targetTree);
                }
                else
                {
                    targetTree = instance.dsCay.First();
                    targetTree.Clear();
                }
                // Đọc tất cả các dòng từ file
                if (!File.Exists(tenFile))
                {
                    return true;
                }
                string[] lines = File.ReadAllLines(tenFile);

                for (int i = 1; i < lines.Length; i++)
                {
                    if (string.IsNullOrWhiteSpace(lines[i])) continue;
                    CustomerRecord record = CustomerRecord.ParseFromCsv(lines[i]);
                    if (record != null)
                    {
                        targetTree.Insert(record);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static bool ghiFile(string tenFile)
        {
            try
            {
                if (instance == null || !instance.dsCay.Any())
                {
                    return false;
                }
                AVLTree sourceTree = instance.dsCay.First();
                if (sourceTree.Root == null)
                {
                    File.WriteAllText(tenFile, GetCsvHeader(), Encoding.UTF8);
                    return true;
                }
                List<CustomerRecord> records = new List<CustomerRecord>();
                InOrderTraversal(sourceTree.Root, records);
                using (StreamWriter sw = new StreamWriter(tenFile, false, Encoding.UTF8))
                {
                    sw.WriteLine(GetCsvHeader());
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
        private static string GetCsvHeader()//
        {
            return "Customer ID,Age,Gender,Item Purchased," +
                "Category,Purchase Amount (USD)," +
                "Location,Size,Color,Season,Review Rating," +
                "Subscription Status,Shipping Type," +
                "Discount Applied,Promo Code Used,Previous Purchases," +
                "Payment Method,Frequency of Purchases";
        }

        private static void InOrderTraversal(AVLNode node, List<CustomerRecord> list)//duyệt trung thứ tự
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
