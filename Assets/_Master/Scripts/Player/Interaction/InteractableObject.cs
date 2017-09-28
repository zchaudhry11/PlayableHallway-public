using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool setColor = false;
    public bool randomColor = false;

    [SerializeField]
    private Color baseColor = Color.black;

    [SerializeField]
    private Color highlightColor = Color.white;

    public bool highlighted { get; set; }

    private float timer = 0.1f;

    private Renderer objRenderer;
    private Material mat;

    private void Start()
    {
        objRenderer = this.GetComponent<Renderer>();
        mat = objRenderer.material;

        if (setColor)
        {
            if (randomColor)
            {
                baseColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
            }
            
            mat.color = baseColor;

            mat.SetColor("_EmissionColor", baseColor);
        }
    }

    private void FixedUpdate()
    {
        if (highlighted)
        {
            if (timer <= 0)
            {
                highlighted = false;
                Highlight();
            }
            else
            {
                timer -= Time.deltaTime;
            }
        }
        
    }

    /// <summary>
    /// Highlights an object that the player is looking at by adjusting material emission.
    /// </summary>
    public void Highlight()
    {
        if (!setColor && !randomColor)
        {
            if (highlighted)
            {
                timer = 0.1f;
                mat.SetColor("_EmissionColor", highlightColor);
            }
            else
            {
                mat.SetColor("_EmissionColor", baseColor);
            }
        }
    }
}