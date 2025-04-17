// Narexa's Modular Interaction System
// MIT License - Feel free to use, modify, and share!
// https://github.com/narexa/ModularInteractionSystem

using UnityEngine;
using UnityEngine.InputSystem;
using ModularInteractionSystem.Core;
using TMPro;

namespace ModularInteractionSystem.Player
{
    // Handles the player's ability to interact with objects in the world
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerInteractor : InteractorBase
    {
        [Header("UI References")]
        [SerializeField]
        private TextMeshProUGUI promptText;    // Text component that shows what we can do

        [SerializeField]
        private GameObject promptPanel;         // Container for the interaction prompt UI

        private PlayerInput playerInput;
        private InputAction interactAction;     // The input action for interaction (usually E key or button)

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            interactAction = playerInput.actions["Interact"];

            if (promptPanel != null)
                promptPanel.SetActive(false);
        }

        private void OnEnable()
        {
            // Hook up our input events
            interactAction.performed += HandleInteractInput;
            interactAction.canceled += HandleInteractInputReleased;
            OnTargetChanged += HandleTargetChanged;
        }

        private void OnDisable()
        {
            // Clean up our input events
            interactAction.performed -= HandleInteractInput;
            interactAction.canceled -= HandleInteractInputReleased;
            OnTargetChanged -= HandleTargetChanged;
        }

        // Called when player presses the interact button
        private void HandleInteractInput(InputAction.CallbackContext context)
        {
            if (currentTarget != null)
            {
                OnInteractionStarted(currentTarget);
            }
        }

        // Called when player releases the interact button
        private void HandleInteractInputReleased(InputAction.CallbackContext context)
        {
            if (currentTarget != null)
            {
                OnInteractionEnded(currentTarget);
            }
        }

        // Updates the UI when we look at/stop looking at interactable objects
        private void HandleTargetChanged(IInteractable newTarget)
        {
            if (promptPanel == null || promptText == null)
                return;

            if (newTarget != null)
            {
                promptText.text = newTarget.GetInteractionPrompt();
                promptPanel.SetActive(true);
            }
            else
            {
                promptPanel.SetActive(false);
            }
        }

        protected override void DetectInteractables()
        {
            base.DetectInteractables();

            // Make sure we can only interact with things in front of us
            if (currentTarget != null)
            {
                Vector3 directionToTarget = (currentTarget as MonoBehaviour).transform.position - transform.position;
                float dotProduct = Vector3.Dot(transform.forward, directionToTarget.normalized);

                // Clear target if it's behind us (120 degree cone)
                if (dotProduct < 0.5f)
                {
                    currentTarget = null;
                    InvokeTargetChanged(null);
                }
            }
        }
    }
} 