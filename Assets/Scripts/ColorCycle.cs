using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCycle : MonoBehaviour
{
    public UnityEngine.UI.Text text;
    private Color c = Color.white;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        c.r = Mathf.Abs(Mathf.Sin(Time.time));
        c.b = Mathf.Abs(Mathf.Cos(Time.time));
        c.g = Mathf.Abs(Time.time % 1f);

        text.color = c;
	}
}
