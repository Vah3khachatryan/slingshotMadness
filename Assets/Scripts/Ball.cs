using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    public Rigidbody rb;
    [SerializeField][Range(0, 100)] float moveSpeed = 0;    
    Vector3 reflectDirection;

    static int multiplyCount;

    GameObject currentObj;

    void Update()
    {
        currentObj = transform.gameObject;
        GetMultiplierNumber();
    }

    void GetMultiplierNumber()
    {
        if (GameObject.FindWithTag("MultiplyNumber") != null)
        {
            string text = GameObject.FindWithTag("MultiplyNumber").GetComponent<TextMeshPro>().text;
            if (text.Contains("x"))
            {
                text = text.Replace("x", "");
            }
            if (text.Contains("+"))
            {
                text = text.Replace("+", "");
            }
            multiplyCount = int.Parse(text);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * moveSpeed;
    }
    

    void OnCollisionEnter(Collision other)
    {
        ReflectWall(other);

        if (other.gameObject.tag == "Humans")
        {
            Destroy(other.gameObject);
        }
    }

    private void ReflectWall(Collision other)
    {
        reflectDirection = Vector3.Reflect(GameManager.self.ballsManager.direction, other.contacts[other.contacts.Length - 1].normal);
        if (reflectDirection != Vector3.zero)
        {
            rb.AddForce(reflectDirection);
            transform.rotation = Quaternion.LookRotation(-reflectDirection, Vector3.up);
            GameManager.self.ballsManager.angle = Vector3.Angle(reflectDirection, Vector3.right);
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.tag == "Death"){
            Destroy(this.gameObject);
        }
        GameManager.self.ballsManager.MultiplyBalls(other, multiplyCount, currentObj, reflectDirection);
    }
}
