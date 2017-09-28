using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    [SerializeField]
    private float interactDistance = 15.0f;

    [SerializeField]
    private LayerMask layer;

    [SerializeField]
    private float pictureDelay = 1.0f;
    private float pictureTimer = 0;
    private bool tookPicture = false;
    private bool cameraFlashed = false;
    private float flashLength = 0.20f;
    private float flashTimer = 0;

    // Phone IK
    public bool phoneEquipped = false;
    private IKHandling playerIK;
    private float equipTimer = 1.0f;
    private float timer = 0;

    public AudioClip cameraSound;

    public Camera phoneCam;
    public GameObject camFlash;
    public GameObject camLight;
    private Camera cam;
    public AudioSource phoneAudio;

    // Flash color
    private Color flashColor;

    private void Start()
    {
        cam = Camera.main;
        playerIK = this.GetComponent<IKHandling>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && !tookPicture && phoneEquipped)
        {
            // Raycast based on player's camera
            Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            Vector3 forward = this.transform.TransformDirection(Vector3.forward) * 100;

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, interactDistance, layer))
            {
                if (hit.transform.tag == "X_Event")
                {
                    X1 event1 = hit.transform.GetComponent<X1>();
                    X2 event2 = hit.transform.GetComponent<X2>();
                    X3 event3 = hit.transform.GetComponent<X3>();
                    X4 event4 = hit.transform.GetComponent<X4>();
                    XBlank eventBlank = hit.transform.GetComponent<XBlank>();

                    if (event1)
                    {
                        StartCoroutine(DelayEvent1(event1));

                        // Play shatter vfx
                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                        Destroy(hit.transform.gameObject, 5.0f);
                    }
                    else if (event2)
                    {
                        StartCoroutine(DelayEvent2(event2));

                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    else if (event3)
                    {
                        StartCoroutine(DelayEvent3(event3));

                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    else if (event4)
                    {
                        StartCoroutine(DelayEvent4(event4));

                        hit.transform.GetComponent<MeshRenderer>().enabled = false;
                        hit.transform.GetComponent<BoxCollider>().enabled = false;
                    }
                    else if (eventBlank)
                    {
                        StartCoroutine(DelayEventBlank(eventBlank));
                    }
                }
                else if (hit.transform.tag == "Door")
                {
                    if (hit.transform.GetComponent<Door>().TargetKeyID == 1337) // hallway puzzle door
                    {
                        StartCoroutine(FlashDoorColor(hit.transform.gameObject, new Color(0, 1, 0.792f, 1)));
                    }
                }
            }

            // Picture effect on phone
            TakePicture();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            EquipPhone();
        }
    }

    private void FixedUpdate()
    {
        if (tookPicture)
        {
            if (pictureTimer > 0)
            {
                pictureTimer -= Time.deltaTime;

                if (pictureTimer == 0.2f)
                {
                    FlashCamera();
                }

            }
            else
            {
                phoneCam.enabled = true;
                tookPicture = false;
            }
        }

        if (cameraFlashed)
        {
            if (flashTimer > 0)
            {
                flashTimer -= Time.deltaTime;
            }
            else
            {
                camFlash.SetActive(false);
                cameraFlashed = false;
            }
        }

        if (phoneEquipped)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (playerIK.ikWeightRight < 1)
                {
                    playerIK.ikWeightRight += Time.deltaTime;
                }
            }
        }
        else
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;

                if (playerIK.ikWeightRight > 0)
                {
                    playerIK.ikWeightRight -= Time.deltaTime;
                }
            }
        }
    }

    public void FlashCamera()
    {
        flashTimer = flashLength;
        cameraFlashed = true;
        camFlash.SetActive(true);
    }

    public void TakePicture()
    {
        // Disable phone camera for 1 second then turn it back on
        phoneCam.enabled = false;
        tookPicture = true;
        pictureTimer = pictureDelay;

        // Play sound effect
        if (phoneAudio)
        {
            if (cameraSound)
            {
                phoneAudio.PlayOneShot(cameraSound, 1);
            }
        }
    }

    public void EquipPhone()
    {
        phoneEquipped = !phoneEquipped;
        timer = equipTimer;

        if (!phoneEquipped)
        {
            if (camLight)
            {
                camLight.SetActive(false);
            }
        }
        else
        {
            if (camLight)
            {
                camLight.SetActive(true);
            }
        }
    }

    private IEnumerator DelayEvent1(X1 event1)
    {
        yield return new WaitForSeconds(0.25f);
        event1.enabled = true;
    }

    private IEnumerator DelayEvent2(X2 event2)
    {
        yield return new WaitForSeconds(0.1f);
        event2.enabled = true;
    }

    private IEnumerator DelayEvent3(X3 event3)
    {
        yield return new WaitForSeconds(0.1f);
        event3.enabled = true;
    }

    private IEnumerator DelayEvent4(X4 event4)
    {
        yield return new WaitForSeconds(0.1f);
        event4.enabled = true;
    }

    private IEnumerator DelayEventBlank(XBlank eventBlank)
    {
        yield return new WaitForSeconds(0.25f);
        eventBlank.enabled = true;
    }

    private IEnumerator FlashDoorColor(GameObject go, Color col)
    {
        Renderer rend = go.transform.GetChild(0).GetComponent<Renderer>();

        rend.material.SetColor("_EmissionColor", col);
        yield return new WaitForSeconds(0.6f);
        rend.material.SetColor("_EmissionColor", Color.black);
    }
}