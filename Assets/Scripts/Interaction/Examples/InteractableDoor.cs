// Narexa's Modular Interaction System
// MIT License - Feel free to use, modify, and share!
// https://github.com/narexa/ModularInteractionSystem

using UnityEngine;
using ModularInteractionSystem.Core;

namespace ModularInteractionSystem.Examples
{
    // Basic door that can be opened/closed through interaction
    public class InteractableDoor : InteractableBase
    {
        [SerializeField]
        private Transform doorPivot;  // The part that actually rotates. If null, uses this transform

        [SerializeField]
        private float openAngle = 90f;

        [SerializeField]
        private float openSpeed = 2f;

        private bool isOpen;
        private Quaternion closedRotation;
        private Quaternion openRotation;

        // TODO: Add door locked state and key item requirement
        // private bool isLocked;
        // [SerializeField] private string requiredKeyId;

        private void Start()
        {
            if (doorPivot == null)
                doorPivot = transform;

            // Cache the rotations so we don't calculate them every frame
            closedRotation = doorPivot.rotation;
            openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);
            
            interactionPrompt = isOpen ? "Close Door" : "Open Door";
        }

        private void Update()
        {
            // Smooth door movement - could be improved with proper easing
            Quaternion targetRotation = isOpen ? openRotation : closedRotation;
            doorPivot.rotation = Quaternion.Lerp(doorPivot.rotation, targetRotation, Time.deltaTime * openSpeed);
        }

        protected override void HandleInteractionBegin(IInteractor interactor)
        {
            isOpen = !isOpen;
            interactionPrompt = isOpen ? "Close Door" : "Open Door";

            // TODO: Add sound effects
            // AudioManager.Instance.PlaySound(isOpen ? "door_open" : "door_close");
        }

        protected override void HandleInteractionEnd(IInteractor interactor)
        {
            // Nothing needed here for a basic door
        }

        #if UNITY_EDITOR
        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();

            if (doorPivot != null)
            {
                // Show which way the door will swing
                Gizmos.color = Color.green;
                Vector3 direction = Quaternion.Euler(0f, openAngle, 0f) * doorPivot.forward;
                Gizmos.DrawLine(doorPivot.position, doorPivot.position + direction * 2f);
            }
        }
        #endif
    }
} 