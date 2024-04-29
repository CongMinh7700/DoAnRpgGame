using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Xu ly cho click Co the bo
public class InstantHarvest : MonoBehaviour,IInteractable
{
    private bool isHarvested = false;

    //The item that will be harvested on click.
    public Item harvestItem;

    //The item is instantly added to the inventory of the interactor on interact.
    public void OnClickInteract(Interactor interactor)
    {
        //Attempt to harvest if not harvested already
        AttemptHarvest(interactor);
    }

    public void AttemptHarvest(Interactor harvestor)
    {
        if (!isHarvested)
        {
            if (harvestor.AddToInventory(harvestItem, gameObject))
            {
                isHarvested = true;
            }
        }
    }
}
