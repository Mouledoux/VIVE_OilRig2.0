using UnityEngine;
using System.Collections;

public class ViveFly : MonoBehaviour
{
    [SerializeField] GameObject CamRig;
    [SerializeField] float speed = 5;
    [SerializeField] SteamVR_TrackedController left;
    [SerializeField] SteamVR_TrackedController right;

    Vector3 takeOffPoint;
    bool inFlight = false;
	
	// Update is called once per frame
	void Update ()
    {
        if (inFlight)
            return;

        if (left.gripped && !inFlight)
            StartCoroutine(Fly());
	}

    IEnumerator Fly()
    {
        takeOffPoint = transform.position;
        inFlight = true;

        while (!left.menuPressed)
        {
            if (right.menuPressed)
                CamRig.transform.position += right.gameObject.transform.forward * speed * Time.deltaTime;
            yield return null;
        }

        CamRig.transform.position = takeOffPoint;
        inFlight = false;
    }
}
