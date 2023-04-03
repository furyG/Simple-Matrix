using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISlowable
{
    bool CheckForSlow();
    void SlowUp();
    void SlowEnd();
}
