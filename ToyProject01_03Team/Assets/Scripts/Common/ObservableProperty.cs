using System;

public class ObservableProperty<T> where T : struct
{
    private Action<T>   _onValueChanged;
    private T           _value;
    public T            Value
    {
        get => _value;
        set
        {
            if (_value.Equals(value)) return;

            _value = value;
            _onValueChanged?.Invoke(_value);
        }
    }

    public void AddListener(Action<T> listener)         => _onValueChanged += listener;
    public void RemoveListener(Action<T> listener)      => _onValueChanged -= listener;
    public void RemoveAllListeners()                    => _onValueChanged = null;
}
