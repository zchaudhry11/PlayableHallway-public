using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtil : MonoBehaviour
{
    public int sceneNum;
    public float changeTime = 0;

    public UIImageFader fader;

    public AudioClip thud;
    private AudioSource aud;

    private void Start()
    {
        StartCoroutine(SceneChange());

        aud = this.GetComponent<AudioSource>();

        if (aud)
        {
            fader.SwitchFadeState();
            aud.PlayOneShot(thud);
        }
    }

    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(changeTime);
        SceneManager.LoadScene(sceneNum);
    }
}