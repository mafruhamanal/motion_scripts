using UnityEngine;

public class PinchToScale : MonoBehaviour
{
    private float _initialDistance;
    private Vector3 _initialScale;
    private bool _isScaling;

    void Update()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                _initialDistance = Vector2.Distance(touch1.position, touch2.position);
                _initialScale = transform.localScale;
                _isScaling = true;
            }
            else if (_isScaling && (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved))
            {
                float currentDistance = Vector2.Distance(touch1.position, touch2.position);
                if (Mathf.Approximately(_initialDistance, 0)) return;

                float scaleFactor = currentDistance / _initialDistance;
                transform.localScale = _initialScale * scaleFactor;
            }
            else if (touch1.phase == TouchPhase.Ended || touch2.phase == TouchPhase.Ended)
            {
                _isScaling = false;
            }
        }
    }
}
