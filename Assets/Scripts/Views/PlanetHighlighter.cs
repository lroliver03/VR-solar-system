using Unity.XR.CoreUtils;
using UnityEngine;

public class PlanetHighlighter : MonoBehaviour
{
  private Renderer outlineSphere;
  private Renderer highlightLine;

  void Start()
  {
    outlineSphere = gameObject.GetNamedChild("OutlineSphere").GetComponent<Renderer>();
    highlightLine = gameObject.GetNamedChild("HighlightLine").GetComponent<Renderer>();

    outlineSphere.enabled = false;
    highlightLine.enabled = false;
  }

  public void EnableHighlight()
  {
    outlineSphere.enabled = true;
    highlightLine.enabled = true;
  }

  public void DisableHighlight()
  {
    outlineSphere.enabled = false;
    highlightLine.enabled = false;
  }
}
