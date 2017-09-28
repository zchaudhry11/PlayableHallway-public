using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class X2 : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(ChangeScene());
    }

    private IEnumerator ChangeScene()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
}