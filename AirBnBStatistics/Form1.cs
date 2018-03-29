using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Windows.Forms.DataVisualization.Charting;



// Väldigt bra skriven kod, Personligen hade jag velat se mer användande av listor och foreach loopar vid initiering av data för att öka normaliseringen.
// så vid eventuella tillägg av städer behöver ma bara lägga in namn vid ett ställe, vilket minskar risk för fel och onödigt arbete.
// Sen är de lite konstigt att Countryconstruktorn inte har ett värde för stadslistan, vilket resulterar vid nullvärden och eventuella fel vid skapande (tror jag?!)
// I övrigt tydligt och bra skrivet.
namespace AirBnBStatistics
{
    public partial class Form1 : Form
    {
        SqlConnection conn = new SqlConnection();

        private List<Country> World = new List<Country>();
        private City valdStad;
        int maxPrice = 500;

        private void ScatterPlot()//
        {
            scatterChart.Series.Clear();
            scatterChart.Series.Add(valdStad.Namn);
            scatterChart.Titles.Clear();

            List<Accomodation> accoList = valdStad.ObjektlistaMedAccommodations;
            accoList.Select(x => MessageBox.Show(x.Overall_satisfaction.ToString()));

            //tar ut price och overall_satisfacton där overall_satisfaction är mindre än 4.5 och priset är mindre än 1000
            var datapoints = from room in accoList
                             where room.Overall_satisfaction < 4.5
                             where room.Price < maxPrice
                             select new { room.Overall_satisfaction, room.Price };

            foreach (var acco in datapoints)
            {
                scatterChart.Series[valdStad.Namn].Points.AddXY(acco.Price, acco.Overall_satisfaction);
            }

            //Sätter namn och utseende på charten
            scatterChart.Series[valdStad.Namn].ChartType = SeriesChartType.Point;
            scatterChart.ChartAreas["ChartArea1"].AxisX.Title = "Pris";
            scatterChart.ChartAreas["ChartArea1"].AxisY.Title = "Betyg";
            scatterChart.Legends["Legend1"].IsDockedInsideChartArea = true;
            scatterChart.Legends["Legend1"].DockedToChartArea = "ChartArea1";
            scatterChart.Titles.Add(valdStad.Namn + " Pris vs Betyg");

        }

        private void HistoChart()
        {
            histoChart.Series.Clear();
            histoChart.Series.Add(valdStad.Namn);
            histoChart.Titles.Clear();

            List<Accomodation> accoList = valdStad.ObjektlistaMedAccommodations;

            int slice = 50; //Ställ in vilket spann kolumnerna ska visa

            var datapoints = from room in accoList
                             where room.Room_type == "Private room"
                             select new { room.Price, rest = room.Price % slice}; //Grupperar inom spannet vi satt i slice

            var setBuckets = from x in datapoints
                             select new { bucket = x.Price - x.rest}; //bestämmer vad spannen ska vara

            var buckets = from y in setBuckets
                          group y by y.bucket into groupedBucket
                          orderby groupedBucket.Key
                          select new { bucket = string.Format("{0}-{1}", groupedBucket.Key, groupedBucket.Key + slice),
                              count = groupedBucket.Count()}; //grupperar buckets och räknar hur många punkter varje bucket har
            
            foreach (var acco in buckets)
            {
                histoChart.Series[valdStad.Namn].Points.AddXY(acco.bucket, acco.count);
            }
            //Sätter namn och utseende på charten
            histoChart.Series[valdStad.Namn].ChartType = SeriesChartType.Column;
            histoChart.ChartAreas["ChartArea1"].AxisX.Interval = 1;
            histoChart.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineWidth = 0;
            histoChart.ChartAreas["ChartArea1"].AxisX.Title = "Pris";
            histoChart.ChartAreas["ChartArea1"].AxisY.Title = "Antal";
            histoChart.Legends["Legend1"].IsDockedInsideChartArea = true;
            histoChart.Legends["Legend1"].DockedToChartArea = "ChartArea1";
            histoChart.Titles.Add(valdStad.Namn + " Pris vs Antal");

        }

        public Form1()
        {
            InitializeComponent();

            conn.ConnectionString = "Data Source=DESKTOP-VRGDF71;Initial Catalog=Cars;Integrated Security=True";

        }

