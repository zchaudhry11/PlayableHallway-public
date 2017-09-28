using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField]
    private float interactDistance = 3.0f;

    [SerializeField]
    private LayerMask layer;

    private Camera cam;
    private IKHandling playerIK;

    // Ghost
    public GameObject ghost;

    private void Start()
    {
        cam = Camera.main;
        playerIK = this.GetComponent<IKHandling>();
    }

    private void Update()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        Vector3 forward = this.transform.TransformDirection(Vector3.forward) * 100;
        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, interactDistance, layer))
        {
            // Set emission
            InteractableObject obj = hit.transform.gameObject.GetComponent<InteractableObject>();

            // If object can be interacted with
            if (obj != null)
            {
                // Make item glow
                obj.highlighted = true;
                obj.Highlight();

                // Enable Player Left Hand IK
                if (playerIK)
                {
                    playerIK.leftIKTargetPos = obj.transform.position;
                    playerIK.SetIKLeftStatus(false);
                }
                
                if (obj.tag == "Key")
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        // Play pickup sound
                        obj.GetComponent<AudioSource>().Play();

                        // Add to inventory
                        this.GetComponent<Inventory>().Keys.Add(obj.GetComponent<Key>().KeyID);

                        // Destroy
                        StartCoroutine(DestroyItem(obj.gameObject));
                    }
                }
            }
            else
            {
                if (hit.transform.tag != "Door")
                {
                    playerIK.SetIKLeftStatus(true);
                }
            }

            if (hit.transform.tag == "Door")
            {
                Door door = hit.transform.GetComponent<Door>();

                // Player Left Hand IK
                if (playerIK && door.openedDoor == false && door.busyAnimating == false)
                {
                    playerIK.leftIKTargetPos = hit.transform.position;
                    playerIK.SetIKLeftStatus(false);
                }

                if (Input.GetMouseButtonDown(0) && door.busyAnimating == false)
                {
                    if (door.TargetKeyID == 555)
                    {
                        // Killknock
                        door.GetComponent<KillKnock>().enabled = true;
                    }
                    else
                    {
                        door.Open();
                    }
                }
            }
            else if (hit.transform.tag == "RedBook")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    // Activate book script
                    hit.transform.GetComponent<RedBook>().enabled = true;
                }
            }
            else if (hit.transform.tag == "GhostSpawnTrigger")
            {
                // Spawn ghost
                StartCoroutine(SetGhostActive());
            }
        }
    }

    private IEnumerator DestroyItem(GameObject item)
    {
        item.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(0.6f);
        Destroy(item);
    }

    private IEnumerator SetGhostActive()
    {
        yield return new WaitForSeconds(1.0f);
        ghost.SetActive(true);
    }
}