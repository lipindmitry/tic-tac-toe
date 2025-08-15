using System;
using UnityEngine;

[Serializable]
public class GameBoardSettings
{
    [SerializeField] private int _size;
    public int Size => _size;
}

