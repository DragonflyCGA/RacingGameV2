using UnityEngine;
using UnityEngine.EventSystems;

public class steerWheel : MonoBehaviour,IPointerDownHandler,IDragHandler,IPointerUpHandler {
    
    RectTransform rect;
    Vector2 centerPoint;

    bool wheelBeingHeld = false;
    private float wheelPrevAngle = 0f;
    private float wheelAngle = 0f;

    public float horizontal;

    public float maximumSteeringAngle = 70f;
    float wheelReleasedSpeed = 250f;

    private void Start () {
        rect = GetComponent<RectTransform>();
        getcenterPoint();    
    }

    private  void Update ()
    {

        if (!wheelBeingHeld && !Mathf.Approximately(0f, wheelAngle))
        {
            float deltaAngle = wheelReleasedSpeed * Time.deltaTime;
            if (Mathf.Abs(deltaAngle) > Mathf.Abs(wheelAngle))
                wheelAngle = 0f;
            else if (wheelAngle > 0f)
                wheelAngle -= deltaAngle;
            else
                wheelAngle += deltaAngle;
        }

        rect.localEulerAngles =new  Vector3(0,0,-1) * wheelAngle;

        horizontal = wheelAngle / maximumSteeringAngle;
    }

    private void getcenterPoint()
    {    
        Vector3[] corners = new Vector3[4];
        rect.GetWorldCorners(corners);
      
        for (int i = 0; i < 4; i++)
        {
            corners[i] = RectTransformUtility.WorldToScreenPoint(null, corners[i]);
        }

        Vector3 bottomLeft = corners[0];
        Vector3 topRight = corners[2];
        float width = topRight.x - bottomLeft.x;
        float height = topRight.y - bottomLeft.y;

        Rect _rect = new Rect(bottomLeft.x, topRight.y, width, height);
        centerPoint = new Vector2(_rect.x + _rect.width * 0.5f, _rect.y - _rect.height * 0.5f);
      }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 pointerPos = ((PointerEventData)eventData).position;
        wheelBeingHeld = true;
        wheelPrevAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
     
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pointerPos = ((PointerEventData)eventData).position;

        float wheelNewAngle = Vector2.Angle(Vector2.up, pointerPos - centerPoint);
      
       
        if (Vector2.Distance(pointerPos, centerPoint) > 20f)
        {
            if (pointerPos.x > centerPoint.x)
                wheelAngle += wheelNewAngle - wheelPrevAngle;
            else
                wheelAngle -= wheelNewAngle - wheelPrevAngle;
        }

        wheelAngle = Mathf.Clamp(wheelAngle, -maximumSteeringAngle, maximumSteeringAngle);
        wheelPrevAngle = wheelNewAngle;

    }
    public void OnPointerUp(PointerEventData eventData)
    {
        OnDrag(eventData);
        wheelBeingHeld = false;
    }

  
}