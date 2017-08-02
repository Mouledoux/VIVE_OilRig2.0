using UnityEngine;
using System.Collections;

public class FireExtinguisher000 : MonoBehaviour
{
    AudioSource audio;
    bool playAudio = false;
    Collider SprayNozzle;
    ParticleSystem SprayFoam;
    EllipsoidParticleEmitter steamParticleSystem;

    SteamVR_TrackedController m_OnHand;
    SteamVR_TrackedController m_OffHand;

    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device device;

    public float m_MaxSpray = 0;
    public float m_CurrentSpray = 0;

    bool m_spraying = false;

    void Start()
    {
        m_CurrentSpray = m_MaxSpray;
        //m_MaxSpray = m_CurrentSpray;

        audio = gameObject.GetComponent<AudioSource>();
        //leftController  = FindObjectOfType<SteamVR_ControllerManager>().left.GetComponent<SteamVR_TrackedController>();
        //rightController = FindObjectOfType<SteamVR_ControllerManager>().right.GetComponent<SteamVR_TrackedController>();
        //trackedObject = rightController.gameObject.GetComponent<SteamVR_TrackedObject>();
        //SprayNozzle = transform.GetComponentInChildren<Collider>();
        
        SprayFoam = transform.GetComponentInChildren<ParticleSystem>();
        SprayNozzle = SprayFoam.gameObject.GetComponent<Collider>();

        SetSpray(false);
    }

    void Update()
    {
        //device = SteamVR_Controller.Input((int)trackedObject.index);

        if (m_CurrentSpray <= 0)
        {
            SprayNozzle.enabled = false;
            SetSpray(false);
        }

        if (m_spraying)
            Spray();

        if (playAudio && !audio.isPlaying)
            audio.Play();
        
        if (playAudio == false)
            audio.Stop();
    }

   

    public void SetSpray(bool active)
    {
        m_spraying = active;
        SprayNozzle.enabled = active;

        if(!active)
        {
            SprayFoam.Stop();
            playAudio = false;
        }

        if (m_CurrentSpray <= 0)
            return;

        
    }

    void Spray()
    {
        if (m_CurrentSpray < 0)
        { 
            m_spraying = false;
            return;
        }
        m_CurrentSpray -= Time.deltaTime;
        SprayFoam.Play();
        playAudio = true;
    }


}

// --------------------------------------------------
public class FireExtinguisher : MonoBehaviour
{
    public enum e_ExtinguisherType
    {
        CO2,
        FOAM,
        WATER,
        POWDER,
        DEFAULT,
    }

    public e_ExtinguisherType m_type;
    private float m_MaxSpray = 0;
    private float m_CurrentSpray = 0;

    [SerializeField]
    private Valve.VR.InteractionSystem.LinearMapping m_linearMapping;

    private Valve.VR.InteractionSystem.Hand m_parentHand;

    private void Start()
    {
        m_parentHand = GetParentHand(gameObject);
    }

    private void Update()
    {
        RechargeSpray(1);
    }

    private void RechargeSpray(float increaseMod)
    {
        if (m_CurrentSpray < m_MaxSpray)
            m_CurrentSpray += (Time.deltaTime * increaseMod);
    }

    public void Spray()
    {
        m_linearMapping.value = Mathf.Clamp(m_parentHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude, 0.01f, 0.99f);
        float[] gripStrength = { m_linearMapping.value };
        Packet data = new Packet(new int[0], new bool[0], gripStrength, new string[0]);

        Mediator.instance.NotifySubscribers("extinguish", data);
    }

    private Valve.VR.InteractionSystem.Hand GetParentHand(GameObject child)
    {
        Valve.VR.InteractionSystem.Hand hand = child.GetComponent<Valve.VR.InteractionSystem.Hand>();

        if (hand == null)
            return GetParentHand(child.transform.parent.gameObject);
        else
            return hand;
    }
}