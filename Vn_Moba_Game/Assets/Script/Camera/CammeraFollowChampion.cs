using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CammeraFollowChampion : MonoBehaviour
{
    [FormerlySerializedAs("_champion")] public GameObject champion;
    private Vector3 _offset;
    
    // Move cam by pointer
    public float scrollSpeed;
    public float borderThickness;

    private void Start()
    {
        _offset = transform.position - champion.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            MoveCamToChampion();
        }
        var transformPosition = transform.position;
        if (Input.GetKey(KeyCode.UpArrow) || Input.mousePosition.y >= Screen.height - borderThickness)
        {
            transformPosition.z += Time.deltaTime * scrollSpeed;
            Debug.Log("Di len");
        }
        
        if ( Input.GetKey(KeyCode.DownArrow) || Input.mousePosition.y <= borderThickness)
        {
            transformPosition.z -= Time.deltaTime * scrollSpeed;
        }

        if ( Input.GetKey(KeyCode.RightArrow) || Input.mousePosition.x >= Screen.width - borderThickness)
        {
            transformPosition.x += Time.deltaTime * scrollSpeed;
        }
        
        if ( Input.GetKey(KeyCode.LeftArrow) || Input.mousePosition.x <= borderThickness)
        {
            transformPosition.x -= Time.deltaTime * scrollSpeed;
        }
        transform.position = transformPosition;
    }

    void MoveCamToChampion()
    {
        transform.position = champion.transform.position + _offset;
        Debug.Log("cam follow player");
    }
}