using CartAPI.Domain.Consts;
using System;

namespace CartAPI.Infrastructure.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; }
        public CustomException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
    public class CustomerNotFoundException : CustomException
    {
        public CustomerNotFoundException(string message = ErrorMessages.CUSTOMER_NOT_FOUND, int statusCode = 400) : base(message, statusCode) { }
    }
    public class CustomerNotCreatedException : CustomException
    {
        public CustomerNotCreatedException(string message = ErrorMessages.CUSTOMER_NOT_CREATED, int statusCode = 400) : base(message, statusCode) { }

    }    
    public class CustomerAlreadyExistException : CustomException
    {
        public CustomerAlreadyExistException(string message = ErrorMessages.CUSTOMER_ALREADY_EXIST, int statusCode = 400) : base(message, statusCode) { }
    }
    public class ProductNotCreatedException : CustomException
    {
        public ProductNotCreatedException(string message = ErrorMessages.PRODUCT_NOT_CREATED, int statusCode = 400) : base(message, statusCode) { }
    }
    public class ProductNotFoundException : CustomException
    {
        public ProductNotFoundException(string message = ErrorMessages.PRODUCT_NOT_FOUND, int statusCode = 400) : base(message, statusCode) { }
    }
    public class ItemAlreadyIntheCartException : CustomException
    {
        public ItemAlreadyIntheCartException(string message = ErrorMessages.ITEM_ALREADY_EXIST_IN_THE_CART, int statusCode = 400) : base(message, statusCode) { }
    }
    public class CartNotFoundException : CustomException
    {
        public CartNotFoundException(string message = ErrorMessages.CART_NOT_FOUND, int statusCode = 400) : base(message, statusCode) { }
    }
    public class CartNotCreatedException : CustomException
    {
        public CartNotCreatedException(string message = ErrorMessages.CART_NOT_CREATED, int statusCode = 400) : base(message, statusCode) { }
    }

}
