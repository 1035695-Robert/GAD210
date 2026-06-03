using UnityEngine;

public class ToggleObject : MonoBehaviour
{

    public GameObject KeyObject;
    private void OnEnable()
    {
        Event.toggleOn += ToggleOn;
        Event.toggleOff += ToggleOff;
    }
    private void OnDisable()
    {
        Event.toggleOn -= ToggleOn;
        Event.toggleOff -= ToggleOff;
    }

    void ToggleOn()
    {
        //randomPosition
        KeyObject.SetActive(true);
    }
    void ToggleOff()
    {
        KeyObject.SetActive(false);
    }
}
