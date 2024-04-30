using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;


public readonly struct Utils
{
    public static void TransferItem(ItemSlot trigger, ItemSlot target)
    {
        if (trigger == target) return;

        Item triggerItem = trigger.slotItem;
        Item targetItem = target.slotItem;

        int triggerItemCount = trigger.itemCount;

        if (!trigger.IsEmpty)
        {
            if (target.IsEmpty || targetItem == triggerItem)
            {
                for (int i = 0; i < triggerItemCount; i++)
                {
                    if (target.Add(triggerItem)) trigger.Remove(1);
                    else return;
                }
            }
            else
            {
                int targetItemCount = target.itemCount;

                target.Clear();
                for (int i = 0; i < triggerItemCount; i++) target.Add(triggerItem);

                trigger.Clear();
                for (int i = 0; i < targetItemCount; i++) trigger.Add(targetItem);
            }
        }
    }
    public static void TransferItemQuantity(ItemSlot trigger, ItemSlot target, int amount)
    {
        if (!trigger.IsEmpty)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!trigger.IsEmpty)
                {
                    if (target.Add(trigger.slotItem)) trigger.Remove(1);
                    else return;
                }
                else return;
            }
        }
    }

    public static void TransferItemQuantity(ItemSlot trigger, ItemContainer targetContainer, int amount)
    {
        if (!trigger.IsEmpty)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!trigger.IsEmpty)
                {
                    if (targetContainer.inventoryEvents.AddItem(trigger.slotItem)) trigger.Remove(1);
                    else return;
                }
                else return;
            }
        }
    }

    public static IEnumerator TweenScaleIn(GameObject obj, float durationInFrames, Vector3 maxScale)
    {
        Transform tf = obj.transform;
        tf.localScale = Vector3.zero;
        tf.gameObject.SetActive(true);

        float frame = 0;
        while (frame < durationInFrames)
        {
            tf.localScale = Vector3.Lerp(Vector3.zero, maxScale, frame / durationInFrames);
            frame++;
            yield return null;
        }
    }

    public static IEnumerator TweenScaleOut(GameObject obj, float durationInFrames, bool destroy)
    {
        float frame = 0;
        while (frame < durationInFrames)
        {
            if (obj != null)
            {
                obj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, frame / durationInFrames);
            }
            frame++;
            yield return null;
        }
        if (obj)
        {
            if (!destroy) obj.SetActive(false);
            else GameObject.Destroy(obj);
        }
    }  
    public static InteractionSettings FindInteractionSettings()
    {
        InteractionSettings settings = null;

        if (Application.isPlaying == false)
        {
#if UNITY_EDITOR
            var settingsAssets = AssetDatabase.FindAssets($"t:{nameof(InteractionSettings)}");
            if (settingsAssets?.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(settingsAssets[0]);
                settings = AssetDatabase.LoadAssetAtPath(assetPath, typeof(InteractionSettings)) as InteractionSettings;
            }
#endif
        }
        else
        {
            if (ItemManager.Instance == null)
            {
                settings = GameObject.FindObjectOfType<ItemManager>(true)?.interactionSettings;
            }
            else settings = ItemManager.Instance.interactionSettings;

            if (settings == null)
            {
                Debug.LogError("No 'InteractionSettings' found. Must assign a 'InteractionSettings' to 'ItemManager'.");
            }
        }
        return settings;
    }

}