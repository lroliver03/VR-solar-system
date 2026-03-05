using System;
using UnityEngine;

public class PlanetSystemController
{
  readonly TimeModel timeModel;
  readonly IPlanetEphemerisService ephemeris;

  readonly PlanetView[] planets;
  readonly float distanceScale;

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

    Debug.Log("[PLANET] PlanetSystemController initialised");
  }

  // Updates all planets' positions.
  void UpdatePlanets(DateTime time)
  {
    // Debug.Log("[PLANET] Updating planets " + time);

    foreach (var planet in planets)
    {
      Vector3 pos = distanceScale * ephemeris.GetPlanetPosition(planet.planet, time);
      planet.SetPosition(pos);
    }
  }
}