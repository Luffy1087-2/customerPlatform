namespace CustomerPlatform.Core.Abstract
{
    public interface ICustomerFactory
    {
        ICustomer Create(string customerType, string jsonString);
    }
}
