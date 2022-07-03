using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitSpawner : NetworkBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject unityPrefab = null;
    [SerializeField] private Transform spawnLocation = null;
    
    #region Server

    [Command]

    private void CMDSpawnUnit()
    {
        GameObject unitInstance = Instantiate(unityPrefab, spawnLocation.position, spawnLocation.rotation);

        NetworkServer.Spawn(unitInstance, connectionToClient);
    }

    #endregion

    #region Client

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        if (!hasAuthority) return;

        CMDSpawnUnit();
    }

    #endregion
}