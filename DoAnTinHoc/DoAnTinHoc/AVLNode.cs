using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    [Serializable]
    public class AVLNode
    {
        public int CustomerIDKey { get; set; }
        public SinglyNode DuplicatesHead { get; set; }
        public CustomerRecord Data { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }
        public int Height { get; set; }

        public AVLNode(CustomerRecord data)
        {
            this.CustomerIDKey = 0;

            // Khởi tạo Danh sách Liên kết Đơn (chứa bản ghi đầu tiên)
            this.DuplicatesHead = new SinglyNode(data);

            this.Data = data;
            this.Left = null;
            this.Right = null;
            this.Height = 1;
        }
    }
}