using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInvUI {
    void SlotChangedState(InvItemSlot slot, int index);
    void OpenInventory();
}
