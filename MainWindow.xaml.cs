using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using SISMA.Desktop.Data;
using SISMA.Desktop.Models;

namespace SISMA.Desktop
{
    public partial class MainWindow : Window
    {
        private List<Mahasiswa> _allData = new List<Mahasiswa>();
        private Mahasiswa? _selectedMahasiswa = null;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allData = DatabaseHelper.GetAll(txtSearch.Text.Trim());
                dgMahasiswa.ItemsSource = _allData;
                UpdateKpi();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memuat data dari database: {ex.Message}", "Error Database", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateKpi()
        {
            int total = _allData.Count;
            txtKpiTotal.Text = total.ToString();

            if (total > 0)
            {
                double avgIpk = _allData.Average(m => m.Ipk);
                txtKpiIpk.Text = avgIpk.ToString("F2", CultureInfo.InvariantCulture);

                int aktif = _allData.Count(m => m.Status.Equals("Aktif", StringComparison.OrdinalIgnoreCase));
                int cuti = total - aktif;
                txtKpiStatus.Text = $"{aktif} Aktif / {cuti} Cuti";
            }
            else
            {
                txtKpiIpk.Text = "0.00";
                txtKpiStatus.Text = "0 Aktif / 0 Cuti";
            }
        }

        private void dgMahasiswa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgMahasiswa.SelectedItem is Mahasiswa m)
            {
                _selectedMahasiswa = m;
                txtNrp.Text = m.Nrp;
                txtNama.Text = m.Nama;
                txtEmail.Text = m.Email;

                // Select Prodi
                foreach (ComboBoxItem item in cmbProdi.Items)
                {
                    if (item.Content.ToString() == m.Prodi)
                    {
                        cmbProdi.SelectedItem = item;
                        break;
                    }
                }

                // Select Angkatan
                foreach (ComboBoxItem item in cmbAngkatan.Items)
                {
                    if (item.Content.ToString() == m.Angkatan.ToString())
                    {
                        cmbAngkatan.SelectedItem = item;
                        break;
                    }
                }

                txtIpk.Text = m.Ipk.ToString("F2", CultureInfo.InvariantCulture);

                // Select Status
                foreach (ComboBoxItem item in cmbStatus.Items)
                {
                    if (item.Content.ToString() == m.Status)
                    {
                        cmbStatus.SelectedItem = item;
                        break;
                    }
                }

                // Mode Ubah
                txtNrp.IsReadOnly = true;
                txtFormTitle.Text = $"✏️ Mode Ubah Mahasiswa";
                txtFormSubtitle.Text = $"NRP {m.Nrp} terkunci untuk menjaga konsistensi kunci utama";

                btnSimpan.IsEnabled = false;
                btnUpdate.IsEnabled = true;
                btnHapus.IsEnabled = true;
            }
        }

        private void btnSimpan_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput(out string nrp, out string nama, out string email, out string prodi, out int angkatan, out double ipk, out string status))
            {
                return;
            }

            var baru = new Mahasiswa
            {
                Nrp = nrp,
                Nama = nama,
                Email = email,
                Prodi = prodi,
                Angkatan = angkatan,
                Ipk = ipk,
                Status = status,
                CreatedAt = DateTime.Now
            };

            try
            {
                bool success = DatabaseHelper.Insert(baru);
                if (success)
                {
                    MessageBox.Show($"Data mahasiswa '{nama}' berhasil disimpan ke database SQL Server!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal menyimpan data (kemungkinan NRP duplikat): {ex.Message}", "Validasi Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMahasiswa == null) return;

            if (!ValidateInput(out string nrp, out string nama, out string email, out string prodi, out int angkatan, out double ipk, out string status))
            {
                return;
            }

            var updated = new Mahasiswa
            {
                Nrp = _selectedMahasiswa.Nrp,
                Nama = nama,
                Email = email,
                Prodi = prodi,
                Angkatan = angkatan,
                Ipk = ipk,
                Status = status
            };

            try
            {
                bool success = DatabaseHelper.Update(updated);
                if (success)
                {
                    MessageBox.Show($"Data mahasiswa '{nama}' (NRP: {updated.Nrp}) berhasil diperbarui!", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetForm();
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Gagal memperbarui data: {ex.Message}", "Error Update", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnHapus_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedMahasiswa == null) return;

            var result = MessageBox.Show($"Apakah Anda yakin ingin menghapus mahasiswa '{_selectedMahasiswa.Nama}' (NRP: {_selectedMahasiswa.Nrp}) secara permanen dari SQL Server?", "Konfirmasi Hapus", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool success = DatabaseHelper.Delete(_selectedMahasiswa.Nrp);
                    if (success)
                    {
                        MessageBox.Show("Data berhasil dihapus dari database.", "Sukses", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetForm();
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Gagal menghapus data: {ex.Message}", "Error Delete", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetForm();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            txtSearch.Text = "";
            LoadData();
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadData();
        }

        private void ResetForm()
        {
            _selectedMahasiswa = null;
            txtNrp.Text = "";
            txtNrp.IsReadOnly = false;
            txtNama.Text = "";
            txtEmail.Text = "";
            cmbProdi.SelectedIndex = 0;
            cmbAngkatan.SelectedIndex = 3; // 2024
            txtIpk.Text = "3.50";
            cmbStatus.SelectedIndex = 0; // Aktif

            txtFormTitle.Text = "📝 Formulir Entri Mahasiswa";
            txtFormSubtitle.Text = "Silakan isi data mahasiswa di bawah ini";

            btnSimpan.IsEnabled = true;
            btnUpdate.IsEnabled = false;
            btnHapus.IsEnabled = false;

            dgMahasiswa.SelectedItem = null;
            txtNrp.Focus();
        }

        private bool ValidateInput(out string nrp, out string nama, out string email, out string prodi, out int angkatan, out double ipk, out string status)
        {
            nrp = txtNrp.Text.Trim();
            nama = txtNama.Text.Trim();
            email = txtEmail.Text.Trim();
            prodi = (cmbProdi.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "";
            string strAngkatan = (cmbAngkatan.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "2024";
            _ = int.TryParse(strAngkatan, out angkatan);
            status = (cmbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Aktif";

            if (string.IsNullOrWhiteSpace(nrp) || !long.TryParse(nrp, out _))
            {
                MessageBox.Show("NRP wajib diisi berupa kombinasi angka!", "Validasi Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNrp.Focus();
                ipk = 0;
                return false;
            }

            if (string.IsNullOrWhiteSpace(nama) || nama.Length < 3)
            {
                MessageBox.Show("Nama lengkap minimal terdiri dari 3 karakter!", "Validasi Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtNama.Focus();
                ipk = 0;
                return false;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || !email.Contains('.'))
            {
                MessageBox.Show("Format email tidak valid (harus mengandung '@' dan domain '.')!", "Validasi Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                ipk = 0;
                return false;
            }

            string rawIpk = txtIpk.Text.Trim().Replace(',', '.');
            if (!double.TryParse(rawIpk, NumberStyles.Any, CultureInfo.InvariantCulture, out ipk) || ipk < 0.0 || ipk > 4.0)
            {
                MessageBox.Show("IPK harus berupa angka desimal di antara 0.00 hingga 4.00!", "Validasi Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtIpk.Focus();
                return false;
            }

            return true;
        }
    }
}
