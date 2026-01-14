using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Invisibility : Ability
{
    private Renderer[] renderers;

    private void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();
        UseAbility();
    }
    public override void Setup()
    {
        UseAbility();
    }

    public void SetVisible(bool isVisible)
    {
        foreach (var renderer in renderers)
        {
            renderer.enabled = isVisible;
        }
    }


    public override void UseAbility()
    {
        SetVisible(false);
    }

    public override void StopAbility()
    {
        SetVisible(true);
    }


}
