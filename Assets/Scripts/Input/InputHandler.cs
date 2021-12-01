using UnityEngine;
using System;

public class InputHandler : MonoBehaviour
{
    public event Action<Vector3> OnFirstTouch;
    public event Action<Vector3> OnTouchMoved;
    public event Action<Vector3> OnTouchEnded;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (OnFirstTouch != null)
                    OnFirstTouch.Invoke(touch.position);
            }

            if (touch.phase == TouchPhase.Moved)
            {
                if (OnTouchMoved != null)
                    OnTouchMoved.Invoke(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                if (OnTouchEnded != null)
                    OnTouchEnded.Invoke(touch.position);
            }
        }


        if (Input.GetMouseButtonDown(0))
        {
            if (OnFirstTouch != null)
                OnFirstTouch(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            if (OnTouchMoved != null)
                OnTouchMoved.Invoke(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (OnTouchEnded != null)
                OnTouchEnded.Invoke(Input.mousePosition);
        }
    }
}
