using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1.Сеть
{
    public class Note
    {
        public string Message { get; set; }

        public string Date { get; set; }

        public uint Rating { get; set; } 

        public Room Owner { get; set; } // какой аудитории принадлежит

    }
}
