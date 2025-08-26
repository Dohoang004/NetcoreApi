using System;
namespace mymvc.Models;

public class DaiLy
    {
        public string MaDaiLy { get; set; }
        public string TenDaiLy { get; set; }
        public string DiaChi { get; set; }
        public string NguoiDaiDien { get; set; }
        public string DienThoai { get; set; }

        // Khóa ngoại đến HeThongPhanPhoi
        public string MaHTPP { get; set; }
        public Hethongphanphoi Htpp { get; set; }  // Navigation property
    }