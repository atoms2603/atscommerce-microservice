﻿namespace AtsCommerce.Core.Errors
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message)
            : base(message)
        {
        }
    }
}
