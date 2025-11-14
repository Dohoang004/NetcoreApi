using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mymvc.Models
{
    [Table("Daily")]
    public class Daily
    {
        [Key]
        public string MaDaiLy { get; set; }
        public string TenDaiLy { get; set; }
        public string DiaChi  { get; set; }
        public string NguoiDaiDien  { get; set; }
        public string DienThoai  { get; set; }
        public string MaHTPP  { get; set; }
    }
}