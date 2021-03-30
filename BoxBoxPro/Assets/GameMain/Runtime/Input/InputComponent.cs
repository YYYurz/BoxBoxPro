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
            var h = Input.GetAxisRaw("Horizontal");
            var v = Input.GetAxisRaw("Vertical");

            if (h != 0f || v != 0f)
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(GameEnum.INPUT_TYPE.Move, h, v));
                inputStatus = true;
            }
            else if (inputStatus)
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