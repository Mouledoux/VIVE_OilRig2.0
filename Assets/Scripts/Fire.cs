/*using UnityEngine;
using System.Collections;

public enum FireType
{
    WOOD,
    ELECTRICAL,
    FLAMMABLELIQUID,
    GASEOUS,
    METAL
}

public class Fire : MonoBehaviour
{
    private float originalScale;
    FireManager fm;
    public FireType fireType;
    [SerializeField] float killFloat = 0f;
    public bool lit = true;
    bool toBig = false;

    void Awake()
    {
        originalScale = transform.localScale.magnitude;
        fm = transform.parent.GetComponent<FireManager>();
    }

	void Update ()
    {
        if (transform.localScale.magnitude >= 5 && lit)
        {
            lit = false;
            fm.Lose();
        }

        if (transform.localScale.magnitude < killFloat)
        {
            lit = false;
            gameObject.SetActive(false);
        }

        if(transform.localScale.magnitude < originalScale)
        {
            transform.localScale += (transform.localScale * (Time.deltaTime / 2));
        }
	}

    public void ResetFire()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1) * originalScale;
        lit = true;
        gameObject.SetActive(true);
    }
    
}*/

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
    /// Called once per frame
    /// </summary>
	void Update ()
    {
        if (m_CurrentScale < m_MinSize) // If the fire is smaller than the alloted size
        {                               //
            m_IsLit = false;                // then it is no longer lit
        }

        //else if (m_CurrentScale < m_MaxSize)    // If the fire is larger than the alloted size
        //{                                       //
        //    GrowBy(0.1f);                           // then grow
        //}
    }

    /// <summary>
    /// Increases the size of the fire
    /// </summary>
    /// <param name="aRate">The rate at which the fire would grow per-second</param>
    /// <returns>Returns 0 to signal successful completion</returns>
    public int GrowBy(float aRate)
    {
        transform.localScale += Vector3.one * (Time.deltaTime * aRate); // Increase the size over time
        return 0;                                                       // Return 0 to show completion
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
