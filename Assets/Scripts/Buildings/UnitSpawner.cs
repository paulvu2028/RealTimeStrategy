using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform unitSpawnPoint = null;

    #region server
    //COMMAND allows for clients calling a method on the server
   [Command]
    private void CmdSpawnUnit(){

        GameObject unitInstance = Instantiate(unitPrefab, unitSpawnPoint.position, unitSpawnPoint.rotation);
        
        //Spawns unit instance with connection to client
        NetworkServer.Spawn(unitInstance, connectionToClient);
    }

    #endregion

    #region client
    public void OnPointerClick(PointerEventData eventData)
    {
        //checking for mouse input and server authority
        if(eventData.button != PointerEventData.InputButton.Left){
            return;
        }
        if(!hasAuthority){
            return;
        }
        CmdSpawnUnit();
    }

    #endregion
}
