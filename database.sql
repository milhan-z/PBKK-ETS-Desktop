-- ==========================================================
-- SKRIP BASIS DATA PROYEK ETS PBKK C: SISMA (SISTEM INFORMASI MAHASISWA)
-- Mahasiswa: Muhammad Hilman Azhar (NRP: 5025241264)
-- Dosen Pengampu: Fajar Baskoro, S.Kom., M.T.
-- ==========================================================

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DbSismaPBKK')
BEGIN
    CREATE DATABASE DbSismaPBKK;
END
GO

USE DbSismaPBKK;
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Mahasiswa' AND xtype='U')
BEGIN
    CREATE TABLE Mahasiswa (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Nrp VARCHAR(15) NOT NULL UNIQUE,
        Nama NVARCHAR(100) NOT NULL,
        Email NVARCHAR(100) NOT NULL,
        Prodi NVARCHAR(60) NOT NULL,
        Angkatan INT NOT NULL,
        Ipk DECIMAL(3,2) NOT NULL CHECK (Ipk >= 0.00 AND Ipk <= 4.00),
        Status NVARCHAR(20) NOT NULL DEFAULT 'Aktif',
        CreatedAt DATETIME NOT NULL DEFAULT GETDATE()
    );

    INSERT INTO Mahasiswa (Nrp, Nama, Email, Prodi, Angkatan, Ipk, Status) VALUES
    ('5025241264', 'Muhammad Hilman Azhar', 'hilman@mhs.its.ac.id', 'Teknik Informatika', 2024, 3.88, 'Aktif'),
    ('5025241010', 'Ahmad Fauzi Rahman', 'fauzi@mhs.its.ac.id', 'Teknik Informatika', 2024, 3.75, 'Aktif'),
    ('5025241025', 'Budi Santoso', 'budi@mhs.its.ac.id', 'Sistem Informasi', 2024, 3.60, 'Aktif'),
    ('5025241040', 'Dewi Lestari', 'dewi@mhs.its.ac.id', 'Teknik Komputer', 2023, 3.70, 'Cuti');
END
GO

-- Kueri Pemeriksaan Data & Agregasi KPI
SELECT COUNT(*) AS TotalMahasiswa, AVG(Ipk) AS RataRataIpk FROM Mahasiswa;
SELECT * FROM Mahasiswa ORDER BY Nrp ASC;
GO
