using UnityEngine;

public class XBlank : MonoBehaviour
{
    public GameObject targetAppear;

    public GameObject triggerX;

    private void Start()
    {
        if (targetAppear)
        {
            targetAppear.SetActive(true);
        }

        if (triggerX)
        {
            triggerX.SetActive(true);
        }

        Destroy(this.gameObject);
    }
}