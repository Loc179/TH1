namespace TH1.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; } // Mật khẩu
        public Branch? Branch { get; set; } //Ngành học
        public Gender? Gender { get; set; }// Giới tính
        public bool IsRegular { get; set; } // Hệ: true-chính quy, false-phi cq
        public string? Address { get; set; } // Địa chỉ
        public DateTime DateOfBorth { get; set; } // Ngày sinh
    }
}
