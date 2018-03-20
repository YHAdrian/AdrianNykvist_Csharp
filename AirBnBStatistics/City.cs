using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnBStatistics
{
    class City
    {
        private string namn;
        private int antalInvanare;
        private int medelinkomstPerInvanare;
        private int antalTuristerPerAr;
        private List<Accomodation> objektlistaMedAccommodations = new List<Accomodation>();
        private int antalAccommodations; //Skall skapas och synkronisera med objektlistan med accommodations! 
        private double medelkostnadenPerNattAirBnBStaden; //Skall skapas och synkronisera med objektlistan med accommodations! 

        public City(string Namn, int AntalInvanare, int MedelinkomstPerInvanare, int AntalTuristerPerAr, 
                    List<Accomodation> ObjektlistaMedAccommodations)
        {
            this.namn = Namn;
            this.antalInvanare = AntalInvanare;
            this.medelinkomstPerInvanare = MedelinkomstPerInvanare;
            this.antalTuristerPerAr = AntalTuristerPerAr;
            this.objektlistaMedAccommodations = ObjektlistaMedAccommodations;
            this.antalAccommodations = ObjektlistaMedAccommodations.Count;
            this.medelkostnadenPerNattAirBnBStaden = ObjektlistaMedAccommodations.Average(x => x.Price);
            
        }

        public string Namn { get => namn; set => namn = value; }
        public int AntalInvanare { get => antalInvanare; set => antalInvanare = value; }
        public int MedelinkomstPerInvanare { get => medelinkomstPerInvanare; set => medelinkomstPerInvanare = value; }
        public int AntalTuristerPerAr { get => antalTuristerPerAr; set => antalTuristerPerAr = value; }
        public int AntalAccommodations { get => antalAccommodations; set => antalAccommodations = value; }
        public double MedelkostnadenPerNattAirBnBStaden { get => medelkostnadenPerNattAirBnBStaden; set => medelkostnadenPerNattAirBnBStaden = value; }
        public List<Accomodation> ObjektlistaMedAccommodations { get => objektlistaMedAccommodations; set => objektlistaMedAccommodations = value; }


    }
}
