namespace Seirs.Models
{
    public class Stats
    {
        private const int SizeParams = 6;
        public int S { get; set; }
        public int E { get; set; }
        public int I { get; set; }
        public int R { get; set; }
        public int Step { get; set; }
        
        public int[] ToIntArray()
        {
            return new int[]
            {
                SizeParams, S, E, I, R, Step
            };
        }
        
        
    }
}