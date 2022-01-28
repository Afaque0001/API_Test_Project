using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject.Models
{
    public class Comment
    {
        public int id { get; set; }
        public int post_id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}
