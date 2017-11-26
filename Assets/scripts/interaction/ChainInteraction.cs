using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainInteraction : MonoBehaviour, Interactive {
    
    public List<MonoBehaviour> interactions;

	public void Interact() {
        for (int i = 0; i < interactions.Count; i++)
        {
            if (interactions[i] == null) { continue; }
            try
            {
                Interactive interaction = (Interactive) interactions[i];
                interaction.Interact();
            }
            catch (InvalidCastException)
            {
                Debug.LogWarning("Non-Interactive object in chain interaction");
            }
        }
	}
}
