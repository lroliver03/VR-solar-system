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

  TimeModel timeModel;
  TimeController timeController;
  PlanetSystemController planetSystemController;
  ScaleController scaleController;
  FocusController focusController;

  private Vector3 initialSolarSystemRootPos;
  private Quaternion initialSolarSystemRootRot;

  void Start()
  {
    Debug.Log("[BOOT] Initializing application");

    if (config == null)
    {
      Debug.LogError("[BOOT/ERROR] Missing SolarSystemConfig! Check inspector");
      return;
    }
    if (planetViews == null || planetViews.Length == 0)
    {
      Debug.LogError("[BOOT/ERROR] Missing PlanetViews! Check inspector");
      return;
    }

    // Getting initial position and rotation
    initialSolarSystemRootPos = solarSystemRoot.transform.position;
    initialSolarSystemRootRot = solarSystemRoot.transform.rotation;

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

  public void ResetView()
  {
    solarSystemRoot.transform.SetPositionAndRotation(initialSolarSystemRootPos, initialSolarSystemRootRot);
    Debug.Log("[APP] Reset system position and rotation");
  }

  public void ResetScale()
  {
    scaleController.ResetScale();
  }

  public void ResetTime()
  {
    timeController.ResetTime();
  }
}