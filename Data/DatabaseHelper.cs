using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using SISMA.Desktop.Models;

namespace SISMA.Desktop.Data
{
    public static class DatabaseHelper
    {
        private const string MasterConnString = @"Server=(localdb)\MSSQLLocalDB;Database=master;Integrated Security=True;TrustServerCertificate=True;";
        public const string AppConnString = @"Server=(localdb)\MSSQLLocalDB;Database=DbSismaPBKK;Integrated Security=True;TrustServerCertificate=True;";

        public static void InitializeDatabase()
        {
            try
            {
                using (var conn = new SqlConnection(MasterConnString))
                {
                    conn.Open();
                    string checkDbQuery = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DbSismaPBKK') CREATE DATABASE DbSismaPBKK;";
                    using var cmd = new SqlCommand(checkDbQuery, conn);
                    cmd.ExecuteNonQuery();
                }

                using (var conn = new SqlConnection(AppConnString))
                {
                    conn.Open();
                    string createTableQuery = @"
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
                        END";
                    using var cmd = new SqlCommand(createTableQuery, conn);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initialize database: {ex.Message}");
            }
        }

        public static List<Mahasiswa> GetAll(string? keyword = null)
        {
            var list = new List<Mahasiswa>();
            using var conn = new SqlConnection(AppConnString);
            conn.Open();

            string query = @"
                SELECT Id, Nrp, Nama, Email, Prodi, Angkatan, Ipk, Status, CreatedAt 
                FROM Mahasiswa 
                WHERE (@Keyword IS NULL OR Nrp LIKE @Pattern OR Nama LIKE @Pattern OR Prodi LIKE @Pattern)
                ORDER BY Nrp ASC";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Keyword", string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : keyword);
            cmd.Parameters.AddWithValue("@Pattern", string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%");

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Mahasiswa
                {
                    Id = reader.GetInt32(0),
                    Nrp = reader.GetString(1),
                    Nama = reader.GetString(2),
                    Email = reader.GetString(3),
                    Prodi = reader.GetString(4),
                    Angkatan = reader.GetInt32(5),
                    Ipk = Convert.ToDouble(reader.GetDecimal(6)),
                    Status = reader.GetString(7),
                    CreatedAt = reader.GetDateTime(8)
                });
            }
            return list;
        }

        public static bool Insert(Mahasiswa m)
        {
            using var conn = new SqlConnection(AppConnString);
            conn.Open();

            string query = @"
                INSERT INTO Mahasiswa (Nrp, Nama, Email, Prodi, Angkatan, Ipk, Status, CreatedAt)
                VALUES (@Nrp, @Nama, @Email, @Prodi, @Angkatan, @Ipk, @Status, @CreatedAt);";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nrp", m.Nrp);
            cmd.Parameters.AddWithValue("@Nama", m.Nama);
            cmd.Parameters.AddWithValue("@Email", m.Email);
            cmd.Parameters.AddWithValue("@Prodi", m.Prodi);
            cmd.Parameters.AddWithValue("@Angkatan", m.Angkatan);
            cmd.Parameters.AddWithValue("@Ipk", Convert.ToDecimal(m.Ipk));
            cmd.Parameters.AddWithValue("@Status", m.Status);
            cmd.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Update(Mahasiswa m)
        {
            using var conn = new SqlConnection(AppConnString);
            conn.Open();

            string query = @"
                UPDATE Mahasiswa 
                SET Nama = @Nama, Email = @Email, Prodi = @Prodi, Angkatan = @Angkatan, Ipk = @Ipk, Status = @Status
                WHERE Nrp = @Nrp;";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nrp", m.Nrp);
            cmd.Parameters.AddWithValue("@Nama", m.Nama);
            cmd.Parameters.AddWithValue("@Email", m.Email);
            cmd.Parameters.AddWithValue("@Prodi", m.Prodi);
            cmd.Parameters.AddWithValue("@Angkatan", m.Angkatan);
            cmd.Parameters.AddWithValue("@Ipk", Convert.ToDecimal(m.Ipk));
            cmd.Parameters.AddWithValue("@Status", m.Status);

            return cmd.ExecuteNonQuery() > 0;
        }

        public static bool Delete(string nrp)
        {
            using var conn = new SqlConnection(AppConnString);
            conn.Open();

            string query = "DELETE FROM Mahasiswa WHERE Nrp = @Nrp;";
            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Nrp", nrp);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
