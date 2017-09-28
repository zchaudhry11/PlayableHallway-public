using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class GhostFly : MonoBehaviour
{
    public float flySpeed = 25.0f;

    public Transform defaultParent;
    public Transform animationParent;

    public GameObject cam;
    public Vector3 camPos;
    public UIImageFader fader;

    private GameObject player;
    private FirstPersonController controller;
    private Animator anim;
    private Phone phone;
    private IKHandling ik;
    private PlayerInteract interact;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<FirstPersonController>();
        anim = player.GetComponent<Animator>();
        phone = player.GetComponent<Phone>();
        ik = player.GetComponent<IKHandling>();
        interact = player.GetComponent<PlayerInteract>();
    }


    private void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * flySpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // Knockout player
            controller.enabled = false;
            phone.enabled = false;
            interact.enabled = false;
            ik.ikWeightLeft = 0;
            ik.ikWeightRight = 0;
            cam.transform.parent = animationParent;
            anim.SetBool("knockedOut", true);
            StartCoroutine(KnockOut());
        }
    }

    private IEnumerator KnockOut()
    {
        fader.SwitchFadeState();
        yield return new WaitForSeconds(3.25f);

        // Change to end scene
        SceneManager.LoadScene(4);
    }
}