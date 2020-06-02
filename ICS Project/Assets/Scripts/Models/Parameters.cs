namespace Seirs.Models
{
    public class Parameters
    {
        private const double SizeParams = 18;
        private const double Dt = 1;
        public readonly double Size = 100;
        public double Radius { get; set; }
        public double Infected { get; set; }
        public double Mobility { get; set; }
        public double Concentration { get; set; }
        public double Beta { get; set; }
        public double Epsilon { get; set; }
        public double Mu { get; set; }
        public double Rho { get; set; }
        public double LinearZones { get; set; }
        public double ProbWearingMask { get; set; }
        public double ProbBeingCourier { get; set; }
        public double ProbHomeInZone { get; set; }
        public double MobilityCouriers { get; set; }
        public double RadiusMask { get; set; }
        public double SpreadingMask { get; set; }
        public double[] ToDoubleArray()
        {
            return new double[]
            {
                SizeParams, Dt, Size, Radius, Mobility, Concentration, Infected, Beta, Epsilon, Mu, Rho, LinearZones,
                ProbWearingMask, ProbBeingCourier, ProbHomeInZone, MobilityCouriers, RadiusMask, SpreadingMask
            };
        }
    }
}