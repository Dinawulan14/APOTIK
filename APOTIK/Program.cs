using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace APOTIK
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Program pr = new Program();
            while (true)
            {
                try
                {
                    Console.WriteLine("Koneksi Ke Database\n");
                    Console.Write("Masukkan User ID : ");
                    string user = Console.ReadLine();
                    Console.Write("Masukkan Password : ");
                    string pass = Console.ReadLine();
                    Console.Write("Masukkan databse tujuan :");
                    string db = Console.ReadLine();
                    Console.Write("\nKetik K untuk Terhubung ke Database: ");
                    char chr = Convert.ToChar(Console.ReadLine());
                    switch (chr)
                    {
                        case 'K':
                            {
                                SqlConnection conn = null;
                                string strKoneksi = "Data source = LAPTOP-CE2MB8Q1\\DINAWULAN; " +
                                    "initial catalog = {0}; " +
                                    "User ID = {1}; password = {2}";
                                conn = new SqlConnection(string.Format(strKoneksi, db, user, pass));
                                conn.Open();
                                Console.Clear();
                                while (true)
                                {
                                    try
                                    {
                                        Console.WriteLine("\nMenu");
                                        Console.WriteLine("1. Melihat Seluruh Data");
                                        Console.WriteLine("2. Tambah Data");
                                        Console.WriteLine("3. Ubah Data");
                                        Console.WriteLine("4. Delete Data");
                                        Console.WriteLine("5. Cari Data");
                                        Console.WriteLine("6. Kel");
                                        Console.Write("\nEnter your choice (1-3): ");
                                        char ch = Convert.ToChar(Console.ReadLine());
                                        switch (ch)
                                        {
                                            case '1':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("APOTE\n");
                                                    Console.WriteLine();
                                                    pr.baca(conn);
                                                }
                                                break;
                                            case '2':
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("INPUT APOTEKER\n");
                                                    Console.WriteLine("Masukkan ID Apoteker : ");
                                                    string ID_Apoteker = Console.ReadLine();
                                                    Console.WriteLine("Masukkan nama Apoteker :");
                                                    string Nama_Apoteker = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Alamat Apoteker:");
                                                    string Alamat_Apoteker = Console.ReadLine();
                                                    Console.WriteLine("Masukkan Jenis Kelamin (L/P) :");
                                                    string Jenis_Kelamin = Console.ReadLine();
                                                    Console.WriteLine("Masukkan No HP :");
                                                    string No_Hp= Console.ReadLine();
                                                    Console.WriteLine("Masukkan Tgl Lahir :");
                                                    string Tgl_Lahir = Console.ReadLine();

                                                    try
                                                    {
                                                        pr.insert(ID_Apoteker, Nama_Apoteker, Alamat_Apoteker, Jenis_Kelamin, No_Hp, Tgl_Lahir, conn);
                                                        conn.Close();
                                                    }
                                                    catch
                                                    {
                                                        Console.WriteLine("\nAnda tidak memiliki " + "akses untuk menambah data");
                                                    }
                                                }
                                                break;
                                            case '3':
                                                conn.Close();
                                                return;
                                            default:
                                                {
                                                    Console.Clear();
                                                    Console.WriteLine("\nInvalid option");
                                                }
                                                break;

                                        }
                                    }
                                    catch
                                    {
                                        Console.WriteLine("\nCheck for the value entered.");
                                    }
                                }
                            }
                        default:
                            {
                                Console.WriteLine("\nInvalid option");
                            }
                            break;
                    }
                }
                catch
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Tidak Dapat Mengakses Database Menggunakan User Tersebut\n");
                    Console.ResetColor();
                }
            }
        }

        public void baca(SqlConnection con)
        {
            SqlCommand cmd = new SqlCommand("Select * From dbo.Apoteker", con);
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                for (int i = 0; i < r.FieldCount; i++)
                {
                    Console.WriteLine(r.GetValue(i));
                }
                Console.WriteLine();
            }
            r.Close();
        }
        public void insert(string ID_Apoteker , string Nama_Apoteker, string Alamat_Apoteker, string Jenis_Kelamin, string No_Hp, string Tgl_Lahir, SqlConnection con)
        {
            string str = "";
            str = "insert into dbo.Apoteker (ID_Apoteker,Nama_Apoteker,Alamat_Apoteker,Jenis_Kelamin,No_Hp,Tgl_Lahir)" + "values(@idapoteker,@namaapoteker,@alamat,@JK,@Phn,@tgllahir)";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add(new SqlParameter("idapoteker", ID_Apoteker));
            cmd.Parameters.Add(new SqlParameter("namaapoteker", Nama_Apoteker));
            cmd.Parameters.Add(new SqlParameter("alamat", Alamat_Apoteker));
            cmd.Parameters.Add(new SqlParameter("JK", Jenis_Kelamin));
            cmd.Parameters.Add(new SqlParameter("tgllahir", Tgl_Lahir));
            cmd.Parameters.Add(new SqlParameter("Phn", No_Hp));

            cmd.ExecuteNonQuery();
            Console.WriteLine("Data Berhasil Ditambahkan");
        }
    }
}
