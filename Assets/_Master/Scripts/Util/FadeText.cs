using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeText : MonoBehaviour
{
    public GameObject text;
    public Vector3 endPos;

    public Color startColor = Color.white;
    public Color fadeColor = new Color(1, 1, 1, 0);

    private RectTransform rect;
    private Text textComp;

    public float transitionSpeed = 40.0f;

    private void Start()
    {
        rect = text.GetComponent<RectTransform>();

        fadeColor = startColor;
        textComp = text.GetComponent<Text>();
    }

    private void FixedUpdate()
    {
        if (rect.localPosition.x < endPos.x)
        {
            Vector3 targetPos = rect.localPosition;
            targetPos.x += (Time.deltaTime * transitionSpeed);
            rect.localPosition = targetPos;
        }

        if (fadeColor.a < 1)
        {
            fadeColor.a += (Time.deltaTime * 0.25f);

            textComp.color = fadeColor;
        }
    }
}