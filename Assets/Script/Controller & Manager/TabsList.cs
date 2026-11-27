using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PairButtonPage
{
    public Button Button;
    public GameObject Page;
}

namespace AYellowpaper.SerializedCollections
{
    public class TabsList : MonoBehaviour
    {
        //[SerializedDictionary("NameTab", "Button")]
        //public SerializedDictionary<string, Button>  = new();
        [SerializeField] protected BagButton _Bag;

        [SerializedDictionary("NameTab")]
        public SerializedDictionary<string, PairButtonPage> _TabsList = new();

        protected void OnTabInteract(string TabInteracted)
        {
            if (!_TabsList.ContainsKey(TabInteracted))
            {
                Debug.Log("NotFoundKeyNameTab");
                return;
            }

            foreach (var kpv in _TabsList)
            {
                if (!kpv.Value.Button.transform.parent.gameObject.activeInHierarchy) return;

                if (kpv.Key == TabInteracted)
                {

                    if (ColorUtility.TryParseHtmlString("#FFFFFF", out var color))
                    {
                        var c = kpv.Value.Button.colors;
                        c.normalColor = color;
                        kpv.Value.Button.colors = c;
                    }

                    if (kpv.Value.Page == null) continue;
                    kpv.Value.Page.SetActive(true);
                }
                else
                {

                    if (ColorUtility.TryParseHtmlString("#BEBEBE", out var color))
                    {
                        var c = kpv.Value.Button.colors;
                        c.normalColor = color;
                        kpv.Value.Button.colors = c;
                    }
                    if (kpv.Value.Page == null) continue;
                    if (kpv.Value.Page.activeInHierarchy == true) kpv.Value.Page.SetActive(false);
                }

                if (TabInteracted == "Sell")
                {
                    _Bag.OpenBag();
                }
                else
                {
                    _Bag.CloseBag();
                }
            }
            /* Brightness up tab that clicked, enable tab page (if already unactive in hierachy)
             * Brightdown others, disable others tab page (if already acitve in hierachy)
             */
        }
        public void BuyTab()
        {
            OnTabInteract("Buy");
        }
        public void SellTab()
        {
            OnTabInteract("Sell");

        }
        public void Test()
        {
            OnTabInteract("Test");
        }
        private void OnEnable()
        {
            BuyTab();
        }

        private void OnDisable()
        {
            _Bag.CloseBag();
        }
    }
}
