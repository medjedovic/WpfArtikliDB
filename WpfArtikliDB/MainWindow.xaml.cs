using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfArtikliDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            clsArtikal a = new clsArtikal("Plazma", 100);

            clsRacun r1 = new clsRacun();

            //r1.artikli.Add(a);
            //r1.artikli.Add(a);

            clsRacun r2 = new clsRacun();
            //r2.artikli.Add(a);
            //r2.artikli.Add(a);

            clsArtikli_racun ar = new clsArtikli_racun();
            ar.a = a;
            ar.artID = a.sifra;
            ar.r = r1;
            ar.racunID = r2.rbr;
            ar.kolicina = 5;



            db baza = new db();
            baza.racunDS.Add(r1);
            baza.racunDS.Add(r2);
            baza.SaveChanges();


        }
    }

    public class db:DbContext
    {
        public db():base (@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=dbArtikli;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
            {}
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<clsArtikal>().HasKey(a => a.sifra)
                                             .HasMany(a => a.racuni)
                                             .WithMany(r => r.artikli)
                                             .Map(art =>
                                             {
                                                 art.MapLeftKey("artikal_idFK");
                                                 art.MapRightKey("racun_idFK");
                                                 art.ToTable("artikli_racun");

                                             });
            modelBuilder.Entity<clsRacun>().HasKey(r => r.rbr);
            modelBuilder.Entity<clsArtikli_racun>().HasKey(ar => new { ar.artID, ar.racunID});


        }
        public DbSet<clsArtikal> artikalDS { get; set; }
        public DbSet<clsRacun> racunDS { get; set; }
        public DbSet<clsArtikli_racun> ARDS { get; set; }
        

    }

    public class clsArtikli_racun
    {
        public clsArtikal a { get; set; }
        public clsRacun r { get; set; }
        public int artID { get; set; }
        public int racunID { get; set; }
        public int kolicina { get; set; }
    }

    public class clsArtikal
    {
        public int sifra { get; set; }
        public string naziv { get; set; }
        public decimal cijena { get; set; }
        //public List<clsRacun> racuni { get; set; } = new List<clsRacun>();

        public clsArtikal(string n, decimal c)
        {
            naziv = n;
            cijena = c;
        }
        public clsArtikal() { }
    }

    public class clsRacun 
    {
        public int rbr { get; set; }
        public DateTime datum { get; set; } = DateTime.Now;
        public int kolicina { get; set; }
        //public List<clsArtikal> artikli { get; set; } = new List<clsArtikal>();
        
    }
}
