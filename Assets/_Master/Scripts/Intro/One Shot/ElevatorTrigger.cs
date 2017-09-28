using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.SceneManagement;

public class ElevatorTrigger : MonoBehaviour
{
    public GameObject ghost;
    public GameObject ghostPos;

    private AudioSource audioSource;
    public AudioClip scareTransition;
    public AudioClip fenceClose;

    public Animator elevatorGateAnim;

    private bool activated = false;

    public Transform defaultParent;
    public Transform animationParent;

    public GameObject cam;
    public Vector3 camPos;
    public UIImageFader fader;

    private GameObject player;
    private Animator playerAnim;
    private FirstPersonController playerController;
    private Phone phone;
    private PlayerInteract interact;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAnim = player.GetComponent<Animator>();
        playerController = player.GetComponent<FirstPersonController>();
        phone = player.GetComponent<Phone>();
        interact = player.GetComponent<PlayerInteract>();

        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !activated)
        {
            activated = true;

            // Start Elevator
            elevatorGateAnim.SetBool("fenceActive", true);
            audioSource.PlayOneShot(fenceClose);

            // Teleport ghost in front of player
            ghost.transform.position = ghostPos.transform.position;

            // Knock out player
            StartCoroutine(KnockOutPlayer());
            StartCoroutine(TeleportGhost());
            StartCoroutine(ChangeScene());
        }
    }

    private void Update()
    {
        if (activated)
        {
            ghost.transform.position = ghostPos.transform.position;
            ghost.transform.rotation = ghostPos.transform.rotation;
        }
    }

    private IEnumerator KnockOutPlayer()
    {
        yield return new WaitForSeconds(3.0f);

        playerController.enabled = false;
        phone.enabled = false;
        interact.enabled = false;
        player.GetComponent<IKHandling>().ikWeightRight = 0;
        cam.transform.parent = animationParent;
        Camera.main.fieldOfView = 90;
        playerAnim.SetBool("knockedOut", true);
    }

    private IEnumerator TeleportGhost()
    {
        yield return new WaitForSeconds(5.0f);

        // Play sound
        audioSource.PlayOneShot(scareTransition);
        fader.SwitchFadeState();

        // Teleport ghost to player
        SetLayerRecursively(ghost, 0);
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(6.75f);
        SceneManager.LoadScene(2);
    }

    private static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}