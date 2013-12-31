using System;

namespace Balanced.Exceptions
{
    public class BalancedException : Exception
    {

        public BalancedError BalancedServiceError { get; private set; }

        public BalancedException(BalancedError error)
        {
            BalancedServiceError = error;
        }
    }
}
