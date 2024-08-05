using System;
using GamePlay;
public static class Events 
{
    public static event Action OnUpdateBulbPower;
    public static event Action<Piece> OnWifiConectionUpdate;
    public static void UpdateBulbPower()
    {
        OnUpdateBulbPower?.Invoke();
    }

    public static void WifiConectionUpdate(Piece source)
    {
        OnWifiConectionUpdate?.Invoke(source);
    }
}
