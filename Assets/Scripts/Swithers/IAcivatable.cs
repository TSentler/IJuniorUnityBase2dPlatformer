using UnityEngine.Events;

namespace Switchers
{
    public interface IActivatable
    {
        bool IsActive { get; }

        void Activate();
        void Deactivate();
    }

    public interface IObservableActivated : IActivatable
    {
        event UnityAction Activated;
        event UnityAction Deactivated;
    }
}