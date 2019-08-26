using System.Collections.Generic;

public class PlayerListener
{
    public List<Player> players = new List<Player>();

    public static PlayerListener instance;

    public void Register(Player player)
    {
        players.Add(player);
    }

    public Player FindPlayer(int id)
    {
        foreach (var player in players)
        {
                return player;
        }
        
        return null;
    }
    
    public static PlayerListener GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerListener();
        }

        return instance;
    }
}
