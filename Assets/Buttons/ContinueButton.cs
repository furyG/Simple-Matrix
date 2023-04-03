using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Architecture;

public class ContinueButton : Clickable
{
    private LifesInteractor lifesInteractor;

    protected void Start()
    {
        lifesInteractor = Game.GetInteractor<LifesInteractor>();
    }

    private void OnEnable()
    {
        if(lifesInteractor != null)
        {
            buttonComponent.interactable = lifesInteractor.IsEnoughLifes();
        }
    }
    protected override void OnClick()
    {
        base.OnClick();
        lifesInteractor.RemoveLife(this, 1);
    }
}
