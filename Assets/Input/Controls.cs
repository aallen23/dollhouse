// GENERATED AUTOMATICALLY FROM 'Assets/Input/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""93896cf1-02fa-4369-a0aa-e85eb454740e"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Value"",
                    ""id"": ""aea14bc6-f1fc-4324-bc40-e0d733855ccd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Value"",
                    ""id"": ""410fd26b-6e54-4998-959b-196552f8dddf"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk"",
                    ""type"": ""Button"",
                    ""id"": ""87b17ba3-958b-4170-96cf-6cb1c03a8746"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pan"",
                    ""type"": ""Value"",
                    ""id"": ""cff38082-01a8-4e60-a9e5-e36cb1dee12a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""92eec82e-eac1-4524-a5b5-788c236235b7"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""de55f098-ca02-4029-a5c5-22bd06e2cf1d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d956e8b-0980-464d-a295-ee23baaab0d1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""51c601f3-c55e-43ec-aa50-36f69218c567"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9c8b434a-0a57-40e9-a278-55a6e676a62f"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9e84f4f2-8673-4cc0-9165-50f4ecfc1ab7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""ca154dc4-59db-466a-9445-bc72afd881c5"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""8aa16626-a624-41f8-afdf-f21b027ed2c6"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pan"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Child"",
            ""id"": ""c2cbb2db-db79-47d6-9978-9b23d4991f6b"",
            ""actions"": [
                {
                    ""name"": ""FPCamera"",
                    ""type"": ""Value"",
                    ""id"": ""2e49fda5-2d2d-48b5-b4b2-71cecfb2c355"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c2fcd82e-2878-4593-bd38-dc75b8734476"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ray"",
                    ""type"": ""Value"",
                    ""id"": ""cf371c91-7fd2-4864-818a-9d739ff089a9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""8a092bc3-2334-41af-93b1-48e339eda5bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.1)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""914d0828-5b5d-4f5e-9f15-d7043873502e"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""FPCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""8b1b402a-f2a7-4eba-a686-09b0e1df4d63"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e6f26bd1-3e23-47a6-bc7a-a587e658fab6"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""57d7e920-0aa7-4c38-a1d4-373c3f27a13c"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e1aeb0c9-1658-4202-8755-1b7316c5267f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""62532cc0-6849-46f6-8a76-5ffe7334ef20"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3da1042c-7a2b-49ec-b8f7-034f80e39368"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ray"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9eef9118-2738-4101-9ad0-113d13752a1e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""PointToPoint"",
            ""id"": ""b1502389-6581-479e-9ecc-91626decf2a5"",
            ""actions"": [
                {
                    ""name"": ""RotRight"",
                    ""type"": ""Button"",
                    ""id"": ""64b40e4a-8906-495c-9ac7-131296693f36"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotLeft"",
                    ""type"": ""Button"",
                    ""id"": ""cf7be491-b2f2-4288-b40f-0c576624f912"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveForward"",
                    ""type"": ""Button"",
                    ""id"": ""0eeeb6fe-8f2d-496f-b6ad-1720ad4f76b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveBackward"",
                    ""type"": ""Button"",
                    ""id"": ""3c25d6bc-7167-4596-8f8f-721790f6f44c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveLeft"",
                    ""type"": ""Button"",
                    ""id"": ""bbf6b374-ffd1-4b67-a18b-8d52e6121d28"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MoveRight"",
                    ""type"": ""Button"",
                    ""id"": ""5ea95700-4f57-4c47-b1a9-c4a536c1268f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""PassThrough"",
                    ""id"": ""f330c7d8-af73-4c83-b998-f62a25faf94b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""c5fa0ba2-072e-44be-bed7-6b6665061512"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""db06567d-adf0-41d0-905e-cae5aaaf107b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cry"",
                    ""type"": ""Button"",
                    ""id"": ""c25db633-88d8-450a-b50b-aeb73ca87b0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fc20edc7-7cc7-4eaa-9e9a-fac7d9e5304e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""988ac27d-8db9-4456-88fa-77c60d8d4bd6"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5e6fd7d-dd5b-4f98-ac08-4e5d7bb01c8a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""53ff502a-5ef9-4ac2-87f9-044ccbe82db7"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""98fbea13-58ad-4917-86ec-9b04ad4b8f11"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""67ead853-8b8a-42ff-a7bb-2ab2e0894e8e"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveForward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""493a4277-534a-4522-82ae-15feccf5ce69"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d9d0adc-d3b2-49b2-a95a-22c0577744e4"",
                    ""path"": ""<Gamepad>/dpad/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveBackward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e1203065-c1d6-495b-8943-2614b404e69a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88085478-9553-4658-a91e-2d3102507558"",
                    ""path"": ""<VirtualMouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""795484c2-a086-46c9-a5f8-602f3584ea1f"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6e8f21c3-da18-4176-a483-5faed2d387cb"",
                    ""path"": ""<VirtualMouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8ab629ef-100b-4036-8540-747ae646839e"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2be8eb56-faf9-41c8-9137-04126c6baa07"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e43ab1bc-14d8-4f18-8f6e-37f33d9ff1be"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75b453b5-84de-436a-b058-6acb75f317aa"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cry"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Rotate = m_Camera.FindAction("Rotate", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        m_Camera_Walk = m_Camera.FindAction("Walk", throwIfNotFound: true);
        m_Camera_Pan = m_Camera.FindAction("Pan", throwIfNotFound: true);
        // Child
        m_Child = asset.FindActionMap("Child", throwIfNotFound: true);
        m_Child_FPCamera = m_Child.FindAction("FPCamera", throwIfNotFound: true);
        m_Child_Movement = m_Child.FindAction("Movement", throwIfNotFound: true);
        m_Child_Ray = m_Child.FindAction("Ray", throwIfNotFound: true);
        m_Child_Zoom = m_Child.FindAction("Zoom", throwIfNotFound: true);
        // PointToPoint
        m_PointToPoint = asset.FindActionMap("PointToPoint", throwIfNotFound: true);
        m_PointToPoint_RotRight = m_PointToPoint.FindAction("RotRight", throwIfNotFound: true);
        m_PointToPoint_RotLeft = m_PointToPoint.FindAction("RotLeft", throwIfNotFound: true);
        m_PointToPoint_MoveForward = m_PointToPoint.FindAction("MoveForward", throwIfNotFound: true);
        m_PointToPoint_MoveBackward = m_PointToPoint.FindAction("MoveBackward", throwIfNotFound: true);
        m_PointToPoint_MoveLeft = m_PointToPoint.FindAction("MoveLeft", throwIfNotFound: true);
        m_PointToPoint_MoveRight = m_PointToPoint.FindAction("MoveRight", throwIfNotFound: true);
        m_PointToPoint_MousePos = m_PointToPoint.FindAction("MousePos", throwIfNotFound: true);
        m_PointToPoint_Interact = m_PointToPoint.FindAction("Interact", throwIfNotFound: true);
        m_PointToPoint_Zoom = m_PointToPoint.FindAction("Zoom", throwIfNotFound: true);
        m_PointToPoint_Cry = m_PointToPoint.FindAction("Cry", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Rotate;
    private readonly InputAction m_Camera_Zoom;
    private readonly InputAction m_Camera_Walk;
    private readonly InputAction m_Camera_Pan;
    public struct CameraActions
    {
        private @Controls m_Wrapper;
        public CameraActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_Camera_Rotate;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputAction @Walk => m_Wrapper.m_Camera_Walk;
        public InputAction @Pan => m_Wrapper.m_Camera_Pan;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotate;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Walk.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnWalk;
                @Walk.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnWalk;
                @Walk.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnWalk;
                @Pan.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnPan;
                @Pan.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnPan;
                @Pan.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnPan;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Walk.started += instance.OnWalk;
                @Walk.performed += instance.OnWalk;
                @Walk.canceled += instance.OnWalk;
                @Pan.started += instance.OnPan;
                @Pan.performed += instance.OnPan;
                @Pan.canceled += instance.OnPan;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Child
    private readonly InputActionMap m_Child;
    private IChildActions m_ChildActionsCallbackInterface;
    private readonly InputAction m_Child_FPCamera;
    private readonly InputAction m_Child_Movement;
    private readonly InputAction m_Child_Ray;
    private readonly InputAction m_Child_Zoom;
    public struct ChildActions
    {
        private @Controls m_Wrapper;
        public ChildActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @FPCamera => m_Wrapper.m_Child_FPCamera;
        public InputAction @Movement => m_Wrapper.m_Child_Movement;
        public InputAction @Ray => m_Wrapper.m_Child_Ray;
        public InputAction @Zoom => m_Wrapper.m_Child_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Child; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ChildActions set) { return set.Get(); }
        public void SetCallbacks(IChildActions instance)
        {
            if (m_Wrapper.m_ChildActionsCallbackInterface != null)
            {
                @FPCamera.started -= m_Wrapper.m_ChildActionsCallbackInterface.OnFPCamera;
                @FPCamera.performed -= m_Wrapper.m_ChildActionsCallbackInterface.OnFPCamera;
                @FPCamera.canceled -= m_Wrapper.m_ChildActionsCallbackInterface.OnFPCamera;
                @Movement.started -= m_Wrapper.m_ChildActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ChildActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ChildActionsCallbackInterface.OnMovement;
                @Ray.started -= m_Wrapper.m_ChildActionsCallbackInterface.OnRay;
                @Ray.performed -= m_Wrapper.m_ChildActionsCallbackInterface.OnRay;
                @Ray.canceled -= m_Wrapper.m_ChildActionsCallbackInterface.OnRay;
                @Zoom.started -= m_Wrapper.m_ChildActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_ChildActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_ChildActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_ChildActionsCallbackInterface = instance;
            if (instance != null)
            {
                @FPCamera.started += instance.OnFPCamera;
                @FPCamera.performed += instance.OnFPCamera;
                @FPCamera.canceled += instance.OnFPCamera;
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Ray.started += instance.OnRay;
                @Ray.performed += instance.OnRay;
                @Ray.canceled += instance.OnRay;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public ChildActions @Child => new ChildActions(this);

    // PointToPoint
    private readonly InputActionMap m_PointToPoint;
    private IPointToPointActions m_PointToPointActionsCallbackInterface;
    private readonly InputAction m_PointToPoint_RotRight;
    private readonly InputAction m_PointToPoint_RotLeft;
    private readonly InputAction m_PointToPoint_MoveForward;
    private readonly InputAction m_PointToPoint_MoveBackward;
    private readonly InputAction m_PointToPoint_MoveLeft;
    private readonly InputAction m_PointToPoint_MoveRight;
    private readonly InputAction m_PointToPoint_MousePos;
    private readonly InputAction m_PointToPoint_Interact;
    private readonly InputAction m_PointToPoint_Zoom;
    private readonly InputAction m_PointToPoint_Cry;
    public struct PointToPointActions
    {
        private @Controls m_Wrapper;
        public PointToPointActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @RotRight => m_Wrapper.m_PointToPoint_RotRight;
        public InputAction @RotLeft => m_Wrapper.m_PointToPoint_RotLeft;
        public InputAction @MoveForward => m_Wrapper.m_PointToPoint_MoveForward;
        public InputAction @MoveBackward => m_Wrapper.m_PointToPoint_MoveBackward;
        public InputAction @MoveLeft => m_Wrapper.m_PointToPoint_MoveLeft;
        public InputAction @MoveRight => m_Wrapper.m_PointToPoint_MoveRight;
        public InputAction @MousePos => m_Wrapper.m_PointToPoint_MousePos;
        public InputAction @Interact => m_Wrapper.m_PointToPoint_Interact;
        public InputAction @Zoom => m_Wrapper.m_PointToPoint_Zoom;
        public InputAction @Cry => m_Wrapper.m_PointToPoint_Cry;
        public InputActionMap Get() { return m_Wrapper.m_PointToPoint; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PointToPointActions set) { return set.Get(); }
        public void SetCallbacks(IPointToPointActions instance)
        {
            if (m_Wrapper.m_PointToPointActionsCallbackInterface != null)
            {
                @RotRight.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnRotRight;
                @RotRight.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnRotRight;
                @RotRight.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnRotRight;
                @RotLeft.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnRotLeft;
                @RotLeft.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnRotLeft;
                @RotLeft.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnRotLeft;
                @MoveForward.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveForward;
                @MoveForward.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveForward;
                @MoveForward.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveForward;
                @MoveBackward.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveBackward;
                @MoveBackward.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveBackward;
                @MoveBackward.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveBackward;
                @MoveLeft.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveLeft;
                @MoveLeft.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveLeft;
                @MoveRight.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveRight;
                @MoveRight.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveRight;
                @MoveRight.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMoveRight;
                @MousePos.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnMousePos;
                @Interact.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnInteract;
                @Zoom.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnZoom;
                @Cry.started -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnCry;
                @Cry.performed -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnCry;
                @Cry.canceled -= m_Wrapper.m_PointToPointActionsCallbackInterface.OnCry;
            }
            m_Wrapper.m_PointToPointActionsCallbackInterface = instance;
            if (instance != null)
            {
                @RotRight.started += instance.OnRotRight;
                @RotRight.performed += instance.OnRotRight;
                @RotRight.canceled += instance.OnRotRight;
                @RotLeft.started += instance.OnRotLeft;
                @RotLeft.performed += instance.OnRotLeft;
                @RotLeft.canceled += instance.OnRotLeft;
                @MoveForward.started += instance.OnMoveForward;
                @MoveForward.performed += instance.OnMoveForward;
                @MoveForward.canceled += instance.OnMoveForward;
                @MoveBackward.started += instance.OnMoveBackward;
                @MoveBackward.performed += instance.OnMoveBackward;
                @MoveBackward.canceled += instance.OnMoveBackward;
                @MoveLeft.started += instance.OnMoveLeft;
                @MoveLeft.performed += instance.OnMoveLeft;
                @MoveLeft.canceled += instance.OnMoveLeft;
                @MoveRight.started += instance.OnMoveRight;
                @MoveRight.performed += instance.OnMoveRight;
                @MoveRight.canceled += instance.OnMoveRight;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
                @Cry.started += instance.OnCry;
                @Cry.performed += instance.OnCry;
                @Cry.canceled += instance.OnCry;
            }
        }
    }
    public PointToPointActions @PointToPoint => new PointToPointActions(this);
    public interface ICameraActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnWalk(InputAction.CallbackContext context);
        void OnPan(InputAction.CallbackContext context);
    }
    public interface IChildActions
    {
        void OnFPCamera(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnRay(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IPointToPointActions
    {
        void OnRotRight(InputAction.CallbackContext context);
        void OnRotLeft(InputAction.CallbackContext context);
        void OnMoveForward(InputAction.CallbackContext context);
        void OnMoveBackward(InputAction.CallbackContext context);
        void OnMoveLeft(InputAction.CallbackContext context);
        void OnMoveRight(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
        void OnCry(InputAction.CallbackContext context);
    }
}
