
namespace Wpm.SharedKernel.DomainEvents
{
    public class DomainEventDispatcher<T> where T : IDomainEvent
    {
        private List<Action<T>> Actions { get; } = new();

        public void Subscribe(Action<T> action)
        {
            if(Actions.Exists(x => x.Method == action.Method))
            {
                return;
            }
            Actions.Add(action);
        }

        public void Publish(T domainEvent)
        {
            foreach (var action in Actions)
            {
                action.Invoke(domainEvent);
            }
        }
    }
}
