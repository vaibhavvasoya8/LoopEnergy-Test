using System;
using GamePlay;
public static class Events 
{
    //This is event is use for update the bulb conection.
    public static event Action OnUpdateBulbPower;
    //This is event is use for update the wifi conection.
    public static event Action<Piece> OnWifiConectionUpdate;
    
    /// <summary>
    /// Fire the event For all Bulb pieces(End Pieces).
    /// </summary>
    public static void UpdateBulbPower()
    {
        OnUpdateBulbPower?.Invoke();
    }

    /// <summary>
    /// Fire the event for all wifi pieces.
    /// </summary>
    /// <param name="source">Pass the reference which WiFi piece is calls this method</param>
    public static void WifiConectionUpdate(Piece source)
    {
        OnWifiConectionUpdate?.Invoke(source);
    }
}
