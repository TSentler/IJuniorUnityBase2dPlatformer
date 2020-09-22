using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Ability
{
    [RequireComponent(typeof(IPlayerCore))]
    public abstract class Ability : SerializedMonoBehaviour
    {
        protected IPlayerCore Core { get; private set; }

        protected virtual void Awake()
        {
            Core = GetComponent<IPlayerCore>();
        }
    }
}