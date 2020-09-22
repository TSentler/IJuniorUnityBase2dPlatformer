using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tools
{

    public static class Random
    {
        /// <summary>
        /// Return a random Vector2 between min[inclusive] and max[inclusive] axis
        /// </summary>
        public static Vector2 Range(Vector2 min, Vector2 max)
        {
            return new Vector2(
                UnityEngine.Random.Range(min.x, max.x),
                UnityEngine.Random.Range(min.y, max.y));
        }

        /// <summary>
        /// Return a random Vector3 between min[inclusive] and max[inclusive] axis
        /// </summary>
        public static Vector3 Range(Vector3 min, Vector3 max)
        {
            return new Vector3(
                UnityEngine.Random.Range(min.x, max.x),
                UnityEngine.Random.Range(min.y, max.y),
                UnityEngine.Random.Range(min.z, max.z));
        }
    }

    public static class Extension
    {
        public static void VerifyNotNull<T>(this object targetObject, string parameterName, string message = "")
                           where T : class
        {
            if (targetObject == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }

        public static void VerifyNotNull<T>(this T? targetObject, string parameterName, string message = "")
                           where T : struct
        {
            if (targetObject == null)
            {
                throw new ArgumentNullException(parameterName, message);
            }
        }
    }
}
