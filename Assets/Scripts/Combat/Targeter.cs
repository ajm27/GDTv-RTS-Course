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
}
