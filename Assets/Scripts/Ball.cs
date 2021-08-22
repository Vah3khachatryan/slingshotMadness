using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    Rigidbody rb;
    Vector3 startPosition;
    Vector3 endPosition;
    [SerializeField][Range(0, 100)] float moveSpeed = 0;

    Vector3 direction;

    LaunchTrajectory lt;

    Multiplier multi;
    
    static int cloneCount;

    float angle;
    
    Vector3 reflectDirection;

    static int multiplyCount;

    Ray worldPosition;

    Slingshot slingshot;

    GameObject curentObj;



    void Awake(){
        rb = GetComponent<Rigidbody>();
        lt = GameObject.FindWithTag("Thrower").GetComponent<LaunchTrajectory>();
        multi = GetComponent<Multiplier>();
        multi.enabled = false;
        slingshot = GameObject.FindWithTag("Rope").GetComponent<Slingshot>();
    }

    void Update(){
        curentObj = transform.gameObject;
        if(GameObject.FindGameObjectsWithTag("Ball").Length > 0){
            cloneCount = GameObject.FindGameObjectsWithTag("Ball").Length;
        }
        if(GameObject.FindWithTag("MultiplyNumber") != null){    
            string text = GameObject.FindWithTag("MultiplyNumber").GetComponent<TextMeshPro>().text;
            if(text.Contains("x")){
                text = text.Replace("x", "");
            }
            if(text.Contains("+")){
                text = text.Replace("+", "");
            }
            multiplyCount = int.Parse(text);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * moveSpeed;
        worldPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Input.GetMouseButtonDown(0) && rb.velocity == Vector3.zero){
            DragStart(worldPosition.direction);
        }
        else if(Input.GetMouseButton(0) && rb.velocity == Vector3.zero){
            DragContinue(worldPosition.direction);
        }
        else if(Input.GetMouseButtonUp(0) && rb.velocity == Vector3.zero){
           DragEnd();
        }
    }
    void DragStart(Vector3 worldPosition){
        startPosition = worldPosition;
        lt.Show();
    }
    void DragContinue(Vector3 worldPosition){
        endPosition = worldPosition;
			
		// Direction and distance
		Vector3 direction = (startPosition - endPosition).normalized;
		direction.y = 0f;
		float distance = Vector3.Distance(startPosition, endPosition);

        lt.UpdateDots(transform.position, direction * distance * 30f);
        slingshot.SlingShot();
        transform.position = new Vector3(Mathf.Clamp(slingshot.coordinates.x, -2.2f, 2.3f), slingshot.coordinates.y, slingshot.coordinates.z + 0.7f);
    }

    void DragEnd(){
        direction = endPosition - startPosition;
        direction.Normalize();
        direction.y = 0f;
         
        rb.AddForce(-direction);
        transform.rotation = Quaternion.LookRotation(-direction, Vector3.up);
        angle = Vector3.Angle(direction, Vector3.right);
        lt.Hide();
        Invoke("Spawn", 0.2f);
        slingshot.SlingShotReset();
    }

    void Spawn(){
        if(lt.ammo > 0 && GameObject.FindWithTag("Ammo").GetComponent<TextMeshProUGUI>() != null){
            lt.ammo--;
            GameObject.FindWithTag("Ammo").GetComponent<TextMeshProUGUI>().text = "+" + lt.ammo.ToString();
            Instantiate(lt.ball, lt.transform.position, Quaternion.identity);
            transform.parent = GameObject.FindWithTag("BallsContainer").transform;
        }
    }



    void OnCollisionEnter(Collision other){
        reflectDirection = Vector3.Reflect(direction, other.contacts[other.contacts.Length - 1].normal);
        if (reflectDirection != Vector3.zero) { 
            rb.AddForce(reflectDirection);
            transform.rotation = Quaternion.LookRotation(-reflectDirection, Vector3.up);
            angle = Vector3.Angle(reflectDirection, Vector3.right);
        }

        if(other.gameObject.tag == "Humans"){
            Destroy(other.gameObject);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Death"){
            Destroy(this.gameObject);
        }
        // if(other.tag == "Humans"){
        //     rb.AddForce(direction * moveSpeed * 2f, ForceMode.VelocityChange);
            
        // }
        if(other.tag == "Multiply"){
            string num = "";
            if(GameObject.FindWithTag("MultiplyNumber") != null){
                num = GameObject.FindWithTag("MultiplyNumber").GetComponent<TextMeshPro>().text;
            }
            multi.enabled = true;

            if(num.Contains("x")){
                if(reflectDirection == Vector3.zero){   
                    multi.Multiply(multiplyCount, direction, angle);
                }
                else{
                    multi.Multiply(multiplyCount, reflectDirection, angle);
                }
            }

            if(num.Contains("+")){
                Vector3 multiPos = other.transform.position;
                int rowCount = 0;

                if(multiplyCount == 5){ 
                    rowCount = 3;
                }
                else if(multiplyCount == 10){ 
                    rowCount = 4;
                }
                else if(multiplyCount == 15){ 
                    rowCount = 5;
                }
                if(reflectDirection == Vector3.zero){   
                    multi.Add(rowCount, direction, angle - 90f, curentObj, multiPos);
                }
                else{
                    multi.Add(rowCount, reflectDirection, angle - 90f, curentObj, multiPos);
                }
            }
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
