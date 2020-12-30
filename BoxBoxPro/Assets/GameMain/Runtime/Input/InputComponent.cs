using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;


namespace BB
{
    public class InputComponent : GameFrameworkComponent
    {
        /// <summary>
        /// 摇杆输入
        /// </summary>
        /// <returns> 返回的偏移量 </returns>
        // public Vector2 OnJoyStick()
        // {
        //     
        // }
        private void Update()
        {
            if (Input.GetKey(KeyCode.W))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(0f, 1f));
            }
            if (Input.GetKey(KeyCode.S))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(0f, -1f));
            }
            if (Input.GetKey(KeyCode.A))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(-1f, 0f));
            }
            if (Input.GetKey(KeyCode.D))
            {
                GameEntry.Event.Fire(this, InputEventArgs.Create(1f, 0f));
            }
        }
    }
}