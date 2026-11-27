using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class MerchantInteract : MonoBehaviour
    {
        [SerializedDictionary("Item", "Quantity")]
        [SerializeField] protected SerializedDictionary<ItemSO, int> _ListItemBuyable;
        public void OnInteract()
        {
            if (MerchantManager.Instance.gameObject.activeInHierarchy)
            {
                return;
            }

            _ = OpenMerchant();
        }

        protected async Awaitable OpenMerchant()
        {
            Debug.Log("1");
            if (MerchantManager.Instance._ListItemBuyable != _ListItemBuyable)
            {
                Debug.Log("2");
                MerchantManager.Instance.ClearCacheListContainer();
                Debug.Log("3");
                MerchantManager.Instance._ListItemBuyable = _ListItemBuyable;
                Debug.Log("4");
                await MerchantManager.Instance.RefeshBuyingList();
                Debug.Log("5");
                await Awaitable.NextFrameAsync();
            }
            MerchantManager.Instance.gameObject.SetActive(true);
        }

        protected void OnDestroy()
        {
            if (MerchantManager.Instance._ListItemBuyable == _ListItemBuyable)
            {
                MerchantManager.Instance._ListItemBuyable = null;
                MerchantManager.Instance.ClearCacheListContainer();
            }
        }
    }
}
