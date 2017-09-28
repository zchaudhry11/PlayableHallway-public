using UnityEngine;

public class AnimateUV : MonoBehaviour
{
    public float xTile = 1;
    public float yTile = 1;
    public float xOffset = 0;
    private float yOffset = 0;
    public float transitionSpeed = 1.0f;
    
    private Material mat;

    private void Start()
    {
        mat = this.GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        //mat.SetTextureOffset("_MainTex", new Vector2(0, Time.deltaTime * transitionSpeed));
        yOffset += Time.deltaTime * transitionSpeed;
        mat.SetVector("_MainTex_ST", new Vector4(xTile, yTile, xOffset, yOffset));
    }
}