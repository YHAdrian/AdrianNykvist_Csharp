using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnBStatistics
{
    class Country
    {
        private string namn;
        private int antalInvanare;
        private int bnpPerCapita;
        private List<City> objektlistCities = new List<City>();

        public Country(string Namn, int AntalInvanare, int BnpPerCapita)
        {
            this.namn = Namn;
            this.antalInvanare = AntalInvanare;
            this.bnpPerCapita = BnpPerCapita;
            
        }

        public string Namn { get => namn; set => namn = value; }
        public int AntalInvanare { get => antalInvanare; set => antalInvanare = value; }
        public int BnpPerCapita { get => bnpPerCapita; set => bnpPerCapita = value; }
        public List<City> ObjektlistCities { get => objektlistCities; set => objektlistCities = value; }

        public void AddCity(City myCity)
        {
            objektlistCities.Add(myCity);
        }
    }
}
