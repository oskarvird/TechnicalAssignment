namespace ConsidTechnicalBackend.Helpers;

public class Calculations : ICalculations
{
    public decimal CalculateSalary(bool isCeo, bool isManager, int rank)
    {
        decimal salaryCoefficient = 1.125m;

        if (isCeo)
        {
            salaryCoefficient = 2.725m;
        }
        else if (isManager)
        {
            salaryCoefficient = 1.725m;
        }

        decimal salary = rank * salaryCoefficient;

        return salary;
    }

}
