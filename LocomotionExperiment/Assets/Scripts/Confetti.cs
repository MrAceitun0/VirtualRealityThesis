using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confetti : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Confetti")
        {
            other.GetComponent<ParticleSystem>().Play();
            other.GetComponent<AudioSource>().Play();
            Destroy(other.gameObject, 2.1f);
        }
    }
}
