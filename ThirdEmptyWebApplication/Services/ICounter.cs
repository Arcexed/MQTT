using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThirdEmptyWebApplication.Services
{
    public interface ICounter
    {
        int Value { get; }

    }

    public class RandomCounter : ICounter
    {
        private int value;
        private static Random rnd = new Random();

        public RandomCounter()
        {
            value = rnd.Next(0,1000000);
        }

        public int Value
        {
            get
            {
                return value;
            }
        }
    }

    public class CounterService
    {
        public ICounter Counter { get; set; }

        public CounterService(ICounter counter)
        {
            Counter = counter;
        }

    }
}
