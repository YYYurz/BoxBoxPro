using System;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace BB
{
    public class InputComponent : GameFrameworkComponent
    {
        private int inputCount = 0;
        private bool inputStatus = false;

        private void Start()
        {
            inputCount = 0;
            inputStatus = false;
        }

        private void Update()
        {
            MoveInput();
            DanceInput();
        }

        private void MoveInput()
        {
            if (Input.GetKey(KeyCode.W))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Move, 0f, 1f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Move, 0f, -1f));
            }
            if (Input.GetKey(KeyCode.A))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Move, -1f, 0f));
            }
            if (Input.GetKey(KeyCode.D))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Move, 1f, 0f));
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                inputCount++;
                inputStatus = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                inputCount++;
                inputStatus = true;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                inputCount++;
                inputStatus = true;
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                inputCount = inputCount < 0 ? ++inputCount : 0;
                inputStatus = true;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                inputCount = inputCount < 0 ? ++inputCount : 0;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                inputCount = inputCount < 0 ? ++inputCount : 0;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                inputCount = inputCount < 0 ? ++inputCount : 0;
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                inputCount = inputCount < 0 ? ++inputCount : 0;
            }

            if (!inputStatus)
            {
                return;
            }

            if (inputCount == 0)
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Move, 0f, 0f));
                inputStatus = false;
            }
        }

        private void DanceInput()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Dance));
            }
        }
    }
}