using System;
using UnityEngine;

public class PlanetSystemController
{
  TimeModel timeModel;
  IPlanetEphemerisService ephemeris;

  PlanetView[] planets;
  float distanceScale;

  public PlanetSystemController(
    TimeModel timeModel,
    IPlanetEphemerisService ephemeris,
    PlanetView[] planets,
    SolarSystemConfig config)
  {
    this.timeModel = timeModel;
    this.ephemeris = ephemeris;
    this.planets = planets;

    foreach (var planet in this.planets)
    {
      planet.SetScale(config.planetSizeScale);
    }
    this.distanceScale = config.distanceScale;

    timeModel.OnTimeChanged += UpdatePlanets;
  }

  // Updates all planets' positions.
  void UpdatePlanets(DateTime time)
  {
    // Debug.Log("[TIME] Updating planets " + time);

    foreach (var planet in planets)
    {
      Vector3 pos = distanceScale * ephemeris.GetPlanetPosition(planet.planet, time);
      planet.SetPosition(pos);
    }
  }
}