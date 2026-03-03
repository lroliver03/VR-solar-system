using UnityEngine;

public class PlanetView : MonoBehaviour
{
  public PlanetData.Planet planet;

  public void SetPosition(Vector3 pos)
  {
    transform.localPosition = pos;
  }

  public void SetScale(float scale)
  {
    transform.localScale = new(scale, scale, scale);
  }
}