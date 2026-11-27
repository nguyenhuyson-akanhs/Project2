using UnityEngine;

public class ItemInfoInMerchant : ItemInfoInInventory
{
    [Header("SomeInfo")]
    public ItemSO _Item;
    public int _Quantity;  //(Store)   // Backing Field: what serializeField but smt like property 
    [HideInInspector] public ItemInstance _ItemInstance; //{ get; protected set; } property trả về ref bruh, ko cho serializeField

    protected override void Start()
    {
        base.Start();
        SetUp();
    }
    public virtual void SetUp()
    {
        _ItemInstance = new(_Item, _Quantity);
        ReadItemInstance(_ItemInstance);
    }
}
