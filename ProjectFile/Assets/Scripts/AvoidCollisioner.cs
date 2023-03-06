using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidCollisioner : Kinematic
{
    CollisionAvoidance myMoveType;

    public Kinematic[] myTargets = new Kinematic[2];

    void Start()
    {
        myMoveType = new CollisionAvoidance();
        myMoveType.character = this;
        myMoveType.targets = myTargets;
    }

    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}
