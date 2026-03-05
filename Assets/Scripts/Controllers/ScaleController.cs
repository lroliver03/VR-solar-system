using System;
using UnityEngine;

public class ScaleController
{
  readonly Transform target;
  readonly float maxScale;
  readonly float minScale;
  readonly float scaleStep;

  float currentScale;

  public ScaleController(
    Transform target,
    float initialScale,
    float minScale,
    float maxScale,
    float scaleStep)
  {
    this.target = target;
    this.minScale = minScale;
    this.maxScale = maxScale;
    this.scaleStep = scaleStep;

    SetScale(initialScale);
    Debug.Log("[INFO] ScaleController initialised");
  }

  public void SetScale(float newScale)
  {
    currentScale = Math.Clamp(newScale, minScale, maxScale);
    target.localScale = Vector3.one * currentScale;

    if (newScale != currentScale)
      Debug.Log($"[WARN] New scale clamped");
    Debug.Log($"[INFO] New scale applied, scale={currentScale:F2}");
  }

  public void IncrementScale()
  {
    Debug.Log("[INFO] Scaling system up");
    SetScale(currentScale + (scaleStep * Time.deltaTime));
  }

  public void DecrementScale()
  {
    Debug.Log("[INFO] Scaling system down");
    SetScale(currentScale - (scaleStep * Time.deltaTime));
  }
}