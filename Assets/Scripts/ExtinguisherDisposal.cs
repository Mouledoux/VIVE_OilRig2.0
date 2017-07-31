using UnityEngine;
using System.Collections;

public class ExtinguisherDisposal : MonoBehaviour
{
    void OnTriggerStay(Collider other)
    {
        Interactable i = other.GetComponent<Interactable>();
        if (i != null && i.transform.parent == null)
        {
            Destroy(i.gameObject);
        }
    }
}
