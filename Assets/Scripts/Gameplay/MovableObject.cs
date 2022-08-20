using System;
using System.Collections;
using Data.ResourceData;
using UnityEngine;

namespace Gameplay
{
    public class MovableObject : MonoBehaviour
    {
        private Transform _currentParent;
        private Vector3 _currentNextPosition;
        private Vector3 _currentWorldStartPosition;
        private float _currentStartTime;
        private Coroutine _lastCoroutine;
        
        private const float MovingTime = 0.5f;
        
        public ResourceName ResourceName { get; private set; }

        public void Init(ResourceName resourceName)
        {
            ResourceName = resourceName;
        }
      
        public void SetLocalPosition(Vector3 position, Transform parent, Action<MovableObject> onMoveEnd = null)
        {
            transform.parent = parent;
            _currentWorldStartPosition = transform.position;
            _currentNextPosition = position;
            _currentParent = parent;
            _currentStartTime = Time.time;

            if(_lastCoroutine != null)
                StopCoroutine(_lastCoroutine);
            _lastCoroutine = StartCoroutine(Move(onMoveEnd));
        }

        private IEnumerator Move(Action<MovableObject> onMoveEnd)
        {
            while (true)
            {
                if ((transform.localPosition - _currentNextPosition).sqrMagnitude < 0.0001f)
                {
                    onMoveEnd?.Invoke(this);
                    yield break;
                }

                transform.position = Vector3.Lerp(_currentWorldStartPosition,
                    _currentParent.TransformPoint(_currentNextPosition),
                    (Time.time - _currentStartTime) / MovingTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}