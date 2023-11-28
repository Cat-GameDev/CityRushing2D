using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] UnityEvent ledgeClimb;
    [SerializeField] UnityEvent CanRoll;
    [SerializeField] UnityEvent Silde;
    [SerializeField] UnityEvent Run;
    [SerializeField] UnityEvent knockBack;
    public void LedgeClimbOver()
    {
        ledgeClimb?.Invoke();
    }

    public void RollAnimFinish()
    {
        CanRoll?.Invoke();
    }

    public void StartSilde()
    {
        Silde?.Invoke();
    }

    public void StartRun()
    {
        Run?.Invoke();
    }

    public void CancelKnockBack()
    {
        knockBack?.Invoke();
    }


}


