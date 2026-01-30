using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;

namespace AuthDesktop.UI.Controls;

public class KeyValueBlock : TemplatedControl
{
    /// <summary>
    /// Define <see cref="Key"/>
    /// </summary>
    public static readonly StyledProperty<string> KeyProperty =
        AvaloniaProperty.Register<KeyValueBlock, string>(nameof(Key), defaultBindingMode: BindingMode.TwoWay,
            defaultValue: string.Empty);

    /// <summary>
    /// Define <see cref="Key"/>
    /// </summary>
    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<KeyValueBlock, string>(nameof(Value), defaultBindingMode: BindingMode.TwoWay,
            defaultValue: string.Empty);

    /// <summary>
    /// Key
    /// </summary>
    public string Key
    {
        get => GetValue(KeyProperty);
        set => SetValue(KeyProperty, value);
    }
    
    /// <summary>
    /// Key
    /// </summary>
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
}
