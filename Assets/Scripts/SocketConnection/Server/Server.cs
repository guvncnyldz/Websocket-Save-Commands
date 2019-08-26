using UnityEngine;

public class Server : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
            Invoker.GetInstance().ExecuteAll();
    }
}