using Switchers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Pickupable
{
    public class Coin : PickupableItem
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent<IPlayerPickerVisitor>(out var picker))
            {
                picker.PickUp(this);
                Deactivate();
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IPlayerPickerVisitor>(out var picker))
            {
                picker.PickUp(this);
                Deactivate();
            }
        }
    }
}
