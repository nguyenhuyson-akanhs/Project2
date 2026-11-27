using UnityEngine;

namespace AYellowpaper.SerializedCollections
{
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance;

        [SerializedDictionary("ID", "ItemSO")]
        public SerializedDictionary<string, ItemSO> _ItemList;

        protected virtual void Awake()
        {
            Instance = this;
        }
    }
}

