using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc_HK1_2025_2026
{
    internal class AVLNode
    {
        public CustomerRecord Data { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }
        public int Height { get; set; }

        public AVLNode(CustomerRecord data)
        {
            Data = data;
            Height = 1;
        }
    }
}
