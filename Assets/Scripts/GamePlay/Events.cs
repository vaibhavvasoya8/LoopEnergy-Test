using System;

public static class Events 
{
    public static event Action OnUpdateBulbPower;

    public static void UpdateBulbPower()
    {
        OnUpdateBulbPower?.Invoke();
    }
}
