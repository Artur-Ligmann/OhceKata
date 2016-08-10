using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ohce
{
    class Application
    {
        public static void Main(string [] args)
        {
            var ohce = new Ohce("Artur", DateTime.Now, new ConsoleService());

            while(!ohce.Finished)
            {
                ohce.Run();
            }
        }
    }
}
