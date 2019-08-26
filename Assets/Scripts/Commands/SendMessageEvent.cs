using UnityEngine;

public class SendMessageEvent : MonoBehaviour, ICommand
{
    private string message;
    private Player player;
    
    public SendMessageEvent(string message, Player player)
    {
        this.message = message;
        this.player = player;
    }
    
    public void Execute()
    {
        Debug.Log($"{player.name}: {message}");
    }
}
