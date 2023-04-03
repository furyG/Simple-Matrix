using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Architecture;

public class ButtonsContainer : MonoBehaviour
{
    [SerializeField] private Clickable[] buttons;

    private ButtonsInteractor buttonsInteractor;

    private void Awake()
    {
        buttonsInteractor = Game.GetInteractor<ButtonsInteractor>();
        buttonsInteractor.SetButtonsArray(buttons);
    }
}
