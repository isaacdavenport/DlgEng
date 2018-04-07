
using DialogEngine.Core;
using Stateless;

namespace DialogEngine.Extensions
{
    public static class StateMachineExtensions
    {
        public static RelayCommand CreateCommand<TState, TTrigger>(this StateMachine<TState, TTrigger> stateMachine, TTrigger trigger)
        {
            return new RelayCommand
              (
                _execute => stateMachine.Fire(trigger),
                _canExecute => stateMachine.CanFire(trigger)
              );
        }
    }
}
