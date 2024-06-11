using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverUI : MonoBehaviour
{
    public static bool IsPointerOverUIElement()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}
