namespace DI.Core.Abstractions
{
    public interface ICustomerProcessor
    {
        void UpdateCustomerOrder(string customer, string product);
    }
}
