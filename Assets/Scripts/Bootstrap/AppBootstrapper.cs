using System;
using UnityEngine;

public class AppBootstrapper : MonoBehaviour
{
  public SolarSystemConfig config;

  public PlanetView[] planets;

  TimeModel timeModel;
  PlanetSystemController controller;

  void Start()
  {
    Debug.Log("[BOOT] Initializing application");

    timeModel = new TimeModel();

    var ephemeris = new PlanetEphemerisService();

    controller = new PlanetSystemController(
      timeModel,
      ephemeris,
      planets
    );

    timeModel.SetTime(DateTime.Now);
  }
}