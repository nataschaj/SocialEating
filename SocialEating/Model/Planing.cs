using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialEating.Model
{
    public class Planing
    {
        public string menu { get; set; }
        public string chefKok { get; set; }
        public string helper { get; set; }
        public string cleaner { get; set; }
        public string day { get; set; }


        public override string ToString()
        {
            return menu + " \n " + chefKok + "\n" + helper + "\n" + cleaner;
        }
    }
}
