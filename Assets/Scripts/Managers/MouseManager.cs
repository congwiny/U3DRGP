using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using System;

public class MouseManager : MonoBehaviour
{
    public event Action<Vector3> onMouseClicked;
    //点击敌人
    public event Action<GameObject> OnEnemyClicked;
    RaycastHit hitInfo;
    public static MouseManager Instance;

    public Texture2D point, doorway, attack, target, arrow;

    void Awake()
    {
        if (Instance != null) 
        {
            Destroy(gameObject);
        }
        Instance = this;
    }

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
            //切换鼠标贴图
            switch (hitInfo.collider.gameObject.tag)
            {
                case "Ground":
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
                case "Enemy":
                    Cursor.SetCursor(attack, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
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
            //判断点击了敌人
            if (hitInfo.collider.gameObject.CompareTag("Enemy"))
            {
                //把点击的敌人的GameObject传递出去
                OnEnemyClicked?.Invoke(hitInfo.collider.gameObject);
            }
        }
    }
}
