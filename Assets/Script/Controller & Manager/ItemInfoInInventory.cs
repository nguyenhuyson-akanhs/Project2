using AYellowpaper.SerializedCollections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoInInventory : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _Text;
    [HideInInspector] public Image _Icon {  get; private set; }
    [HideInInspector] public ItemType _Type {  get; private set; }
    [HideInInspector] public int _IndexInPool { get; private set; }
    [HideInInspector] public string _ItemNameID { get; private set; }

    protected virtual void Awake()
    {
        _Text = GetComponentInChildren<TextMeshProUGUI>();
        _Icon = GetComponentInChildren<Image>();
    }

    protected virtual void Start()
    {
        _IndexInPool = 0;
        _ItemNameID = string.Empty;
    }
    public virtual void ReadItemInstance(in ItemInstance Item)
    {
        _Text.text = $"{Item._NameID} stack: {Item._CurrentQuantity}";
        _IndexInPool = Item._IndexInPool;
        _ItemNameID = Item._NameID;
        _Icon = ItemManager.Instance._ItemList[_ItemNameID]._Icon;
        _Type = ItemManager.Instance._ItemList[_ItemNameID]._Type;
    }
}
