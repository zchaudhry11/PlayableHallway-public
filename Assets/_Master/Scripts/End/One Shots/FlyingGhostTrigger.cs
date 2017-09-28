using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingGhostTrigger : MonoBehaviour
{
    public GameObject flyingGhost;
    public AudioClip scareTransition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            flyingGhost.SetActive(true);

            this.GetComponent<AudioSource>().PlayOneShot(scareTransition);
        }
    }
}