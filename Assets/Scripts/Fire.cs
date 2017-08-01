using UnityEngine;
using System.Collections;

public enum eFireType
{
    WOOD,
    ELECTRICAL,
    FLAMMABLELIQUID,
    GASEOUS,
    METAL
}

public class Fire : MonoBehaviour
{
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

    /// <summary>
    /// The type of fire (wood, gas, etc) used for extinguishing
    /// </summary>
    public eFireType m_FireType;

    /// <summary>
    /// Called when the application is started up
    /// </summary>
    void Awake()
    {
        m_OriginalScale = m_CurrentScale;   // Sets the original size, to the current size
    }

    /// <summary>
    /// Increases the size of the fire
    /// </summary>
    /// <param name="aRate">The rate at which the fire would grow per-second</param>
    public void GrowBy(float aRate)
    {
        transform.localScale += Vector3.one * (Time.deltaTime * aRate); // Increase the size over time

        m_IsLit = m_CurrentScale > m_MinSize; // If the fire is smaller than the alloted size, then it is no longer lit
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
}
