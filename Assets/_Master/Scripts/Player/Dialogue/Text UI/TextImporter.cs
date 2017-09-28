using UnityEngine;
using System.Collections;

public class TextImporter : MonoBehaviour
{
    public TextAsset textFile; //Text file to be parsed
    public string[] text;

    void Start()
    {
        if (textFile != null)
        {
            text = textFile.text.Split('\n'); //Split text based on each line
        }
    }
}