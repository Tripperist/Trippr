namespace Trippr.Services;

using Microsoft.Maui.Devices.Sensors;
using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;

public class TripDataService {
    public async Task<IEnumerable<Trip>> GetItems(double distanceFilter) {
        Microsoft.Maui.Devices.Sensors.Location location = await Geolocation.GetLocationAsync();

        IEnumerable<string> tripsList = new List<string> { "2022.10.Roadtrip", "2023.03.MSYAUS" };
        IEnumerable<Trip> trips = await GetKmlTrips(tripsList, location, distanceFilter);
        return trips;
    }

    public static async Task<IEnumerable<Trip>> GetKmlTrips(IEnumerable<string> tripsList, Microsoft.Maui.Devices.Sensors.Location location, double distanceFilter) 
    {
        List<Trip> trips = new();
        // Opportunity to filter out further away trips.

        foreach (var trip in tripsList) 
        {
            using Stream stream = await FileSystem.OpenAppPackageFileAsync($"Kml/{trip}.kml");
            KmlFile kmlFile = KmlFile.Load(stream);
            Kml kml = (Kml)kmlFile.Root;

            var feature = kml.Feature;
            var description = feature.Description;
            
            List<PointOfInterest> pointsOfInterest = new List<PointOfInterest>();
            foreach (var kmlPlacemark in kml.Flatten().OfType<SharpKml.Dom.Placemark>()) {
                var descriptionText = (kmlPlacemark.Description == null) ? kmlPlacemark.Name : kmlPlacemark.Description.Text;
                Console.WriteLine($"{descriptionText}");
                var placemark = new Microsoft.Maui.Devices.Sensors.Placemark();
                SharpKml.Dom.Point point = kmlPlacemark.Geometry as SharpKml.Dom.Point;
                placemark.Location = new Microsoft.Maui.Devices.Sensors.Location((double)point.Coordinate.Latitude, (double)point.Coordinate.Longitude, (double)point.Coordinate.Altitude);

                // Opportunity to filter out longer distance points.
                var distance = Math.Round(location.CalculateDistance(placemark.Location, DistanceUnits.Miles), 2);
                Console.WriteLine($"{kmlPlacemark.Name}");
                pointsOfInterest.Add(new PointOfInterest(kmlPlacemark.Name, descriptionText, distance, placemark));
            }
            pointsOfInterest = pointsOfInterest.OrderBy(p => p.Distance).ToList<PointOfInterest>();

            trips.Add(new Trip(trip, trip, 1.0d, new Microsoft.Maui.Devices.Sensors.Placemark(), pointsOfInterest));
        }

        return trips.OrderBy(p => p.Distance);
    }
}
