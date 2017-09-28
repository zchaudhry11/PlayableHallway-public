using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class TextZone : MonoBehaviour
{
    [SerializeField]
    private bool useRaycastInteraction = true; // Determines whether dialogue will be based on raycasting an object or being inside an object's trigger zone.

    public bool playerTriggered = false; // Raised when player triggers a text zone.
    public bool playerStartedChat = false; // Raised when player begins talking to object/NPC.
    public bool finishedChat = true; // Raised when player finishes talking to object/NPC.

    // Get state info
    private GameObject player;
    private DialogueDatabase dialogueDB;
    private InteractableObject obj;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        dialogueDB = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<DialogueDatabase>();
        obj = this.GetComponent<InteractableObject>();
    }

    void Update()
    {
        if (useRaycastInteraction)
        {
            if (obj.highlighted)
            {
                playerTriggered = true;
            }
            else
            {
                playerTriggered = false;
            }

            if (playerTriggered && playerStartedChat == false)
            {
                if (Input.GetMouseButtonDown(0) && finishedChat == true)
                {
                    //print("in");
                    finishedChat = false;
                    playerStartedChat = true;
                    dialogueDB.DisplayText(this.GetComponent<DialogueID>().objectID, this);
                    player.GetComponent<FirstPersonController>().enabled = false;
                }
                else if (playerStartedChat == false && finishedChat == false)
                {
                    //print("in2");
                    //finishedChat = true;
                }
            }
        }
        else
        {
            if (playerTriggered && playerStartedChat == false)
            {
                if (Input.GetKeyDown(KeyCode.X) && finishedChat == true)
                {
                    finishedChat = false;
                    playerStartedChat = true;
                    dialogueDB.DisplayText(this.GetComponent<DialogueID>().objectID, this);
                }
                else if (Input.GetKeyDown(KeyCode.X) && playerStartedChat == false && finishedChat == false)
                {
                    finishedChat = true;
                }
            }
        }
    }

    /// <summary>
    /// Starts a dialogue with this object or character.
    /// </summary>
    public void TriggerDialogue()
    {
        playerTriggered = true;

    }

    void OnTriggerEnter(Collider col)
    {
        if (!useRaycastInteraction)
        {
            if (col.tag == "Player" && playerTriggered == false)
            {
                playerTriggered = true;
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (!useRaycastInteraction)
        {
            if (col.tag == "Player" && playerTriggered)
            {
                playerTriggered = false;
                playerStartedChat = false;
            }
        }
    }

    public void ResetInteraction()
    {
        //print("ENTERED!!!");
        //playerStartedChat = false;
        player.GetComponent<FirstPersonController>().enabled = true;
        StartCoroutine(ResetChat());
        finishedChat = true;
    }

    private IEnumerator ResetChat()
    {
        yield return new WaitForSeconds(0.25f);
        playerStartedChat = false;
    }
}