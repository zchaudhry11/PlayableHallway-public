using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBook : MonoBehaviour
{
    public Door doorToLock;

    private void Start()
    {
        doorToLock.Locked = true;
        doorToLock.TargetKeyID = 999;
        doorToLock.GetComponent<Animator>().SetBool("openedDoor", false);
        doorToLock.activatedRedBook = true;
    }
}