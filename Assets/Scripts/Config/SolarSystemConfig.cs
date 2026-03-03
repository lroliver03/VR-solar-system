using UnityEngine;

[CreateAssetMenu(fileName = "SolarSystemConfig", menuName = "XR/Solar System Config")]
public class SolarSystemConfig : ScriptableObject
{
  [Header("Planets Configuration")]
  public float distanceScale = 0.000001f;
  public float planetSizeScale = 0.01f;

  [Header("Orbit Configuration")]
  public bool showOrbits = true;
}
