using UnityEngine;

public class Shop : MonoBehaviour
{
    public void BuyHp()
    {
        if(Manager.instance.Buy(10))
        {
            Player.instanse.UpgradeHp();
        }
    }
}
