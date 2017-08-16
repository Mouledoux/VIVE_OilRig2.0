using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
public class VRButton : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnClick;

    private void HandHoverUpdate(Valve.VR.InteractionSystem.Hand hand)
    {
        if (hand.GetStandardInteractionButton())
        {
            OnClick.Invoke();
        }
    }
}
