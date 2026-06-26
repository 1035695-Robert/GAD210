using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public Prototype2 Controls;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Controls = new Prototype2();
            Controls.Enable();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

