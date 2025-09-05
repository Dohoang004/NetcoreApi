using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mymvc.Models;

[Table("Hethongphanphoi")]
public class Hethongphanphoi
{
    [Key]
    public string MaHTPP { get; set; }
    public string? TenHTPP { get; set; }
}