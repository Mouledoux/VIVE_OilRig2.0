using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider))]

public class OnTriggerEvent : MonoBehaviour
{
    public string m_Tag = "";
    public UnityEngine.Events.UnityEvent OnEnter;
    public UnityEngine.Events.UnityEvent OnExit;

    private GameObject collisionObject;

    void Start ()
    {
        GetComponent<Collider>().isTrigger = true;
	}



    private void OnTriggerEnter(Collider other)
    {
        if (m_Tag != "")
            if (!other.CompareTag(m_Tag))
                return;

        collisionObject = other.gameObject;
        OnEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_Tag != "")
            if (!other.CompareTag(m_Tag))
                return;

        collisionObject = other.gameObject;
        OnExit.Invoke();
    }

    public void SetParent(bool parent)
    {
        if (parent)
            collisionObject.transform.parent = transform;
        else
            collisionObject.transform.parent = null;
    }
}
