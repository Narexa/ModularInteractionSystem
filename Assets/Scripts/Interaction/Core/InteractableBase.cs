// Narexa's Modular Interaction System
// MIT License - Feel free to use, modify, and share!
// https://github.com/narexa/ModularInteractionSystem

using UnityEngine;
using System.Collections.Generic;

namespace ModularInteractionSystem.Core
{
    // Base class that handles common interaction functionality
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField]
        protected string interactionPrompt = "Interact";

        [SerializeField]
        protected float interactionRange = 2f;

        protected HashSet<IInteractor> currentInteractors = new HashSet<IInteractor>();
        protected bool isInteractable = true;

        // Returns true if interaction is allowed (checks range and if object is interactable)
        public virtual bool CanInteract(IInteractor interactor)
        {
            if (!isInteractable || interactor == null)
                return false;

            float distance = Vector3.Distance(transform.position, interactor.Transform.position);
            return distance <= interactionRange;
        }

        // Returns the text to show in UI when player looks at this
        public virtual string GetInteractionPrompt()
        {
            return interactionPrompt;
        }

        // Called by the interaction system when someone tries to interact
        public virtual void OnInteractionBegin(IInteractor interactor)
        {
            if (!CanInteract(interactor))
                return;

            currentInteractors.Add(interactor);
            HandleInteractionBegin(interactor);
        }

        // Called when interaction ends (player walks away, cancels, etc)
        public virtual void OnInteractionEnd(IInteractor interactor)
        {
            if (interactor == null || !currentInteractors.Contains(interactor))
                return;

            currentInteractors.Remove(interactor);
            HandleInteractionEnd(interactor);
        }

        // Override this to implement what happens when interaction starts
        protected abstract void HandleInteractionBegin(IInteractor interactor);

        // Override this to implement what happens when interaction ends
        protected abstract void HandleInteractionEnd(IInteractor interactor);

        // Call this to enable/disable interaction with this object
        public virtual void SetInteractable(bool canInteract)
        {
            isInteractable = canInteract;
        }

        protected virtual void OnDrawGizmosSelected()
        {
            // Shows interaction range in editor
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionRange);
        }
    }
} 