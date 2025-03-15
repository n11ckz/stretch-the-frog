using System;
using UnityEngine;

namespace Project
{
    public abstract class BaseCell : MonoBehaviour
    {
        public event Action<BaseCell> Occupied;

        public bool IsOccupied { get; private set; }

        public virtual void Occupy(ICellOccupant occupant)
        {
            IsOccupied = true;
            Occupied?.Invoke(this);
        }
    }
}
