using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Xử lý nhặt đồ
public class Interactor : RPGMonoBehaviour
{
    [SerializeField] private Camera mainCamera;

    public ItemContainer inventory;

    private InteractionTarget interactionTarget;

    public Vector3 ItemDropPosition { get { return transform.position + transform.forward; } }

    private void Update()
    {
        HandleInteractions();
    }

    private void OnDrawGizmos()
    {
        if (InteractionSettings.Current.drawRangeIndicators)
        {
            Gizmos.DrawWireSphere(transform.position, InteractionSettings.Current.interactionRange);
        }
    }
    private void HandleInteractions()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (interactionTarget?.gameObject != null) Utils.UnhighlightObject(interactionTarget.gameObject);

        if (Physics.Raycast(ray, out hit) && InRange(hit.transform.position))
        {
            IInteractable target = hit.transform.GetComponent<IInteractable>();
            if (target != null)
            {
                interactionTarget = new InteractionTarget(target, hit.transform.gameObject);
                Utils.HighlightObject(interactionTarget.gameObject);
            }
            else interactionTarget = null;
        }
        else
        {
            interactionTarget = null;
        }
        if (Input.GetMouseButtonDown(0)) InitInteraction();

    }
    private bool InRange(Vector3 targetPostition)
    {
        return Vector3.Distance(targetPostition, transform.position) <= InteractionSettings.Current.interactionRange;
    }

    private void InitInteraction()
    {
        if (interactionTarget == null) return;
        interactionTarget.interactable.OnClickInteract(this);
    }
    public bool AddToInventory(Item item, GameObject instance)
    {
        if (inventory.AddItem(item))
        {
            if (instance) Destroy(instance);
            return true;
        }
        return false;
    }
    internal class InteractionTarget
    {
        internal IInteractable interactable;
        internal GameObject gameObject;
        public  InteractionTarget(IInteractable interactable,GameObject gameObject)
        {
            this.interactable = interactable;
            this.gameObject = gameObject;
        }
    }
}
