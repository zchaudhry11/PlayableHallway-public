using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostJumpScare : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(JumpScareDestroy());
    }

    private IEnumerator JumpScareDestroy()
    {
        yield return new WaitForSeconds(0.9f);
        Destroy(this.gameObject);
    }
}