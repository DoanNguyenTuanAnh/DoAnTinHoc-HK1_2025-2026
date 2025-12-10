using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnTinHoc
{
    [Serializable]
    public class SinglyNode
    {

        public CustomerRecord Data { get; set; }
        public SinglyNode Next { get; set; }

        public SinglyNode(CustomerRecord data)
        {
            this.Data = data;
            this.Next = null;
        }
    }
}