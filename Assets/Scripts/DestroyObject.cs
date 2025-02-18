﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    public void RemoveComponent(Component com)
    {
        Destroy(com);
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

    public void InvertEnabled()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void DetechFromViveHand()
    {
        GetParentHand(gameObject).DetachObject(gameObject);
        Valve.VR.InteractionSystem.ControllerButtonHints.HideAllTextHints(GetParentHand(gameObject));
    }

     private Valve.VR.InteractionSystem.Hand GetParentHand(GameObject child)
    {
        Valve.VR.InteractionSystem.Hand hand = child.GetComponent<Valve.VR.InteractionSystem.Hand>();

        if (hand == null)
            return GetParentHand(child.transform.parent.gameObject);
        else
            return hand;
    }
}
