using UnityEngine;

public class X1 : MonoBehaviour
{
    public GameObject targetDoor;
    public GameObject wallToHide;

    private void Start()
    {
        wallToHide.SetActive(false);
        targetDoor.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true; // Enable door
        targetDoor.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true; // Enable handle
        targetDoor.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true; // Enable hinges
        targetDoor.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true; // Enable frame
        targetDoor.GetComponent<AnimateDoor>().SwitchDoorState(true);
    }
}