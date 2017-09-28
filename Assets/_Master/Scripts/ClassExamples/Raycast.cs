using System.Collections;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Camera camera;

    private void Start()
    {
        camera = this.GetComponent<Camera>();
    }

    private void Update()
    {
        /*
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 forward = this.transform.TransformDirection(Vector3.forward) * 100;
        Debug.DrawRay(transform.position, forward, Color.red);


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            print("I'm looking at: " + hit.transform.name);
        }
        else
        {
            print("I'm looking at nothing!");
        }
        */

    }
}