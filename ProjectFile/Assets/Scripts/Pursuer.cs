using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuer : Kinematic
{
    Pursue myMoveType;
    Face myPursueRotateType;
    LookWhereGoing myEvadeRotateType;

    public bool evade = false;

    private void Start()
    {
        myMoveType = new Pursue();
        myMoveType.character = this;
        myMoveType.target = myTarget;

        myPursueRotateType = new Face();
        myPursueRotateType.character = this;
        myPursueRotateType.target = myTarget;

        myEvadeRotateType = new LookWhereGoing();
        myEvadeRotateType.character = this;
        myEvadeRotateType.target = myTarget;

    }
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = evade ? myEvadeRotateType.getSteering().angular : myPursueRotateType.getSteering().angular;
        base.Update();
    }


}