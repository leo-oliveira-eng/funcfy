using Funcfy.Extensions;

namespace Funcfy.Monads.Extensions;

internal static class MessageExtensions
{
    internal static bool RepresentsAnError(this Message message)
        => message.Type.GetCategory().Equals("Error", StringComparison.OrdinalIgnoreCase);
}
