using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public int goldAmount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Gold")
        {
            other.GetComponent<AudioSource>().Play();
            //other change gold material
            Destroy(this.gameObject);
        }
    }
}
