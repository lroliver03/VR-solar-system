using System;
using UnityEngine;

[RequireComponent(typeof(TimeController))]
public class AppBootstrapper : MonoBehaviour
{
  public SolarSystemConfig config;

  public GameObject solarSystemRoot;
  public PlanetView[] planets;

  TimeModel timeModel;
  TimeController timeController;
  PlanetSystemController planetSystemController;
  ScaleController scaleController;

  void Start()
  {
    Debug.Log("[BOOT] Initializing application");

    if (config == null)
    {
      Debug.LogError("[BOOT] Missing SolarSystemConfig! Check inspector");
      return;
    }
    if (planets == null || planets.Length == 0)
    {
      Debug.LogError("[BOOT] Missing Planets/PlanetViews! Check inspector");
      return;
    }

    // Instantiating models and services
    timeModel = new TimeModel();
    var ephemeris = new PlanetEphemerisService();

    // Creating Planet System Controller
    planetSystemController = new PlanetSystemController(
      timeModel,
      ephemeris,
      planets,
      config
    );
    
    // Getting and initializing Time Controller
    timeController = GetComponent<TimeController>();
    timeController.Init(timeModel);

    // Creating Scale Controller and initializing Scale Action Handler
    scaleController = new ScaleController(
      solarSystemRoot.transform,
      1f,
      config.minimumScale,
      config.maximumScale,
      config.scaleStep
    );
    
    solarSystemRoot.GetComponent<ScaleActionHandler>().Init(scaleController);
  }
}