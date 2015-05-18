using UnityEngine;
using UnityEngine.UI;
using InControl;
using System.Collections;

public class Menu : MonoBehaviour 
{
    public Text[] menuOptions;
    public Image[] cursor;

    int current;
    bool recentMove;
    float timeBetweenMoves = 0.2f;
    float timeRight;
    float timeLeft;

    bool selectionMade;

    private InputDevice device;
    private InputControl stickControlY;
    private InputControl selectItem;

    void Start()
    {
            device = InputManager.ActiveDevice;
            stickControlY = device.GetControl(InputControlType.LeftStickY);
            selectItem = device.GetControl(InputControlType.Action1);
            current = 0;
            timeLeft = 0.0f;
            selectionMade = false;
            foreach(Text t in menuOptions)
            {
                t.enabled = true;
            }
            UpdateCursorPosition();
    }

    void Update()
    {
        bool changed = false;
        timeLeft += Time.deltaTime;
        timeRight += Time.deltaTime;

        if(timeLeft >= timeBetweenMoves)
        {
            if (stickControlY.Value < -0.1f)
            {
                timeLeft = 0.0f;
                if(current < menuOptions.Length - 1)
                {
                    current++;
                    changed = true;
                }
            }
            else if (stickControlY.Value > 0.1f)
            {
                timeLeft = 0.0f;
                if (current >0)
                {
                    current--;
                    changed = true;
                }
            }

        }

        if(selectItem.IsPressed && !selectionMade)
        {
            selectionMade = true;
            ItemSelected(current);
        }

        if (changed)
        {
            UpdateCursorPosition();
        }
    }

    void UpdateCursorPosition()
    {
        for(int i = 0; i < menuOptions.Length; i++)
        {
            if(i == current)
            {
                cursor[i].enabled = true;
            }
            else
            {
                cursor[i].enabled = false;
            }
        }
    }

    void ItemSelected(int selection)
    {
        /*
         * if(selection == 0)
         * {
         *      //do a thing
         * }
         * etc
         */
    }
}
