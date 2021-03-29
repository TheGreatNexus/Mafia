using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class SoundManager : Manager<SoundManager>
{
    protected override IEnumerator InitCoroutine()
    {
        yield break;
    }
    public override void SubscribeEvents()
    {
        base.SubscribeEvents();
        
    }
    public override void UnsubscribeEvents()
    {
        base.UnsubscribeEvents();
        
    }

}
