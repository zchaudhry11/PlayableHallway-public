using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;

public class DebugManager : MonoBehaviour
{
    public GameObject fpsCounter;

    public PostProcessingProfile highProfile;
    public PostProcessingProfile lowProfile;

    private PostProcessingBehaviour camProfile;
    public bool lowSettings = false;

    private void Start()
    {
        camProfile = Camera.main.GetComponent<PostProcessingBehaviour>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            fpsCounter.SetActive(!fpsCounter.activeInHierarchy);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SceneManager.LoadScene(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            lowSettings = !lowSettings;

            if (lowSettings)
            {
                camProfile.profile = lowProfile;
            }
            else
            {
                camProfile.profile = highProfile;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SceneManager.LoadScene(4);
        }
    }
}