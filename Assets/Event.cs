using UnityEngine;

public class Event
{
    public delegate void ToggleObject();
    public static ToggleObject toggleOn; 
    public static ToggleObject toggleOff;
}