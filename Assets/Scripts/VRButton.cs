using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Valve.VR.InteractionSystem.Interactable))]
public class VRButton : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnClick;
    public string actionName;

    private void HandHoverUpdate(Valve.VR.InteractionSystem.Hand hand)
    {
        //Valve.VR.InteractionSystem.ControllerButtonHints.ShowTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger, actionName);
        if (hand.GetStandardInteractionButton())
        {
            OnClick.Invoke();
        }
    }

    private void OnHandHoverEnd(Valve.VR.InteractionSystem.Hand hand)
    {
        //Valve.VR.InteractionSystem.ControllerButtonHints.HideTextHint(hand, Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger);
    }

}
