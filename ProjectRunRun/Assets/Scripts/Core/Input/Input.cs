//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/Core/Input/Input.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Input : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Input()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Input"",
    ""maps"": [
        {
            ""name"": ""CheatKeys"",
            ""id"": ""fdfa2c25-b428-4a57-9426-8a50e1145a67"",
            ""actions"": [
                {
                    ""name"": ""Cheat"",
                    ""type"": ""Button"",
                    ""id"": ""9dcf9fff-088f-4641-b877-de138fd8d544"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""591775a6-7b91-4e05-9ca3-98b9a4ded9c9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1161b34e-0547-4ed1-8e16-da985495f6e0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cheat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Interaction"",
            ""id"": ""69d002f5-0b1a-47af-954b-c9bc52c412fb"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""4b0f54ef-571e-4237-8021-9abd0d17f11c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""78401832-d0fa-42fb-9105-21513cbd5c7d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CheatKeys
        m_CheatKeys = asset.FindActionMap("CheatKeys", throwIfNotFound: true);
        m_CheatKeys_Cheat = m_CheatKeys.FindAction("Cheat", throwIfNotFound: true);
        // Interaction
        m_Interaction = asset.FindActionMap("Interaction", throwIfNotFound: true);
        m_Interaction_Click = m_Interaction.FindAction("Click", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // CheatKeys
    private readonly InputActionMap m_CheatKeys;
    private ICheatKeysActions m_CheatKeysActionsCallbackInterface;
    private readonly InputAction m_CheatKeys_Cheat;
    public struct CheatKeysActions
    {
        private @Input m_Wrapper;
        public CheatKeysActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Cheat => m_Wrapper.m_CheatKeys_Cheat;
        public InputActionMap Get() { return m_Wrapper.m_CheatKeys; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CheatKeysActions set) { return set.Get(); }
        public void SetCallbacks(ICheatKeysActions instance)
        {
            if (m_Wrapper.m_CheatKeysActionsCallbackInterface != null)
            {
                @Cheat.started -= m_Wrapper.m_CheatKeysActionsCallbackInterface.OnCheat;
                @Cheat.performed -= m_Wrapper.m_CheatKeysActionsCallbackInterface.OnCheat;
                @Cheat.canceled -= m_Wrapper.m_CheatKeysActionsCallbackInterface.OnCheat;
            }
            m_Wrapper.m_CheatKeysActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Cheat.started += instance.OnCheat;
                @Cheat.performed += instance.OnCheat;
                @Cheat.canceled += instance.OnCheat;
            }
        }
    }
    public CheatKeysActions @CheatKeys => new CheatKeysActions(this);

    // Interaction
    private readonly InputActionMap m_Interaction;
    private IInteractionActions m_InteractionActionsCallbackInterface;
    private readonly InputAction m_Interaction_Click;
    public struct InteractionActions
    {
        private @Input m_Wrapper;
        public InteractionActions(@Input wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Interaction_Click;
        public InputActionMap Get() { return m_Wrapper.m_Interaction; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InteractionActions set) { return set.Get(); }
        public void SetCallbacks(IInteractionActions instance)
        {
            if (m_Wrapper.m_InteractionActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_InteractionActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_InteractionActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_InteractionActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_InteractionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public InteractionActions @Interaction => new InteractionActions(this);
    public interface ICheatKeysActions
    {
        void OnCheat(InputAction.CallbackContext context);
    }
    public interface IInteractionActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
}
