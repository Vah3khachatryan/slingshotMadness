using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TouchManager : MonoBehaviour
{
    public UnityAction<Vector2, Vector2> OnTouchDrag;
    public UnityAction<Vector2> OnTouchUp;
    public UnityAction<Vector2> OnTouchDown;

    public bool TouchActive { get; set; }
    public Vector2 DeltaPosition
    {
        get { return m_DeltaPosition; }
    }

    Vector2 m_StartPosition;
    Vector2 m_CurrentPosition;
    Vector2 m_LastPosition;
    Vector2 m_DeltaPosition;
    Vector2 m_TotalDeltaPosition;

    public bool isTouchDown = false;

    void Update()
    {
        //        if (!GameManager.self.GameStarted) return;

        if (Input.GetMouseButtonDown(0))
        {

            
            m_StartPosition = Input.mousePosition;
            m_LastPosition = m_StartPosition;
            m_DeltaPosition = Vector3.zero;

            OnTouchDown?.Invoke(m_StartPosition);

            isTouchDown = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            m_DeltaPosition = Vector3.zero;

            OnTouchUp?.Invoke(m_DeltaPosition);

            isTouchDown = false;
        }
        if (Input.GetMouseButton(0))
        {
            m_CurrentPosition = Input.mousePosition;
            m_DeltaPosition = m_CurrentPosition - m_LastPosition;
            m_LastPosition = m_CurrentPosition;
            m_TotalDeltaPosition = m_CurrentPosition - m_StartPosition;

            OnTouchDrag?.Invoke(m_CurrentPosition, m_TotalDeltaPosition);
        }
    }
}
