using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public enum CONTROLS
    {
        W_HOLD, A_HOLD, S_HOLD, D_HOLD,
        CTRL_TAP, SHIFT_HOLD, 
        SPACE_TAP,
        RIGHTCLICK_TAP
    }
    private List<CONTROLS> controlsActivated = new List<CONTROLS>();

    public List<CONTROLS> ControlsActivated { get { return controlsActivated; } }

    public void SetInput()
    {
        controlsActivated = new List<CONTROLS>();

        if (Input.GetKey(KeyCode.W))
            controlsActivated.Add(CONTROLS.W_HOLD);
        if (Input.GetKey(KeyCode.S))
            controlsActivated.Add(CONTROLS.S_HOLD);
        if (Input.GetKey(KeyCode.A))
            controlsActivated.Add(CONTROLS.A_HOLD);
        if (Input.GetKey(KeyCode.D))
            controlsActivated.Add(CONTROLS.D_HOLD);

        if (Input.GetKeyDown(KeyCode.LeftControl))
            controlsActivated.Add(CONTROLS.CTRL_TAP);
        if (Input.GetKey(KeyCode.LeftShift))
            controlsActivated.Add(CONTROLS.SHIFT_HOLD);

        if (Input.GetKeyDown(KeyCode.Space))
            controlsActivated.Add(CONTROLS.SPACE_TAP);
        if (Input.GetMouseButtonDown(1))
            controlsActivated.Add(CONTROLS.RIGHTCLICK_TAP);
    }
}
