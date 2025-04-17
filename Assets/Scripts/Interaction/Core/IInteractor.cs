// Narexa's Modular Interaction System
// MIT License - Feel free to use, modify, and share!
// https://github.com/narexa/ModularInteractionSystem

using UnityEngine;

namespace ModularInteractionSystem.Core
{
    // Interface for anything that can interact with IInteractable objects (player, AI, etc)
    public interface IInteractor
    {
        // Reference to this object's transform component
        Transform Transform { get; }

        // The object this interactor is currently looking at/focusing on
        IInteractable CurrentTarget { get; }

        // Called when successfully starting an interaction
        void OnInteractionStarted(IInteractable interactable);

        // Called when stopping an interaction
        void OnInteractionEnded(IInteractable interactable);
    }
} 