        private List<Accomodation> GetAcco(string myCity)
        {
            List<Accomodation> roomList = new List<Accomodation>();
            try
            {
                conn.Open();
                stadValjare.Items.Add(myCity);

                SqlCommand myCommand = new SqlCommand($"select * from {myCity};", conn);

                SqlDataReader myReader = myCommand.ExecuteReader();
                
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

                while (myReader.Read())
                {                    

                    int room_id = (int)myReader["room_id"];
                    host_id = (int)myReader["host_id"];
                    room_type = myReader["room_type"].ToString();
                    borough = myReader["borough"].ToString();
                    neighbourhood = myReader["neighborhood"].ToString();
                    reviews = (int)myReader["reviews"];
                    overall_satisfaction = (double)myReader["overall_satisfaction"];
                    accomodates = (int)myReader["accommodates"];
                    bedrooms = int.TryParse(myReader["bedrooms"].ToString(), out int x) ? x : 0;
                    price = (double)myReader["price"];
                    minstay = int.TryParse(myReader["minstay"].ToString(), out int y) ? y : 0;
                    latitude = (double)myReader["latitude"];
                    longitude = (double)myReader["longitude"];
                    last_modified = myReader["last_modified"].ToString();

                    Accomodation tempRoom = new Accomodation(room_id, host_id, room_type, borough,
                                                            neighbourhood, reviews, overall_satisfaction,
                                                            accomodates, bedrooms, price, minstay, latitude,
                                                            longitude, last_modified);

                    roomList.Add(tempRoom);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                conn.Close();
            }

            return roomList;
        }       
       
        private void InitData()
        {
                        
            List<Accomodation> amsterdamList = GetAcco("Amsterdam");
            
            List<Accomodation> barcelonaList = GetAcco("Barcelona");
            
            List<Accomodation> bostonList = GetAcco("Boston");

            
            City Amsterdam = new City("Amsterdam", 838668, 21398, 15854000, amsterdamList);

            City Barcelona = new City("Barcelona", 1619337, 19000, 7400000, barcelonaList);

            City Boston = new City("Boston", 673184, 38034, 2000000, bostonList);

            Country Usa = new Country("USA", 343, 57466);
            Usa.AddCity(Boston);            
            
            Country Spanien = new Country("Spanien", 46, 29651);
            Spanien.AddCity(Barcelona);

            Country Nederlanderna = new Country("Nederländerna", 17, 53139);
            Nederlanderna.AddCity(Amsterdam);
            valdStad = Amsterdam;

            World.Add(Usa);
            World.Add(Spanien);
            World.Add(Nederlanderna);
        }

        private void Form1_Load(object sender, EventArgs e)
            {

            InitData();
            ScatterPlot();
            HistoChart();
            
            }       

        private void stadValjare_DropDownClosed(object sender, EventArgs e)
        {
            //Grottar in i listorna för att hitta staden man väljer i dropdown listan och visa den stadens data
           foreach (Country x in World)
            {
                foreach (City y in x.ObjektlistCities)
                {
                    if (y.Namn == stadValjare.SelectedItem as string)
                    {
                        valdStad = y;
                        ScatterPlot();
                        HistoChart();
                    }                        
                }
            }
        }
        //Förstorar och förminskar charts när man klickar/dubbelklickar på de
        private void scatterChart_Click(object sender, EventArgs e)
            {
                tableLayoutPanel1.SetRowSpan(scatterChart, 2);
                histoChart.Visible = false;
            }

        private void scatterChart_DoubleClick(object sender, EventArgs e)
            {
                tableLayoutPanel1.SetRowSpan(scatterChart, 1);
                histoChart.Visible = true;
            }

        private void histoChart_Click(object sender, EventArgs e)
            {
                tableLayoutPanel1.SetRow(histoChart, 0);
                tableLayoutPanel1.SetRowSpan(histoChart, 2);
                scatterChart.Visible = false;
            }

        private void histoChart_DoubleClick(object sender, EventArgs e)
            {
                tableLayoutPanel1.SetRow(scatterChart, 0);
                tableLayoutPanel1.SetRow(histoChart, 1);
                tableLayoutPanel1.SetRowSpan(histoChart, 1);
                scatterChart.Visible = true;
            }

        private void button1_Click(object sender, EventArgs e)
            {
                maxPrice = int.TryParse(textBox1.Text, out int y) ? y : 500; //Sätter startvärde på Scatterplot till 500 och ändrar till värde i textbox vid click
                ScatterPlot();
            }
    }
}
