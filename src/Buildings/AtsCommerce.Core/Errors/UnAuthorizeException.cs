namespace AtsCommerce.Core.Errors
{
    public class UnAuthorizeException : Exception
    {
        public UnAuthorizeException(string message)
            : base(message)
        {
        }
    }
}
