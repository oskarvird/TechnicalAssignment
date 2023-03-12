namespace ConsidTechnicalBackend.Helpers;

public interface IHelper
{
    decimal CalculateSalary(bool isCeo, bool isManager, int rank);
    string AddAcronym(string title);
}
