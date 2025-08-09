using System;
using UnityEngine;

namespace Project
{
    [SelectionBase]
    public abstract class BaseCell : MonoBehaviour
    {
        public event Action<BaseCell> Occupied;

        public bool IsOccupied { get; private set; }

        public virtual void Occupy(ICellOccupant cellOccupant)
        {
            IsOccupied = true;
            Occupied?.Invoke(this);
        }
    }
}
