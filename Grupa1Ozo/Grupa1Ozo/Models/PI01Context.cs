using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Grupa1Ozo.Models
{
    public partial class PI01Context : DbContext
    {
        public PI01Context()
        {
        }

        public PI01Context(DbContextOptions<PI01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Certifikati> Certifikati { get; set; }
        public virtual DbSet<Grad> Grad { get; set; }
        public virtual DbSet<JavniNatjecaj> JavniNatjecaj { get; set; }
        public virtual DbSet<JavniNatjecajPonude> JavniNatjecajPonude { get; set; }
        public virtual DbSet<KategorijaUsluge> KategorijaUsluge { get; set; }
        public virtual DbSet<Kupac> Kupac { get; set; }
        public virtual DbSet<Lokacija> Lokacija { get; set; }
        public virtual DbSet<Narudzba> Narudzba { get; set; }
        public virtual DbSet<Natjecaj> Natjecaj { get; set; }
        public virtual DbSet<Opcina> Opcina { get; set; }
        public virtual DbSet<Oprema> Oprema { get; set; }
        public virtual DbSet<OpremaPosla> OpremaPosla { get; set; }
        public virtual DbSet<Posao> Posao { get; set; }
        public virtual DbSet<Skladiste> Skladiste { get; set; }
        public virtual DbSet<Struka> Struka { get; set; }
        public virtual DbSet<Usluga> Usluga { get; set; }
        public virtual DbSet<UslugaNarudzba> UslugaNarudzba { get; set; }
        public virtual DbSet<Zaposlenici> Zaposlenici { get; set; }
        public virtual DbSet<ZaposleniciCertifikati> ZaposleniciCertifikati { get; set; }
        public virtual DbSet<ZaposleniciPosla> ZaposleniciPosla { get; set; }
        public virtual DbSet<ZaposleniciStruka> ZaposleniciStruka { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=rppp.fer.hr,3000;Database=PI-01;User Id=pi01;Password=1-plete-1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certifikati>(entity =>
            {
                entity.Property(e => e.CertifikatiId).HasColumnName("Certifikati_ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Grad>(entity =>
            {
                entity.Property(e => e.GradId).HasColumnName("Grad_ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.OpcinaId).HasColumnName("Opcina_ID");

                entity.Property(e => e.PostanskiBroj).HasColumnName("Postanski_broj");

                entity.HasOne(d => d.Opcina)
                    .WithMany(p => p.Grad)
                    .HasForeignKey(d => d.OpcinaId)
                    .HasConstraintName("FK_Grad_Opcina");
            });

            modelBuilder.Entity<JavniNatjecaj>(entity =>
            {
                entity.ToTable("Javni_natjecaj");

                entity.Property(e => e.JavniNatjecajId).HasColumnName("Javni_natjecaj_ID");

                entity.Property(e => e.Dobitnik)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JavniNatjecajPonude>(entity =>
            {
                entity.ToTable("Javni_natjecaj_ponude");

                entity.Property(e => e.JavniNatjecajPonudeId).HasColumnName("Javni_natjecaj_ponude_ID");

                entity.Property(e => e.Cijena).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Firma)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.JavniNatjecajId).HasColumnName("Javni_natjecaj_ID");

                entity.HasOne(d => d.JavniNatjecaj)
                    .WithMany(p => p.JavniNatjecajPonude)
                    .HasForeignKey(d => d.JavniNatjecajId)
                    .HasConstraintName("FK_Javni_natjecaj_ponude_Javni_natjecaj");
            });

            modelBuilder.Entity<KategorijaUsluge>(entity =>
            {
                entity.ToTable("Kategorija_usluge");

                entity.Property(e => e.KategorijaUslugeId)
                    .HasColumnName("Kategorija_usluge_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Kupac>(entity =>
            {
                entity.Property(e => e.KupacId)
                    .HasColumnName("Kupac_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UslugaId).HasColumnName("Usluga_ID");

                entity.HasOne(d => d.Usluga)
                    .WithMany(p => p.Kupac)
                    .HasForeignKey(d => d.UslugaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Kupac_Usluga");
            });

            modelBuilder.Entity<Lokacija>(entity =>
            {
                entity.Property(e => e.LokacijaId)
                    .HasColumnName("Lokacija_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Adresa)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Grad)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Narudzba>(entity =>
            {
                entity.Property(e => e.NarudzbaId)
                    .HasColumnName("Narudzba_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DatumNarudzbe)
                    .HasColumnName("Datum_narudzbe")
                    .HasColumnType("date");

                entity.Property(e => e.StatusNarudzbe)
                    .IsRequired()
                    .HasColumnName("Status_narudzbe")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Natjecaj>(entity =>
            {
                entity.Property(e => e.NatjecajId)
                    .HasColumnName("Natjecaj_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.JavniNatjecajId).HasColumnName("Javni_natjecaj_ID");

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.JavniNatjecaj)
                    .WithMany(p => p.Natjecaj)
                    .HasForeignKey(d => d.JavniNatjecajId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Natjecaj_Javni_natjecaj");
            });

            modelBuilder.Entity<Opcina>(entity =>
            {
                entity.Property(e => e.OpcinaId).HasColumnName("Opcina_ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Oprema>(entity =>
            {
                entity.Property(e => e.OpremaId)
                    .HasColumnName("Oprema_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NazivOpreme)
                    .IsRequired()
                    .HasColumnName("Naziv_opreme")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SkladisteId).HasColumnName("Skladiste_ID");

                entity.Property(e => e.UslugaId).HasColumnName("Usluga_ID");

                entity.HasOne(d => d.Skladiste)
                    .WithMany(p => p.Oprema)
                    .HasForeignKey(d => d.SkladisteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Oprema_Skladiste");

                entity.HasOne(d => d.Usluga)
                    .WithMany(p => p.Oprema)
                    .HasForeignKey(d => d.UslugaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Oprema_Usluga");
            });

            modelBuilder.Entity<OpremaPosla>(entity =>
            {
                entity.ToTable("Oprema_posla");

                entity.Property(e => e.OpremaPoslaId).HasColumnName("Oprema_posla_ID");

                entity.Property(e => e.OpremaId).HasColumnName("Oprema_ID");

                entity.Property(e => e.PosaoId).HasColumnName("Posao_ID");

                entity.HasOne(d => d.Posao)
                    .WithMany(p => p.OpremaPosla)
                    .HasForeignKey(d => d.PosaoId)
                    .HasConstraintName("FK_Oprema_posla_Posao");
            });

            modelBuilder.Entity<Posao>(entity =>
            {
                entity.Property(e => e.PosaoId).HasColumnName("Posao_ID");

                entity.Property(e => e.NatjecajId).HasColumnName("Natjecaj_ID");

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasMaxLength(75)
                    .IsUnicode(false);

                entity.HasOne(d => d.Natjecaj)
                    .WithMany(p => p.Posao)
                    .HasForeignKey(d => d.NatjecajId)
                    .HasConstraintName("FK_Posao_Natjecaj");
            });

            modelBuilder.Entity<Skladiste>(entity =>
            {
                entity.Property(e => e.SkladisteId)
                    .HasColumnName("Skladiste_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.LokacijaId).HasColumnName("Lokacija_ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Lokacija)
                    .WithMany(p => p.Skladiste)
                    .HasForeignKey(d => d.LokacijaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Skladiste_Lokacija");
            });

            modelBuilder.Entity<Struka>(entity =>
            {
                entity.Property(e => e.StrukaId).HasColumnName("Struka_ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usluga>(entity =>
            {
                entity.Property(e => e.UslugaId)
                    .HasColumnName("Usluga_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.KategorijaUslugeId).HasColumnName("Kategorija_usluge_ID");

                entity.Property(e => e.NatjecajId).HasColumnName("Natjecaj_ID");

                entity.Property(e => e.Naziv)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Opis)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.KategorijaUsluge)
                    .WithMany(p => p.Usluga)
                    .HasForeignKey(d => d.KategorijaUslugeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usluga_Kategorija_usluge");

                entity.HasOne(d => d.Natjecaj)
                    .WithMany(p => p.Usluga)
                    .HasForeignKey(d => d.NatjecajId)
                    .HasConstraintName("FK_Usluga_Natjecaj");
            });

            modelBuilder.Entity<UslugaNarudzba>(entity =>
            {
                entity.ToTable("Usluga_narudzba");

                entity.Property(e => e.UslugaNarudzbaId)
                    .HasColumnName("Usluga_narudzba_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NarudzbaId).HasColumnName("Narudzba_ID");

                entity.Property(e => e.UslugaId).HasColumnName("Usluga_ID");

                entity.HasOne(d => d.Narudzba)
                    .WithMany(p => p.UslugaNarudzba)
                    .HasForeignKey(d => d.NarudzbaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usluga_narudzba_Narudzba");

                entity.HasOne(d => d.Usluga)
                    .WithMany(p => p.UslugaNarudzba)
                    .HasForeignKey(d => d.UslugaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Usluga_narudzba_Usluga");
            });

            modelBuilder.Entity<Zaposlenici>(entity =>
            {
                entity.Property(e => e.ZaposleniciId).HasColumnName("Zaposlenici_ID");

                entity.Property(e => e.Ime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OpcinaId).HasColumnName("Opcina_ID");

                entity.Property(e => e.Prezime)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UslugaId).HasColumnName("Usluga_ID");

                entity.HasOne(d => d.Opcina)
                    .WithMany(p => p.Zaposlenici)
                    .HasForeignKey(d => d.OpcinaId)
                    .HasConstraintName("FK_Zaposlenici_Opcina");

                entity.HasOne(d => d.Usluga)
                    .WithMany(p => p.Zaposlenici)
                    .HasForeignKey(d => d.UslugaId)
                    .HasConstraintName("FK_Zaposlenici_Usluga");
            });

            modelBuilder.Entity<ZaposleniciCertifikati>(entity =>
            {
                entity.HasKey(e => new { e.ZaposleniciId, e.CertifikatiId });

                entity.ToTable("Zaposlenici_certifikati");

                entity.Property(e => e.ZaposleniciId).HasColumnName("Zaposlenici_ID");

                entity.Property(e => e.CertifikatiId).HasColumnName("Certifikati_ID");

                entity.HasOne(d => d.Certifikati)
                    .WithMany(p => p.ZaposleniciCertifikati)
                    .HasForeignKey(d => d.CertifikatiId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposlenici_certifikati_Certifikati");

                entity.HasOne(d => d.Zaposlenici)
                    .WithMany(p => p.ZaposleniciCertifikati)
                    .HasForeignKey(d => d.ZaposleniciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposlenici_certifikati_Zaposlenici");
            });

            modelBuilder.Entity<ZaposleniciPosla>(entity =>
            {
                entity.ToTable("Zaposlenici_posla");

                entity.Property(e => e.ZaposleniciPoslaId).HasColumnName("Zaposlenici_posla_ID");

                entity.Property(e => e.PosaoId).HasColumnName("Posao_ID");

                entity.Property(e => e.ZaposlenikId).HasColumnName("Zaposlenik_ID");

                entity.HasOne(d => d.Posao)
                    .WithMany(p => p.ZaposleniciPosla)
                    .HasForeignKey(d => d.PosaoId)
                    .HasConstraintName("FK_Zaposlenici_posla_Posao");
            });

            modelBuilder.Entity<ZaposleniciStruka>(entity =>
            {
                entity.HasKey(e => new { e.ZaposleniciId, e.StrukaId })
                    .HasName("PK_Zaposlenici_struka_1");

                entity.ToTable("Zaposlenici_struka");

                entity.Property(e => e.ZaposleniciId).HasColumnName("Zaposlenici_ID");

                entity.Property(e => e.StrukaId).HasColumnName("Struka_ID");

                entity.HasOne(d => d.Struka)
                    .WithMany(p => p.ZaposleniciStruka)
                    .HasForeignKey(d => d.StrukaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposlenici_struka_Struka");

                entity.HasOne(d => d.Zaposlenici)
                    .WithMany(p => p.ZaposleniciStruka)
                    .HasForeignKey(d => d.ZaposleniciId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Zaposlenici_struka_Zaposlenici");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
