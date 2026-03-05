using UnityEngine;
using UnityEngine.InputSystem;

public class ScaleActionHandler : MonoBehaviour
{
  ScaleController scaleController;

  public InputAction incrementScale;
  public InputAction decrementScale;

  public void Init(ScaleController controller)
  {
    scaleController = controller;

    incrementScale.Enable();
    decrementScale.Enable();
  }

  void OnDestroy()
  {
    incrementScale.Disable();
    decrementScale.Disable();
  }

  void Update()
  {
    if (incrementScale.IsPressed())
      scaleController.IncrementScale();

    if (decrementScale.IsPressed()) 
      scaleController.DecrementScale();
  }
}