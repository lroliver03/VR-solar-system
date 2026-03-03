using System;
using UnityEngine;

public class AppBootstrapper : MonoBehaviour
{
  public SolarSystemConfig config;

  public PlanetView[] planets;

  TimeModel timeModel;
  TimeController timeController;
  PlanetSystemController controller;

  void Start()
  {
    Debug.Log("[BOOT] Initializing application");

    timeModel = new TimeModel();
    timeController = new TimeController();

    var ephemeris = new PlanetEphemerisService();

    controller = new PlanetSystemController(
      timeModel,
      ephemeris,
      planets
    );

    // timeModel.SetTime(DateTime.Now); // Executed by timeController.
    timeController.Init(timeModel);
  }
}