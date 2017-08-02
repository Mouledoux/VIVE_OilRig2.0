using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherNoz : MonoBehaviour
{
    private ParticleSystem m_sprayParticles;

    private Mediator.Subscriptions subscriptions = new Mediator.Subscriptions();
    private Mediator.Callback onSpray;
    private Mediator.Callback offSpray;

	// Use this for initialization
	void Start ()
    {
        m_sprayParticles = GetComponentInChildren<ParticleSystem>();

        onSpray += StartSpray;
        offSpray += StopSpray;

        subscriptions.Subscribe("extinguish.start", onSpray);
        subscriptions.Subscribe("extinguish.stop", offSpray);
    }
	
    private void StartSpray(Packet data)
    {
        float strength = data.floats[0];
        m_sprayParticles.emissionRate = (strength * 64f) -1;
    }

    private void StopSpray(Packet data)
    {
        m_sprayParticles.emissionRate = 0;
    }

    private void OnDestroy()
    {
        subscriptions.UnsubscribeAll();
    }

}
