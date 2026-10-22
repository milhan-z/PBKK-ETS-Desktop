using System;
using Microsoft.Data.SqlClient;

namespace TutorMate.Repositories
{
    public static class DatabaseConfig
    {
        private static string? _cachedConnectionString;
        private static readonly object _lock = new object();

        public static string ConnectionString
        {
            get
            {
                if (_cachedConnectionString != null)
                {
                    return _cachedConnectionString;
                }

                lock (_lock)
                {
                    if (_cachedConnectionString != null)
                    {
                        return _cachedConnectionString;
                    }

                    // Urutan deteksi: localhost (standar repo tim), LocalDB (standar Visual Studio), titik (.)
                    string[] candidates = new[]
                    {
                        "Server=(localdb)\\MSSQLLocalDB;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;",
                        "Server=localhost;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;",
                        "Server=.;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;Connect Timeout=2;"
                    };

                    foreach (var connStr in candidates)
                    {
                        try
                        {
                            using var conn = new SqlConnection(connStr);
                            conn.Open();
                            _cachedConnectionString = connStr;
                            return _cachedConnectionString;
                        }
                        catch
                        {
                            // Lanjut ke kandidat server berikutnya
                        }
                    }

                    // Fallback default jika belum terkoneksi
                    _cachedConnectionString = "Server=(localdb)\\MSSQLLocalDB;Database=TutorMateDB;Trusted_Connection=True;TrustServerCertificate=True;";
                    return _cachedConnectionString;
                }
            }
        }
    }
}
