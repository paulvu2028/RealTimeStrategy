using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.AI;

public class UnitMovement : NetworkBehaviour
{
    [SerializeField] private NavMeshAgent agent = null;

    [SerializeField] private Targeter targeter = null;

    [SerializeField] private float chaseRange = 10f;

    private Camera mainCamera;

    #region Server

    //ServerCallback is for functions we do not control but should also be server exclusive
    [ServerCallback]
    private void Update(){
        Targetable target = targeter.GetTarget();

        //chasing movement
        if(targeter.GetTarget() != null){
            if((target.transform.position - transform.position).sqrMagnitude > chaseRange * chaseRange){
                agent.SetDestination(targeter.GetTarget().transform.position);
            }
            else if(agent.hasPath){
                agent.ResetPath();
            }


            return;
        }

        //normal movement
        if(!agent.hasPath){return;}

        if(agent.remainingDistance > agent.stoppingDistance){return;}

        agent.ResetPath();
    }

    [Command]
    public void CmdMove(Vector3 position){

        targeter.ClearTarget();

        if(!NavMesh.SamplePosition(position,out NavMeshHit hit, 1f, NavMesh.AllAreas)){
            return;
        }
        agent.SetDestination(hit.position);
    }

    #endregion

}
