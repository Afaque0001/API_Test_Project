using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject.Models
{
    public class Post
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
