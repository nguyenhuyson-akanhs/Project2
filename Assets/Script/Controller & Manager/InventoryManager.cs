using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    [SerializeField] protected GridLayoutGroup _GridGroup;
    [SerializeField] protected AssetReferenceGameObject _SlotPrefabAsset;
    protected AsyncOperationHandle<GameObject> _HandleLinkReference;
    protected GameObject _GameObjectHandle;
    [SerializeField] protected GameObject _SlotsContainer;
    protected List<GameObject> _ListSlot = new();
    public SortedDictionary<string, ItemInstance> _ID_Item { get; private set; } = new();
    protected Queue<ItemInstance> _ListItemQueue = new();
    protected Queue<ItemInstance> _ListRemoveItemQueue = new();

    protected virtual async Awaitable Start()
    {
        Instance = this;

        _IsLoading = true;

        _HandleLinkReference = Addressables.LoadAssetAsync<GameObject>(_SlotPrefabAsset);
        _GameObjectHandle = await _HandleLinkReference.Task;

        _IsLoading = false;

        _GridGroup.enabled = true;
        await Awaitable.EndOfFrameAsync();
        _GridGroup.enabled = false;
    }
    public virtual void AddItem(ItemInstance Item)
    {
        _ListItemQueue.Enqueue(Item);

        if (!_Executing)
        {
            _Executing = true;
            _ = QueueCheck();
        }
    }

    public virtual void RemoveItem(ItemInstance Item)
    {

        _ListRemoveItemQueue.Enqueue(Item);

        if (!_Executing)
        {
            _Executing = true;
            _ = QueueCheck();
        }
    }

    protected virtual async Awaitable QueueCheck()
    {
        while (_ListItemQueue.Count != 0 || _ListRemoveItemQueue.Count != 0)
        {
            await AddItemFromQueue();
            await RemoveItemFromQueue();
        }
        _Executing = false;
    }

    protected bool _Executing = false;
    protected virtual async Awaitable AddItemFromQueue()
    {
        while (_ListItemQueue.Count != 0)
        {
            ItemInstance Item = _ListItemQueue.Dequeue();

            if (_ID_Item.ContainsKey(Item._NameID))
            {
                Item._CurrentQuantity += _ID_Item[Item._NameID]._CurrentQuantity;
                
                _ID_Item[Item._NameID] = Item;

                int index = Item._IndexInPool;
                GameObject obj = _ListSlot[index];
                if (obj.TryGetComponent<ItemInfoInInventory>(out var itemInfo))
                {
                    itemInfo.ReadItemInstance(Item);
                }
            }

            else
            {
                _ID_Item.Add(Item._NameID, Item);
                await Awaitable.NextFrameAsync();

                await CreateNewSlot();
            }
        }
    }
    protected virtual async Awaitable RemoveItemFromQueue()
    {
        while (_ListRemoveItemQueue.Count != 0)
        {
            ItemInstance Item = _ListRemoveItemQueue.Dequeue();
            Item._CurrentQuantity = _ID_Item[Item._NameID]._CurrentQuantity - Item._CurrentQuantity;
            _ID_Item[Item._NameID] = Item;

            int index = Item._IndexInPool;
            GameObject obj = _ListSlot[index];

            if (_ID_Item[Item._NameID]._CurrentQuantity > 0)
            {
                if (obj.TryGetComponent<ItemInfoInInventory>(out var itemInfo))
                {
                    itemInfo.ReadItemInstance(Item);
                }
            }

            else
            { 
                _ListSlot.RemoveAt(index);
                _ID_Item.Remove(Item._NameID);
                Destroy(obj);
                await Awaitable.NextFrameAsync();
                _GridGroup.enabled = true;
                await Awaitable.NextFrameAsync();
                _GridGroup.enabled = false;

                UpdateUI();
            }
            await Awaitable.NextFrameAsync();
        }

    }

    protected bool _IsLoading = false;
    protected virtual async Awaitable CreateNewSlot()
    {
        while (_IsLoading) await Awaitable.NextFrameAsync();
        _GridGroup.enabled = true;

        while (_ID_Item.Count != _ListSlot.Count)
        {
            GameObject newSlot = Instantiate(_GameObjectHandle, _SlotsContainer.transform, false);
            _ListSlot.Add(newSlot);
        }

        await Awaitable.NextFrameAsync();
        _GridGroup.enabled = false;
        UpdateUI();
    }

    protected virtual void UpdateUI()
    {
        int index = 0;
        List<string> keys = new List<string>(_ID_Item.Keys);

        foreach (var key in keys)
        {
            ItemInstance i = _ID_Item[key];
            i._IndexInPool = index;
            _ID_Item[key] = i;

            GameObject obj = _ListSlot[index];
            if (obj.TryGetComponent<ItemInfoInInventory>(out var info))
            {
                if (i._NameID != info._ItemNameID)
                {

                    info.ReadItemInstance(i);
                }
            }

            index++;
        }
    }

    private void OnDestroy()
    {
        if (_HandleLinkReference.IsValid())
        {
            Addressables.Release(_HandleLinkReference);
        }
    }
}
