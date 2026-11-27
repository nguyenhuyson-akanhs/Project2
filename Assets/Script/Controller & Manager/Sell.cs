using UnityEngine;

public class Sell : Consume
{
    protected override void ConsumeItem()
    {
        base.ConsumeItem();
        Debug.Log("Sold !");
    }
    protected override string PolarType()
    {
        return "Sell";
    }
    protected override bool Conditions()
    {
        return !base.Conditions();
    }
}
