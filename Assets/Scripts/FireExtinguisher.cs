using UnityEngine;
using System.Collections;

public class FireExtinguisher : MonoBehaviour
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
