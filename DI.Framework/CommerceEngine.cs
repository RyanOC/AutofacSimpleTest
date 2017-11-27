using DI.Core.Abstractions;

namespace DI.Framework
{
    public class CommerceEngine: ICommerceEngine
    {
        private readonly ICustomerProcessor _customerProcessor;

        public CommerceEngine(ICustomerProcessor customerProcessor)
        {
            _customerProcessor = customerProcessor;
        }

        public void ProcessOrder()
        {
            _customerProcessor.UpdateCustomerOrder(string.Empty, string.Empty);
        }
    }
}