
using System.Numerics;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestProject1"), InternalsVisibleTo("WinFormsApp1")]

namespace Lab
{
    class Item : IComparable<Item>
    {
        public int id { get; }
        public int value { get; }
        public int weight { get; }

        public Item(int v, int w, int id)
        {
            this.id = id;
            this.value = v;
            this.weight = w;
        }

        public int CompareTo(Item other)
        {
            double v1 = this.value / this.weight;
            double v2 = other.value / other.weight;

            if (v1 == v2)
                return 0;

            if (v1 > v2)
                return -1;

            return 1;
        }
        public override string ToString()
        {
            return String.Format("value: {0};  weight: {1}", this.value, this.weight);
        }
    }

    class Result
    {
        public int weight { get; set; }
        public int value { get; set; }
        public List<int> items { get; }

        public Result()
        {
            this.items = [];
            this.weight = 0;
            this.value = 0;
        }

        public override string ToString()
        {
            var list = "";
            this.items.ForEach(i => list += i + " ");
            return String.Format("total weight: {0}\ntotal value: {1}\nitems: {2}", this.weight, this.value, list);
        }
    }

    class Problem
    {
        int item_count;
        public List<Item> item_list { get; }

        public Problem(int item_count, int seed)
        {
            this.item_list = [];
            this.item_count = item_count;

            Random rng = new Random(seed);

            for (int i = 0; i < item_count; i++)
            {
                this.item_list.Add(new Item(rng.Next(1, 10), rng.Next(1, 10), i));
            }
        }

        public Result solve(int capacity)
        {
            Result result = new Result();

            this.item_list.Sort();

            foreach (Item i in this.item_list)
            {
                if (i.weight + result.weight > capacity)
                    continue;

                result.weight += i.weight;
                result.value += i.value;
                result.items.Add(i.id);
            }

            return result;
        }

        public override string ToString()
        {
            var res = "";

            this.item_list.ForEach(i => res += i + "\n");
            return res;
        }

    }


    class MyApp
    {
        static void Main()
        {
            Console.WriteLine("Enter Seed: ");
            int seed = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter item count: ");
            int item_count = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter backpack capacity: ");
            int capacity = int.Parse(Console.ReadLine());
            Problem problem = new Problem(item_count, seed);
            Console.WriteLine(problem);
            Result result = problem.solve(capacity);
            Console.WriteLine(result);
        }
    }
}
