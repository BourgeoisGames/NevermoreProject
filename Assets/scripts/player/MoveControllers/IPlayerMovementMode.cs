using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerMovementMode {

    void InitializeControl();
    void TearDownControl();
    void Control();
    bool ShouldTakeControl();
    bool ShouldLoseControl();
    void SeizeControl();
    void ReleaseControl();
}
