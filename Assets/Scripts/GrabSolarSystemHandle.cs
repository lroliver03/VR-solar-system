using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class GrabObjectHandle : XRGrabInteractable
{
  private Vector3 localPositionOffset;
  private Quaternion localRotationOffset;
  // private Vector3 localScaleOffset; // NOT IMPLEMENTED

  public Transform targetObject;

  protected override void Awake()
  {
    base.Awake();
    movementType = MovementType.Instantaneous;
  }

  protected override void OnSelectEntered(SelectEnterEventArgs args)
  {
    base.OnSelectEntered(args);

    if (targetObject == null)
      return;

    IXRSelectInteractor interactor = args.interactorObject;
    Transform interactorTransform = interactor.GetAttachTransform(this); //

    localPositionOffset = interactorTransform.InverseTransformPoint(targetObject.position);
    localRotationOffset = Quaternion.Inverse(interactorTransform.rotation) * targetObject.rotation;
  }

  public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
  {
    base.ProcessInteractable(updatePhase);

    if (!isSelected || targetObject == null)
      return;

    if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
    {
      IXRSelectInteractor interactor = firstInteractorSelecting;
      Transform interactorTransform = interactor.GetAttachTransform(this);

      targetObject.position = interactorTransform.TransformPoint(localPositionOffset);
      targetObject.transform.rotation = interactorTransform.rotation * localRotationOffset;
    }
  }
}
