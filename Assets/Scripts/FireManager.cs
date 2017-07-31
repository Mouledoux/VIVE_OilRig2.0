using UnityEngine;
using System.Collections.Generic;

public class FireManager : MonoBehaviour
{
    private bool m_InProgress = true;

    [SerializeField] private GameObject m_CameraRig;
    [SerializeField] private Vector3 m_ResetPos;
    [SerializeField] private TextWindow m_TextWindow;
    [SerializeField] private List<Fire> m_Fires = new List<Fire>();
    [SerializeField] private List<GameObject> m_WinButtons = new List<GameObject>();

    [SerializeField] private string m_LoseMessage;
    [SerializeField] private string m_WinMessage;    

    void Awake()
    {
        Fire[] fireChildren = GetComponentsInChildren<Fire>();
        foreach (Fire f in fireChildren)
        {
            m_Fires.Add(f);
        }
    }

    void Update()
    {
        if(m_InProgress)
            CheckForWin();
    }

    private int CheckForWin()
    {
        bool canWin = true;

        foreach (Fire f in m_Fires)
        {
            if (f.m_IsLit)
            {
                canWin = false;
                if(f.transform.localScale.magnitude > f.m_MaxSize * 2f)
                {
                    Lose();
                }
            }
            else
            {
                f.gameObject.SetActive(false);
            }
        }
        
        if(canWin)
            Win();
        return 0;
    }

    private void Win()
    {
        m_InProgress = false;
        if(m_TextWindow != null)
            m_TextWindow.PushText(m_WinMessage);
        foreach (GameObject g in m_WinButtons)
        {
            g.SetActive(true);
        }
    }

    public void Lose()
    {
        m_CameraRig.transform.position = m_ResetPos;
        ResetFires();
        m_TextWindow.PushText(m_LoseMessage);
    }

    public void ResetFires()
    {
        m_InProgress = true;
        foreach (Fire f in m_Fires)
        {
            f.ResetFire();
        }
    }
}