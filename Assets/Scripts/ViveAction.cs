using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public class ViveAction : MonoBehaviour, IInteractableObject
{
    [SerializeField] UnityEvent pressAction;

    [SerializeField] GameObject Highlight;

    bool m_inUse = false;
    bool m_isUsable = true;
    bool m_highlightOnTouch = true;

    public bool highlightOnTouch
    {
        get
        {
            return m_highlightOnTouch;
        }

        set
        {
            m_highlightOnTouch = value;
        }
    }

    public bool inUse
    {
        get
        {
            return m_inUse;
        }

        set
        {
            m_inUse = value;
        }
    }

    public bool isUsable
    {
        get
        {
            return m_isUsable;
        }

        set
        {
            m_isUsable = value;
        }
    }

    public void Interact()
    {
        if(pressAction != null)
            pressAction.Invoke();
    }

    public void Touch(bool beingTouched)
    {
        if (highlightOnTouch && !inUse)
        {
            Highlight.SetActive(beingTouched);
        }
    }
}
