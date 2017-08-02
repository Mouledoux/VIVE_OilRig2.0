using UnityEngine;
using System.Collections;

enum eExtinguisherType
{
    CO2,
    FOAM,
    WATER,
    POWDER,
}

public class ExtinguisherSpray : MonoBehaviour
{
    [SerializeField] eExtinguisherType m_type;

    private void OnParticleCollision(GameObject other)
    {
        string[] extinguisherData = { m_type.ToString() };
        Packet data = new Packet(new int[0], new bool[0], new float[0], extinguisherData);

        Mediator.instance.NotifySubscribers(other.GetInstanceID().ToString(), data);

        print(other.GetInstanceID());
    }
}