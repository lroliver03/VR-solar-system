using System;
using UnityEngine;

public class TimeController : MonoBehaviour
{
  public float secondsPerDay = 1f;
  
  TimeModel timeModel;
  DateTime now;

  public void Init(TimeModel model)
  {
    timeModel = model;
    now = DateTime.Now;
    timeModel.SetTime(now);

    Debug.Log("[INFO] TimeController initialised");
  }

  void Update()
  {
    if (!timeModel.IsPlaying)
      return;

    now = now.AddDays(Time.deltaTime * secondsPerDay);
    timeModel.SetTime(now);
  }
}