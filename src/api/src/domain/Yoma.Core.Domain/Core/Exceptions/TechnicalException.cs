namespace Yoma.Core.Domain.Exceptions
{
    public class TechnicalException : Exception
    {
        public TechnicalException(string message) : base(message)
        {
        }
    }
}