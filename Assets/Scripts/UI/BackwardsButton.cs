using UnityEngine;
using UnityEngine.EventSystems;

public class BackwardsButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    TouchButtons androidButtonMenu;
    void Start()
    {
        androidButtonMenu = GetComponentInParent<TouchButtons>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        androidButtonMenu.MovementButton(-1);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        androidButtonMenu.MovementButton(0);
    }
}