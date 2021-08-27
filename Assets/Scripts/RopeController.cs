using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeController : MonoBehaviour
{
    LineRenderer lr;

    Ray worldPosition;

    public Vector3 coordinates;

    [SerializeField] float moveSpeed = 5f;

    void Start(){
        lr = GetComponent<LineRenderer>();
    }

    public void SlingShotReset(){
        Vector3 reset = new Vector3(-0.1f, 2f, -14.5f);
        lr.SetPosition(1, reset);
    }
    public void SlingShot(){
        worldPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        Vector3 middle = lr.GetPosition(1);
        coordinates = new Vector3(Mathf.Clamp(worldPosition.direction.normalized.x * moveSpeed, -2.7f, 2.7f), middle.y, -16f);
        lr.SetPosition(1, coordinates);
    }
}
