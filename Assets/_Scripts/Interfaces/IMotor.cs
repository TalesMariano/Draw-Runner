using System;
using System.Collections.Generic;
using UnityEngine;


public interface IMotor
{
    void SetForce(float force);

    void SetSpeed(float speed);
}
