using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExtinguisherManager : MonoBehaviour
{
    public List<GameObject> extinguishers = new List<GameObject>();
    private List<GameObject> m_ActiveExtinguishers = new List<GameObject>();

	void Awake()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.GetComponent<Interactable>() != null && child.gameObject.activeSelf)
            {
                GameObject e = Instantiate(child.gameObject, transform) as GameObject;
                extinguishers.Add(e);
                m_ActiveExtinguishers.Add(child.gameObject);
                e.SetActive(false);
            }
        }
    }

    void Update()
    {
        for(int i = 0; i < m_ActiveExtinguishers.Count; ++i)
        {
            if (m_ActiveExtinguishers[i] == null)
            {
                GameObject g = Instantiate(extinguishers[i], transform) as GameObject;
                g.SetActive(true);
                m_ActiveExtinguishers[i] = g;
            }
        }
    }

    //public void ResetFireExtinguisher()
    //{
    //    foreach(GameObject g in extinguishers)
    //    {
    //        g.GetComponent<PickUpObject>().PutBack();
    //    }
    //    GameObject[] t = GameObject.FindGameObjectsWithTag("PickUp");
    //    foreach(GameObject g in t)
    //    {
    //        Destroy(g);
    //    }

    //    SteamVR_RenderModel[] m = FindObjectsOfType<SteamVR_RenderModel>();
    //    foreach(SteamVR_RenderModel rm in m)
    //    {
    //        rm.enabled = true;
    //    }
    //}
}
