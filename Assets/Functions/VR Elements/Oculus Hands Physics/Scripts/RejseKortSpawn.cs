using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class RejseKortSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    public XRController rightHand;
    public InputHelpers.Button button;

    void Update()
    {

        bool pressed;
        rightHand.inputDevice.IsPressed(button, out pressed);

        if (pressed)
        {
            Debug.Log("Hello - " + button);
        }
    }
}
