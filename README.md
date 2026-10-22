# 🎓 ETS PBKK C: TutorMate - Sistem Pendataan & Manajemen Tutor Akademik

Repositori ini memuat implementasi proyek desktop, jawaban soal ujian, serta rancangan poster infografis untuk **Evaluasi Tengah Semester (ETS)** mata kuliah **Pemrograman Berbasis Kerangka Kerja (PBKK) Kelas C**. Proyek ini dikembangkan oleh **Kelompok 8 PBKK C**.

---

## 👥 Identitas Pengembang (Kelompok 8 - PBKK C)
1. **Muhammad Hilman Azhar** — NRP: `5025241264` *(Ketua / Full-stack Developer)*
2. **A. Wildan Kevin Assyauqi** — NRP: `5025241265` *(System Analyst & UI/UX)*
3. **Al Jad Kaukabudduri Hardianto** — NRP: `5025241248` *(Database & Backend Developer)*

- **Dosen Pengampu:** Bapak Fajar Baskoro, S.Kom., M.T.
- **Referensi Soal ETS:** [Studi Kasus - Aplikasi Desktop Rich UI (Blogspot Fajar Baskoro)](https://fajarbaskoro.blogspot.com/2026/10/studi-kasus-aplikasi-desktop-rich-ui.html)
- **Halaman Laporan Notion:** [Dokumentasi Lengkap ETS Kelompok 8 di Notion](https://app.notion.com/p/ETS-Studi-Kasus-Aplikasi-Desktop-Rich-UI-SISMA-4019c0720c5a8182a67bf2f41de8d588)
- **Form Pengumpulan ETS:** [Google Form Evaluasi ETS PBKK](https://docs.google.com/forms/d/e/1FAIpQLSesHiTZeZORmjc0G2PsvOjD_vNoZsJK3yfMPeVUelbAd7m9oA/viewform?usp=sf_link)
- **Dashboard Monitoring:** [Looker Studio Monitoring Evaluasi PBKK](https://lookerstudio.google.com/reporting/6b210206-b019-4eae-b642-90a3acabb008)

---

## 📌 Jawaban Soal ETS

### 1. Perbedaan Aplikasi Desktop dengan Aplikasi Lain & Mengapa Masih Dibutuhkan
- **Aplikasi Desktop:** Berjalan langsung (*native execution*) di atas sistem operasi dengan akses penuh ke perangkat keras (CPU multicore, GPU rendering, memori lokal, driver printer, webcam, periferal barcode). Memiliki latensi komputasi sangat rendah (*microsecond-level latency*), serta beroperasi secara mandiri (*offline-first*) menggunakan database lokal (seperti SQL Server LocalDB) tanpa ketergantungan jaringan internet.
- **Aplikasi Web:** Berjalan di dalam lingkungan terisolasi (*sandbox*) browser yang dibatasi oleh protokol HTTP/HTTPS dan model keamanan peramban. Sangat rentan terhadap latensi jaringan (*network latency*), pemutusan koneksi server kampus, dan beban konkurensi tinggi.
- **Aplikasi Mobile:** Didesain untuk interaksi layar sentuh perangkat bergerak dengan restriksi ketat terhadap konsumsi daya baterai, manajemen memori background, dan ukuran tampilan yang ringkas.

#### Mengapa Aplikasi Desktop Masih Sangat Dibutuhkan?
1. **Zero-Downtime & Offline Reliability:** Sistem administrasi akademik, laboratorium, kasir (POS), dan operasional krusial tidak boleh terhenti saat koneksi internet kampus mengalami gangguan.
2. **High-Performance Data Processing & Rich Visualization:** Mampu merender ribuan baris data pada *DataGrid* secara instan dengan virtualisasi UI perangkat keras (*hardware-accelerated rendering*) tanpa *lag* browser.
3. **Native Hardware & Peripherals Interfacing:** Mengintegrasikan perangkat pemindai kartu pintar RFID, printer struk kasir, kamera verifikasi ujian, dan periferal lokal lainnya tanpa dependensi API web yang terbatas.
4. **Enterprise Data Privacy & Security:** Memastikan data internal dan kualifikasi akademik tersimpan aman di jaringan lokal/server internal tanpa risiko kebocoran di internet publik.

---

### 2. Problem Statement & Definisi Fitur Aplikasi TutorMate
Berdasarkan dokumen PRD (*Product Requirement Document*) Kelompok 8, mahasiswa seringkali menghadapi kesulitan mencari tutor sebaya (*peer-to-peer tutoring*) untuk mata kuliah tingkat lanjut karena informasi yang tersebar acak di grup chat, ketiadaan verifikasi kualifikasi nilai, serta bentrok jadwal bimbingan.

#### Problem yang Diselesaikan:
1. **Informasi Tersebar & Tidak Terstruktur:** Ketiadaan repositori terpusat untuk profil tutor yang kompeten di setiap mata kuliah.
2. **Ketiadaan Validasi Kualifikasi Akademik:** Mahasiswa ragu terhadap kompetensi tutor karena tidak ada bukti nilai huruf minimal (misal: syarat nilai A/AB).
3. **Kesulitan Sinkronisasi Jadwal:** Pengaturan jadwal yang manual lewat chat sering menyebabkan bentrok waktu bimbingan.
4. **Ketergantungan pada Jaringan Internet Terbuka:** Kebutuhan administrasi bimbingan di laboratorium departemen yang membutuhkan akses cepat dan responsif tanpa terpengaruh internet lemot.

#### Definisi Fitur Aplikasi TutorMate:
1. **Multi-Role Authentication (Login & Register):** Autentikasi berbasis peran (*Admin*, *Mentor/Tutor*, dan *Peserta*) dengan validasi kredensial NIM dan Password.
2. **Katalog Mata Kuliah Interaktif:** DataGrid komprehensif yang menampilkan daftar mata kuliah, kode MK, dan silabus deskripsi.
3. **Formulir Tambah Mata Kuliah (Admin):** Kemudahan penambahan kurikulum dan mata kuliah baru langsung tersimpan ke basis data.
4. **Pendaftaran & Pengajuan Calon Tutor:** Mahasiswa yang memenuhi kualifikasi dapat mendaftar menjadi tutor dengan melampirkan nilai huruf, hari ketersediaan jadwal, serta topik keahlian spesifik.
5. **DataGrid Daftar Tutor Aktif:** Menampilkan daftar tutor terdaftar, mata kuliah yang diampu, nilai kualifikasi, jadwal bimbingan, dan deskripsi topik.
6. **Smart Multi-Server Connection Fallback:** Mendukung koneksi otomatis ke SQL Server `localhost` maupun SQL Server `(localdb)\MSSQLLocalDB` tanpa konfigurasi manual.
7. **Clean MVVM & Repository Pattern:** Pemisahan kode yang bersih antara antarmuka (XAML Views), logika bisnis (ViewModels), model data (Models), dan query data akses (Repositories).

---

### 3. Rancangan Desain Poster Infografis (1 Halaman A4/A3)
*(Sesuai arahan, rancangan struktur 13 kotak telah dirancang terstandarisasi untuk eksekusi grafis mandiri di Canva/Figma):*

| No. Kotak | Nama Elemen | Konten & Copywriting |
|---|---|---|
| **Header** | Top Banner | Logo TutorMate + Judul Proyek + Identitas Kelompok 8 PBKK C |
| **Kotak 1** | Problem & Solution | Latar belakang kesulitan pencarian tutor sebaya & solusi aplikasi desktop TutorMate |
| **Kotak 2** | Learning Goals | Penguasaan C# 13, .NET 10, WPF XAML, MVVM, dan ADO.NET SQL Server |
| **Kotak 3** | Tech Stack Icons | C#, .NET 10, WPF, SQL Server LocalDB, ADO.NET, Git, Visual Studio |
| **Kotak 4** | 4 Fitur Utama | Multi-role Auth, Katalog Matkul, Pendaftaran Tutor, Manajemen Jadwal |
| **Kotak 5** | Sidebar Navigation | Navigasi modern: List Tutor, Katalog MK, Form Tutor, Tambah MK, Logout |
| **Kotak 6** | Mockup Login Window | Desain login bersih dengan validasi role dan link registrasi |
| **Kotak 7** | Mockup Form Tutor | Formulir pendaftaran tutor dengan dropdown MK, grade, jadwal, & topik |
| **Kotak 8** | Mockup DataGrid | Tabel visual daftar tutor aktif beserta kolom kualifikasi dan jadwal |
| **Kotak 9** | User Journey Flow | Flow: Login ➔ Pilih Role ➔ Akses Katalog / Ajukan Tutor ➔ DataGrid |
| **Kotak 10** | Arsitektur Solusi | Struktur folder: `Models/`, `Views/`, `ViewModels/`, `Repositories/` |
| **Kotak 11** | Matriks Pengujian | Tabel verifikasi: Auth valid, Role separation, DataGrid binding, SQL Insert |
| **Kotak 12 & 13** | Deliverables & Progress | Repo GitHub, PRD PDF Kelompok 8, Database Script SQL, dan Laporan Notion |

---

## 🛠️ Struktur Direktori Proyek

```
ETS-Proyek-Aplikasi-Desktop/
├── Commands/
│   └── RelayCommand.cs            # Implementasi ICommand untuk MVVM data binding
├── Models/
│   ├── Subject.cs                 # Model entitas mata kuliah
│   ├── TutorApplication.cs        # Model entitas pendaftaran bimbingan tutor
│   └── User.cs                    # Model entitas pengguna (NIM, Nama, Role)
├── Repositories/
│   ├── DatabaseConfig.cs          # Smart fallback koneksi SQL Server (localhost & LocalDB)
│   ├── TutorRepository.cs         # ADO.NET query untuk Subject & TutorApplication
│   └── UserRepository.cs          # ADO.NET query untuk autentikasi & registrasi pengguna
├── ViewModels/
│   └── MainViewModel.cs           # Logika interaksi dan state management MVVM
├── Views/
│   ├── LoginWindow.xaml           # Antarmuka login pengguna
│   ├── LoginWindow.xaml.cs        # Code-behind event handler login
│   ├── RegisterWindow.xaml        # Antarmuka pendaftaran akun baru
│   └── RegisterWindow.xaml.cs     # Code-behind registrasi
├── App.xaml                       # Styling global & StartupUri LoginWindow
├── App.xaml.cs                    # Entry point aplikasi WPF
├── MainWindow.xaml                # Antarmuka dashboard utama (Sidebar + DataGrid + Form)
├── MainWindow.xaml.cs             # Logika interaksi navigasi menu
├── TutorMate.csproj               # Spesifikasi proyek .NET 10 WPF & SqlClient
├── TutorMate.sql                  # Skema database DDL dan data awal (seeding)
└── README.md                      # Dokumentasi komprehensif ETS
```

---

## 🚀 Panduan Menjalankan Aplikasi

### 1. Prasyarat
- Sistem Operasi: Windows 10/11
- .NET SDK versi 8.0 atau 10.0
- SQL Server (LocalDB atau SQL Server Developer/Express pada `localhost`)

### 2. Inisialisasi Database
Jalankan skrip `TutorMate.sql` menggunakan SQL Server Management Studio (SSMS) atau via terminal `sqlcmd`:
```powershell
sqlcmd -S "(localdb)\MSSQLLocalDB" -Q "CREATE DATABASE TutorMateDB;"
sqlcmd -S "(localdb)\MSSQLLocalDB" -d TutorMateDB -i TutorMate.sql
```

### 3. Akun Pengguna Uji Coba (Demo Credentials)
Aplikasi telah dilengkapi dengan data akun awal yang dapat langsung digunakan untuk pengujian:
| NIM | Nama | Password | Role |
|---|---|---|---|
| `5025241264` | Muhammad Hilman Azhar | `hilman123` | **Mentor** |
| `5025241265` | A. Wildan Kevin Assyauqi | `wildan123` | **Peserta** |
| `5025241248` | Al Jad Kaukabudduri Hardianto | `Alt.Kbdr323` | **Mentor** |
| `admin` | Administrator TutorMate | `admin123` | **Admin** |

### 4. Eksekusi Program
Buka terminal pada folder proyek dan jalankan:
```powershell
dotnet run
```
Aplikasi akan membuka jendela **LoginWindow**. Masukkan salah satu kredensial di atas untuk masuk ke **MainWindow**.

---
*Dibuat untuk memenuhi Evaluasi Tengah Semester (ETS) PBKK C Tahun 2026.*
