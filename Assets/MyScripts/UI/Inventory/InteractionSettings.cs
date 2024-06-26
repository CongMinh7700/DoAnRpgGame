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
    public ItemCollectorMode itemCollectorMode;//Không dung
    public Color highlightColor;//Không dung
    public float itemDropHeightOffset = 0.3f;//Không dung
    public float interactionRange = 5f;//Không dung
    public bool autoCloseExternalContainer = true;//Không dung
    public bool forceAddRequiredComponents = true;//Không dung

    public GameObject optionsMenuButtonPrefabs;
    public SlotOptions[] internalSlotOptions;
    public SlotOptions[] externalSlotOptions;//Không dung(du dinh dung cho rương do ben ngoài)

    public KeyCode transferSingleItem = KeyCode.Mouse1;//Không dung(chuyen ruong đo)
    public KeyCode transferHalfStack = KeyCode.LeftShift;//Không dung

    public bool drawRangeIndicators = false;//Không dung



}
[Serializable]
public enum ItemCollectorMode//Không dung
{
    Static = 0,
    PhysicsBody = 1
}