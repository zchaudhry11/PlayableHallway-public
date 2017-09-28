using UnityEngine;
using System.Collections;

public class ShoutTextZone : MonoBehaviour
{
    private bool playerTriggered = false; // Raised when player triggers a shout_text zone.

    // Get state info
    private DialogueDatabase dialogueDB;

    void Start()
    {
        dialogueDB = GameObject.FindGameObjectWithTag("Dialogue Manager").GetComponent<DialogueDatabase>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player" && playerTriggered == false)
        {
            playerTriggered = true;
            dialogueDB.DisplayText(this.GetComponent<DialogueID>().objectID, this);
        }
    }

}