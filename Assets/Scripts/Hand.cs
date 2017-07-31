using UnityEngine;
using System.Collections;

public class Hand : MonoBehaviour
{
    public Interactable m_InHand;
    private GameObject m_ObjectInHand;

    public int PickUp(Interactable aObject)
    {
        if (m_InHand == null)
        {
            m_InHand = aObject;
            m_InHand.transform.parent = gameObject.transform;
            m_InHand.transform.localPosition = Vector3.zero;

            //m_ObjectInHand = Instantiate(m_InHand.m_ObjectInHand, transform) as GameObject;
            m_InHand.gameObject.SetActive(false);
        }
        return 0;
    }
    
    public int Drop()
    {
        if(m_ObjectInHand != null)
        {
            m_InHand.gameObject.SetActive(true);
            Destroy(m_ObjectInHand);
            m_InHand.transform.parent = null;
            m_InHand = null;
        }

        return 0;
    }
}