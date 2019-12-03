using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DXC_Project.Models
{
    public class QuestionsContext
    {
        public IEnumerable<Java_tbl1> Questions_apti { get; set; }
        public IEnumerable<Java_tbl1> Questions_java { get; set; }
        public IEnumerable<Java_tbl1> Questions_python { get; set; }
        public IEnumerable<Java_tbl1> Questions_csharp { get; set; }

    }
}