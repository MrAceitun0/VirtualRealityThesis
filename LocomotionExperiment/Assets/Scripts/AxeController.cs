using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public AudioSource audioSource;
    public int health = 4;
    public GameObject fullWood;
    public GameObject choppedWood;

    float canHit = 1.1f;

    private void Update()
    {
        canHit += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wood" && canHit > 1f)
        {
            audioSource.Play();
            health--;
            if(health == 0)
            {
                fullWood.SetActive(false);
                choppedWood.SetActive(true);
            }
            canHit = 0f;
        }
    }
}
