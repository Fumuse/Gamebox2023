using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public static StateMachine instance = null;
    
    private List<IState> _gameStages = new()
    {
        new StartState(),
        new AfterBuyMillState(),
        new AfterBuyBakeState(),
    };

    private IState _currentState;

    private IState CurrentState
    {
        get => _currentState;
        set
        {
            _currentState = value;
            SaveSerial.State = _currentState.StateMnemonic;
        }
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else if (instance == this) Destroy(gameObject);
        
        DontDestroyOnLoad(gameObject);

        LoadStart();
    }

    public void LoadStart()
    {
        if (SaveSerial.State == null)
        {
            ChangeState("start");
            return;
        }

        foreach (IState state in _gameStages)
        {
            if (CurrentState != null)
            {
                CurrentState.BeforeStateCloseAction();
            }
        
            state.OnStateSetAction();

            _currentState = state;
            
            if (state.StateMnemonic == SaveSerial.State)
            {
                break;
            }
        }
    }

    [CanBeNull]
    private IState FindByStateMnemonic(string mnemonic)
    {
        foreach (IState state in _gameStages)
        {
            if (state.StateMnemonic.Equals(mnemonic))
                return state;
        }

        return null;
    }

    public void ChangeState(string mnemonic)
    {
        if (CurrentState != null)
        {
            CurrentState.BeforeStateCloseAction();
        }

        IState stateByMnemonic = FindByStateMnemonic(mnemonic);
        if (stateByMnemonic != null)
        {
            stateByMnemonic.OnStateSetAction();
            CurrentState = stateByMnemonic;
        }
    }

    public void ChangeStateNext()
    {
        if (CurrentState == null) return;
        if (CurrentState.NextState == null) return;
        
        CurrentState.BeforeStateCloseAction();
        CurrentState.NextState.OnStateSetAction();
        
        CurrentState = CurrentState.NextState;
    }
}
