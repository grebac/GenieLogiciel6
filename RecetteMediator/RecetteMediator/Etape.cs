using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecetteMediator
{
    public class Etape
    {
        static int count = 0;

        public string etapeName;
        public string etapeDesc;
        public List<string> ingredients;

        public Etape(string etapeDesc, List<string> ingredients)
        {
            this.etapeName = "etape" + ++count;
            this.etapeDesc = etapeDesc;
            this.ingredients = ingredients;
        }

        public override string ToString()
        {
            var str = String.Format("{0} ({1}) : ", etapeName, etapeDesc);
            foreach (var item in ingredients) {  str += " " + item.ToString(); }
            return str;
        }
    }
}
