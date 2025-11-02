using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoAnTinHoc_HK1_2025_2026
{
    internal class AVLTree
    {
        private AVLNode Root { get; set; }

        // Delegate (Func) để lấy KHÓA CHÍNH (int) từ CustomerRecord
        public Func<CustomerRecord, int> KeySelector { get; private set; }

        public AVLTree(Func<CustomerRecord, int> keySelector)
        {
            Root = null;
            KeySelector = keySelector;
        }

        // Hàm hỗ trợ lấy khóa chính
        private int GetKey(CustomerRecord record)
        {
            return KeySelector(record);
        }

        // --- HỖ TRỢ CÂN BẰNG ---
        private int GetHeight(AVLNode node) => (node == null) ? 0 : node.Height;

        private void UpdateHeight(AVLNode node)
        {
            if (node != null)
            {
                node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            }
        }

        private int GetBalanceFactor(AVLNode node) => (node == null) ? 0 : GetHeight(node.Left) - GetHeight(node.Right);

        // --- CÁC PHÉP QUAY (ROTATIONS) ---
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

        // --- CHÈN (INSERT) KHÓA 2 CẤP ---

        public void Insert(CustomerRecord record)
        {
            Root = Insert(Root, record);
        }

        private AVLNode Insert(AVLNode node, CustomerRecord record)
        {
            if (node == null)
                return new AVLNode(record);

            int recordKey = GetKey(record);
            int nodeKey = GetKey(node.Data);

            // 1. So sánh Cấp 1 (Khóa Chính Linh hoạt)
            if (recordKey < nodeKey)
            {
                node.Left = Insert(node.Left, record);
            }
            else if (recordKey > nodeKey)
            {
                node.Right = Insert(node.Right, record);
            }
            else // Khóa Chính trùng
            {
                // 2. So sánh Cấp 2 (CustomerID)
                if (record.CustomerID < node.Data.CustomerID)
                    node.Left = Insert(node.Left, record);
                else if (record.CustomerID > node.Data.CustomerID)
                    node.Right = Insert(node.Right, record);
                else
                    return node; // ID trùng (trùng lặp hoàn toàn)
            }

            // Cập nhật chiều cao
            UpdateHeight(node);

            // Cân bằng
            int balance = GetBalanceFactor(node);

            // Trường hợp 1: Left Left Case (Nút con trái lệch trái hoặc cân bằng)
            if (balance > 1 && GetBalanceFactor(node.Left) >= 0) // >= 0: Bao gồm cả trường hợp chèn vào nhánh trái của nút con trái và trường hợp chèn vào nút có khóa trùng
                return RotateRight(node);

            // Trường hợp 2: Right Right Case (Nút con phải lệch phải hoặc cân bằng)
            if (balance < -1 && GetBalanceFactor(node.Right) <= 0) // <= 0: Bao gồm cả trường hợp chèn vào nhánh phải của nút con phải và trường hợp chèn vào nút có khóa trùng
                return RotateLeft(node);

            // Trường hợp 3: Left Right Case
            if (balance > 1 && GetBalanceFactor(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            // Trường hợp 4: Right Left Case
            if (balance < -1 && GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        // --- TÌM KIẾM (SEARCH) LINH HOẠT ---

        public CustomerRecord Search(int keyValue)
        {
            return Search(Root, keyValue);
        }

        private CustomerRecord Search(AVLNode node, int keyValue)
        {
            if (node == null)
                return null;

            int nodeKey = GetKey(node.Data);

            if (keyValue == nodeKey)
            {
                return node.Data; // Trả về bản ghi đầu tiên có khóa chính trùng
            }

            if (keyValue < nodeKey)
                return Search(node.Left, keyValue);
            else
                return Search(node.Right, keyValue);
        }

        // --- TÌM KIẾM MIN (Hỗ trợ Xóa) ---
        private AVLNode FindMin(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        // --- XÓA (DELETE) KHÓA 2 CẤP ---

        public void Delete(int primaryKey, int secondaryID)
        {
            Root = Delete(Root, primaryKey, secondaryID);
        }

        private AVLNode Delete(AVLNode node, int primaryKey, int secondaryID)
        {
            if (node == null)
                return node;

            int nodeKey = GetKey(node.Data);

            // 1. Tìm kiếm nút cần xóa (2 cấp)
            if (primaryKey < nodeKey)
            {
                node.Left = Delete(node.Left, primaryKey, secondaryID);
            }
            else if (primaryKey > nodeKey)
            {
                node.Right = Delete(node.Right, primaryKey, secondaryID);
            }
            else // primaryKey == nodeKey
            {
                if (secondaryID < node.Data.CustomerID)
                    node.Left = Delete(node.Left, primaryKey, secondaryID);
                else if (secondaryID > node.Data.CustomerID)
                    node.Right = Delete(node.Right, primaryKey, secondaryID);
                else // Đã tìm thấy nút chính xác (Xóa BST)
                {
                    // 0 hoặc 1 con
                    if (node.Left == null || node.Right == null)
                    {
                        AVLNode temp = (node.Left != null) ? node.Left : node.Right;
                        node = temp;
                    }
                    // 2 con
                    else
                    {
                        AVLNode temp = FindMin(node.Right);
                        node.Data = temp.Data;
                        // Xóa nút kế thừa (dùng 2 khóa)
                        node.Right = Delete(node.Right, GetKey(temp.Data), temp.Data.CustomerID);
                    }
                }
            }

            if (node == null)
                return node;

            // 2. Cập nhật chiều cao và Cân bằng
            UpdateHeight(node);
            int balance = GetBalanceFactor(node);

            // Cân bằng (giữ nguyên logic chuẩn của AVL sau khi xóa)
            if (balance > 1 && GetBalanceFactor(node.Left) >= 0) return RotateRight(node);
            if (balance > 1 && GetBalanceFactor(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }
            if (balance < -1 && GetBalanceFactor(node.Right) <= 0) return RotateLeft(node);
            if (balance < -1 && GetBalanceFactor(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }


        // --- DUYỆT CÂY & THỐNG KÊ (Giữ nguyên) ---

        public List<CustomerRecord> GetAllRecords()
        {
            List<CustomerRecord> records = new List<CustomerRecord>();
            InorderTraversal(Root, records);
            return records;
        }

        private void InorderTraversal(AVLNode node, List<CustomerRecord> records)
        {
            if (node != null)
            {
                InorderTraversal(node.Left, records);
                records.Add(node.Data);
                InorderTraversal(node.Right, records);
            }
        }

        public int GetTreeHeight() => GetHeight(Root);
        public int CountNodes() => CountNodesRecursive(Root);

        private int CountNodesRecursive(AVLNode node) =>
            (node == null) ? 0 : 1 + CountNodesRecursive(node.Left) + CountNodesRecursive(node.Right);

        public int CountLeafNodes() => CountLeafNodesRecursive(Root);

        private int CountLeafNodesRecursive(AVLNode node)
        {
            if (node == null) return 0;
            if (node.Left == null && node.Right == null) return 1;
            return CountLeafNodesRecursive(node.Left) + CountLeafNodesRecursive(node.Right);
        }
    }
}