using System;

namespace Project
{
    public interface IInput
    {
        public event Action<Direction> DirectionReceived;
    }
}
