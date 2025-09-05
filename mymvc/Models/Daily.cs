using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace mymvc.Models;

[Table("Daily1")]
public class Daily
{
    [Key]
    public string MaDaiLy { get; set; }
    public string? TenDaiLy { get; set; }
    public string? DiaChi { get; set; }
    public string? NguoiDaiDien { get; set; }
    public string? DienThoai { get; set; }

    // Khóa ngoại đến HeThongPhanPhoi
    [ForeignKey("Hethongphanphoi")]
    public string MaHTPP { get; set; }
    //public Hethongphanphoi Hethongphanphoi { get; set; }=new Hethongphanphoi();
    }