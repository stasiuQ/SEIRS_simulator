namespace Seirs.Models
{
    public class Parameters
    {
        private const double SizeParams = 11;
        public readonly double Size = 100;
        public double Dt {get; set;}
        public double Infected { get; set; }
        public double Radius { get; set; }
        public double Mobility { get; set; }
        public double Concentration { get; set; }
        public double Beta { get; set; }
        public double Epshilon { get; set; }
        public double Mu { get; set; }
        public double Rho { get; set; }

        public double[] ToDoubleArray()
        {
            return new double[] {SizeParams, Dt, Size, Infected, Radius, Mobility, Concentration, Beta, Epshilon, Mu, Rho};
        }
    }
}