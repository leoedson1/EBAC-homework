using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using System;

namespace Ebac.StateMachine
{
    public class StateMachine<T> where T : System.Enum
    {
        public Dictionary<T, StateBase> dictionaryState;

        private StateBase _currentState;
        public float timeToStartGame = 1f;

        public StateBase CurrentState
        {
            get {return _currentState; }
        }

        public void Init()
        {
            dictionaryState = new Dictionary<T, StateBase>();
        }

        public void RegisterStates(T typeEnum, StateBase state)
        {
            if(!dictionaryState.ContainsKey(typeEnum))
            {
                dictionaryState.Add(typeEnum, state);
            }
            else
            {
                Debug.LogError("Chave já registrada no dicionário" + typeEnum.ToString());
            }
            //dictionaryState =  new Dictionary<T, StateBase>();   
        }

        public void SwitchState(T state)
        {
            if (_currentState != null) _currentState.OnStateExit();
            _currentState = dictionaryState[state];
            _currentState.OnStateEnter();
        }

        public void Update()
        {
            if (_currentState != null) _currentState.OnStateStay();
        }
    }
}