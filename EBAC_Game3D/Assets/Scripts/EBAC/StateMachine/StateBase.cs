using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBase
{
    public virtual void OnStateEnter(object o = null)
    {
        Debug.Log("OnStateEnter");
    }

    public virtual void OnStateStay()
    {
        Debug.Log("OnStateStay");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("OnStateExit");
    }
}

public class StateRunning : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}

public class StateDead : StateBase
{
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
    }
}
