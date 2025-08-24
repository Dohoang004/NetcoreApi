using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mymvc.Models
{
    [Table("Persons")]
public class Person
{
    public string PersonId { get; set; }
    public string FirstName { get; set; }
    public string Address { get; set;}
}
}