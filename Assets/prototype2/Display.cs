
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class Display : MonoBehaviour
{
    public LayerMask displayUI;
    public List<GameObject> DisplayArray;
    public InputAction displayToggle;
    

    public void Awake()
    {
        DisplayArray.AddRange(GameObject.FindGameObjectsWithTag("Draggable"));
        DisplayArray.AddRange(GameObject.FindGameObjectsWithTag("Display"));
            
        
    }
 
    public void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("toggle on/off");
            foreach (GameObject ui in DisplayArray)
            {
                ui.SetActive(!ui.activeSelf);
            }
        }
    }
}
