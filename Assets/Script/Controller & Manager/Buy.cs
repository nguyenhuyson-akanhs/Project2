using UnityEngine;

public class Buy : InteractItem
{
    protected ItemInfoInMerchant _InfoMer;
    protected override void Awake()
    {
        _InfoMer = GetComponentInParent<ItemInfoInMerchant>();
    }    
    public override void OnInteract()
    {
        base.OnInteract();
    }
    protected override void SetText()
    {
        PolarText.instance.SetText(PolarType(), _InfoMer._ItemNameID);
    }
    protected override void BuyItem()
    {
        base.BuyItem();
        if (TotalValue.instance.ReturnTotalValue() == 0) return;
        if(_InfoMer._ItemInstance._CurrentQuantity < TotalValue.instance.ReturnTotalValue())
        {
            Debug.Log("oh hell nad, u buy so much");
            return;
        }
        ItemInstance item = new(_InfoMer._Item, TotalValue.instance.ReturnTotalValue());
        _InfoMer._ItemInstance._CurrentQuantity -= item._CurrentQuantity;
        InventoryManager.Instance.AddItem(item);
        _InfoMer.ReadItemInstance(_InfoMer._ItemInstance);
    }
    protected override string PolarType() => "Buy";
}
