using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlanetSelectable : XRBaseInteractable
{
  public PlanetHighlighter highlighter;

  void OnAwake()
  {
    highlighter.DisableHighlight();
  }

  public void Select()
  {
    highlighter.EnableHighlight();
  }

  public void Deselect()
  {
    highlighter.DisableHighlight();
  }
}