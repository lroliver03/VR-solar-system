using System;
using UnityEngine;

public class ScaleController
{
  readonly Transform target;
  readonly float maxScale;
  readonly float minScale;
  readonly float scaleStep;
  readonly float initialScale;


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
    this.initialScale = initialScale;

    SetScale(initialScale);
    Debug.Log("[SCALE] ScaleController initialised");
  }

  public void SetScale(float newScale)
  {
    currentScale = Math.Clamp(newScale, minScale, maxScale);
    target.localScale = Vector3.one * currentScale;

    if (newScale != currentScale)
      Debug.LogWarning($"[SCALE/WARN] New scale clamped");
    Debug.Log($"[SCALE] New scale applied, scale={currentScale:F2}");
  }

  public void IncrementScale()
  {
    Debug.Log("[SCALE] Scaling system up");
    SetScale(currentScale + (scaleStep * Time.deltaTime));
  }

  public void DecrementScale()
  {
    Debug.Log("[SCALE] Scaling system down");
    SetScale(currentScale - (scaleStep * Time.deltaTime));
  }

  public void ResetScale()
  {
    Debug.Log("[SCALE] Reset scale");
    SetScale(initialScale);
  }
}