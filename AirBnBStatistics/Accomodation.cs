using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBnBStatistics
{
    class Accomodation
    {
        int room_id;
        int host_id;
        string room_type;
        string borough;
        string neighbourhood;
        int reviews;
        double overall_satisfaction;
        int accomodates;
        int bedrooms;
        double price;
        int minstay;
        double latitude;
        double longitude;
        string last_modified;

        
       public Accomodation(int Room_id, int Host_id, string Room_type, string Borough, string Neighbourhood, int Reviews,
        double Overall_satisfaction, int Accomodates, int Bedrooms, double Price, int Minstay, double Latitude,
        double Longitude, string Last_modified)

        {
            this.room_id = Room_id;
            this.host_id = Host_id;
            this.room_type = Room_type;
            this.borough = Borough;
            this.neighbourhood = Neighbourhood;
            this.reviews = Reviews;
            this.overall_satisfaction = Overall_satisfaction;
            this.accomodates = Accomodates;
            this.bedrooms = Bedrooms;
            this.price = Price;
            this.minstay = Minstay;
            this.latitude = Latitude;
            this.longitude = Longitude;
            this.last_modified = Last_modified;
        }

        public int Room_id { get => room_id; set => room_id = value; }
        public int Host_id { get => host_id; set => host_id = value; }
        public string Room_type { get => room_type; set => room_type = value; }
        public string Borough { get => borough; set => borough = value; }
        public string Neighbourhood { get => neighbourhood; set => neighbourhood = value; }
        public int Reviews { get => reviews; set => reviews = value; }
        public double Overall_satisfaction { get => overall_satisfaction; set => overall_satisfaction = value; }
        public int Accomodates { get => accomodates; set => accomodates = value; }
        public int Bedrooms { get => bedrooms; set => bedrooms = value; }
        public double Price { get => price; set => price = value; }
        public int Minstay { get => minstay; set => minstay = value; }
        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
        public string Last_modified { get => last_modified; set => last_modified = value; }
    }
}
