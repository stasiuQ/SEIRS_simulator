using System.Collections.Generic;

namespace Seirs.Models
{
    public class BatchStats
    
    {
        public int Size { get; set; } = 0;
        public Queue<int[]> Stats { get; set; } = new Queue<int[]>();

        public void Add(int[] stats)
        {
            Stats.Enqueue(stats);
            Size++;
        }

        public bool CanAdd()
        {
            return Size < 100;
        }

        public IEnumerable<int[]> ToEnumerable() => Stats;
    }
}