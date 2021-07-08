using System;

namespace Cake.ServiceFabric.Internal.Utilities
{
    internal static class Guard
    {
        internal static T NotNull<T>(T value, string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }

            return value;
        }

        internal static string NotEmpty(string value, string parameterName)
        {
            if (ReferenceEquals(value, null))
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentNullException(parameterName);
            }
            else if (value.Trim().Length == 0)
            {
                NotEmpty(parameterName, nameof(parameterName));
                throw new ArgumentException($"The string argument '{value}' cannot be empty.");
            }

            return value;
        }
    }
}
