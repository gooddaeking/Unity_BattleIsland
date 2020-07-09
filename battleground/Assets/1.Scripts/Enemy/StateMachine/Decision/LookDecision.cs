﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 타겟이 시야가 막히지않은 상태에서 타겟(플레이어)이 시야각 1/2 사이에 있는지 판별한다
/// </summary>
[CreateAssetMenu(menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision
{
    private bool myHandleTargets(StateController controller, bool hasTarget, Collider[] targetsInRadius)
    {
        if(hasTarget)
        {
            // 플레이어의 위치
            Vector3 target = targetsInRadius[0].transform.position;
            Vector3 dirToTarget = target - controller.transform.position;
            bool inFOVCondition = (Vector3.Angle(controller.transform.forward, dirToTarget) < controller.viewAngle / 2);
            if(inFOVCondition && !controller.BlockedSight())
            {
                controller.targetInSight = true;
                controller.personalTarget = controller.aimTarget.position;
                return true;
            }
        }
        return false;
    }

    public override bool Decide(StateController controller)
    {
        controller.targetInSight = false;
        return CheckTargetInRadius(controller, controller.viewRadius, myHandleTargets);
    }
}
