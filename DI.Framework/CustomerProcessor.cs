using System;
using DI.Core.Abstractions;

namespace DI.Framework
{
    public class CustomerProcessor: ICustomerProcessor
    {
        public void UpdateCustomerOrder(string customer, string product)
        {
            Console.WriteLine("Order updated");
        }
    }
}
