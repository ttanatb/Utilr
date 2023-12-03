using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utilr.Structs;

namespace Utilr.Utility
{
    public static partial class Helper
    {
        /// <summary>
        /// Utility function to invoke an action after a specified amount of time.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static IEnumerator ExecuteAfter(this UnityAction action, float seconds)
        {
            yield return new WaitForSeconds(seconds);
            action.Invoke();
        }
        
        /// <summary>
        /// Executes an action in the next frame
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerator ExecuteNextFrame(this UnityAction action)
        {
            yield return new WaitForEndOfFrame();
            action.Invoke();
        }

        /// <summary>
        /// A coroutine to interpolate between two values using a specific curve.
        /// </summary>
        /// <param name="animData"></param>
        /// <param name="lerpFunc"></param>
        /// <param name="assignmentFunc"></param>
        /// <param name="onCoroutineCompleted"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerator LerpOverTime<T>(LerpAnimData<T> animData, 
            System.Func<T,T,float,T> lerpFunc, 
            System.Action<T> assignmentFunc,
            System.Action onCoroutineCompleted)
        {
            for (float timer = 0; timer < animData.Duration; timer += Time.deltaTime)
            {
                float percent = timer / animData.Duration;
                float sampledValue = animData.Curve.Evaluate(percent);
                var res = lerpFunc.Invoke(animData.InitialValue, animData.FinalValue, sampledValue);
                assignmentFunc.Invoke(res);
                
                yield return new WaitForEndOfFrame();
            }
            
            assignmentFunc.Invoke(lerpFunc.Invoke(animData.InitialValue, animData.FinalValue, 1.0f));
            onCoroutineCompleted?.Invoke();
        }
    }
}
