using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace SocialEating.Model
{
    public class PlaningList : ObservableCollection<Planing>
    {
        public PlaningList() : base()
        {

        }

        public string GetJson()
        {
            string json = JsonConvert.SerializeObject(this);
            return json;
        }

        public void IndsetJson(string jsonText)
        {
            List<Planing> nyListe = JsonConvert.DeserializeObject<List<Planing>>(jsonText);

            foreach (var plan in nyListe)
            {
                this.Add(plan);
            }
        }
    }
}
