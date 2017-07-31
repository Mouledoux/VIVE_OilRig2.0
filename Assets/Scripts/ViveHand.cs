using UnityEngine;
using System.Collections;

public class ViveHand : MonoBehaviour
{
    //[SerializeField] bool hapticOnStay;

    //[SerializeField]
    //GameObject selectedObject = null;
    //GameObject viveControllerModel;
    //SteamVR_Controller.Device device;
    //SteamVR_TrackedObject trackedObject;
    //SteamVR_TrackedController trackedHand;
    //bool trigger = false;

    //void Start ()
    //{
    //    trackedObject = GetComponent<SteamVR_TrackedObject>();
    //    trackedHand = GetComponent<SteamVR_TrackedController>();
    //    device = SteamVR_Controller.Input((int)trackedObject.index);
    //}

    //void Update()
    //{
    //    if (selectedObject == null)
    //        return;

    //    if (trackedHand.triggerPressed && !trigger && selectedObject != null)
    //    {
    //        trigger = true;
    //        selectedObject.GetComponent<IInteractableObject>().Interact();
    //    }
    //    else if (!trackedHand.triggerPressed && trigger)
    //    {
    //        trigger = false;
    //    }
    //}

    //void OnTriggerEnter(Collider c)
    //{
    //    if (c.gameObject.GetComponent<IInteractableObject>() != null)
    //    {
    //        if (selectedObject != null)
    //        {
    //            selectedObject.GetComponent<IInteractableObject>().Touch(false);
    //        }

    //        selectedObject = c.gameObject;
    //        selectedObject.GetComponent<IInteractableObject>().Touch(true);
    //        HapticPulse();
    //    }
    //}

    //void OnTriggerExit(Collider c)
    //{
    //    if (c.gameObject.GetComponent<IInteractableObject>() != null)
    //    {
    //        selectedObject.GetComponent<IInteractableObject>().Touch(false);
    //        selectedObject = null;
    //        HapticPulse();
    //    }
    //}

    //void HapticPulse()
    //{
    //    //device = SteamVR_Controller.Input((int)trackedObject.index);
    //    device.TriggerHapticPulse(500); // 
    //}

    /// /////////////////////////////////////////////////////////////////////////
    //[SerializeField] bool hapticOnStay;
    [SerializeField] GameObject m_SelectedObject = null;

    SteamVR_Controller.Device device;
    SteamVR_TrackedObject trackedObject;
    SteamVR_TrackedController trackedHand;

    [SerializeField] GameObject viveControllerModel;
    bool trigger = false;
    
    private GameObject m_ObjectInHand;
    private Interactable m_SelectedInteractable
    {
        get
        {
            Interactable i = m_SelectedObject.GetComponent<Interactable>();
            return i != null ? i : null;
        }
    }

    void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
        trackedHand = GetComponent<SteamVR_TrackedController>();
        device = SteamVR_Controller.Input((int)trackedObject.index);
    }

    void Update()
    {
        if (m_SelectedObject == null && m_ObjectInHand == null)
        {
            return;
        }

        else if (m_ObjectInHand != null)
        {
            if (m_ObjectInHand.transform.parent != gameObject.transform)
            {
                m_ObjectInHand = null;
                return;
            }

            if (trackedHand.triggerPressed && !trigger)
            {
                trigger = true;
                m_SelectedInteractable.m_OnInteraction.Invoke();
            }

            else if (!trackedHand.triggerPressed && trigger)
            {
                trigger = false;
                m_SelectedInteractable.m_OffInteraction.Invoke();
            }
        }

        else if (trackedHand.triggerPressed && !trigger)
        {
            trigger = true;

            if (m_SelectedInteractable.m_IsPickUp)
            {
                PickUp();
            }
            else
            {
                m_SelectedInteractable.m_OnInteraction.Invoke();
            }

        }
        else if (!trackedHand.triggerPressed && trigger)
        {
            trigger = false;
        }

        if(trackedHand.gripped && m_ObjectInHand != null)
        {
            Drop();
        }

        viveControllerModel.SetActive(m_ObjectInHand == null);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == m_ObjectInHand)
            return;

        else if (other.gameObject.GetComponent<Interactable>() != null && m_SelectedObject == null)
        {
            m_SelectedObject = other.gameObject;
            m_SelectedInteractable.m_OnTouch.Invoke();
            HapticPulse();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == m_SelectedObject)
        {
            m_SelectedInteractable.m_OffTouch.Invoke();
            m_SelectedObject = null;
            HapticPulse();
        }
        if (other.GetComponent<Interactable>() != null)
        {
            other.GetComponent<Interactable>().m_OffTouch.Invoke();
        }
    }

    void HapticPulse()
    {
        device.TriggerHapticPulse(500);
    }

    [ContextMenu("PICK UP")]
    public int PickUp()
    {
        if (m_SelectedInteractable != null)
        {
            m_SelectedInteractable.m_OffTouch.Invoke();
            m_SelectedInteractable.m_OnPickUp.Invoke();

            m_ObjectInHand = m_SelectedInteractable.gameObject;
            m_SelectedInteractable.transform.parent = gameObject.transform;

            //m_SelectedInteractable.transform.localPosition = Vector3.zero +
            //    m_SelectedInteractable.m_PickUpOffsetPOS;
            
            //m_SelectedInteractable.transform.localEulerAngles = Vector3.zero +
            //    m_SelectedInteractable.m_PickUpOffsetROT;

        }
        return 0;
    }

    [ContextMenu("DROP")]
    public int Drop()
    {
        if (m_ObjectInHand != null)
        {
            m_SelectedInteractable.m_OnDrop.Invoke();

            m_SelectedObject.transform.parent = null;
            m_SelectedObject = null;
            m_ObjectInHand = null;
        }

        return 0;
    }

}
