using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TimeController))]
[RequireComponent(typeof(FocusController))]
public class AppBootstrapper : MonoBehaviour
{
  public SolarSystemConfig config;

  public GameObject solarSystemRoot;
  public PlanetView[] planetViews;
  public PlanetSelectable[] planetSelectables;

  TimeModel timeModel;
  TimeController timeController;
  PlanetSystemController planetSystemController;
  ScaleController scaleController;
  FocusController focusController;

  void Start()
  {
    Debug.Log("[BOOT] Initializing application");

    if (config == null)
    {
      Debug.LogError("[BOOT] Missing SolarSystemConfig! Check inspector");
      return;
    }
    if (planetViews == null || planetViews.Length == 0)
    {
      Debug.LogError("[BOOT] Missing PlanetViews! Check inspector");
      return;
    }
    if (planetSelectables == null || planetSelectables.Length == 0)
    {
      Debug.LogError("[BOOT] Missing PlanetSelectables! Check inspector");
      return;
    }

    // Instantiating models and services
    timeModel = new TimeModel();
    var ephemeris = new PlanetEphemerisService();

    // Creating Planet System Controller
    planetSystemController = new PlanetSystemController(
      timeModel,
      ephemeris,
      planetViews,
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

    // Creating Focus Controller
    focusController = GetComponent<FocusController>();
  }
}