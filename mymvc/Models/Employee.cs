using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mymvc.Models
{
   [Table("Employee1")]
public class Employee : Person
{
    public string EmployeeId { get; set; }
    public int Age { get; set;}
}
}
