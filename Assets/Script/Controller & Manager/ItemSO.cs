using UnityEngine;
using UnityEngine.UI;

public enum ItemType 
{
    None,
    Consumable,
    Weapon,

}

[CreateAssetMenu(fileName = "ItemSO",menuName ="ScriptableObjects/ItemSO")]
public class ItemSO : ScriptableObject
{
    [Header("Static Info")]
    [SerializeField] private string __NameID;
    public string _NameID => __NameID;

    [SerializeField] private ItemType __Type;
    public ItemType _Type => __Type;

    [Header("Icon")]
    [SerializeField] private Image __Icon = null;
    public Image _Icon => __Icon;

    [Header("Model")]
    [SerializeField] private GameObject __Prefab = null;
    public GameObject _Prefab => __Prefab;
    
}
