using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public ParticleSystem emmision;
    private ParticleSystem e;

    private void Start()
    {
       e= Instantiate(emmision, transform.position,Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
        // Rotate the object around the Z-axis
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnDisable()
    {
        Destroy(e);
    }
}
