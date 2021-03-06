using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UnitSpawner : NetworkBehaviour//, IPointerClickHandler
{
    [SerializeField] private Health health = null;
    [SerializeField] private GameObject unitPrefab = null;
    [SerializeField] private Transform spawnLocation = null;

    #region Server

    public override void OnStartServer()
    {
        health.ServerOnDie += ServerHandleDie;
    }

    public override void OnStopServer()
    {
        health.ServerOnDie -= ServerHandleDie;
    }

    [Server]
    private void ServerHandleDie()
    {
        NetworkServer.Destroy(gameObject);
    }

    [Command]
    private void CMDSpawnUnit()
    {
        GameObject unitInstance = Instantiate(unitPrefab, spawnLocation.position, spawnLocation.rotation);

        NetworkServer.Spawn(unitInstance, connectionToClient);
    }

    #endregion

    #region Client

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    if (eventData.button != PointerEventData.InputButton.Left) return;
    //    if (!hasAuthority) return;

    //    CMDSpawnUnit();
    //}

    public void OnMouseDown()
    {
        if (!hasAuthority) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {

            CMDSpawnUnit();
        }
    }

    #endregion
}
