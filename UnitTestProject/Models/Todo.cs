using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject.Models
{
   public class Todo
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string due_on { get; set; }
        public string status { get; set; }
    }
}
