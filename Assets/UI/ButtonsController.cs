using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
    public static event Action StartButtonPushed;
    public static event Action ContinueButtonPushed;

    public void StartGame()
    {
        StartButtonPushed?.Invoke();
    }
    public void ContinueGame()
    {
        ContinueButtonPushed?.Invoke();
    }
}
