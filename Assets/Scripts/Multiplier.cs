using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Multiplier : MonoBehaviour
{
    GameObject go;
    [SerializeField] GameObject ballPrefab = null;

    GameObject ballContainer;

    public void Add(int rowCount, Vector3 direction, float angle, GameObject ball, Vector3 pos){
        Vector3 targetPosition = Vector3.left;
        float rowOffset = -0.5f;
        float zOffset = -1f;
        float xOffset = 1f;
            for(int i = 1; i <= rowCount; i++){
                for(int j = 0; j < i; j++){
                    go = Instantiate(ball, targetPosition, Quaternion.identity);
                    go.transform.parent = GameObject.FindWithTag("BallsContainer").transform;
                    targetPosition = new Vector3(targetPosition.x + xOffset, targetPosition.y, targetPosition.z);
                    go.transform.position = targetPosition;
                    go.GetComponent<Rigidbody>().AddForce(-direction);
                }
                targetPosition = new Vector3((rowOffset * i) - 1f, targetPosition.y, targetPosition.z + zOffset);
            }
        if(transform.parent != null){
            ballContainer = transform.parent.gameObject;
            ballContainer.transform.position = pos;
            ball.SetActive(false);
            Destroy(ball, 1.5f);
            ballContainer.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
        }
    }

    // public void Add(int rowCount, Vector3 direction, float angle, GameObject ball, Vector3 pos){
    //     float rowOffset = -0.5f;
    //     float zOffset = -1f;
    //     float xOffset = 1f;
    //     Vector3 targetPosition = pos;
    //     for(int i = 1; i < rowCount; i++){
    //         targetPosition = new Vector3((rowOffset * i) - 1f, targetPosition.y, targetPosition.z + zOffset);
    //         for(int j = 0; j < i + 1; j++){
    //             go = Instantiate(ballPrefab, targetPosition, Quaternion.identity);
    //             go.GetComponent<Multiplier>().enabled = false;
    //             go.transform.parent = GameObject.FindWithTag("BallsContainer").transform;
    //             targetPosition = new Vector3(targetPosition.x + xOffset, targetPosition.y, targetPosition.z);
    //             go.transform.position = targetPosition;
    //             go.GetComponent<Rigidbody>().AddForce(-direction);
    //         }
    //     }
    //     ballContainer = transform.parent.gameObject;
    //     ballContainer.transform.localRotation = Quaternion.Euler(0f, angle, 0f);
    // }

    public void Multiply(int num, Vector3 direction, float angle){
        int sides = 0 - ((num - 1) / 2);
        for(int i = 0; i < num; i++){
            if(angle > 80f && angle < 110f){
                sides++;
                    if(sides == 0){
                        continue;
                    }
                go = Instantiate(ballPrefab, new Vector3(transform.position.x + sides, transform.position.y, transform.position.z), transform.rotation);
                go.GetComponent<Rigidbody>().AddForce(-direction);
                go.transform.parent = GameObject.FindWithTag("BallsContainer").transform;
            }
        }
        for(int i = 1; i < num; i++){
            if(angle <= 80f){
                go = Instantiate(ballPrefab, new Vector3(transform.position.x + i, transform.position.y, transform.position.z + i), transform.rotation);
                go.GetComponent<Rigidbody>().AddForce(-direction);
                go.transform.parent = GameObject.FindWithTag("BallsContainer").transform;
            }
            else if(angle >= 110f){
                go = Instantiate(ballPrefab, new Vector3(transform.position.x - i, transform.position.y, transform.position.z + i), transform.rotation);
                go.GetComponent<Rigidbody>().AddForce(-direction);
                go.transform.parent = GameObject.FindWithTag("BallsContainer").transform;
            }
        }
        
    }
}
