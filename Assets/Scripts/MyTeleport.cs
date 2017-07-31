using UnityEngine;
using System.Collections;

public class MyTeleport : MonoBehaviour
{
    public bool teleportEnabled = true;
    [SerializeField] GameObject viveRig;
    [SerializeField] GameObject viveEye;
    [SerializeField] GameObject teleportReticle;

    SteamVR_TrackedController leftControl;
    SteamVR_TrackedController rightControl;

    [SerializeField] GameObject rightPointer;
    [SerializeField] GameObject leftPointer;

    Vector3 rayCastPos = new Vector3(0,0,0);

    bool castingTeleport = false;

    // Debug var ///////////////
    bool onTarget = false;
    ////////////////////////////

	// Use this for initialization
	void Start ()
    {
        leftControl  = GameObject.FindObjectOfType<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
        rightControl = GameObject.FindObjectOfType<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
    }

	void FixedUpdate()
    {
        if (!teleportEnabled)
            return;

        if(leftControl.padPressed && !castingTeleport)
        {
            StartCoroutine(Teleport(leftControl));
        }

        if (rightControl.padPressed && !castingTeleport)
        {
            StartCoroutine(Teleport(rightControl));
        }
    }

    IEnumerator Teleport(SteamVR_TrackedController teleportingHand)
    {
        LineRenderer line = teleportingHand.gameObject.GetComponent<LineRenderer>();
        line.enabled = false;

        castingTeleport = true;

        Vector3 targetPos = Vector3.zero;
        bool havePos = false;

        GameObject hand;
        if (teleportingHand == rightControl)
        {
            hand = rightPointer;
        }
        else
        {
            hand = leftPointer;
        }

        while (teleportingHand.padPressed)
        {
            RaycastHit hit;
            //Ray ray = new Ray(teleportingHand.gameObject.transform.position, teleportingHand.gameObject.transform.forward, 100);

            if (Physics.Raycast(hand.gameObject.transform.position, hand.transform.forward, out hit, 5))
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject.CompareTag("TeleportTarget"))
                    {
                        targetPos = hit.point;
                        line.SetPosition(0, hand.gameObject.transform.position);
                        line.SetPosition(1, hit.point);
                        line.enabled = true;
                        Debug.DrawLine(teleportingHand.gameObject.transform.position, hit.point, Color.red);
                        havePos = true;
                    }
                    else
                    {
                        havePos = false;
                        line.enabled = havePos;
                    }
                }

                onTarget = havePos;
            }

            else
            {
                havePos = false;
            }

            
            teleportReticle.gameObject.SetActive(havePos);
            teleportReticle.transform.position = targetPos;
            
            yield return null;
        }

        Vector3 adjust = new Vector3(viveEye.transform.localPosition.x, 0, viveEye.transform.localPosition.z);
        if (havePos)
            viveRig.transform.position = targetPos - adjust;

        teleportReticle.SetActive(false);
        castingTeleport = false;
        line.enabled = false;
    }
}
