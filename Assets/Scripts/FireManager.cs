/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FireManager : MonoBehaviour
{
    [SerializeField] List<Fire> fire;
    [SerializeField] TextWindow text;
    [SerializeField] Vector3 resetPos;
    bool inProgress = true;
    [SerializeField] GameObject camrig;
    [SerializeField] List<GameObject> winButtons;


    [SerializeField] string loseMessage;
    [SerializeField] string winMessage;

    // Update is called once per frame
    void Update ()
    {
        CheckForWin();
	}

    void CheckForWin()
    {
        bool firesStillActive = false;

        foreach (Fire f in fire)
        {
            if (f.m_IsLit)
            {
                firesStillActive = true;
            }
        }

        if (!firesStillActive)  // if no fires are lit
        {
            if (inProgress)
                Win();
        }
    }

    void Win()
    {
        inProgress = false;
        text.PushText(winMessage);
        foreach(GameObject g in winButtons)
        {
            g.SetActive(true);
        }
    }

    public void Lose()
    {
        camrig.transform.position = resetPos; // Hall way just before fire room
        ResetFires();
        text.PushText(loseMessage);
    }

    public void ResetFires()
    {
        foreach(Fire f in fire)
        {
            f.ResetFire();
        }
    }
}*/

using UnityEngine;
using System.Collections.Generic;

public class FireManager : MonoBehaviour
{
    public TextTypingBubble helpText;

    private bool m_InProgress = true;
    
    [SerializeField] private List<Fire> m_Fires = new List<Fire>();    

    void Awake()
    {
        helpText.enabled = false;

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
                if(f.transform.localScale.magnitude > f.m_MaxSize *2f)
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
        helpText.enabled = false;
        string[] s = { m_Fires[0].Type };
        Mediator.instance.NotifySubscribers("Fire", new Packet(new int[0], new bool[0], new float[0], s));
    }

    public void Lose()
    {
        if (helpText.enabled) return;

        helpText.enabled = true;
        //ResetFires();
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