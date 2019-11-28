using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1.Сеть
{
    public class User
    {
        public string FirstName  { get; set; }
        public string SecondName { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }

        public List<Note> Notes { get; set; }

    }
}
