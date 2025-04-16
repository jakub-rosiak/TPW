using System.Windows.Input;

namespace ViewModel;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _action;
    private readonly Func<object?, bool> _canExecute;

    public RelayCommand(Action<object?> action, Func<object?, bool> canExecute = null)
    {
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _canExecute = canExecute;
    }
    
    public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;
    public void Execute(object? parameter) => _action?.Invoke(parameter);

    public event EventHandler? CanExecuteChanged;
    
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}