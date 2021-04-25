using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimeController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "START")
        {
            FirebaseManager.Instance.enableGameTime();
        }
        else if (other.gameObject.name == "END")
        {
            FirebaseManager.Instance.disableGameTime();
        }
    }
}
