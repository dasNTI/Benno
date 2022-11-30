// GENERATED AUTOMATICALLY FROM 'Assets/Packages/InputMaster.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMaster : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMaster()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMaster"",
    ""maps"": [
        {
            ""name"": ""MainInput"",
            ""id"": ""883a1dad-4678-41fa-b85a-07445900a78b"",
            ""actions"": [
                {
                    ""name"": ""Dir"",
                    ""type"": ""Value"",
                    ""id"": ""af44ba7b-a1b1-4c36-a1ce-347ddcf9f32c"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""6e6f3a1d-e025-4152-a123-bca3d1937894"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Enter"",
                    ""type"": ""Button"",
                    ""id"": ""81996e76-651d-4da8-b83c-ff11c9aa2157"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""42ad165e-6305-46c0-833d-8588f091c36e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                },
                {
                    ""name"": ""Quit"",
                    ""type"": ""Button"",
                    ""id"": ""d9de65d6-5947-4c45-b451-c89384742062"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Kb1"",
                    ""id"": ""0f7dfa75-4599-46d7-bd7f-38979f38b3a3"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""c613f87f-c1d2-4eb1-bf3d-ce3ce48c59e9"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""61b8559f-d040-4301-a789-b79d4c8766e7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""b88d9225-d767-48f3-9bb7-040d69dee108"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""937fcc0c-f7a0-477f-8086-d5608932205f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Gp"",
                    ""id"": ""d1bfc459-4bd0-449d-ad05-7d4bb113746c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e015e65f-57ec-4a77-a88f-3385539db5fb"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""0fe0794b-424e-4339-8eb4-b91659baeb15"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e945505a-d5b6-431a-89da-74ecff15af25"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b8bd7010-9709-4179-9f2d-a08ca5c63548"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Kb2"",
                    ""id"": ""18136251-4e14-486c-a5a9-200f1c0d3c7d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""16546524-de1b-4f43-9274-cb1ae52dcedb"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""859c4568-121f-4773-8c3d-39386d6ce0ce"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c4ba1307-e96e-421f-80a0-5a499127c671"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8fb08c84-7719-4589-adae-b3561918661d"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dir"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c4fd148c-a567-4ab6-b05a-0019388a5593"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a30e87c8-a182-4790-ac6d-550a60203f96"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c9cd1a7e-3655-4dda-98e0-33190a86f4dc"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9ba364f9-dcc9-4592-a06b-745e90ed870a"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89cb3478-d213-4dcf-a4ec-e0fa4f1d7d72"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2e0a7e3a-b1a3-4815-8eaa-145f8ff84f22"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Enter"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0c2e417b-3af5-4d37-a75c-53ae531f0a6d"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ac10cecc-c7c9-4f06-abeb-c04430fd9d23"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65d87832-8c34-4361-889c-88c9464710b0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9e9de31c-be34-4762-9e82-49cea27a3989"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Quit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // MainInput
        m_MainInput = asset.FindActionMap("MainInput", throwIfNotFound: true);
        m_MainInput_Dir = m_MainInput.FindAction("Dir", throwIfNotFound: true);
        m_MainInput_Click = m_MainInput.FindAction("Click", throwIfNotFound: true);
        m_MainInput_Enter = m_MainInput.FindAction("Enter", throwIfNotFound: true);
        m_MainInput_Inventory = m_MainInput.FindAction("Inventory", throwIfNotFound: true);
        m_MainInput_Quit = m_MainInput.FindAction("Quit", throwIfNotFound: true);
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

    // MainInput
    private readonly InputActionMap m_MainInput;
    private IMainInputActions m_MainInputActionsCallbackInterface;
    private readonly InputAction m_MainInput_Dir;
    private readonly InputAction m_MainInput_Click;
    private readonly InputAction m_MainInput_Enter;
    private readonly InputAction m_MainInput_Inventory;
    private readonly InputAction m_MainInput_Quit;
    public struct MainInputActions
    {
        private @InputMaster m_Wrapper;
        public MainInputActions(@InputMaster wrapper) { m_Wrapper = wrapper; }
        public InputAction @Dir => m_Wrapper.m_MainInput_Dir;
        public InputAction @Click => m_Wrapper.m_MainInput_Click;
        public InputAction @Enter => m_Wrapper.m_MainInput_Enter;
        public InputAction @Inventory => m_Wrapper.m_MainInput_Inventory;
        public InputAction @Quit => m_Wrapper.m_MainInput_Quit;
        public InputActionMap Get() { return m_Wrapper.m_MainInput; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainInputActions set) { return set.Get(); }
        public void SetCallbacks(IMainInputActions instance)
        {
            if (m_Wrapper.m_MainInputActionsCallbackInterface != null)
            {
                @Dir.started -= m_Wrapper.m_MainInputActionsCallbackInterface.OnDir;
                @Dir.performed -= m_Wrapper.m_MainInputActionsCallbackInterface.OnDir;
                @Dir.canceled -= m_Wrapper.m_MainInputActionsCallbackInterface.OnDir;
                @Click.started -= m_Wrapper.m_MainInputActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MainInputActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MainInputActionsCallbackInterface.OnClick;
                @Enter.started -= m_Wrapper.m_MainInputActionsCallbackInterface.OnEnter;
                @Enter.performed -= m_Wrapper.m_MainInputActionsCallbackInterface.OnEnter;
                @Enter.canceled -= m_Wrapper.m_MainInputActionsCallbackInterface.OnEnter;
                @Inventory.started -= m_Wrapper.m_MainInputActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_MainInputActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_MainInputActionsCallbackInterface.OnInventory;
                @Quit.started -= m_Wrapper.m_MainInputActionsCallbackInterface.OnQuit;
                @Quit.performed -= m_Wrapper.m_MainInputActionsCallbackInterface.OnQuit;
                @Quit.canceled -= m_Wrapper.m_MainInputActionsCallbackInterface.OnQuit;
            }
            m_Wrapper.m_MainInputActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Dir.started += instance.OnDir;
                @Dir.performed += instance.OnDir;
                @Dir.canceled += instance.OnDir;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Enter.started += instance.OnEnter;
                @Enter.performed += instance.OnEnter;
                @Enter.canceled += instance.OnEnter;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @Quit.started += instance.OnQuit;
                @Quit.performed += instance.OnQuit;
                @Quit.canceled += instance.OnQuit;
            }
        }
    }
    public MainInputActions @MainInput => new MainInputActions(this);
    public interface IMainInputActions
    {
        void OnDir(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnEnter(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnQuit(InputAction.CallbackContext context);
    }
}
