using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PanelController<T> : MonoBehaviour where T : System.Enum
{
    public T panelType;
}
