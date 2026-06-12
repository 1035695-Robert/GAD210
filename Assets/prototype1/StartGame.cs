
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public GameObject UI;
    public delegate void StartGameDelegate();
    public static StartGameDelegate startGame;
    public void Game()
    {
     UI.SetActive(false);
        startGame.Invoke();
        
    }
}

