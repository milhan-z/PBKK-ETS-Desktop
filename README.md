# 🎓 ETS PBKK C: SISMA (Sistem Informasi Mahasiswa Desktop Rich UI)

Repositori ini memuat implementasi proyek desktop, jawaban soal ujian, serta rancangan poster infografis untuk **Evaluasi Tengah Semester (ETS)** mata kuliah **Pemrograman Berbasis Kerangka Kerja (PBKK) Kelas C**.

## 👤 Identitas Mahasiswa
- **Nama Lengkap:** Muhammad Hilman Azhar
- **NRP:** 5025241264
- **Kelas:** PBKK C (Tahun 2026)
- **Dosen Pengampu:** Bapak Fajar Baskoro, S.Kom., M.T.
- **Referensi Soal ETS:** [Studi Kasus - Aplikasi Desktop Rich UI (Blogspot Fajar Baskoro)](https://fajarbaskoro.blogspot.com/2026/10/studi-kasus-aplikasi-desktop-rich-ui.html)
- **Halaman Laporan Notion:** [Dokumentasi ETS Notion](https://app.notion.com/p/ETS-Studi-Kasus-Aplikasi-Desktop-Rich-UI-SISMA-4019c0720c5a8182a67bf2f41de8d588)
- **Form Pengumpulan ETS:** [Google Form Evaluasi ETS PBKK](https://docs.google.com/forms/d/e/1FAIpQLSesHiTZeZORmjc0G2PsvOjD_vNoZsJK3yfMPeVUelbAd7m9oA/viewform?usp=sf_link)
- **Dashboard Evaluasi:** [Looker Studio Monitoring Evaluasi](https://lookerstudio.google.com/reporting/6b210206-b019-4eae-b642-90a3acabb008)

---

## 📌 Jawaban Soal ETS

### 1. Perbedaan Aplikasi Desktop dengan Aplikasi Lain & Mengapa Masih Dibutuhkan
- **Aplikasi Desktop:** Berjalan langsung (*native*) di sistem operasi dengan akses perangkat keras penuh (printer ESC-POS, barcode scanner, GPU, memori lokal), latensi komputasi sangat rendah (*microsecond*), serta beroperasi secara mandiri (*offline-first*) menggunakan database lokal tanpa ketergantungan internet.
- **Aplikasi Web:** Berjalan di dalam *sandbox* browser yang dibatasi protokol HTTP dan keamanan browser; sangat bergantung pada koneksi internet dan server cloud.
- **Aplikasi Mobile:** Didesain untuk interaksi layar sentuh satu jendela dan dibatasi oleh konsumsi daya baterai.
- **Mengapa Masih Dibutuhkan?**
  1. *Zero-Downtime Offline Reliability:* Sistem kasir (POS), gardu tol, dan administrasi operasional kampus/rumah sakit tidak boleh berhenti saat koneksi internet terputus.
  2. *Hardware Interfacing:* Integrasi langsung dengan perangkat fisik industri tanpa batasan browser.
  3. *High-Throughput Data Processing:* Pemrosesan dan rendering jutaan baris data di DataGrid dengan virtualisasi GPU.
  4. *Enterprise Data Privacy:* Menjamin kedaulatan data sensitif di server internal tanpa risiko paparan internet terbuka.

### 2. Problem Statement & Definisi Fitur Aplikasi SISMA
- **Problem yang Diselesaikan:**
  1. Pencatatan administrasi mahasiswa yang manual dan terfragmentasi di berbagai spreadsheet mandiri yang rentan redudansi data.
  2. Lambatnya pencarian dan tingginya risiko *human error* (NRP ganda, email cacat, field kosong).
  3. Ketiadaan visibilitas metrik performa akademik (rata-rata IPK, rasio aktif/cuti) secara real-time.
  4. Ketergantungan penuh pada server web kampus yang rawan *down* saat periode FRS/KRS.
- **Fitur Solusi SISMA:**
  1. *Real-time KPI Dashboard:* Kartu statistik otomatis (Total Mahasiswa, Rata-rata IPK, Rasio Status) yang dihitung langsung dari server SQL Server.
  2. *Rich UI Registration Form:* Formulir modern dengan validasi input berlapis (C# logic) sebelum eksekusi ke database.
  3. *Interactive DataGrid:* Tabel data mahasiswa multi-kolom dengan sorting otomatis dan visual badge status.
  4. *Live Multi-Parameter Search:* Filter instan berdasarkan NRP, Nama, maupun Program Studi.
  5. *Full Persistent CRUD:* Operasi Create, Read, Update (dengan proteksi kunci primary key NRP), dan Delete berbasis SQL Server LocalDB.
  6. *Parameterized Query & Auto-Provisioning:* Proteksi terhadap SQL Injection dan auto-create database/tabel saat startup.

### 3. Rancangan Desain Poster Infografis 1 Halaman
*(Rancangan struktur 13 kotak terstandarisasi untuk dibuat mandiri oleh mahasiswa di Canva/Figma):*
- **Dimensi & Rasio:** A4/A3 Portrait (1024 × 1536 px, 300 DPI).
- **Palet Warna:** *Deep Navy* (`#0F172A`), *Accent Blue* (`#2563EB`), *Emerald Green* (`#059669`), *Amber Gold* (`#D97706`), dan *Light Slate Canvas* (`#F8FAFC`).
- **Pembagian 13 Elemen Poster:**
  1. Header Banner & Identitas Mahasiswa
  2. Kotak 1: Deskripsi Masalah & Solusi SISMA
  3. Kotak 2: Tujuan Pembelajaran (.NET, WPF, XAML, ADO.NET)
  4. Kotak 3: Ikon Teknologi (C# 12, .NET 8, WPF, XAML, SQL Server LocalDB, ADO.NET, SSMS, Git)
  5. Kotak 4: 4 Kartu Fitur Utama (KPI Dashboard, Form Registrasi, DataGrid, SQL Database)
  6. Kotak 5: Menu Navigasi Desktop
  7. Kotak 6: Mockup Dashboard & KPI Metrik
  8. Kotak 7: Mockup Formulir Registrasi Mahasiswa
  9. Kotak 8: Mockup Manajemen Data DataGrid CRUD
  10. Kotak 9: Diagram Alir Penggunaan Sistem (User Flow)
  11. Kotak 10: Struktur Solusi Proyek
  12. Kotak 11: Matriks Skenario Pengujian Sistem
  13. Kotak 12 & 13: Tahapan Pengerjaan & Deliverables ETS

---

## 🚀 Cara Menjalankan Program

### Prasyarat
- Windows 10/11
- .NET SDK versi 8.0 atau 10.0
- SQL Server LocalDB (bawaan Visual Studio)

### Langkah Eksekusi
1. Buka terminal atau Command Prompt pada folder proyek ini:
   ```bash
   cd ETS-Proyek-Aplikasi-Desktop
   ```
2. Jalankan aplikasi:
   ```bash
   dotnet run
   ```
3. Database `DbSismaPBKK` dan tabel `Mahasiswa` akan dibuat secara otomatis (*auto-seed*) saat aplikasi pertama kali dibuka.
