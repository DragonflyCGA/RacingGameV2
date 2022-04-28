using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TouchButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed;
    Image touchImage;
    float color = 1f;

    void Start()
    {
        touchImage = GetComponent<Image>();
        touchImage.color = new Color(color, color, color, 0.2f);
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        touchImage.color = new Color(color, color, color, 0.6f);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        touchImage.color = new Color(color, color, color, 0.2f);
    }
}