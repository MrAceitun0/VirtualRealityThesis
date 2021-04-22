using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldController : MonoBehaviour
{
    public MeshRenderer goldA;
    public MeshRenderer goldB;
    public MeshRenderer goldC;

    public Material opaqueMaterial;
    public Material transparentMaterial;

    string transparentName;

    private void Start()
    {
        transparentName = transparentMaterial.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Chest")
        {
            if(goldA.material.name.Contains(transparentName))
            {
                goldA.material = opaqueMaterial;
            }
            else if (goldB.material.name.Contains(transparentName))
            {
                goldB.material = opaqueMaterial;
            }
            else if (goldC.material.name.Contains(transparentName))
            {
                goldC.material = opaqueMaterial;
            }
            other.GetComponent<AudioSource>().Play();
            Destroy(this.gameObject);
        }
    }
}
