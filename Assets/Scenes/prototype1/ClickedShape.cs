using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class ClickedShape : MonoBehaviour, IPointerClickHandler
{
   
     
    QuickTimeEvent textSelection;

    private void OnEnable()
    {
        textSelection = FindAnyObjectByType<QuickTimeEvent>();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        textSelection.Clicked(this.name);
    }
}
