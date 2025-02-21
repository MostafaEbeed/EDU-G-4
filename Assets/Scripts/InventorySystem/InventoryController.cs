using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;

    [SerializeField] private InventorySO inventoryData;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    private bool isInventoryVisible;

    private void OnEnable()
    {
        EventsManager.OnOpenCloseInventory += OpenCloseInventory;
    }

    private void OnDisable()
    {
        EventsManager.OnOpenCloseInventory -= OpenCloseInventory;
    }

    private void Start()
    {
        PrepareUI();
        PrepareInventoryData();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            if(inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }

    private void PrepareInventoryData()
    {
        inventoryData.Initialize();
        inventoryData.OnInventoryUpdated += UpdateInventoryUI;

        foreach (InventoryItem item in initialItems)
        {
            if (item.IsEmpty)
                continue;
            inventoryData.AddItem(item);
        }
    }

    private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
    {
        inventoryUI.ResetAllItems();
        foreach (var item in inventoryState)
        {
            inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
        }
    }

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);

        this.inventoryUI.OnDescreptionRequested += HandleDescreptionRequested;
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequested;
    }

    private void HandleItemActionRequested(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        //inventoryUI.ResetSelection();

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            inventoryUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            inventoryUI.ShowItemAction(itemIndex);
        }


        /*IDestroyableInventoryItem destroyableInventoryItem = inventoryItem.item as IDestroyableInventoryItem;
         if (destroyableInventoryItem != null)
         {
             inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
         } */
    }

    private void DropItem(int itemIndex, int quantity)
    {
        inventoryData.RemoveItem(itemIndex, quantity);
        inventoryUI.ResetSelection();
    }

    public void PerformAction(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;


        IDestroyableInventoryItem destroyableInventoryItem = inventoryItem.item as IDestroyableInventoryItem;
        if (destroyableInventoryItem != null)
        {
            inventoryData.RemoveItem(itemIndex, 1);
        }

        IItemAction itemAction = inventoryItem.item as IItemAction;
        if (itemAction != null)
        {
            itemAction.PerformAction(gameObject, inventoryItem.itemState);



            if (inventoryData.GetItemAt(itemIndex).IsEmpty)
                inventoryUI.ResetSelection();
        }



    }

    private void HandleDragging(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
            return;

        inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
        inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    private void HandleDescreptionRequested(int itemIndex)
    {
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if (inventoryItem.IsEmpty)
        {
            inventoryUI.ResetSelection();
            return;
        }
        ItemSO item = inventoryItem.item;
        inventoryUI.UpdateDescription(itemIndex, item.ItemImage, item.name, item.Description);
    }

    private string PrepareDescription(InventoryItem inventoryItem)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append(inventoryItem.item.Description);
        sb.AppendLine();
        for (int i = 0; i < inventoryItem.itemState.Count; i++)
        {
            sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} " +
                $":{inventoryItem.itemState[i].value / inventoryItem.item.DefaultParametersList[i].value}");
            sb.AppendLine();
        }
        return sb.ToString();
    }

    public void OpenCloseInventory()
    {
        // Toggle the visibility state
        isInventoryVisible = !isInventoryVisible;

        if (isInventoryVisible)
        {
            inventoryUI.Show();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }
        else
        {
            inventoryUI.Hide();
        }
    }
}