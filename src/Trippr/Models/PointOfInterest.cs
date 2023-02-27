using Microsoft.Maui.Devices.Sensors;

namespace Trippr.Models;

public class PointOfInterest 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Distance { get; set; }
    public bool IsFavorite { get; set; }
    public Uri Link { get; set; }

    public Microsoft.Maui.Devices.Sensors.Location Location { get; set; }
    public Microsoft.Maui.Devices.Sensors.Placemark Placemark { get; set; }

    public PointOfInterest()
    { }

    public PointOfInterest(string name, string description, Microsoft.Maui.Devices.Sensors.Placemark placemark)
    {
        Name = name;
        Description = description;
        IsFavorite = false;
        Distance = 0d;
        Placemark = placemark;
    }

    public PointOfInterest(string name, string description, double distance, Microsoft.Maui.Devices.Sensors.Placemark placemark)
    {
        Name = name;
        Description = description;
        IsFavorite = false;
        Distance = distance;
        Placemark = placemark;
    }

    public PointOfInterest(string name, string description, double distance, Uri link, Microsoft.Maui.Devices.Sensors.Placemark placemark)
    {
        Name = name;
        Description = description;
        IsFavorite = false;
        Distance = distance;
        Link = link;
        Placemark = placemark;
    }

}
