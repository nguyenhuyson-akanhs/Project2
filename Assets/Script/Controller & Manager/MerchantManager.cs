using System.Collections.Generic;
using UnityEngine;

public class MerchantManager : MonoBehaviour
{
    public static MerchantManager Instance;

    [Header("BuyBlock")]
    [SerializeField] protected GameObject _Container;
    private List<GameObject> _ListContainer = new();
    [SerializeField] protected GameObject _PrefabItemDisplay;
    [HideInInspector] public Dictionary<ItemSO,int> _ListItemBuyable;
    protected virtual void Awake()
    {
        Instance = this;
    }
    protected virtual void Start()
    {
        gameObject.SetActive(false);
    }
    public virtual async Awaitable RefeshBuyingList()
    {
        if(_ListItemBuyable == null)
        {
            Debug.Log("Null");
            return;
        }

        int c = 0;
        foreach (var item in _ListItemBuyable)
        {
            GameObject gobj = Instantiate(_PrefabItemDisplay, _Container.transform, false);
            _ListContainer.Add(gobj);

            ItemInfoInMerchant itemInfo = gobj.GetComponentInChildren<ItemInfoInMerchant>();
            itemInfo._Item = item.Key;
            itemInfo._Quantity = item.Value;
            itemInfo.SetUp();

            c++;
            if(c == 5) await Awaitable.NextFrameAsync();
        }
    }

    public void ClearCacheListContainer()
    {
        if (_ListContainer == null) return;
        _ListContainer.RemoveAll(go =>
        {
            Destroy(go);
            return true;
        });
    }

    public void MerchantClose()
    {
        transform.gameObject.SetActive(false);
    }
}
