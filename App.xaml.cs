using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TutorMate.Models;
using TutorMate.Views;

namespace TutorMate
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (e.Args.Length > 0 && e.Args[0] == "--capture")
            {
                this.ShutdownMode = ShutdownMode.OnExplicitShutdown;
                RunCapture();
                Shutdown(0);
                return;
            }

            var login = new LoginWindow();
            login.Show();
        }

        private void RunCapture()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string projectDir = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.FullName;
            string targetDirEts = Path.Combine(projectDir, "ETS-Proyek-Aplikasi-Desktop", "Screenshots");
            string targetDirP7 = Path.Combine(projectDir, "Pertemuan-07-PRD-Proyek-Desktop", "Screenshots");

            Directory.CreateDirectory(targetDirEts);
            Directory.CreateDirectory(targetDirP7);

            var navyBrush = new SolidColorBrush(Color.FromRgb(0x2C, 0x3E, 0x50));

            // 1. Capture LoginWindow
            var login = new LoginWindow();
            login.WindowState = WindowState.Normal;
            login.Width = 1000;
            login.Height = 700;
            login.txtNIM.Text = "5025241264";
            login.txtPassword.Password = "hilman123";
            login.Show();
            DoEvents();
            CaptureAndSave(login, "01_login_window.png", targetDirEts, targetDirP7, 1000, 700, navyBrush);
            login.Close();

            // 2. Capture RegisterWindow
            var register = new RegisterWindow();
            register.WindowState = WindowState.Normal;
            register.Width = 1000;
            register.Height = 700;
            register.txtNIM.Text = "5025241265";
            register.txtNama.Text = "A. Wildan Kevin Assyauqi";
            register.txtPassword.Password = "wildan123";
            register.cmbRole.SelectedIndex = 0;
            register.Show();
            DoEvents();
            CaptureAndSave(register, "02_register_window.png", targetDirEts, targetDirP7, 1000, 700, navyBrush);
            register.Close();

            // 3. Capture MainWindow views (Role Mentor)
            User testUser = new User
            {
                UserId = 1,
                Nama = "Muhammad Hilman Azhar",
                NIM = "5025241264",
                Role = "Mentor"
            };

            var main = new MainWindow(testUser);
            main.WindowState = WindowState.Normal;
            main.Width = 1280;
            main.Height = 800;
            main.Show();
            DoEvents();

            // 3a. List Tutor
            main.ShowPanelDaftarTutor();
            DoEvents();
            CaptureAndSave(main, "03_dashboard_list_tutor.png", targetDirEts, targetDirP7, 1280, 800);

            // 3b. Katalog MK
            main.ShowPanelKatalogMK();
            DoEvents();
            CaptureAndSave(main, "04_katalog_mata_kuliah.png", targetDirEts, targetDirP7, 1280, 800);

            // 3c. Form Daftar Tutor
            main.ShowPanelFormDaftarTutor();
            if (main.CmbSubjectList.Items.Count > 0) main.CmbSubjectList.SelectedIndex = 0;
            main.TxtHariJadwal.Text = "Senin & Kamis (19.00 - 21.00 WIB)";
            DoEvents();
            CaptureAndSave(main, "05_form_daftar_tutor.png", targetDirEts, targetDirP7, 1280, 800);

            // 3d. Form Tambah MK
            main.ShowPanelFormTambahMK();
            main.TxtKodeMK.Text = "IF184502";
            main.TxtNamaMK.Text = "Pengembangan Aplikasi Web";
            main.TxtDeskripsiMK.Text = "Pengembangan aplikasi web berbasis ASP.NET Core, RESTful API, dan arsitektur microservices.";
            DoEvents();
            CaptureAndSave(main, "06_form_tambah_mk.png", targetDirEts, targetDirP7, 1280, 800);

            main.Close();

            Console.WriteLine($"[SUCCESS] 6 High-Quality Screenshots saved to {targetDirEts} and {targetDirP7}");
        }

        private static void DoEvents()
        {
            var frame = new System.Windows.Threading.DispatcherFrame();
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Background,
                new System.Windows.Threading.DispatcherOperationCallback(exitFrame =>
                {
                    ((System.Windows.Threading.DispatcherFrame)exitFrame!).Continue = false;
                    return null;
                }), frame);
            System.Windows.Threading.Dispatcher.PushFrame(frame);
            System.Threading.Thread.Sleep(300);
        }

        private static void CaptureAndSave(Window window, string filename, string dir1, string dir2, int width, int height, Brush? backgroundBrush = null)
        {
            window.UpdateLayout();
            Visual visualToRender = (window.Content as Visual) ?? window;

            var rtb = new RenderTargetBitmap(width, height, 96, 96, PixelFormats.Pbgra32);

            var brush = backgroundBrush ?? window.Background ?? new SolidColorBrush(Color.FromRgb(0xEC, 0xF0, 0xF1));
            var drawingVisual = new DrawingVisual();
            using (var dc = drawingVisual.RenderOpen())
            {
                dc.DrawRectangle(brush, null, new Rect(0, 0, width, height));
            }
            rtb.Render(drawingVisual);
            rtb.Render(visualToRender);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            string path1 = Path.Combine(dir1, filename);
            using (var stream1 = new FileStream(path1, FileMode.Create))
            {
                encoder.Save(stream1);
            }

            string path2 = Path.Combine(dir2, filename);
            File.Copy(path1, path2, true);
        }
    }
}
