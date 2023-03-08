namespace ConsidTechnicalBackend.Helpers;

public interface ICalculations
{
    decimal CalculateSalary(bool isCeo, bool isManager, int rank);
}
