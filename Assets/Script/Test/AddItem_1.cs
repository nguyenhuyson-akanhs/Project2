using UnityEngine;

public class AddItem_1 : MonoBehaviour
{
    public ItemSO ItemSO;
    public void Interact()
    {
        InventoryManager.Instance.AddItem(new ItemInstance(ItemSO));
    }
}
