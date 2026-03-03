using System;
using UnityEditor;
using UnityEngine;

public sealed class TurnManager 
{
    public int CurrentTurn
    {
        get => _currentTurn;
        private set
        {
            int prevTurn = _currentTurn;
            _currentTurn = value;
            if (prevTurn != _currentTurn) {
                OnTurnChanged?.Invoke(_currentTurn, _currentTurn-prevTurn);
            }
        }
    }

    private int _currentTurn;
    private float turnInterval = 3f;
    private float currentTime = 0;

    public event Action<int, int> OnTurnChanged; // current, delta

    public void InitTurn() {
        _currentTurn = 1;
        OnTurnChanged?.Invoke(_currentTurn,0);
    }

    public void RaiseTurn(int turnDelta=1) {
        CurrentTurn += turnDelta;
    }

    internal void UpdateTurn(float _, float deltaTime) {
        currentTime += deltaTime;
        if (currentTime >= turnInterval) {
            currentTime -= turnInterval;
            RaiseTurn();
        }
        //CurrentTurn = (int)(currentTime / turnInterval) + 1;
    }
}
