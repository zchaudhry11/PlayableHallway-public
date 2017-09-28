using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LibraryGhostScare : MonoBehaviour
{
    public Transform defaultParent;
    public Transform animationParent;

    public GameObject cam;
    public Vector3 camPos;

    public AudioClip jumpScareSound;

    public UIImageFader fader;

    private GameObject player;
    private Animator anim;
    private FirstPersonController controller;
    private IKHandling ik;
    private AudioSource playerAudio;
    private Phone phone;

    private void Start()
    {
        // Disable player
        player = GameObject.FindGameObjectWithTag("Player");
        anim = player.GetComponent<Animator>();
        controller = player.GetComponent<FirstPersonController>();
        ik = player.GetComponent<IKHandling>();
        playerAudio = player.GetComponent<AudioSource>();
        phone = player.GetComponent<Phone>();

        controller.enabled = false;
        ik.ikWeightRight = 0;
        ik.ikWeightLeft = 0;
        ik.enabled = false;
        playerAudio.PlayOneShot(jumpScareSound, 1);
        phone.enabled = false;
        cam.transform.parent = animationParent;
        anim.SetBool("knockedOut", true);
        StartCoroutine(FadeSwitch());
        StartCoroutine(ChangeScene());
    }

    private IEnumerator FadeSwitch()
    {
        yield return new WaitForSeconds(2.0f);
        fader.SwitchFadeState();
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(6.75f);
        SceneManager.LoadScene(3);
    }
}