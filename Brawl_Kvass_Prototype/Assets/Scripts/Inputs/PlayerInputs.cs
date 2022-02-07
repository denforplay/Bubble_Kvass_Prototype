// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Inputs/PlayerInputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInputs : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputs"",
    ""maps"": [
        {
            ""name"": ""JumpGameInputs"",
            ""id"": ""866b68f0-df6b-4a1f-b591-4124ce929c00"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""64c684c1-1a37-4aee-8b53-15c5035440cc"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""cfa05e78-201e-429f-a07e-13eece51b0f2"",
                    ""path"": ""<Accelerometer>/acceleration"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""RunGameInputs"",
            ""id"": ""eab43029-040e-4f64-92bf-60450576532e"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""4740248c-f0ad-41b1-9a60-729fa6079794"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""72b3909d-3cad-44de-b232-5468eccb3378"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // JumpGameInputs
        m_JumpGameInputs = asset.FindActionMap("JumpGameInputs", throwIfNotFound: true);
        m_JumpGameInputs_Movement = m_JumpGameInputs.FindAction("Movement", throwIfNotFound: true);
        // RunGameInputs
        m_RunGameInputs = asset.FindActionMap("RunGameInputs", throwIfNotFound: true);
        m_RunGameInputs_Jump = m_RunGameInputs.FindAction("Jump", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // JumpGameInputs
    private readonly InputActionMap m_JumpGameInputs;
    private IJumpGameInputsActions m_JumpGameInputsActionsCallbackInterface;
    private readonly InputAction m_JumpGameInputs_Movement;
    public struct JumpGameInputsActions
    {
        private @PlayerInputs m_Wrapper;
        public JumpGameInputsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_JumpGameInputs_Movement;
        public InputActionMap Get() { return m_Wrapper.m_JumpGameInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(JumpGameInputsActions set) { return set.Get(); }
        public void SetCallbacks(IJumpGameInputsActions instance)
        {
            if (m_Wrapper.m_JumpGameInputsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_JumpGameInputsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_JumpGameInputsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_JumpGameInputsActionsCallbackInterface.OnMovement;
            }
            m_Wrapper.m_JumpGameInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
            }
        }
    }
    public JumpGameInputsActions @JumpGameInputs => new JumpGameInputsActions(this);

    // RunGameInputs
    private readonly InputActionMap m_RunGameInputs;
    private IRunGameInputsActions m_RunGameInputsActionsCallbackInterface;
    private readonly InputAction m_RunGameInputs_Jump;
    public struct RunGameInputsActions
    {
        private @PlayerInputs m_Wrapper;
        public RunGameInputsActions(@PlayerInputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_RunGameInputs_Jump;
        public InputActionMap Get() { return m_Wrapper.m_RunGameInputs; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(RunGameInputsActions set) { return set.Get(); }
        public void SetCallbacks(IRunGameInputsActions instance)
        {
            if (m_Wrapper.m_RunGameInputsActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_RunGameInputsActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_RunGameInputsActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_RunGameInputsActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_RunGameInputsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public RunGameInputsActions @RunGameInputs => new RunGameInputsActions(this);
    public interface IJumpGameInputsActions
    {
        void OnMovement(InputAction.CallbackContext context);
    }
    public interface IRunGameInputsActions
    {
        void OnJump(InputAction.CallbackContext context);
    }
}
