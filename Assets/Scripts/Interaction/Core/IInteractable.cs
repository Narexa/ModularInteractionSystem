// Narexa's Modular Interaction System
// MIT License - Feel free to use, modify, and share!
// https://github.com/narexa/ModularInteractionSystem

using UnityEngine;

namespace ModularInteractionSystem.Core
{
    // Interface for any object that can be interacted with in the game
    public interface IInteractable
    {
        // Start interaction - called when player initiates interaction
        void OnInteractionBegin(IInteractor interactor);

        // End interaction - called when player stops interacting
        void OnInteractionEnd(IInteractor interactor);

        // Returns true if this object can be interacted with right now
        bool CanInteract(IInteractor interactor);

        // Gets the text to show in the UI when player looks at this object
        string GetInteractionPrompt();
    }
} 