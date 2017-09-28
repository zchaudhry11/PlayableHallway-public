using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class DialogueDatabase : MonoBehaviour
{
    // Dictionary with all the dialogue the player can read from objects such as characters in the scene. Key = object ID, Value = string list with the dialogue ready to be parsed.
    // If a character/object has a "shout" dialogue in which they talk to the player when not present, their key is equal to the negative of their id.
    private Dictionary<int, List<string>> interactiveDialogue = new Dictionary<int, List<string>>();

    private List<string> scrubbedFileNames = new List<string>(); // List of the dialogue files to be parsed
    private List<string> scrubbedDialogue = new List<string>(); // List of the final dialogue to be displayed

    private string[] dialogueFiles;
    private string pathName;

    // Get state info
    private TextBoxManager tbManager;

    void Start()
    {
        DialogueID[] sceneObjects = Object.FindObjectsOfType<DialogueID>(); // Get all ID'd items in scene
        string sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        pathName = Application.dataPath + "\\Resources\\" + sceneName + "\\Dialogue\\";
        dialogueFiles = Directory.GetFiles(pathName);

        for (int i = 0; i < dialogueFiles.Length; i++) // Remove all metadata files from query
        {
            string[] fileName;
            if (!dialogueFiles[i].Contains("meta"))
            {
                fileName = dialogueFiles[i].Split('\\');
                dialogueFiles[i] = fileName[fileName.Length - 1];
                scrubbedFileNames.Add(dialogueFiles[i]);
            }
        }

        for (int i = 0; i < scrubbedFileNames.Count; i++) // Check owner of dialogue and put into table
        {
            string[] objectID;
            objectID = scrubbedFileNames[i].Split('_');

            int n;
            bool isNumeric = int.TryParse(objectID[0], out n);
            string dialogueType = objectID[objectID.Length - 1];

            if (isNumeric || dialogueType[0].Equals('s')) // Check to see if start of dialogue text has an ID. If it does, add it to the hashtable with the file
            {
                if (dialogueType[0].Equals('s'))
                {
                    n *= -1;
                }

                // Parse the dialogue documents
                StreamReader sr = new StreamReader(pathName + scrubbedFileNames[i]);
                string fileContents = sr.ReadToEnd();
                sr.Close();
                string[] lines = fileContents.Split('\n');

                for (int q = 0; q < lines.Length; q++)
                {
                    int errorCounter = Regex.Matches(lines[q], @"[a-zA-Z.]").Count;

                    if (errorCounter != 0) // If text is not an empty line, then prepare it for printing
                    {
                        scrubbedDialogue.Add(lines[q]);
                    }
                }

                interactiveDialogue.Add(n, scrubbedDialogue);
                scrubbedDialogue = new List<string>(); // Reset list of dialogue for next file          
            } 
        }
    }

    // Display Standard Text Box
    public void DisplayText(int objectID, TextZone textZone)
    {
        if (interactiveDialogue.ContainsKey(objectID))
        {
            tbManager = this.GetComponent<TextBoxManager>();

            if (tbManager != null)
            {
                tbManager.CreateTextBox(interactiveDialogue[objectID], null, textZone);
            }
            else
            {
                print("TextBoxManager is null!");
            }
        }
    }

    // Display Shout Text Box
    public void DisplayText(int objectID, ShoutTextZone shoutZone)
    {
        if (shoutZone != null) // Pass the shoutZone as a reference to destroy it once text is finished.
        {
            if (interactiveDialogue.ContainsKey(objectID))
            {
                tbManager = this.GetComponent<TextBoxManager>();

                if (tbManager != null)
                {
                    tbManager.CreateTextBox(interactiveDialogue[objectID], shoutZone, null);
                }
                else
                {
                    print("TextBoxManager is null!");
                }
            }
        }
    }

}