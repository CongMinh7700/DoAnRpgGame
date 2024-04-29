using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Interaction Settings", menuName = "SO/Interaction Settings")]
public class InteractionSettings : ScriptableObject
{
    private static InteractionSettings current;

    public static InteractionSettings Current
    {
        get
        {
            if(current == null)
            {
                InteractionSettings settings = Utils.FindInteractionSettings();
                if (settings == null)
                {
                    Debug.LogError("No interaction settings were found. Please create one.");
                }
                else current = settings;
            }
            return current;
        }
    }
    public ItemCollectorMode itemCollectorMode;
    public Color highlightColor;
    public float itemDropHeightOffset = 0.3f;
    public float interactionRange = 5f;
    public bool autoCloseExternalontainer = true;
    public bool forceAddRequiredComponents = true;

    public GameObject optionsMenuButtonPrefabs;
    public SlotOptions[] internalSlotOptions;
    public SlotOptions[] externalSlotOptions;

    public KeyCode transferSingleItem = KeyCode.Mouse1;
    public KeyCode transferHalfStack = KeyCode.LeftShift;

    public bool drawRangeIndicators = false;


   
}
[Serializable]
public enum ItemCollectorMode
{
    Static = 0,
    PhysicsBody = 1
}