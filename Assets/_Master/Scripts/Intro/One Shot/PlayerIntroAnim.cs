using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerIntroAnim : MonoBehaviour
{
    public Transform defaultParent;
    public Transform animationParent;

    public GameObject cam;
    public Vector3 camPos;

    private Animator anim;
    private FirstPersonController controller;
    private Phone phone;
    private PlayerInteract interact;

    private void Start()
    {
        Cursor.visible = false;

        anim = this.GetComponent<Animator>();
        controller = this.GetComponent<FirstPersonController>();
        phone = this.GetComponent<Phone>();
        interact = this.GetComponent<PlayerInteract>();

        StartCoroutine(PlayStandUpAnim());
    }

    private IEnumerator PlayStandUpAnim()
    {
        controller.enabled = false;
        phone.enabled = false;
        interact.enabled = false;
        cam.transform.parent = animationParent;
        anim.SetBool("standingUp", true);

        yield return new WaitForSeconds(7.0f);

        anim.SetBool("standingUp", false);
        cam.transform.parent = defaultParent;
        cam.transform.localPosition = camPos;
        cam.transform.localRotation = Quaternion.Euler(Vector3.zero);
        controller.enabled = true;
        phone.enabled = true;
        interact.enabled = true;

        this.enabled = false;
    }
}