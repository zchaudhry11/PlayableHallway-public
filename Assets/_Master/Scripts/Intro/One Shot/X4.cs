using UnityEngine;

public class X4 : MonoBehaviour
{
    public GameObject elevatorWall;
    public GameObject elevator;

    private void Start()
    {
        elevatorWall.SetActive(false);
        elevator.SetActive(true);

        Destroy(this.gameObject);
    }
}