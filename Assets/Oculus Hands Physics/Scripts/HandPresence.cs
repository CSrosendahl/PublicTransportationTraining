using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public InputDeviceCharacteristics controllerCharacteristics;    
    private InputDevice targetDeviceRight; // Controller for right hand
    private InputDevice targetDeviceLeft; // Controller for left hand
    public Animator rightHandAnimator;
    public Animator leftHandAnimator;
    public GameObject skoleKort; // Object triggered by right hand (e.g., school card)
    public GameObject phoneObject; // Object triggered by left hand (e.g., phone)

    void Start()
    {
        TryInitialize();
        skoleKort.SetActive(false);
        phoneObject.SetActive(false);
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        foreach (var device in devices)
        {
            if (device.characteristics.HasFlag(InputDeviceCharacteristics.Right))
            {
                targetDeviceRight = device;
            }
            else if (device.characteristics.HasFlag(InputDeviceCharacteristics.Left))
            {
                targetDeviceLeft = device;
            }
        }
    }

    void UpdateHandAnimation()
    {
        UpdateObjectActivation(targetDeviceRight, rightHandAnimator, skoleKort, CommonUsages.trigger);
        UpdateObjectActivation(targetDeviceLeft, leftHandAnimator, phoneObject, CommonUsages.grip);
    }

    void UpdateObjectActivation(InputDevice device, Animator animator, GameObject obj, InputFeatureUsage<float> usage)
    {
        if (device == null || !device.isValid)
        {
            return;
        }

        if (device.TryGetFeatureValue(usage, out float value))
        {
            animator.SetFloat(usage.name, value);

            if (usage == CommonUsages.trigger && value > 0.1f)
            {
                obj.SetActive(true);
            }
            else if (usage == CommonUsages.grip && value > 0.5f)
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(false);
            }
        }
        else
        {
            animator.SetFloat(usage.name, 0);
            obj.SetActive(false);
        }
    }

    void Update()
    {
        UpdateHandAnimation();
    }
}