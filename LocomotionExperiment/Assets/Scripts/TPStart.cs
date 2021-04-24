using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPStart : MonoBehaviour
{
    bool first = true;

    // Update is called once per frame
    void Update()
    {
        if (first && transform.position.z < 30f && transform.position.x < 5f)
        {
            FirebaseManager.Instance.enableGameTime();
            first = false;
        }
    }
}
