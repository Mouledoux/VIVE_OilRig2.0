using UnityEngine;
using System.Collections;

public class ExtinguisherController : MonoBehaviour
{
    void Start()
    {
        SetSpray(false);
    }

	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Space))
        {
            SetSpray(true);
        }
        
        else if(Input.GetKeyUp(KeyCode.Space))
        {
            SetSpray(false);
        }
	}
    
    public void SetSpray(bool active)
    {
        SprayNozzle.enabled = active;
        
        switch(active)
        {
            case true:
                SprayFoam.Play();
                break;
            case false:
                SprayFoam.Stop();
                break;
        }
    }

    public Collider SprayNozzle;
    public ParticleSystem SprayFoam;
}
