using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    public string Type;

    /// <summary>
    /// The original size of the fire
    /// </summary>
    private float m_OriginalScale;

    /// <summary>
    /// The current size of the fire
    /// </summary>
    private float m_CurrentScale
    { get { return transform.localScale.magnitude; } }

    /// <summary>
    /// The smallest a fire can be without going out
    /// </summary>
    public float m_MinSize = 0f;

    /// <summary>
    /// The largest a fire can be without loosing
    /// </summary>
    public float m_MaxSize = 0f;

    /// <summary>
    /// Is the fire currently lit
    /// </summary>
    public bool m_IsLit = true;

    public bool CO2, FOAM, WATER, POWDER;


    private Mediator.Subscriptions subscriptions = new Mediator.Subscriptions();
    private Mediator.Callback onExtinguish;

    /// <summary>
    /// Called when the application is started up
    /// </summary>
    void Awake()
    {
        m_OriginalScale = m_CurrentScale;   // Sets the original size, to the current size
    }

    private void Start()
    {
        onExtinguish += DouseFire;
        
        subscriptions.Subscribe(gameObject.GetInstanceID().ToString(), onExtinguish);
    }

    /// <summary>
    /// Increases the size of the fire
    /// </summary>
    /// <param name="aRate">The rate at which the fire would grow per-second</param>
    public void GrowBy(float aRate)
    {
        transform.localScale += Vector3.one * (Time.deltaTime * aRate); // Increase the size over time

        m_IsLit = m_CurrentScale > m_MinSize; // If the fire is smaller than the alloted size, then it is no longer lit

        if (!m_IsLit)
        {
            string[] s = { Type };
            Mediator.instance.NotifySubscribers("Fire", new Packet(new int[0], new bool[0], new float[0], s));

            transform.localScale = Vector3.zero;
            //gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Resets the fire to its original state
    /// </summary>
    public void ResetFire()
    {
        m_IsLit = true;                                                 // Relight the fire
        gameObject.SetActive(true);                                     // Turn its object back on
        gameObject.transform.localScale = Vector3.one * m_OriginalScale;// Set it back to its original size
    }

    private void DouseFire(Packet data)
    { 
        string extType = data.strings[0];

        if( eExtinguisherType.CO2.ToString() == extType && CO2)
        {
            GrowBy(-1);
        }
        else if (eExtinguisherType.FOAM.ToString() == extType && FOAM)
        {
            GrowBy(-1);
        }
        else if (eExtinguisherType.WATER.ToString() == extType && WATER)
        {
            GrowBy(-1);
        }
        else if (eExtinguisherType.POWDER.ToString() == extType && POWDER)
        {
            GrowBy(-1);
        }
        else
        {
            GrowBy(1);
        }
    }

    private void OnDestroy()
    {
        subscriptions.UnsubscribeAll();
    }
}
