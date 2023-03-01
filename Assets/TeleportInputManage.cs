using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleportInputManage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private InputActionAsset actionAsset;
    [SerializeField] private XRRayInteractor rayInteractor;
    [SerializeField] private TeleportationProvider provider;
    private InputAction _thms;
    private bool _isActive;
    void Start()
    {
        rayInteractor.enabled = false;
        var activate = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Activate");
        activate.Enable();
        activate.performed += OnTeleportActivate;
        var cancel = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Teleport Mode Cancel");
        cancel.Enable();
        cancel.performed += OnTeleportCancel;
        _thms = actionAsset.FindActionMap("XRI LeftHand Locomotion").FindAction("Move");
        _thms.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isActive)
            return;
        if (!rayInteractor.enabled)
            return;
        if (_thms.IsPressed())
            return;
        if (!rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            rayInteractor.enabled = false;
            _isActive = false;
            return;
        }
        TeleportRequest request = new TeleportRequest()
        {
            destinationPosition = hit.point,
            //destinationRotation = ,

        };
        provider.QueueTeleportRequest(request);
        rayInteractor.enabled = false;
        _isActive = false;

    }
    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Exited");
            Application.Quit();
        }
    }
    private void OnTeleportActivate(InputAction.CallbackContext context)
    {
        if (!_isActive)
        {
            rayInteractor.enabled = true;
            _isActive = true;
        }
    }
    private void OnTeleportCancel(InputAction.CallbackContext context)
    {
        if (_isActive && rayInteractor.enabled == true)
        {
            rayInteractor.enabled = false;
            _isActive = false;
        }
    }
}
