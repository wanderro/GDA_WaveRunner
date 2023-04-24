using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class MovingObstacle : MonoBehaviour
{
   [FormerlySerializedAs("_maxMoveDelta")] [SerializeField] 
   private Vector3 _maxDelta;

   [SerializeField] 
   private float _minMovementDuration;
   
   [SerializeField] 
   private float _maxMovementDuration;

   private float _movementDuration;
   private Vector3 _startState;
   private Vector3 _endState;
   private float _currentTime;

   private void Awake()
   {
      _startState = GetStartState(_maxDelta);
      _endState = GetEndState(_maxDelta);

      _movementDuration = Random.Range(_minMovementDuration, _maxMovementDuration);
      _currentTime = Random.Range(0, _movementDuration);
   }

   private void Update()
   {
      _currentTime += Time.deltaTime;
      
      var pingPongTime = Mathf.PingPong(_currentTime, _movementDuration);
      var progress = pingPongTime / _movementDuration;
      
      var currentState = Vector3.Lerp(_startState, _endState, progress);
      ApplyNewState(currentState);
   }
   
   protected virtual Vector3 GetEndState(Vector3 maxDelta)
   {
      return transform.position + maxDelta;
   }

   protected virtual Vector3 GetStartState(Vector3 maxDelta)
   {
      return transform.position - maxDelta;
   }

   protected virtual void ApplyNewState(Vector3 delta)
   {
      transform.position = delta;
   }
}

public class ScaleObstacle : MovingObstacle
{
   protected override Vector3 GetStartState(Vector3 maxDelta)
   {
      return transform.localScale - maxDelta;
   }
   
   protected override Vector3 GetEndState(Vector3 maxDelta)
   {
      return transform.localScale + maxDelta;
   }
   
   protected override void ApplyNewState(Vector3 newState)
   {
       transform.localScale = newState;
   }
}
