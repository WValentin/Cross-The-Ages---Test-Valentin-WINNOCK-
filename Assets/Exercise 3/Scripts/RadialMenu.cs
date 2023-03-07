using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenu : MonoBehaviour
{
    [SerializeField]
    private RectTransform radialMenu;

    [SerializeField]
    private RectTransform cursor;

    private Vector3 cursorPos;


    void Update()
    {
        float radius = (radialMenu.sizeDelta.x / 2) - (cursor.sizeDelta.x / 2);
        Vector3 position = Input.mousePosition - radialMenu.position;
        float distance = Mathf.Clamp(position.magnitude, 0f, radius);
        cursor.position = position.normalized * distance + radialMenu.position;
    }
}
