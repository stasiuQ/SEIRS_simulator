namespace Seirs.Models
{
    public enum Instruction
    {
        CreateSimulation = 0,
        DeleteSimulation = 1,
        ProceedSimulation = 2,
        ChangeParameters = 3
    }

    public static class InstructionExtensions{
       public static int ToInt(this Instruction toConvert)
       {
           return (int) toConvert;
       }
   }
}
