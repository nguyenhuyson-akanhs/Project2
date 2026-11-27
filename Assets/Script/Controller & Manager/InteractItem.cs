using UnityEngine;

public class InteractItem : MonoBehaviour
{
    protected ItemInfoInInventory _Info;

    protected virtual void Awake()
    {
        _Info = GetComponentInParent<ItemInfoInInventory>();
    }

    /* giải thích tí: (mọi)hàm oninteract tự viết(lq đến cái này) 
     * "sẽ chỉ đăng kí event cho chính nó"
     *  và hủy ngay sau khi hoàn tất nhiệm vụ
     * nên sẽ không phải sợ cái 1 gọi thằng cả làng nghe -_-
     */
    public virtual void OnInteract() // con ko cần thì override r để ko đấy
    {
        // example
        GameObject polarQuestion = PolarText.instance.transform.parent.gameObject;
        polarQuestion.SetActive(true);
        TotalValue.instance.transform.parent.parent.gameObject.SetActive(true);

        PolarText.instance._RemoveItem += ConsumeItem;
        PolarText.instance._RemoveItem += BuyItem;

        SetText();

    }
    protected virtual void SetText()
    {
        PolarText.instance.SetText(PolarType(), _Info._ItemNameID);
    }
    protected virtual void ConsumeItem()
    {
        PolarText.instance._RemoveItem -= ConsumeItem;
    }
    protected virtual void BuyItem()
    {
        PolarText.instance._RemoveItem -= ConsumeItem;
    }
    protected virtual string PolarType()
    {
        return "";
    }
}
