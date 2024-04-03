namespace AtsCommerce.Core.Errors
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message)
            : base(message)
        {
        }
    }
}
