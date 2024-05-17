using System;

public class Economy
{
    private static int _coins;
    private static int _murders;
    private static int _spentCoins;
    
    public static event Action<int> OnCoinsChanged;

    public static int Coins
    {
        get => _coins;
        set
        {
            _coins = value;
            OnCoinsChanged?.Invoke(_coins);
        }
    }

    public static int Murders
    {
        get => _murders;
        set => _murders = value;
    }

    public static int SpentCoins
    {
        get => _spentCoins;
        set => _spentCoins = value;
    }
}
