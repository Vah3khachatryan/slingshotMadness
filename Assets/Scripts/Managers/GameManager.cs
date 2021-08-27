using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager self;

    [Header("Managers")]
    public TouchManager touchManager;
    public LevelManager levelManager;
    public BallsManager ballsManager;
    public MultiplierManager multiplierManager;

    [Header("Controllers")]
    public TrajectoryController trajectoryController;
    public RopeController ropeController;
    public SlingshotController slingshotController;

    private void Awake()
    {
        self = this;
    }
}
