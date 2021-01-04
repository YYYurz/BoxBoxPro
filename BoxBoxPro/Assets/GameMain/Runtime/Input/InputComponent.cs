using UnityEngine;
using UnityGameFramework.Runtime;

namespace BB
{
    public class InputComponent : GameFrameworkComponent
    {
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