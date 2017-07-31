using UnityEngine;
using System.Collections;

public class Interactable : MonoBehaviour
{
    public bool m_IsPickUp;
    public Vector3 m_PickUpOffsetPOS;
    public Vector3 m_PickUpOffsetROT;

    public UnityEngine.Events.UnityEvent m_OnTouch;
    public UnityEngine.Events.UnityEvent m_OffTouch;

    public UnityEngine.Events.UnityEvent m_OnPickUp;
    public UnityEngine.Events.UnityEvent m_OnDrop;

    public UnityEngine.Events.UnityEvent m_OnInteraction;
    public UnityEngine.Events.UnityEvent m_OffInteraction;

    [SerializeField]
    private float m_ResetDelay;
    private float m_ResetTimer = 0;
    private void FixedUpdate()
    {
        if (m_IsPickUp)
        {
            if (transform.parent == null)
            {
                m_ResetTimer += Time.deltaTime;
                if (m_ResetTimer >= m_ResetDelay)
                {
                    Destroy(gameObject);
                }
            }
        }
    } 
}