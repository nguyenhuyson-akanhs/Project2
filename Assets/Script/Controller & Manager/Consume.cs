using UnityEngine;

public class Consume : InteractItem
{
    public override void OnInteract()
    {
        if (Conditions()) return;
        if (_Info._Type == ItemType.Consumable)
        {
            base.OnInteract();
        }
        else Debug.Log("Wrong ItemType or script");
    }
    protected override void ConsumeItem()
    {
        base.ConsumeItem();
        ItemInstance item = InventoryManager.Instance._ID_Item[_Info._ItemNameID];
        if (item._CurrentQuantity < TotalValue.instance.ReturnTotalValue())
        {
            Debug.Log("Nah");
            return;
        }
        item._CurrentQuantity = TotalValue.instance.ReturnTotalValue();
        InventoryManager.Instance.RemoveItem(item);
    }

    protected override string PolarType() => "Consume";

    protected virtual bool Conditions()
    {
        return MerchantManager.Instance.gameObject.activeInHierarchy;
    }
    /* Truy xuất vị trí của _PolarQuestion
     * ClickYes --event--> Consume call RemoveItem , hủy toàn bộ event từ bên text
     */
}
