using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DianaController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Diana")
        {
            other.GetComponent<AudioSource>().Play();
            other.GetComponent<ParticleSystem>().Play();
            Destroy(this.gameObject, 2f);
        }
    }
}
