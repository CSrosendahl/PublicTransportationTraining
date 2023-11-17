using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics leftControllerCharacteristics;
    public InputDeviceCharacteristics rightControllerCharacteristics;
    private InputDevice leftController;
    private InputDevice rightController;
    public Animator leftHandAnimator;
    public Animator rightHandAnimator;
    public GameObject phone;     // Reference to the phone GameObject (in your left hand)
    public GameObject skoleKort; // Reference to the skoleKort GameObject (in your right hand)

    void Start()
    {
        TryInitialize();
        skoleKort.SetActive(false);
        phone.SetActive(false);
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        // Initialize the left controller
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            leftController = devices[0];
        }

        // Initialize the right controller
        devices.Clear();
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devices);
        if (devices.Count > 0)
        {
            rightController = devices[0];
        }
    }

    void UpdateHandAnimation(InputDevice device, Animator handAnimator)
    {
        if (device.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);


            if (triggerValue > 0.1f)
            {
                // Check which hand the device belongs to and activate/deactivate accordingly
                if (handAnimator == leftHandAnimator)
                {
                    phone.SetActive(true);
                }
                else if (handAnimator == rightHandAnimator)
                {
                    skoleKort.SetActive(true);
                }
            }
            else
            {
                // Check which hand the device belongs to and activate/deactivate accordingly
                if (handAnimator == leftHandAnimator)
                {
                    phone.SetActive(false);
                }
                else if (handAnimator == rightHandAnimator)
                {
                    skoleKort.SetActive(false);
                }
            }
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!leftController.isValid || !rightController.isValid)
        {
            TryInitialize();
        }
        else
        {
            UpdateHandAnimation(leftController, leftHandAnimator);
            UpdateHandAnimation(rightController, rightHandAnimator);
        }
    }
}
