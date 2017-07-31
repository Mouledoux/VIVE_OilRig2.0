using UnityEngine;
using System.Collections;

public class ToTheFire : MonoBehaviour
{
    [SerializeField] GameObject viveRig;
    [SerializeField] GameObject viveBox;
    [SerializeField] Vector3 firePos;
    public GameObject m_headlight;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == viveBox)
        {
            viveRig.transform.position = firePos;
            m_headlight.SetActive(true);
            GameObject.Destroy(gameObject);
        }
    }

}
