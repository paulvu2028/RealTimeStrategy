using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class RTSNetworkManager : NetworkManager
{
    [SerializeField] private GameObject unitSpawnerPrefab = null;

    //fuction is called when a person connects to the server
    //conn parameter is for connection of client to server
    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        base.OnServerAddPlayer(conn);

        //creates instance of player
        GameObject unitSpawnerInstance =  Instantiate(
            unitSpawnerPrefab, 
            conn.identity.transform.position, 
            conn.identity.transform.rotation);

        NetworkServer.Spawn(unitSpawnerInstance, conn);
    }

}
