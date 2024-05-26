using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeID.BOP.Entities
{
    public class CustomerHistory
    {
        public string CommentedBy { get; set; }

        public DateTime CommentedOn { get; set; }

        public string Status { get; set; }

        public string Text { get; set; }
    }
}
