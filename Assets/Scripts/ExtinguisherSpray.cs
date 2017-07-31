using UnityEngine;
using System.Collections;

enum ExtinguisherType
{
    CO2,
    POWDER,
    FOAM,
    WATER
}

public class ExtinguisherSpray : MonoBehaviour
{
    [SerializeField] ExtinguisherType type;

    [SerializeField]private bool m_shake;
    private Vector3 m_lastYRotation;

    
    [SerializeField]float shakeBreak = 0.5f;

    void Start()
    {
        StartCoroutine(Extinguish());
    }

    IEnumerator Extinguish()
    {
        while(true)
        {
            float currentYRotation = Vector3.Distance(gameObject.transform.eulerAngles, m_lastYRotation);

            m_shake = (Mathf.Abs(currentYRotation) > shakeBreak);
            m_lastYRotation = transform.eulerAngles;

            yield return new WaitForSeconds(0.1f);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((other.gameObject.GetComponent<Fire>() != null) && m_shake)
        {
            DownFire(other);
        }
    }

    void DownFire(Collider other)
    {
        Fire F = other.gameObject.GetComponent<Fire>();

        if (F != null)
        {
            if (Mathf.Abs(transform.position.y - F.transform.position.y) < 1.0f)
            {
                Vector3 fireScale = other.gameObject.transform.localScale;

                #region Fire check
                switch (F.m_FireType)
                {
                    case eFireType.WOOD:
                        other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * 0.8f);
                        break;

                    case eFireType.ELECTRICAL:
                        if (type == ExtinguisherType.FOAM || type == ExtinguisherType.WATER)
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * -0.8f);
                        else
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * 0.8f);
                        break;

                    case eFireType.FLAMMABLELIQUID:
                        if (type == ExtinguisherType.WATER)
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * -1);
                        else
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * 0.8f);
                        break;

                    case eFireType.GASEOUS:
                        if (type == ExtinguisherType.FOAM || type == ExtinguisherType.WATER)
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * -1);
                        else
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * 0.8f);
                        break;

                    case eFireType.METAL:
                        if (type == ExtinguisherType.FOAM || type == ExtinguisherType.WATER)
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * -1f);
                        else
                            other.gameObject.transform.localScale -= ((fireScale * Time.deltaTime) * 0.8f);
                        break;

                    default:
                        break;

                }
                #endregion
            }
        }
    }
}