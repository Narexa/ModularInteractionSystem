// Narexa's Modular Interaction System
// MIT License - Feel free to use, modify, and share!
// https://github.com/narexa/ModularInteractionSystem

using UnityEngine;
using System;

namespace ModularInteractionSystem.Core
{
    // Base class for things that can interact with objects (like players or AI)
    public abstract class InteractorBase : MonoBehaviour, IInteractor
    {
        [SerializeField]
        protected LayerMask interactableLayer = -1;  // Which layers to check for interactables
        
        [SerializeField]
        protected float detectionRadius = 2f;  // How far to look for interactable objects

        protected IInteractable currentTarget;
        public IInteractable CurrentTarget => currentTarget;

        public Transform Transform => transform;

        // Fired when we start/stop looking at an interactable
        public event Action<IInteractable> OnTargetChanged;

        // Protected method to safely invoke the event
        protected void InvokeTargetChanged(IInteractable newTarget)
        {
            OnTargetChanged?.Invoke(newTarget);
        }

        protected virtual void Update()
        {
            DetectInteractables();
        }

        // Looks for the closest interactable object in range
        protected virtual void DetectInteractables()
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, interactableLayer);
            IInteractable bestTarget = null;
            float closestDistance = float.MaxValue;

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<IInteractable>(out var interactable))
                {
                    if (!interactable.CanInteract(this))
                        continue;

                    float distance = Vector3.Distance(transform.position, collider.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        bestTarget = interactable;
                    }
                }
            }

            if (currentTarget != bestTarget)
            {
                currentTarget = bestTarget;
                InvokeTargetChanged(currentTarget);
            }
        }

        // Start interacting with an object
        public virtual void OnInteractionStarted(IInteractable interactable)
        {
            if (interactable == null)
                return;

            interactable.OnInteractionBegin(this);
        }

        // Stop interacting with an object
        public virtual void OnInteractionEnded(IInteractable interactable)
        {
            if (interactable == null)
                return;

            interactable.OnInteractionEnd(this);
        }

        protected virtual void OnDrawGizmosSelected()
        {
            // Shows detection radius in editor
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
} 