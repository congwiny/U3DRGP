using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3> 
{ 
}

public class MouseManager : MonoBehaviour
{
    public EventVector3 onMouseClicked;
    RaycastHit hitInfo;

    void Update()
    {
        SetCursorTexture();
        MouseControl();
    }

    void SetCursorTexture()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hitInfo)) 
        {
             //«–ªª Û±ÍÃ˘Õº
        }
    }

    void MouseControl()
    {
        if (Input.GetMouseButtonDown(0) && hitInfo.collider != null)
        {
            if (hitInfo.collider.gameObject.CompareTag("Ground")) 
            {
                onMouseClicked?.Invoke(hitInfo.point);
            }
        }
    }
}
