using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pickupable;

namespace Player.Ability
{
    public class ItemPicker : MonoBehaviour, IPlayerPickerVisitor
    {
        public void PickUp(Coin coin)
        {
            Debug.Log("Player pick up Coin!!!!");
        }
    }
}
