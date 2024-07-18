using System.Collections;
using System.Collections.Generic;

public class PlayerInfo : IService
{
    public Wallet Wallet { get; }
    public Reputation playerRep;
    public Reputation restaurantRep;
    public Difficulty Difficulty { get; }
    public PlayerInfo(Wallet wallet, Reputation playerRep, Reputation restaurantRep, Difficulty difficulty)
    {
        Wallet = wallet;
        this.playerRep = playerRep;
        this.restaurantRep = restaurantRep;
        Difficulty = difficulty;
    }
}
