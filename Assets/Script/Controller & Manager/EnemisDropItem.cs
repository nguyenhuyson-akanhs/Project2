using System.Linq;
using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class EnemisDropItem : MonoBehaviour
    {
        [Header("Asset")]
        [SerializedDictionary("ItemSO","Quantity")]
        [SerializeField] protected SerializedDictionary<ItemSO,int> _ListDropAble;

        public virtual void Drop()
        {
            var item = _ListDropAble.ElementAt(UnityEngine.Random.Range(0, _ListDropAble.Count));
            ItemInstance instance = new(item.Key);
            instance._CurrentQuantity = item.Value;

            InventoryManager.Instance.AddItem(instance);
        }
    }
}
