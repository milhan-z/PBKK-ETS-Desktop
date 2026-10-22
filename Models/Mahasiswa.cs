using System;

namespace SISMA.Desktop.Models
{
    public class Mahasiswa
    {
        public int Id { get; set; }
        public string Nrp { get; set; } = string.Empty;
        public string Nama { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Prodi { get; set; } = string.Empty;
        public int Angkatan { get; set; }
        public double Ipk { get; set; }
        public string Status { get; set; } = "Aktif";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
