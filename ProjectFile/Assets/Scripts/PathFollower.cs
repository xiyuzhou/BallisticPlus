using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Kinematic
{
    public PathFollow myMoveType;
    LookWhereGoing myRotateType;

    public List<GameObject> myPath = new List<GameObject>();
    void Start()
    {
        myMoveType = new PathFollow();
        myMoveType.character = this;

        AddToPath addToPath = FindObjectOfType<AddToPath>();
        myPath = addToPath.pathList;

        myMoveType.path = new List<GameObject>(myPath);

        myRotateType = new LookWhereGoing();
        myRotateType.character = this;
        myRotateType.target = myTarget;

    }
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.angular = myRotateType.getSteering().angular;
        steeringUpdate.linear = myMoveType.getSteering().linear;
        base.Update();

    }
}