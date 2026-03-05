static public class PlanetInfo
{
  static public string GetPlanetDescription(string name)
  {
    return name switch
    {
      "Mercury" => "Diameter: 2440 km\nDistance to the Sun: 58 million km\nDay duration: 59 Earth days\nOrbit duration: 88 Earth days\nSurface temperature: 430 to -180 degrees C\nNo atmosphere",
      "Venus" => "Diameter: 12104 km\nDistance to the Sun: 108 million km\nDay duration: 243 Earth days\nOrbit duration: 225 Earth days\nSurface temperature: 460 degrees C\nAtmosphere: Mostly CO2",
      "Earth" => "Diameter: 12756 km\nDistance to the Sun: 150 million km\nDay duration: 24 hours\nOrbit duration: 365.25 days\nSurface temperature: 15 degrees C\nAtmosphere: Pretty pleasant",
      "Mars" => "Diameter: 6779 km\nDistance to the Sun: 228 million km\nDay duration: 24.6 hours\nOrbit duration: 687 Earth days\nSurface temperature: -63 degrees C\nAtmosphere: Mostly CO2",
      "Jupiter" => "Diameter: 139820 km\nDistance to the Sun: 778 million km\nDay duration: 10 hours\nOrbit duration: 12 Earth years\nTemperature: -170 degrees C\nGas giant!",
      "Saturn" => "Diameter: 116460 km\nDistance to the Sun: 1.4 billion km\nDay duration: 10.7 hours\nOrbit duration: 29.4 Earth years\nTemperature: 11700 degrees C\nAnother gas giant!",
      "Uranus" => "Diameter: 50724 km\nDistance to the Sun: 2.9 billion km\nDay duration: 17 hours\nOrbit duration: 84 Earth years\nSurface temperature: 4700 degrees C\nAtmosphere: Mostly H and He",
      "Neptune" => "Diameter: 49244 km\nDistance to the Sun: 4.5 billion km\nDay duration: 16 hours\nOrbit duration: 165 Earth years\nSurface temperature: -235 degrees C\nAtmosphere: Mostly H and He",
      _ => "Invalid planet!"
    };
  }
}