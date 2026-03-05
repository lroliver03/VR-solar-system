using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class FocusController : MonoBehaviour
{
  static private PlanetSelectable selectedPlanet;
  // Name, diameter, distance to sun, day duration, temperature and atmosphere
  public TextMeshProUGUI nameField;
  public TextMeshProUGUI descriptionField;

  public void SelectPlanet(PlanetSelectable planet)
  {
    if (selectedPlanet != null)
      selectedPlanet.Deselect();
    selectedPlanet = planet;
    planet.Select();
    Debug.Log($"[FOCUS] Selected planet {planet.name}");
    GetInfo(planet.name);
  }

  // Figure out some way to show the info in a separate method.
  void GetInfo(string planetName)
  {
    nameField.text = planetName;
    descriptionField.text = PlanetInfo.GetPlanetDescription(planetName);
  }
}