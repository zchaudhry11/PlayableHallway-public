using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKnocker : MonoBehaviour
{
    public AudioClip knocking;
    private bool activated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!activated)
            {
                activated = true;
                this.GetComponent<AudioSource>().PlayOneShot(knocking);
            }
        }
    }
}