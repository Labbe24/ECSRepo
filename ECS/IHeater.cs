using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public interface IHeater
    {
        void TurnOn();
        void TurnOff();
        bool RunSelfTest();
    }
}
