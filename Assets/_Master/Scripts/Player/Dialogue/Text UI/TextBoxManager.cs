using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour
{
    public GameObject textBoxPrefab;

    private GameObject textPanel;
    private Text boxText;

    private List<string> scrubbedText; // Final text to be printed

    private int currLine;
    private int endLine = 0; // Last line to read. Set to 0 to read entire script

    // Animated typing controls
    public float typingSpeed = 0.03f;
    private bool isTyping = false;
    private bool interruptedTyping = false;

    // References to the text components that create the text boxes. Shoutzones are destroyed after chatting.
    private GameObject shoutZoneObject;
    private GameObject textZoneObject;

    private bool finishedLine = false;

    // Get state info
    private GameObject canvas;
    private GameObject dialogueDB;

    public Font textFont;
    public AudioClip scribble;
    private AudioSource playerAudio;

    void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Main Canvas");

        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }

    void Update()
    {
        finishedLine = false;

        if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0)) // 
        {
            finishedLine = true;
        }

        if (!isTyping && finishedLine)
        {
            //print("typing");
            currLine++;

            if (currLine > endLine)
            {
                if (shoutZoneObject != null) // Destroy the shoutzone gameobject if chat was started by shouting
                {
                    Destroy(shoutZoneObject);
                }
                else if (textZoneObject != null) // Reset player interaction with NPC/Object so they can talk again in the future.
                {
                    textZoneObject.GetComponent<TextZone>().ResetInteraction();
                    textZoneObject = null;
                }

                Destroy(textPanel);
                endLine = 0;
                currLine = 0;
            }
            else
            {
                StartCoroutine(TextScroll(scrubbedText[currLine]));
            }
        }
        else if (isTyping && !interruptedTyping)
        {
            //interruptedTyping = true;
        }
    }

    // Animate text
    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        boxText.text = "";
        isTyping = true;
        interruptedTyping = false;

        if (scribble)
        {
            playerAudio.PlayOneShot(scribble);
        }

        while (isTyping && !interruptedTyping && (letter < lineOfText.Length-1))
        {
            boxText.text += lineOfText[letter];
            letter++;

            yield return new WaitForSeconds(typingSpeed);
        }
        boxText.text = lineOfText;
        isTyping = false;
        interruptedTyping = false;
    }

    // Create a text box and animate the spoken dialogue
    public void CreateTextBox(List<string> dialogue, ShoutTextZone shoutZone, TextZone textZone)
    {
        if (shoutZone != null)
        {
            shoutZoneObject = shoutZone.transform.gameObject;
            textZoneObject = null;
        }
        else if (textZone != null)
        {
            shoutZoneObject = null;
            textZoneObject = textZone.transform.gameObject;
        }
        
        if (dialogue != null)
        {
            GameObject clone = Instantiate(textBoxPrefab) as GameObject;
            textPanel = clone.transform.GetChild(0).gameObject; // Set panel
            boxText = textPanel.transform.GetChild(0).GetComponent<Text>(); // Set text

            if (textFont)
            {
                boxText.font = textFont;
                boxText.color = Color.red;
            }

            clone.transform.SetParent(canvas.transform, false); // Parent text box to canvas
            scrubbedText = dialogue;

            if (endLine == 0)
            {
                endLine = scrubbedText.Count - 1;
            }

            StartCoroutine(TextScroll(scrubbedText[currLine]));
        }
    }

}