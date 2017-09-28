using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateDoor : MonoBehaviour
{
    public AudioClip openDoorSound;
    private AudioSource doorAudio;

    private Animator anim;

    private void Start()
    {
        anim = this.GetComponent<Animator>();
        doorAudio = this.GetComponent<AudioSource>();
    }

    public void SwitchDoorState(bool open)
    {
        anim.SetBool("openedDoor", true);
        doorAudio.PlayOneShot(openDoorSound);
    }
}