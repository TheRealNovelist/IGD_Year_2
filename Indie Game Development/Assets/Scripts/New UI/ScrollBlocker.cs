using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBlocker : MonoBehaviour
{
    public ScrollRect scrollRect;
    public GameObject inputBlocker;

    private void Update()
    {
        inputBlocker.SetActive(Mathf.Abs(scrollRect.velocity.y) > 40f);
    }
}
