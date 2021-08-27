using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallsManager : MonoBehaviour
{
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public float angle;
    [SerializeField] Transform ballsContainer = null;
   
    public void GenerateBall()
    {
        if(GameManager.self.slingshotController.ammo > 0){
            Instantiate(GameManager.self.trajectoryController.ball, GameManager.self.trajectoryController.transform.position, Quaternion.identity, ballsContainer);
        }
    }

    public void ShootBall(Ball ball)
    {
        direction = GameManager.self.slingshotController.endPosition - GameManager.self.slingshotController.startPosition;
        direction.y = 0f;
        if(direction != Vector3.zero){
            GameManager.self.slingshotController.ammo--;
            GameObject.FindWithTag("Ammo").GetComponent<TextMeshProUGUI>().text = "+" + GameManager.self.slingshotController.ammo.ToString();
            
            ball.rb.AddForce(-direction);
            ball.transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);
            angle = Vector3.Angle(direction, Vector3.right);
        }
        else{
            Destroy(ball.gameObject);
        }
    }


    public void MultiplyBalls(Collider other, int multiplyCount, GameObject currentObj, Vector3 reflectDirection)
    {
        if(other.tag == "Multiply")
        {
            string num = "";
            if (GameObject.FindWithTag("MultiplyNumber") != null)
            {
                num = GameObject.FindWithTag("MultiplyNumber").GetComponent<TextMeshPro>().text;
            }
            GameManager.self.multiplierManager.enabled = true;
            CallMultiplyFunction(other, multiplyCount, reflectDirection, num);
            CallAddFunction(other, multiplyCount, currentObj, reflectDirection, num);
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

    private void CallMultiplyFunction(Collider other, int multiplyCount, Vector3 reflectDirection, string num)
    {
        if (num.Contains("x"))
        {
            if (reflectDirection == Vector3.zero)
            {
                GameManager.self.multiplierManager.Multiply(multiplyCount, direction, angle, other);
            }
            else
            {
                GameManager.self.multiplierManager.Multiply(multiplyCount, reflectDirection, angle, other);
            }
        }
    }

    private void CallAddFunction(Collider other, int multiplyCount, GameObject currentObj, Vector3 reflectDirection, string num)
    {
        if (num.Contains("+"))
        {
            Vector3 multiPos = other.transform.position;
            int rowCount = 0;

            if (multiplyCount == 5)
            {
                rowCount = 3;
            }
            else if (multiplyCount == 10)
            {
                rowCount = 4;
            }
            else if (multiplyCount == 15)
            {
                rowCount = 5;
            }
            if (reflectDirection == Vector3.zero)
            {
                GameManager.self.multiplierManager.Add(rowCount, direction, angle - 90f, currentObj, multiPos, other);
            }
            else
            {
                GameManager.self.multiplierManager.Add(rowCount, reflectDirection, angle - 90f, currentObj, multiPos, other);
            }
        }
    }
}
