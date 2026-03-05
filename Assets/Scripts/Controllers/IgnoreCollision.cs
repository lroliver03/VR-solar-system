using UnityEngine;

[RequireComponent(typeof(Collider))]
public class IgnoreCollision : MonoBehaviour
{
  public Transform ignoredObject;

  void Start()
  {
    if (!ignoredObject.TryGetComponent<Collider>(out var ignoredObjectCollider))
    {
      Debug.LogError("[IgnoreCollision] This IgnoreCollision component is invalid! Delete it or change the ignored object to something with a collider!");
      return;
    }

    Physics.IgnoreCollision(ignoredObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
  }
}
