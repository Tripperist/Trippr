namespace Trippr.Models;

public class Trip {
    public string Name { get; set; }
    public string Description { get; set; }
    public double Distance { get; set; }
    public bool IsFavorite { get; set; }
    public Uri Link { get; set; }
    public IEnumerable<PointOfInterest> PointOfInterests { get; set; }

    public Microsoft.Maui.Devices.Sensors.Location Location { get; set; }
    public Microsoft.Maui.Devices.Sensors.Placemark Placemark { get; set; }

    public Trip() 
    {
        PointOfInterests = new List<PointOfInterest>();
    }

    public Trip(string name, string description, Microsoft.Maui.Devices.Sensors.Placemark placemark, List<PointOfInterest> pointOfInterests) {
        Name = name;
        Description = description;
        IsFavorite = false;
        Distance = 0d;
        Placemark = placemark;
        PointOfInterests = pointOfInterests;
    }

    public Trip(string name, string description, double distance, Microsoft.Maui.Devices.Sensors.Placemark placemark, List<PointOfInterest> pointOfInterests) {
        Name = name;
        Description = description;
        IsFavorite = false;
        Distance = distance;
        Placemark = placemark;
        PointOfInterests = pointOfInterests;
    }

    public Trip(string name, string description, double distance, Uri link, Microsoft.Maui.Devices.Sensors.Placemark placemark, List<PointOfInterest> pointOfInterests) {
        Name = name;
        Description = description;
        IsFavorite = false;
        Distance = distance;
        Link = link;
        Placemark = placemark;
        PointOfInterests = pointOfInterests;
    }

}
