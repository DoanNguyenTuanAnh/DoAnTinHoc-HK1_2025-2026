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
        private int GetHeight(AVLNode node)//Lấy chiều cao
        {
            return node == null ? 0 : node.Height;
        }
        private void UpdateHeight(AVLNode node)//Cập nhật chiều cao
        {
            if (node != null)
            {
                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            }
        }
        private int GetBalanceFactor(AVLNode node)//Kiểm Tra Cân bằng
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
        public AVLNode CanBang(AVLNode node)
        {
            UpdateHeight(node);
            int balance = GetBalanceFactor(node);
            if (balance > 1 && GetBalanceFactor(node.Left) >= 0)
            {
                return RotateRight(node);
            }
            if (balance < -1 && GetBalanceFactor(node.Right) <= 0)
            {
                return RotateLeft(node);
            }
            if (balance > 1 && GetBalanceFactor(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }
            return node;
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
            AVLNode balancedNode = CanBang(node);
            return balancedNode;
        }
        public CustomerRecord Search(int key)
        {
            return Search(Root, key);
        }

        private CustomerRecord Search(AVLNode node, int key)//Tìm kiếm bản ghi theo key
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
        public AVLNode FindNode(int key)//Tìm nút AVLNode theo key
        {
            return FindNodeRecursive(Root, key);
        }

        private AVLNode FindNodeRecursive(AVLNode node, int key)//Hàm đệ quy tìm nút AVLNode theo key
        {
            if (node == null) return null;
            if (key < node.Data.ID)
                return FindNodeRecursive(node.Left, key);
            else if (key > node.Data.ID)
                return FindNodeRecursive(node.Right, key);
            else
                return node; // Trả về nút AVLNode
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
        // Hàm hỗ trợ để tìm nút có giá trị nhỏ nhất trong cây con
        private AVLNode FindMinNode(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
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
            AVLNode balancedNode = CanBang(node);
            return balancedNode;
        }
       
        public void Clear()//Xóa toàn bộ cây
        {
            Root = null;
        }
        public int DemChieuCao() => GetHeight(Root);
        public int DemNode() => LayNode(Root);

        private int LayNode(AVLNode node)//Đếm số nút trong cây
        {
            if (node == null) return 0;
            return 1 + LayNode(node.Left) + LayNode(node.Right);
        }
        public List<int> GetDuplicateIDs()//Lấy danh sách ID bị trùng
        {
            List<int> duplicateIDs = new List<int>();
            FindDuplicateIDsRecursive(Root, duplicateIDs);
            return duplicateIDs;
        }
        private void FindDuplicateIDsRecursive(AVLNode node, List<int> duplicateIDs)//Hàm đệ quy tìm ID bị trùng
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
        private int CountTotalRecordsInNode(AVLNode node)//Đếm tổng số bản ghi trong một nút AVLNode
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
        public int CountDuplicateNodes()//Đếm số nút có ID bị trùng
        {
            return CountDuplicateNodesRecursive(Root);
        }

        private int CountDuplicateNodesRecursive(AVLNode node)//Hàm đệ quy đếm số nút có ID bị trùng
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
        public (int ID, int MaxCount) FindMostDuplicateID()//Tìm ID có số bản ghi trùng nhiều nhất
        {
            int maxID = -1;
            int maxCount = 0;

            FindMostDuplicateIDRecursive(Root, ref maxID, ref maxCount);

            return (maxID, maxCount);
        }

        private void FindMostDuplicateIDRecursive(AVLNode node, ref int maxID, ref int maxCount)//Hàm đệ quy tìm ID có số bản ghi trùng nhiều nhất
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
        
        public List<CustomerRecord> GetAllDuplicateRecords()//Lấy tất cả các bản ghi trùng lặp trong cây
        {
            List<CustomerRecord> duplicates = new List<CustomerRecord>();
            GetAllDuplicateRecordsRecursive(Root, duplicates);
            return duplicates;
        }
        private void GetAllDuplicateRecordsRecursive(AVLNode node, List<CustomerRecord> list)//Hàm đệ quy lấy tất cả các bản ghi trùng lặp trong cây
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


