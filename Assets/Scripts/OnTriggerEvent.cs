using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider))]

public class OnTriggerEvent : MonoBehaviour
{
    public string m_Tag = "";
    public UnityEngine.Events.UnityEvent m_Event;
	void Start ()
    {
        GetComponent<Collider>().isTrigger = true;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (m_Tag != "")
            if (!other.CompareTag(m_Tag))
                return;

        m_Event.Invoke();
    }
}
