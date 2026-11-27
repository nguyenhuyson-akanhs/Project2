using UnityEngine;
public struct ItemInstance
{
    public string _NameID;
    public int _IndexInPool;
    public int _CurrentQuantity;
    public ItemInstance(ItemSO item)
    {
        _NameID = item._NameID;
        _IndexInPool = 0;
        _CurrentQuantity = 0;
    }
    public ItemInstance(ItemSO item,int Quanity)
    {
        _NameID = item._NameID;
        _IndexInPool = 0;
        _CurrentQuantity = Quanity;
    }

    public ItemInstance(string NameID)
    {
        _NameID = NameID;
        _IndexInPool = 0;
        _CurrentQuantity = 0;
    }
}
