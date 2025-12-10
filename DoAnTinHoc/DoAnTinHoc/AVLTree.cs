using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    [Serializable]
    public class AVLTree
    {
        
        public AVLNode Root { get; private set; }
        private int GetHeight(AVLNode node)
        {
            return node == null ? 0 : node.Height;
        }
        private void UpdateHeight(AVLNode node)
        {
            if (node != null)
            {
                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            }
        }
        private int GetBalanceFactor(AVLNode node)//Cân bằng
        {
            if (node == null)
            {
                return 0;
            }
            return GetHeight(node.Left) - GetHeight(node.Right);
        }



        // Xoay Phải 
        private AVLNode RotateRight(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            UpdateHeight(y);
            UpdateHeight(x);

            return x;
        }

        // Xoay Trái 
        private AVLNode RotateLeft(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            UpdateHeight(x);
            UpdateHeight(y);

            return y;
        }



        public void Insert(CustomerRecord data)
        {
            Root = Insert(Root, data);
        }

        private AVLNode Insert(AVLNode node, CustomerRecord data)
        {
            if (node == null)
            {
                return new AVLNode(data);
            }
            if (data.ID < node.Data.ID)
            {
                node.Left = Insert(node.Left, data);
            }
            else if (data.ID > node.Data.ID)
            {
                node.Right = Insert(node.Right, data);
            }
            else
            {

                // ID trùng khớp (node.Data.ID == data.ID)
                SinglyNode current = node.DuplicatesHead;

                if (current == null)
                {
                    // SỬA LỖI: Đây là bản ghi trùng lặp đầu tiên (bản ghi thứ 2 có ID này)
                    node.DuplicatesHead = new SinglyNode(data);
                }
                else
                {
                    // Đã có bản ghi trùng lặp, thêm vào cuối danh sách
                    while (current.Next != null)
                    {
                        current = current.Next;
                    }
                    current.Next = new SinglyNode(data);
                }
                return node;              
            }
            UpdateHeight(node);
            int balance = GetBalanceFactor(node);
            if (balance > 1 && data.ID < node.Left.Data.ID)
            {
                return RotateRight(node);
            }
            if (balance < -1 && data.ID > node.Right.Data.ID)
            {
                return RotateLeft(node);
            }
            if (balance > 1 && data.ID > node.Left.Data.ID)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && data.ID < node.Right.Data.ID)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            return node;
        }
        public CustomerRecord Search(int key)
        {
            return Search(Root, key);
        }

        private CustomerRecord Search(AVLNode node, int key)
        {
            if (node == null)
            {
                return null;
            }
            if (key < node.Data.ID)
            {
                return Search(node.Left, key);
            }
            else if (key > node.Data.ID)
            {
                return Search(node.Right, key);
            }
            else
            {
                return node.Data;
            }
        }
        public List<CustomerRecord> SearchAll(int key)
        {
            return SearchAllRecursive(Root, key);
        }

        private List<CustomerRecord> SearchAllRecursive(AVLNode node, int key)
        {
            if (node == null)
            {
                return new List<CustomerRecord>(); // Trả về danh sách rỗng nếu không tìm thấy
            }
            if (key < node.Data.ID)
            {
                return SearchAllRecursive(node.Left, key);
            }
            else if (key > node.Data.ID)
            {
                return SearchAllRecursive(node.Right, key);
            }
            else
            {
                // Nút có ID trùng khớp được tìm thấy (node.Data.ID == key)
                List<CustomerRecord> result = new List<CustomerRecord>();

                // 1. Thêm bản ghi chính
                result.Add(node.Data);

                // 2. Thêm tất cả các bản ghi trùng lặp từ DuplicatesHead
                SinglyNode current = node.DuplicatesHead;
                while (current != null)
                {
                    result.Add(current.Data);
                    current = current.Next;
                }
                return result; // Trả về danh sách chứa tất cả các bản ghi trùng ID
            }
        }
        private AVLNode FindMinNode(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }
        // Hàm hỗ trợ để đếm số lượng bản ghi trong danh sách liên kết
        private int CountDuplicates(AVLNode node)
        {
            int count = 0;
            SinglyNode current = node.DuplicatesHead;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
        public void Delete(int key)
        {
            Root = Delete(Root, key);
        }
        private AVLNode Delete(AVLNode node, int key)
        {

            if (node == null) return null;

            // 1. Duyệt tìm nút 
            if (key < node.Data.ID)
            {
                node.Left = Delete(node.Left, key);
            }
            else if (key > node.Data.ID)
            {
                node.Right = Delete(node.Right, key);
            }
            else // ĐÃ TÌM THẤY NÚT
            {

                int duplicateCount = CountDuplicates(node);

                if (duplicateCount > 1)// Nút có nhiều bản ghi trùng lặp
                {

                    node.DuplicatesHead = node.DuplicatesHead.Next; // Xóa bản ghi đầu tiên trong danh sách liên kết

                    UpdateHeight(node);
                    return node;
                }
                else
                {
                    //Chỉ còn 1 bản ghi (hoặc nút lá) xóa nút AVL tiêu chuẩn


                    if (node.Left == null || node.Right == null)
                    {
                        AVLNode temp = (node.Left != null) ? node.Left : node.Right;
                        return temp; 
                    }

                    else
                    {
                        AVLNode temp = FindMinNode(node.Right);

                        node.Data = temp.Data;

                        node.DuplicatesHead = temp.DuplicatesHead;

                        node.Right = Delete(node.Right, temp.Data.ID);
                    }
                }
            }

            // 4. Cân bằng lại cây 
            if (node == null)
            {
                return node;
            }
            UpdateHeight(node);
            int balance = GetBalanceFactor(node);

            if (balance > 1 && GetBalanceFactor(node.Left) >= 0)
            {
                return RotateRight(node);
            }
            if (balance > 1 && GetBalanceFactor(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && GetBalanceFactor(node.Right) <= 0)
            {
                return RotateLeft(node);
            }
            if (balance < -1 && GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            return node;
        }
        public void DisplayNodesAtLevel(int k)
        {
            if (k <= 0)
            {
                Console.WriteLine("Tầng phải là số dương (bắt đầu từ 1).");
                return;
            }
            if (Root == null)
            {
                Console.WriteLine("Cây rỗng.");
                return;
            }
            Console.Write($"Các nút ở tầng {k}: ");
            bool found = DisplayNodesAtLevelRecursive(Root, k, 1);

            if (!found)
            {
                if (k > GetHeight(Root))
                {
                    Console.WriteLine("Tầng này không tồn tại trong cây.");
                }
                else
                {
                    Console.WriteLine("Không tìm thấy nút nào (Lỗi logic).");
                }
            }
            Console.WriteLine();
        }

        private bool DisplayNodesAtLevelRecursive(AVLNode node, int targetLevel, int currentLevel)
        {
            if (node == null)
            {
                return false;
            }
            if (currentLevel == targetLevel)
            {
                Console.Write($"{node.Data.ID} ");
                return true;
            }
            if (currentLevel < targetLevel)
            {
                bool leftFound = DisplayNodesAtLevelRecursive(node.Left, targetLevel, currentLevel + 1);
                bool rightFound = DisplayNodesAtLevelRecursive(node.Right, targetLevel, currentLevel + 1);

                return leftFound || rightFound;
            }
            return false;
        }
        public AVLNode FindNode(int key)
        {
            return FindNodeRecursive(Root, key);
        }

        private AVLNode FindNodeRecursive(AVLNode node, int key)
        {
            if (node == null) return null;
            if (key < node.Data.ID)
                return FindNodeRecursive(node.Left, key);
            else if (key > node.Data.ID)
                return FindNodeRecursive(node.Right, key);
            else
                return node; // Trả về nút AVLNode
        }
        public int DemChieuCao() => GetHeight(Root);
        public int DemNode() => LayNode(Root);

        private int LayNode(AVLNode node)
        {
            if (node == null) return 0;
            return 1 + LayNode(node.Left) + LayNode(node.Right);
        }
        //Liệt kê ID trung lặp
        public List<int> GetDuplicateIDs()
        {
            List<int> duplicateIDs = new List<int>();
            FindDuplicateIDsRecursive(Root, duplicateIDs);
            return duplicateIDs;
        }
        private void FindDuplicateIDsRecursive(AVLNode node, List<int> duplicateIDs)
        {
            if (node == null) return;

            // 1. Duyệt cây con bên trái
            FindDuplicateIDsRecursive(node.Left, duplicateIDs);

            // 2. Kiểm tra Nút hiện tại

            // Dựa trên logic của bạn, DuplicatesHead chứa TẤT CẢ các bản ghi (bao gồm bản ghi đầu tiên).
            // Nếu số lượng > 1, ID này bị trùng.
            if (CountDuplicates(node) > 1)
            {
                duplicateIDs.Add(node.Data.ID);
            }

            // 3. Duyệt cây con bên phải
            FindDuplicateIDsRecursive(node.Right, duplicateIDs);
        }
        private int CountTotalRecordsInNode(AVLNode node)
        {
            if (node == null) return 0;

            int count = 0;
            SinglyNode current = node.DuplicatesHead;
            while (current != null)
            {
                count++;
                current = current.Next;
            }
            return count;
        }
        public int CountDuplicateNodes()
        {
            return CountDuplicateNodesRecursive(Root);
        }

        private int CountDuplicateNodesRecursive(AVLNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int count = CountDuplicateNodesRecursive(node.Left) + CountDuplicateNodesRecursive(node.Right);

            // Kiểm tra: Nếu số lượng bản ghi >= 2, tức là có ID bị trùng
            if (CountTotalRecordsInNode(node) >= 2)
            {
                count++;
            }

            return count;
        }
        public (int ID, int MaxCount) FindMostDuplicateID()
        {
            int maxID = -1;
            int maxCount = 0;

            FindMostDuplicateIDRecursive(Root, ref maxID, ref maxCount);

            return (maxID, maxCount);
        }

        private void FindMostDuplicateIDRecursive(AVLNode node, ref int maxID, ref int maxCount)
        {
            if (node == null)
            {
                return;
            }

            // 1. Duyệt sang trái
            FindMostDuplicateIDRecursive(node.Left, ref maxID, ref maxCount);

            // 2. Xử lý nút hiện tại
            int currentCount = CountTotalRecordsInNode(node);

            if (currentCount > maxCount)
            {
                maxCount = currentCount;
                maxID = node.Data.ID;
            }
            // LƯU Ý: Nếu currentCount == maxCount, ID đầu tiên tìm thấy sẽ được giữ lại.

            // 3. Duyệt sang phải
            FindMostDuplicateIDRecursive(node.Right, ref maxID, ref maxCount);
        }
        public void Clear()
        {
            Root = null;
        }
        public List<CustomerRecord> GetAllDuplicateRecords()
        {
            List<CustomerRecord> duplicates = new List<CustomerRecord>();
            GetAllDuplicateRecordsRecursive(Root, duplicates);
            return duplicates;
        }

        // Phương thức đệ quy để duyệt cây và thu thập các bản ghi trùng lặp
        private void GetAllDuplicateRecordsRecursive(AVLNode node, List<CustomerRecord> list)
        {
            if (node == null)
            {
                return;
            }

            // 1. Duyệt cây con bên trái
            GetAllDuplicateRecordsRecursive(node.Left, list);

            if (node.DuplicatesHead != null && node.DuplicatesHead.Next != null)
            {
                // Bắt đầu từ bản ghi thứ 2 (là bản ghi trùng lặp đầu tiên)
                SinglyNode current = node.DuplicatesHead.Next;

                while (current != null)
                {
                    // Thêm các bản ghi trùng lặp vào danh sách kết quả
                    list.Add(current.Data);
                    current = current.Next;
                }
            }

            // 3. Duyệt cây con bên phải
            GetAllDuplicateRecordsRecursive(node.Right, list);
        }
    }
}


