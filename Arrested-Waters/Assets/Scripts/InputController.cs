using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputController : MonoBehaviour
{
    public BoatController boat;

    public Button button_keyThrottle;
    public Button button_keyBrake;
    public Button button_keyEbrake;
    public Button button_keySteerLeft;
    public Button button_keySteerRight;

    public Button button_mouseThrottle;
    public Button button_mouseBrake;
    public Button button_mouseEbrake;
    private Button lastButton;

    public Color normalButtonColor;
    public Color disabledButtonColor;

    public KeyCode key_throttle = KeyCode.W;
    public KeyCode key_brake = KeyCode.S;
    public KeyCode key_ebrake = KeyCode.Space;
    public KeyCode key_steerLeft = KeyCode.A;
    public KeyCode key_steerRight = KeyCode.D;

    public int mouse_throttle = 0;
    public int mouse_brake = 1;
    public int mouse_ebrake = 2;

    public bool useMouseSteering = false;
    private bool listenForKeyInput = false;
    private bool listenForMouseInput = false;
    private bool listenForAxisInput = false;

    public void getKeyInput(Button button)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "...";
        listenForKeyInput = true;
        lastButton = button;
    }

    public void getMouseInput(Button button)
    {
        button.GetComponentInChildren<TextMeshProUGUI>().text = "...";
        listenForMouseInput = true;
        lastButton = button;
    }

    public void getAxisInput()
    {
        listenForAxisInput = true;
    }

    public void toggleMouseInput()
    {
        useMouseSteering = !useMouseSteering;
        button_keySteerLeft.interactable = !button_keySteerLeft.interactable;
        button_keySteerRight.interactable = !button_keySteerRight.interactable;
        var colors = button_keySteerLeft.colors;

        if (useMouseSteering)
        {
            colors.normalColor = disabledButtonColor;
            button_keySteerLeft.colors = colors;
            button_keySteerRight.colors = colors;
        }
        else
        {
            colors.normalColor = normalButtonColor;
            button_keySteerLeft.colors = colors;
            button_keySteerRight.colors = colors;
        }
    }

    private string getMouseText(int i)
    {
        if (i == 0)
        {
            return "M1";
        }else if(i == 1)
        {
            return "M2";
        }
        else
        {
            return "M3";
        }
    }

    private void updateButtons()
    {
        button_keyThrottle.GetComponentInChildren<TextMeshProUGUI>().text = key_throttle.ToString();
        button_keyBrake.GetComponentInChildren<TextMeshProUGUI>().text = key_brake.ToString();
        button_keyEbrake.GetComponentInChildren<TextMeshProUGUI>().text = key_ebrake.ToString();
        button_keySteerLeft.GetComponentInChildren<TextMeshProUGUI>().text = key_steerLeft.ToString();
        button_keySteerRight.GetComponentInChildren<TextMeshProUGUI>().text = key_steerRight.ToString();

        button_mouseThrottle.GetComponentInChildren<TextMeshProUGUI>().text = getMouseText(mouse_throttle);
        button_mouseBrake.GetComponentInChildren<TextMeshProUGUI>().text = getMouseText(mouse_brake);
        button_mouseEbrake.GetComponentInChildren<TextMeshProUGUI>().text = getMouseText(mouse_ebrake);
    }

    private void updateKeycode(KeyCode k)
    {
        if(lastButton == button_keyThrottle)
        {
            key_throttle = k;
        }else if(lastButton == button_keyBrake)
        {
            key_brake = k;
        }else if(lastButton == button_keyEbrake)
        {
            key_ebrake = k;
        }else if(lastButton == button_keySteerLeft)
        {
            key_steerLeft = k;
        }else if(lastButton == button_keySteerRight)
        {
            key_steerRight = k;
        }
    }

    private void updateMouseInput(int i)
    {
        if(lastButton == button_mouseThrottle)
        {
            mouse_throttle = i;
        }else if(lastButton == button_mouseBrake)
        {
            mouse_brake = i;
        }else if(lastButton == button_mouseEbrake)
        {
            mouse_ebrake = i;
        }
    }

    private void Start()
    {
        updateButtons();
    }

    private void Update()
    {
        if (listenForKeyInput)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                listenForKeyInput = false;
                updateKeycode(KeyCode.None);
            }

            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(kcode))
                {
                    listenForKeyInput = false;
                    updateKeycode(kcode);
                    updateButtons();
                }
            }
        }

        if (listenForMouseInput)
        {
            for (int i = 0; i < 3; i++)
            {
                if (Input.GetMouseButtonDown(i))
                {
                    listenForMouseInput = false;
                    updateMouseInput(i);
                    updateButtons();
                }
            }
        }
    }

    public void saveConfig()
    {
        if(listenForKeyInput || listenForMouseInput || listenForAxisInput)
        {
            listenForKeyInput = false;
            listenForMouseInput = false;
        }
        else
        {
            boat.updateControls();
        }
    }

}
