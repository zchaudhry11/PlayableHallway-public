using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Door : MonoBehaviour
{
    public int TargetKeyID = 0;
    public bool Locked = false;

    public bool openedDoor = false;
    public bool busyAnimating = false;
    private bool checkedIK = false;
    private bool finishedAnimating = true;
    private float ikResetDelay = 0.1f;
    private float ikResetTimer = 0;

    public AudioClip lockedSound;
    public AudioClip unlockedSound;
    public AudioClip openSound;

    public bool activatedRedBook = false;

    private Animator doorAnim;
    private Inventory playerInventory;
    private AudioSource doorAudio;
    private GameObject player;
    private IKHandling playerIK;

    // Ghost
    public GameObject ghostSpawner;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
        doorAudio = this.GetComponent<AudioSource>();
        doorAnim = this.GetComponent<Animator>();
        playerIK = player.GetComponent<IKHandling>();
    }

    private void FixedUpdate()
    {
        if (!finishedAnimating)
        {
            if (ikResetTimer > 0)
            {
                ikResetTimer -= Time.deltaTime;
            }
            else
            {
                if (checkedIK == false)
                {
                    playerIK.SetIKLeftStatus(true);
                    checkedIK = true;
                }
                finishedAnimating = true;
            }
        }

        if (!busyAnimating)
        {
            if (playerIK.ikWeightLeft > 0.1f)
            {
                if (checkedIK == false)
                {
                    playerIK.SetIKLeftStatus(true);
                    checkedIK = true;
                }
            }
        }
    }

    public void Open()
    {
        if (Locked == true)
        {
            if (playerInventory.ContainsTargetKey(TargetKeyID)) // Player has a key to open the door
            {
                Unlock();
            }
            else // Door is locked
            {
                // Play locked sound
                doorAudio.clip = lockedSound;
                doorAudio.Play();

                if (activatedRedBook)
                {
                    // Spawn spooky ghost
                    ghostSpawner.SetActive(true);
                }
            }
        }
        else // Open the door
        {
            if (openedDoor == false)
            {
                openedDoor = true;

                // Wait for animation
                StartCoroutine(PlayDoorOpenAnimation(2.0f));

                // Play open sound
                doorAudio.clip = openSound;
                doorAudio.Play();
            }
            else
            {
                openedDoor = false;

                // Wait for animation
                StartCoroutine(PlayCloseDoorAnimation(2.0f));

                // Play open sound
                doorAudio.clip = openSound;
                doorAudio.Play();
            }
        }
    }

    private void Unlock()
    {
        Locked = false;

        // Play unlock sound
        doorAudio.clip = unlockedSound;
        doorAudio.Play();
    }

    private IEnumerator PlayDoorOpenAnimation(float time)
    {
        busyAnimating = true;
        doorAnim.SetBool("openedDoor", true);

        // Set player left hand IK to doorknob
        playerIK.leftIKTargetPos = this.transform.GetChild(0).position;
        playerIK.SetIKLeftStatus(false);

        // Disable player controller
        FirstPersonController playerControls = player.GetComponent<FirstPersonController>();
        playerControls.enabled = false;

        // Enable player root motion
        Animator playerAnim = player.GetComponent<Animator>();
        playerAnim.applyRootMotion = true;

        // Play open door animations
        playerAnim.SetBool("openedDoor", true);

        yield return new WaitForSeconds(time);

        // Revert animation state
        playerAnim.SetBool("openedDoor", false);

        // Disable root motion
        playerAnim.applyRootMotion = false;

        // Disable left hand IK
        playerIK.SetIKLeftStatus(true);

        // Enable player controller
        playerControls.enabled = true;

        busyAnimating = false;
        finishedAnimating = false;
        checkedIK = false;
    }

    private IEnumerator PlayCloseDoorAnimation(float time)
    {
        busyAnimating = true;
        doorAnim.SetBool("openedDoor", false);

        // Set player left hand IK to doorknob
        playerIK.leftIKTargetPos = this.transform.GetChild(0).position;
        playerIK.SetIKLeftStatus(false);

        // Disable player controller
        FirstPersonController playerControls = player.GetComponent<FirstPersonController>();
        playerControls.enabled = false;

        // Enable player root motion
        Animator playerAnim = player.GetComponent<Animator>();
        playerAnim.applyRootMotion = true;

        // Play open door animations
        playerAnim.SetBool("openedDoor", true);

        yield return new WaitForSeconds(time);

        // Revert animation state
        playerAnim.SetBool("openedDoor", false);

        // Disable root motion
        playerAnim.applyRootMotion = false;

        // Disable left hand IK
        playerIK.SetIKLeftStatus(true);

        // Enable player controller
        playerControls.enabled = true;

        busyAnimating = false;
        finishedAnimating = false;
        checkedIK = false;
    }
}