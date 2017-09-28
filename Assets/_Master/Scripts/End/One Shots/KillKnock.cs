using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class KillKnock : MonoBehaviour
{
    public AudioClip kill;
    public AudioClip forceKnock;

    private AudioSource doorAudio;
    private FirstPersonController controller;
    public GameObject[] blood;
    public GameObject[] eyes;
    public GameObject ghostTrigger;

    private void Start()
    {
        this.tag = "Untagged";

        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        doorAudio = this.GetComponent<AudioSource>();

        controller.enabled = false;
        Camera.main.fieldOfView = 45;
        StartCoroutine(ForceKnock());
        StartCoroutine(Blood());

        StartCoroutine(EnableEye());
    }

    private IEnumerator ForceKnock()
    {
        doorAudio.PlayOneShot(forceKnock);
        yield return new WaitForSeconds(6.0f);
        doorAudio.PlayOneShot(kill);
    }

    private IEnumerator Blood()
    {
        yield return new WaitForSeconds(7.6f);

        for (int i = 0; i < blood.Length; i++)
        {
            blood[i].SetActive(true);
        }

        ghostTrigger.SetActive(true);
        Camera.main.fieldOfView = 60;
        controller.enabled = true;
    }

    private IEnumerator EnableEye()
    {
        eyes[0].SetActive(true);
        eyes[2].SetActive(true);
        eyes[4].SetActive(true);
        eyes[6].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        eyes[1].SetActive(true);
        eyes[3].SetActive(true);
        eyes[5].SetActive(true);
        eyes[7].SetActive(true);
    }

}