﻿using System.ComponentModel.DataAnnotations;

namespace ConsidTechnicalBackend.Models;

public class EmployeeResponse
{
    public int Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public int Rank { get; set; }

    public bool IsCEO { get; set; }

    public bool IsManager { get; set; }

    public int? ManagerId { get; set; }

}
