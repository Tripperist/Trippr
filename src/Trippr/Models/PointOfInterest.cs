using Microsoft.Maui.Devices.Sensors;


namespace Trippr.Models
{
    public class PointOfInterest 
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Distance { get; set; }
        public bool IsFavorite { get; set; }
        public Uri Link { get; set; }

        public Location Location { get; set; }
        public Placemark Placemark { get; set; }

        public PointOfInterest()
        { }

        public PointOfInterest(string name, string description, Placemark placemark)
        {
            Name = name;
            Description = description;
            IsFavorite = false;
            Distance = 0d;
            Placemark = placemark;
        }

        public PointOfInterest(string name, string description, double distance, Placemark placemark)
        {
            Name = name;
            Description = description;
            IsFavorite = false;
            Distance = distance;
            Placemark = placemark;
        }

        public PointOfInterest(string name, string description, double distance, Uri link, Placemark placemark)
        {
            Name = name;
            Description = description;
            IsFavorite = false;
            Distance = distance;
            Link = link;
            Placemark = placemark;
        }

    }
}
