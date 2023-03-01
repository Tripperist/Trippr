namespace Trippr.Services;

using SharpKml.Dom;
 using SharpKml.Engine;

public class PointOfInterestService
{
    public async Task<IEnumerable<PointOfInterest>> GetItems(double distanceFilter)
    {
        Microsoft.Maui.Devices.Sensors.Location location = await Geolocation.GetLocationAsync();
        //List<PointOfInterest> pointsOfInterest = await GetKmlPointsOfInterest(location, distanceFilter);
        //return await PointOfInterestService.GetHmdbPointsOfInterest(location, distanceFilter);

        var pointsOfInterest = await PointOfInterestService.GetHmdbPointsOfInterest(location, distanceFilter);
        return pointsOfInterest.Concat(await GetKmlPointsOfInterest(location, distanceFilter)).ToList();
    }

    public static async Task<IEnumerable<PointOfInterest>> GetKmlPointsOfInterest(Microsoft.Maui.Devices.Sensors.Location location, double distanceFilter)
    {
        IList<string> tripsList = new List<string> { "2022.10.Roadtrip", "2023.03.Roadtrip" };
        List<PointOfInterest> pointsOfInterest = new();

        foreach (var trip in tripsList)
        {
            using Stream stream = await FileSystem.OpenAppPackageFileAsync($"Kml/{trip}.kml");
            KmlFile kmlFile = KmlFile.Load(stream);
            Kml kml = (Kml)kmlFile.Root;

            foreach (var kmlPlacemark in kml.Flatten().OfType<SharpKml.Dom.Placemark>())
            {
                var placemark = new Microsoft.Maui.Devices.Sensors.Placemark();
                SharpKml.Dom.Point point = kmlPlacemark.Geometry as SharpKml.Dom.Point;
                placemark.Location = new Microsoft.Maui.Devices.Sensors.Location((double)point.Coordinate.Latitude, (double)point.Coordinate.Longitude, (double)point.Coordinate.Altitude);

                // Opportunity to filter out longer distance points.
                var distance = Math.Round(location.CalculateDistance(placemark.Location, DistanceUnits.Miles), 2);

                if(distance <= distanceFilter )
                    pointsOfInterest.Add(new PointOfInterest(kmlPlacemark.Name, kmlPlacemark.Description.Text, distance, placemark));
            }
        }

        return new List<PointOfInterest>(pointsOfInterest.OrderBy(p => p.Distance));
    }

    public  static async Task<IEnumerable<PointOfInterest>> GetHmdbPointsOfInterest(Microsoft.Maui.Devices.Sensors.Location location, double distanceFilter)
    {
        IList<string> statesList = new List<string> {"AZ", "CA", "OR", "NV", "OK", "TX", "WA" };
        List<PointOfInterest> pointsOfInterest = new();

        foreach (var state in statesList)
        {

            using Stream stream = await FileSystem.OpenAppPackageFileAsync($"Hmdb/HMdb-{state}.csv");

            using var reader = new StreamReader(stream);
            var csvOpts = new CsvDataReaderOptions { DateTimeFormat = "yyyy-MM-ddTHH:mm:ss" };
            var csvReader = CsvDataReader.Create(reader, csvOpts);

            while (await csvReader.ReadAsync())
            {
                var placemark = new Microsoft.Maui.Devices.Sensors.Placemark
                {
                    Location = new Microsoft.Maui.Devices.Sensors.Location(csvReader.GetDouble(7), csvReader.GetDouble(8))
                };

                var distance = Math.Round(location.CalculateDistance(placemark.Location, DistanceUnits.Miles), 2);

                if (distance <= distanceFilter)
                {
                    var description = String.Format($"{csvReader.GetString(3)}");
                    var erectedBy = csvReader.GetString(6);
                    if (description.Length > 0)
                        description = String.Format($"{description}\n");

                    if (erectedBy.Length > 0)
                        erectedBy = String.Format($"Erected by {csvReader.GetString(6)}");
                    else
                        erectedBy = "";

                    description = String.Format($"{description}{erectedBy}");
                    pointsOfInterest.Add(new PointOfInterest(csvReader.GetString(2), description, distance, new Uri(csvReader.GetString(15)), placemark));
                }

            }
        }
        return new List<PointOfInterest>(pointsOfInterest.OrderBy(p => p.Distance));
    }
}
