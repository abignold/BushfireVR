using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        transform.localScale -= new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log("COLLISION");
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("P-COL");
    }
}
