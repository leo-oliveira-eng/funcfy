using Funcfy.Types;

namespace Funcfy.Monads.Extensions;

internal static class ActionExtensions
{
    internal static Func<TValue, Unit> WrapAsFunc<TValue>(this Action<TValue> action)
        =>  value => { action(value); return new Unit(); };

    internal static Func<Unit> WrapAsFunc(this Action action)
        => () => { action(); return new Unit(); };
}
