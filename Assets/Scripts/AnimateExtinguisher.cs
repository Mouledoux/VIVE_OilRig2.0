using UnityEngine;
using System.Collections;

public class AnimateExtinguisher : MonoBehaviour
{
    [SerializeField]Animator anim;
    public SteamVR_TrackedController controller
    {
        get
        {
            if(transform.parent !=null)
                return transform.parent.GetComponent<SteamVR_TrackedController>() != null ? transform.parent.GetComponent<SteamVR_TrackedController>() : null;

            return null;
        }
    }

    void Update ()
    {
        if(controller != null)
            anim.SetBool("TriggerPressed", controller.triggerPressed);
	}
}
