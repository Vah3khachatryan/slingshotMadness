using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SlingshotController : MonoBehaviour
{
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public Vector3 endPosition;
    Ball ballToShoot;
    Vector3 direction;
    [HideInInspector] public int ammo;
    void Start(){
        ammo = int.Parse(GameObject.FindWithTag("Ammo").GetComponent<TextMeshProUGUI>().text);
        GameManager.self.ballsManager.GenerateBall();
        GameManager.self.touchManager.OnTouchDown += TouchDown;
        GameManager.self.touchManager.OnTouchDrag += TouchDrag;
        GameManager.self.touchManager.OnTouchUp += TouchUp;
    }
    public void TouchDown(Vector3 touchPos)
    {
        if(ammo > 0){
            startPosition = touchPos;
            GameManager.self.trajectoryController.Show();
            ballToShoot = GameObject.FindWithTag("Ball").GetComponent<Ball>();
        }
        else return;

    }
    public void TouchUp(Vector3 touchPos)
    {
        if(ammo > 0){
            GameManager.self.trajectoryController.Hide();
            GameManager.self.ropeController.SlingShotReset();
            GameManager.self.ballsManager.ShootBall(ballToShoot);
            ballToShoot.tag = "Untagged";
            Invoke("SpawnAnotherBall", 0.2f);
        }
        else return;
    }

    public void TouchDrag(Vector3 currentPos, Vector3 touchDelta)
    {
        if(ammo > 0){
            endPosition = currentPos;
            direction = touchDelta;
            direction.y = 0f;
            float distance = Vector3.Distance(startPosition, endPosition);
            GameManager.self.trajectoryController.UpdateDots(ballToShoot.transform.position, -direction * distance * 50f);
            GameManager.self.ropeController.SlingShot();
            ballToShoot.gameObject.transform.position = new Vector3(Mathf.Clamp(GameManager.self.ropeController.coordinates.x, -2.2f, 2.3f), GameManager.self.ropeController.coordinates.y, GameManager.self.ropeController.coordinates.z + 0.7f);
        }
        else return;
    }

    void SpawnAnotherBall() => GameManager.self.ballsManager.GenerateBall();
}
