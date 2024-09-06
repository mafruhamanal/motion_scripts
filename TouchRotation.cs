using UnityEngine;

public class TouchRotation : MonoBehaviour
{
    public float rotationSpeed = 1f; // Adjust rotation speed as needed
    private Vector2 lastTouchPosition;
    private bool isTouching = false;
    private float currentRotationY = 0f;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    StartTouch(touch.position);
                    break;

                case TouchPhase.Moved:
                    if (isTouching)
                    {
                        UpdateRotation(touch.position);
                    }
                    break;

                case TouchPhase.Ended:
                    isTouching = false;
                    break;
            }
        }
    }

    private void StartTouch(Vector2 touchPosition)
    {
        lastTouchPosition = touchPosition;
        isTouching = true;
    }

    private void UpdateRotation(Vector2 currentTouchPosition)
    {
        // Calculate rotation based on swipe distance
        float delta = currentTouchPosition.x - lastTouchPosition.x;
        currentRotationY += delta * rotationSpeed * Time.deltaTime;

        // Apply rotation to the object this script is attached to
        transform.rotation = Quaternion.Euler(0f, currentRotationY, 0f);

        // Update last touch position for next frame
        lastTouchPosition = currentTouchPosition;
    }
}
