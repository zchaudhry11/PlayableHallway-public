using UnityEngine;
using UnityEngine.UI;

public class UIImageFader : MonoBehaviour
{
    public float fadeSpeed = 0.05f;

    public bool fade = false;
    public Color startColor = Color.black;
    public Color fadeColor;

    private float alpha = 1;
    private Image fadeImage;

    private void Start()
    {
        fadeImage = this.GetComponent<Image>();

        fadeColor = startColor;

        fade = true;
    }

    private void FixedUpdate()
    {
        if (fade)
        {
            if (fadeColor.a > 0)
            {
                fadeColor.a -= (Time.deltaTime * fadeSpeed);

                fadeImage.color = fadeColor;
            }
        }
        else
        {
            if (fadeColor.a < 1)
            {
                fadeColor.a += (Time.deltaTime * 0.5f);

                fadeImage.color = fadeColor;
            }
        }
    }

    public void SwitchFadeState()
    {
        fade = !fade;
    }
}