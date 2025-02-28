using System;
using UnityEngine;

public class TutoLevelBase : MonoBehaviour
{
    private int action;
    
    public void Update()
    {
        DoTuto();
    }
    
    public virtual void DoTuto()
    {
        
    }
}
