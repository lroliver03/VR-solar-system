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

    Debug.Log("[TIME] TimeController initialised");
  }

  public void SetPlaying(bool playing)
  {
    if (playing)
    {
      Debug.Log("[TIME] Resumed time");
      timeModel.Play();
    }
    else
    {
      Debug.Log("[TIME] Paused time");
      timeModel.Pause();
    }
  }

  public void ResetTime()
  {
    now = DateTime.Now;
    timeModel.SetTime(now);
    Debug.Log("[TIME] Reset time");
  }

  public void SetTimeScale(float scale)
  {
    timeModel.SetScale(scale);
    Debug.Log($"[TIME] Set time scale {scale}");
  }

  void Update()
  {
    if (!timeModel.IsPlaying)
      return;

    now = now.AddDays(Time.deltaTime * secondsPerDay * timeModel.TimeScale);
    timeModel.SetTime(now);
  }
}