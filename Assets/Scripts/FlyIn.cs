using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyIn : MonoBehaviour
{
    public float radius;
    public float height;
    public float speed;
    public UnityEngine.Events.UnityEvent OnEnd;

    private Vector3 origin;
    private Quaternion originalRot;

    private float time;

    private void Start()
    {
        originalRot = transform.rotation;
        origin = transform.position;

        time = 0;
    }
    
    void Update()
    {
        time += Time.deltaTime;

        Vector3 pos = Vector3.zero;
        pos.x = Mathf.Cos(time) * radius;
        pos.y = Mathf.Clamp(height - time * 2, 0, height);
        pos.z = Mathf.Sin(time) * radius;

        pos += origin;

        transform.position = pos;
        Vector3 v = transform.forward;
        v.y = 0;
        transform.LookAt(origin + v * 10);

        radius = Mathf.Clamp(radius - Time.deltaTime * speed, 0, radius);

        if (radius == 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, Time.deltaTime);

            if(transform.rotation == originalRot)
            {
                OnEnd.Invoke();
                Destroy(this);
            }
        }
    }
}