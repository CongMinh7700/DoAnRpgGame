using System.Collections;
using System.IO;
using UnityEditor;
using UnityEngine;


public readonly struct Utils 
{


    public static void TransferItem(ItemSlot trigger,ItemSlot target)
    {
        if (trigger == target) return;

        Item triggerItem = trigger.slotItem;
        Item targetItem = target.slotItem;

        int triggerItemCount = trigger.itemCount;

        if (!trigger.isEmpty)
        {
            if(target.isEmpty || targetItem == triggerItem)
            {
                for(int i =0;i< triggerItemCount; i++)
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

    public static void TransferItemQuatity(ItemSlot trigger,ItemSlot target,int amount)
    {
        if (!trigger.isEmpty)
        {
            for(int i = 0; i < amount; i++)
            {
                if (!trigger.isEmpty)
                {
                    if (target.Add(trigger.slotItem)) trigger.Remove(1);
                    else return;
                }
                else return;
            }
        }
    }
    public static void TransferItemQuatity(ItemSlot trigger, ItemContainer targetContainer, int amount)
    {
        if (!trigger.isEmpty)
        {
            for (int i = 0; i < amount; i++)
            {
                if (!trigger.isEmpty)
                {
                    if (targetContainer.AddItem(trigger.slotItem)) trigger.Remove(1);
                    else return;
                }
                else return;
            }
        }
    }

    public static IEnumerator TweenScaleIn(GameObject obj,float durationInFrames,Vector3 maxScale)
    {
        Transform transform = obj.transform;
        transform.localScale = Vector3.zero;
        transform.gameObject.SetActive(true);

        float frame = 0;
        while(frame < durationInFrames)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, maxScale, frame / durationInFrames);
            frame++;
            yield return null;
        }
    }

    public static IEnumerator TweenScaleOut(GameObject obj, float durationInFrames, bool destroy)
    {
        float frame = 0;
        while(frame < durationInFrames)
        {
            if(obj != null)
            {
                obj.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, frame / durationInFrames);
            }
            frame ++;
            yield return null;
        }
        if (obj)
        {
            if (!destroy) obj.SetActive(false);
            else GameObject.Destroy(obj);
        }
    }

    public static void InstantiateItemCollector(Item item,Vector3 position)
    {
        position.y += InteractionSettings.Current.itemDropHeightOffset;

        if (InteractionSettings.Current.itemCollectorMode == ItemCollectorMode.Static)
        {
            Vector3 targetSize = Vector3.one * 0.5f;
            GameObject instance = GameObject.Instantiate(item.prefab, position, Quaternion.identity);
            float maxSizeComponent = MaxVec3Component(instance.GetComponent<MeshRenderer>().bounds.size);

            instance.transform.localScale = instance.transform.localScale * (MaxVec3Component(targetSize) / maxSizeComponent);

            var interacable = instance.GetComponent<IInteractable>();

            if (interacable != null) GameObject.Destroy((Object)interacable);

            if (instance.TryGetComponent(out Collider col)) col.isTrigger = true;
            else
            {
                if (InteractionSettings.Current.forceAddRequiredComponents)
                {
                    MeshCollider _col = instance.AddComponent<MeshCollider>();
                    _col.convex = true;
                    _col.isTrigger = true;
                }
                else Debug.LogError($"[{item.name}] Item prefab does not have a Collider component .\nAll item prefabs must have a collider");

            }
            instance.AddComponent<ItemCollector>().Create(item);
        } else if (InteractionSettings.Current.itemCollectorMode == ItemCollectorMode.PhysicsBody)
        {
            GameObject instance = GameObject.Instantiate(item.prefab, position, Quaternion.identity);


            if (instance.TryGetComponent(out Collider col)) col.isTrigger = false;
            else
            {
                if (InteractionSettings.Current.forceAddRequiredComponents)
                {
                    MeshCollider _col = instance.AddComponent<MeshCollider>();
                    _col.convex = true;
                    _col.isTrigger = false;

                }
                else Debug.LogError($"[{item.name}] Item prefab does not have a Collider component .\nAll item prefabs must have a collider");
            }
            if (instance.TryGetComponent(out Rigidbody rb)) rb.useGravity = true;
            else
            {
                if (InteractionSettings.Current.forceAddRequiredComponents)
                {
                    Rigidbody _rb = instance.AddComponent<Rigidbody>();
                    _rb.useGravity = true;
                }
                else Debug.LogError($"[{item.name}] Item prefab does not have a Rigidbody component.\nPlease set collector type to static or add the required components.");
            }
        }
    }
    public static float MaxVec3Component(Vector3 vec)
    {
        return Mathf.Max(Mathf.Max(vec.x, vec.y), vec.z);
    }
    public static void HighlightObject(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material.color = InteractionSettings.Current.highlightColor;
    }
    public static void UnHighlightObject(GameObject obj,Color original)
    {
        obj.GetComponent<MeshRenderer>().material.color = original;
    }

    public static void UnHighlightObject(GameObject obj)
    {
        UnHighlightObject(obj, Color.white);
    }

    public static InteractionSettings FindInteractorSettings()
    {
        InteractionSettings settings = null;
        if(Application.isPlaying == false)
        {
#if UNITY_EDITOR
            var settingAssets = AssetDatabase.FindAssets($"t:{nameof(InteractionSettings)}");
            if(settingAssets.Length > 0)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(settingAssets[0]);
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
            if(settings == null)
            {
                Debug.LogError("No 'InteractionSettings' found. Must assign a 'InteractionSettings' to 'ItemManager'.");
            }
        }
        return settings;
    }
}
