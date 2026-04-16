using UnityEngine;
using UnityEngine.EventSystems;

public class ForwardButton : MonoBehaviour, IPointerClickHandler, IPointerUpHandler
{
    TouchButtons androidButtonMenu;
    void Start()
    {
        androidButtonMenu = GetComponentInParent<TouchButtons>();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicking Down On forward Button");
        androidButtonMenu.MovementButton(1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("Button Released");
        androidButtonMenu.MovementButton(0);
    }
}