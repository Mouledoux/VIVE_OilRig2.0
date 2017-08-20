using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    public UnityEngine.UI.Toggle flair, oil, wood, electric, tower;
    public Animation anim;

    private Mediator.Callback FireOut;
    private Mediator.Callback Traversal;

    private Mediator.Subscriptions subs = new Mediator.Subscriptions();

	// Use this for initialization
	void Awake ()
    {
        anim = GetComponent<Animation>();

        FireOut += FireCheck;
        Traversal += LocCheck;

        subs.Subscribe("Location", Traversal);
        subs.Subscribe("Fire", FireOut);
    }

    public void LocCheck(Packet p)
    {
        if(p.strings[0] == "Flair")
        {
            Check(flair);
        }
        else if(p.strings[0] == "Tower")
        {
            Check(tower);
        }
    }

    private void FireCheck(Packet p)
    {
        if (p.strings[0] == "Oil")
        {
            Check(oil);
        }
        else if (p.strings[0] == "Wood")
        {
            Check(wood);
        }
        else if (p.strings[0] == "Electric")
        {
            Check(electric);
        }
    }

    private void Check(UnityEngine.UI.Toggle t)
    {
        if (t.isOn) return;

        t.isOn = true;

        t.gameObject.SetActive(false);
        t.gameObject.SetActive(true);

        PlayAnimation();
    }

    public void PLayAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    public void PlayAnimation()
    {
        anim.Play();
    }
}
