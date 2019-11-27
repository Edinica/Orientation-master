using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public class Room
    {
        private string Name;        // Имя аудитории
        private Floor  Floor;       // этаж
        private string Description; // описание
        private string AnyInform;   // доп инфа 
        private string Staff;       // работники
        private string Timetable;   // расписание
        private string Phone;       // телефон
        private string Site;        // сайт
        public  bool   favorite;    // избранность

        private List<string> Note; //заметки

        private List<IVertex> Door; // выходы из аудиторий 

        private List<IVertex> Walls; // cтеныа удитории

        public Room()
        {
            Door = new List<IVertex>();
            Walls = new List<IVertex>();
        }
        public Room(string name,Floor floor, string timetable,
                    string description,      string anyinform,
                    List<IVertex>door,List<IVertex>walls):base()
        {
            Name = name;
            Floor = floor;
            Timetable = timetable;
            Description = description;
            AnyInform = anyinform;

            foreach (var i in door)
                Door.Add(i);

            foreach (var i in walls)
                Walls.Add(i);

        }


    }
}