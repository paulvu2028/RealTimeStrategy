﻿using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable target; 

    public Targetable GetTarget(){
        return target;
    }

    #region Server

    [Command]
    public void CmdSetTarget(GameObject targetGameObject){

        if(!targetGameObject.TryGetComponent<Targetable>(out Targetable newTarget)){return;}

        target = newTarget;
    }

    public void ClearTarget(){
        target = null;
    }

    #endregion

    #region Client

    #endregion
}
