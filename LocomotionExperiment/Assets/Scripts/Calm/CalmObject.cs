using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalmObject : MonoBehaviour
{
    public MeshRenderer objRenderer;

    public Material opaqueMat;

    string opaqueName;

    private void Start()
    {
        opaqueName = opaqueMat.name;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (objRenderer.material.name.Contains(opaqueName))
        {
            return;
        }

        if (other.tag == "Altar")
        {
            objRenderer.material = opaqueMat;
            Destroy(this.gameObject);
        }
    }
}
