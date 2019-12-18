using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeColorOnHover : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    
    private void OnMouseOver()
    {
        Debug.Log("On mouseover");
        _textMeshPro.color = new Color(1f, 0.64f, 0f);
    }

    private void OnMouseExit()
    {
        Debug.Log("On mouse exit");
        _textMeshPro.color = new Color(1,1,1);
    }
}
