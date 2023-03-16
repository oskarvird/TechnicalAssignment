namespace ConsidTechnicalBackend.Helpers;

public class Helper : IHelper
{
    // All helpers is "business logic" gatherd here, could easily be called anywhere in the code with DI
    public decimal CalculateSalary(bool isCeo, bool isManager, int rank)
    {
        decimal salary = 0m;
        decimal salaryCoefficient = 1.125m;

        if (isCeo)
        {
            salaryCoefficient = 2.725m;
        }
        else if (isManager)
        {
            salaryCoefficient = 1.725m;
        }


        if (rank >= 0 && rank <= 10)
        {
            salary = rank * salaryCoefficient;
        }

        return salary;
    }
    public int CalculateRank(bool isCeo, bool isManager, decimal salary)
    {
        int rank = 0;
        decimal salaryCoefficient = 1.125m;

        if (isCeo)
        {
            salaryCoefficient = 2.725m;
        }
        else if (isManager)
        {
            salaryCoefficient = 1.725m;
        }


        rank = (int)Math.Floor(salary / salaryCoefficient);


        return rank;
    }
    public string AddAcronym(string title)
    {
        string cleanedTitle = new string(title.Where(char.IsLetter).ToArray());

        string[] splitedTitle = cleanedTitle.Split(' ');

        string acronym = "";

        foreach (string word in splitedTitle)
        {
            if (word.Length > 0)
            {
                acronym += word[0];
            }
        }

        return acronym.ToUpper();
    }
}