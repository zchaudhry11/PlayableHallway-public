using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X3 : MonoBehaviour
{
    public GameObject ventGround;
    public GameObject vent;

    private void Start()
    {
        ventGround.SetActive(false);
        vent.SetActive(true);

        Destroy(this.gameObject);
    }
}