using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clipboard : MonoBehaviour
{
    public UnityEngine.UI.Toggle flair, oil, wood, electric, tower;
    public Animation anim;

    Mediator.Callback FireOut;
    Mediator.Callback Traversal;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animation>();


        Mediator.Subscriptions subs = new Mediator.Subscriptions();
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

    public void FireCheck(Packet p)
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

    public void Check(UnityEngine.UI.Toggle t)
    {
        if (t.isOn) return;

        t.isOn = true;
        PlayAnim();
    }

    public void PLayAudio()
    {
        GetComponent<AudioSource>().Play();
    }

    public void PlayAnim()
    {
        anim.Play();
    }
}
