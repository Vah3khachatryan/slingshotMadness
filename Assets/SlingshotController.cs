using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    private void Awake()
    {
        GameManager.self.touchManager.OnTouchDown += TouchDown;
        GameManager.self.touchManager.OnTouchDrag += TouchDrag;
        GameManager.self.touchManager.OnTouchUp += TouchUp;
    }


    void TouchDown(Vector2 touchPos)
    {
        GameManager.self.ballsManager.GenerateBall();


        if (SlingshotDragRoutineC != null) StopCoroutine(SlingshotDragRoutineC);

        SlingshotDragRoutineC = StartCoroutine(SlingshotDragRoutine());
    }

    void TouchUp(Vector2 touchPos)
    {
        if (SlingshotDragRoutineC != null) StopCoroutine(SlingshotDragRoutineC);

        GameManager.self.ballsManager.ShootBall();
    }

    void TouchDrag(Vector2 currentPos, Vector2 touchDelta)
    {

    }


    Coroutine SlingshotDragRoutineC;
    IEnumerator SlingshotDragRoutine()
    {
        while(true)
        {


            yield return new WaitForFixedUpdate();
        }
    }


}
