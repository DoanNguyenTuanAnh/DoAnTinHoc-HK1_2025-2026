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
        private int LayKhoa(CustomerRecord record)
        {
            return KeySelector(record);
        }

        // --- CÂN BẰNG ---
        private int LayChieuCao(AVLNode node)
        {
            return (node == null) ? 0 : node.Height;
        }

        private void CapNhatChieuCao(AVLNode node)
        {
            if (node != null)
            {
                node.Height = 1 + Math.Max(LayChieuCao(node.Left), LayChieuCao(node.Right));
            }
        }

        // kiểm tra cân bằng tại nút
        private int KTCanBang(AVLNode node)
        {
            if (node == null)
                return 0;
            return LayChieuCao(node.Left) - LayChieuCao(node.Right);
        }

        // --- CÁC PHÉP QUAY (ROTATIONS) ---
        private AVLNode QuayPhai(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;
            x.Right = y;
            y.Left = T2;
            CapNhatChieuCao(y);
            CapNhatChieuCao(x);
            return x;
        }

        private AVLNode QuayTrai(AVLNode x)
        {
            AVLNode y = x.Right;
            AVLNode T2 = y.Left;
            y.Left = x;
            x.Right = T2;
            CapNhatChieuCao(x);
            CapNhatChieuCao(y);
            return y;
        }

        // --- CHÈN (INSERT) KHÓA 2 CẤP ---
        private AVLNode CanBang(AVLNode node)
        {
            CapNhatChieuCao(node);
            int balance = KTCanBang(node);
            // Trường hợp 1: lệch trái trái
            if (balance > 1 && KTCanBang(node.Left) >= 0)
                return QuayPhai(node);
            // Trường hợp 2:lệch phải phải
            if (balance < -1 && KTCanBang(node.Right) <= 0)
                return QuayTrai(node);
            // Trường hợp 3: lệch trái phải
            if (balance > 1 && KTCanBang(node.Left) < 0)
            {
                node.Left = QuayTrai(node.Left);
                return QuayPhai(node);
            }
            // Trường hợp 4: lệch phải trái
            if (balance < -1 && KTCanBang(node.Right) > 0)
            {
                node.Right = QuayPhai(node.Right);
                return QuayTrai(node);
            }
            return node;
        }

        public void Chen(CustomerRecord record)
        {
            Root = Chen(Root, record);
        }

        private AVLNode Chen(AVLNode node, CustomerRecord record)
        {
            if (node == null)
                return new AVLNode(record);

            int recordKey = LayKhoa(record);
            int nodeKey = LayKhoa(node.Data);

            // 1. So sánh Cấp 1 (Khóa Chính Linh hoạt)
            if (recordKey < nodeKey)
            {
                node.Left = Chen(node.Left, record);
            }
            else if (recordKey > nodeKey)
            {
                node.Right = Chen(node.Right, record);
            }
            else // Khóa Chính trùng
            {
                return null; // Không chèn bản ghi có khóa chính trùng
            }
            AVLNode aVLNode = CanBang(node);
            return aVLNode;
        }

        // --- TÌM KIẾM (SEARCH) ---

        public CustomerRecord TimKiem(int keyValue)
        {
            return TimKiem(Root, keyValue);
        }

        private CustomerRecord TimKiem(AVLNode node, int keyValue)
        {
            if (node == null)
                return null;

            int nodeKey = LayKhoa(node.Data);

            if (keyValue == nodeKey)
            {
                return node.Data; // Trả về bản ghi đầu tiên có khóa chính trùng
            }

            if (keyValue < nodeKey)
                return TimKiem(node.Left, keyValue);
            else
                return TimKiem(node.Right, keyValue);
        }

        // --- TÌM KIẾM MIN (Hỗ trợ Xóa) ---
        private AVLNode TimMin(AVLNode node)
        {
            AVLNode current = node;
            while (current.Left != null)
            {
                current = current.Left;
            }
            return current;
        }

        // --- XÓA (DELETE) ---

        public void Xoa(int primaryKey, int secondaryID)
        {
            Root = Xoa(Root, primaryKey, secondaryID);
        }

        private AVLNode Xoa(AVLNode node, int primaryKey, int secondaryID)
        {
            if (node == null)
                return node;

            int nodeKey = LayKhoa(node.Data);

            // 1. Tìm kiếm nút cần xóa (2 cấp)
            if (primaryKey < nodeKey)
            {
                node.Left = Xoa(node.Left, primaryKey, secondaryID);
            }
            else if (primaryKey > nodeKey)
            {
                node.Right = Xoa(node.Right, primaryKey, secondaryID);
            }
            else // primaryKey == nodeKey
            {
                if (secondaryID < node.Data.CustomerID)
                    node.Left = Xoa(node.Left, primaryKey, secondaryID);
                else if (secondaryID > node.Data.CustomerID)
                    node.Right = Xoa(node.Right, primaryKey, secondaryID);
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
                        AVLNode temp = TimMin(node.Right);
                        node.Data = temp.Data;
                        // Xóa nút kế thừa (dùng 2 khóa)
                        node.Right = Xoa(node.Right, LayKhoa(temp.Data), temp.Data.CustomerID);
                    }
                }
            }

            if (node == null)
                return node;

            AVLNode aVLNode = CanBang(node);
            return aVLNode;

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

        public int GetTreeHeight() => LayChieuCao(Root);
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