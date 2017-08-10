using UnityEngine;
using System.Collections;

// --------------------------------------------------
public class FireExtinguisher : MonoBehaviour
{
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

    public void StartSpray()
    {
        m_linearMapping.value =
            Mathf.Clamp(m_parentHand.controller.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger).magnitude, 0.01f, 0.99f);
        float[] gripStrength = { m_linearMapping.value };
        Packet data = new Packet(new int[0], new bool[0], gripStrength, new string[0]);

        Mediator.instance.NotifySubscribers("extinguish.start", data);
    }

    public void StopSpray()
    {
        Mediator.instance.NotifySubscribers("extinguish.stop", new Packet());
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