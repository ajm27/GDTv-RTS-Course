using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : NetworkBehaviour
{
    private Targetable target;

    public Targetable GetTarget()
    {
        return target;
    }

    public override void OnStartServer()
    {
        GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    public override void OnStopServer()
    {
        GameOverHandler.ServerOnGameOver += ServerHandleGameOver;
    }

    [Command]
    public void CmdSetTarget(GameObject targetGO)
    {
        if (!targetGO.TryGetComponent<Targetable>(out Targetable target)) return;

        this.target = target;
    }

    [Server]
    public void ClearTarget()
    {
        target = null;
    }

    [Server]
    private void ServerHandleGameOver()
    {
        ClearTarget();
    }
}
