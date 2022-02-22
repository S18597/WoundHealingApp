using Microsoft.EntityFrameworkCore;
using WoundHealingDb.Models;

namespace WoundHealingDb
{
    public partial class WoundHealingContext : DbContext
    {
        private const string SqlConnectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=WoundHealingDb;Persist Security Info=True;User ID=whUser;Password=whUser;";

        public WoundHealingContext(DbContextOptions<WoundHealingContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(SqlConnectionString);
            }
        }

        public DbSet<User> User { get; set; }
        public DbSet<Auth> Auth { get; set; }

        public DbSet<WoundType> WoundType { get; set; }
        public DbSet<WoundLocation> WoundLocation { get; set; }
        public DbSet<WoundSize> WoundSize { get; set; }
        public DbSet<WoundColor> WoundColor { get; set; }
        public DbSet<WoundOdor> WoundOdor { get; set; }
        public DbSet<WoundExudate> WoundExudate { get; set; }
        public DbSet<WoundBleeding> WoundBleeding { get; set; }
        public DbSet<SurroundingSkin> SurroundingSkin { get; set; }
        public DbSet<PainType> PainType { get; set; }
        public DbSet<PainLevel> PainLevel { get; set; }

        public DbSet<Wound> Wound { get; set; }

        public DbSet<Treatment> Treatment { get; set; }
        public DbSet<Appointment> Appointment { get; set; }

        public DbSet<MedicalData> MedicalData { get; set; }
        public DbSet<WoundPhoto> WoundPhoto { get; set; }

        public DbSet<Chat> Chat { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(e => e.UserId).HasName("PK_User_UserId");
                
                entity.Property(e => e.Firstname).IsUnicode().IsRequired().HasMaxLength(50);
                entity.Property(e => e.Lastname).IsUnicode().IsRequired().HasMaxLength(50);
                entity.Property(e => e.Pesel).IsRequired().HasMaxLength(11);
                entity.Property(e => e.Address).IsUnicode().IsRequired().HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(e => e.EmailAddress).IsRequired().HasMaxLength(50);
                entity.Property(e => e.DateOfBirth).IsRequired();
                entity.Property(e => e.IsPatient).IsRequired();
                entity.Property(e => e.IsDoctor).IsRequired();
            });

            // Auth
            modelBuilder.Entity<Auth>(entity =>
            {
                entity.ToTable("Auth");
                entity.Property(e => e.UserId).HasColumnType("int");
                entity.HasKey(e => e.UserId).HasName("PK_Auth_UserId");
                

                entity.Property(e => e.Salt).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Hash).IsRequired().HasMaxLength(50);
            });
            modelBuilder.Entity<Auth>().HasOne(e => e.User).WithOne();

            // WoundType
            modelBuilder.Entity<WoundType>(entity =>
            {
                entity.ToTable("WoundType");
                entity.HasKey(e => e.WoundTypeId).HasName("PK_WoundType_WoundTypeId");

                entity.Property(e => e.WoundTypeName).IsRequired().HasMaxLength(50);
            });

            // WoundLocation
            modelBuilder.Entity<WoundLocation>(entity =>
            {
                entity.ToTable("WoundLocation");
                entity.HasKey(e => e.WoundLocationId).HasName("PK_WoundLocation_WoundLocationId");

                entity.Property(e => e.WoundLocationName).IsRequired().HasMaxLength(50);
            });

            // WoundSize
            modelBuilder.Entity<WoundSize>(entity =>
            {
                entity.ToTable("WoundSize");
                entity.HasKey(e => e.WoundSizeId).HasName("PK_WoundSize_WoundSizeId");

                entity.Property(e => e.WoundSizeName).IsRequired().HasMaxLength(50);
            });

            // WoundColor
            modelBuilder.Entity<WoundColor>(entity =>
            {
                entity.ToTable("WoundColor");
                entity.HasKey(e => e.WoundColorId).HasName("PK_WoundColor_WoundColorId");

                entity.Property(e => e.WoundColorName).IsRequired().HasMaxLength(50);
            });

            // WoundOdor
            modelBuilder.Entity<WoundOdor>(entity =>
            {
                entity.ToTable("WoundOdor");
                entity.HasKey(e => e.WoundOdorId).HasName("PK_WoundOdor_WoundOdorId");

                entity.Property(e => e.WoundOdorName).IsRequired().HasMaxLength(50);
            });

            // WoundExudate
            modelBuilder.Entity<WoundExudate>(entity =>
            {
                entity.ToTable("WoundExudate");
                entity.HasKey(e => e.WoundExudateId).HasName("PK_WoundExudate_WoundExudateId");

                entity.Property(e => e.WoundExudateName).IsRequired().HasMaxLength(50);
            });

            // WoundBleeding
            modelBuilder.Entity<WoundBleeding>(entity =>
            {
                entity.ToTable("WoundBleeding");
                entity.HasKey(e => e.WoundBleedingId).HasName("PK_WoundBleeding_WoundBleedingId");

                entity.Property(e => e.WoundBleedingName).IsRequired().HasMaxLength(50);
            });

            // SurroundingSkin
            modelBuilder.Entity<SurroundingSkin>(entity =>
            {
                entity.ToTable("SurroundingSkin");
                entity.HasKey(e => e.SurroundingSkinId).HasName("PK_SurroundingSkin_SurroundingSkinId");

                entity.Property(e => e.SurroundingSkinName).IsRequired().HasMaxLength(50);
            });

            // PainType
            modelBuilder.Entity<PainType>(entity =>
            {
                entity.ToTable("PainType");
                entity.HasKey(e => e.PainTypeId).HasName("PK_PainType_PainTypeId");

                entity.Property(e => e.PainTypeName).IsRequired().HasMaxLength(50);
            });

            // PainLevel
            modelBuilder.Entity<PainLevel>(entity =>
            {
                entity.ToTable("PainLevel");
                entity.HasKey(e => e.PainLevelId).HasName("PK_PainLevel_PainLevelId");

                entity.Property(e => e.PainLevelName).IsRequired().HasMaxLength(50);
            });

            // Wound
            modelBuilder.Entity<Wound>(entity =>
            {
                entity.ToTable("Wound");
                entity.HasKey(e => e.WoundId).HasName("PK_Wound_WoundId");

                entity.Property(e => e.WoundRegisterDate).IsRequired();
            });
            modelBuilder.Entity<Wound>().HasOne(e => e.User).WithMany(e => e.Wounds);

            modelBuilder.Entity<Wound>().HasOne(e => e.WoundType).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.WoundLocation).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.WoundSize).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.WoundColor).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.WoundOdor).WithMany(); 
            modelBuilder.Entity<Wound>().HasOne(e => e.WoundExudate).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.WoundBleeding).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.SurroundingSkin).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.PainType).WithMany();
            modelBuilder.Entity<Wound>().HasOne(e => e.PainLevel).WithMany();

            // Treatment
            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.ToTable("Treatment");
                entity.HasKey(e => e.TreatmentId).HasName("PK_Treatment_TreatmentId");

                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.PatientId).IsRequired();
                entity.Property(e => e.WoundId).IsRequired();

                entity.Property(e => e.Status).IsRequired();

                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired(false);
            });
            modelBuilder.Entity<Treatment>().HasOne(e => e.Doctor).WithMany();
            modelBuilder.Entity<Treatment>().HasOne(e => e.Patient).WithMany();
            modelBuilder.Entity<Treatment>().HasOne(e => e.Wound).WithOne();

            // Appointment
            modelBuilder.Entity<Appointment>(entity =>
            {
                entity.ToTable("Appointment");
                entity.HasKey(e => e.AppointmentId).HasName("PK_Appointment_AppointmentId");

                entity.Property(e => e.TreatmentId).IsRequired();

                entity.Property(e => e.AppointmentNotes).IsRequired(false).HasMaxLength(1000);
                entity.Property(e => e.AppointmentDate).IsRequired();
            });
            modelBuilder.Entity<Appointment>().HasOne(e => e.Treatment).WithMany(e => e.Appointments);

            // Medical Data
            modelBuilder.Entity<MedicalData>(entity =>
            {
                entity.ToTable("MedicalData");
                entity.Property(e => e.UserId).HasColumnType("int");
                entity.HasKey(e => e.UserId).HasName("PK_MedicalData_UserId");

                entity.Property(e => e.ChronicDeseases).HasMaxLength(1000);
                entity.Property(e => e.Allergies).HasMaxLength(500);
                entity.Property(e => e.Medication).HasMaxLength(500);

            });
            modelBuilder.Entity<MedicalData>().HasOne(e => e.User).WithOne();

            // Wound Photo
            modelBuilder.Entity<WoundPhoto>(entity =>
            {
                entity.ToTable("WoundPhoto");
                entity.HasKey(e => e.WoundPhotoId).HasName("PK_WoundPhoto_WoundPhotoId");

                entity.Property(e => e.WoundId).IsRequired();

                entity.Property(e => e.FileData).IsRequired();
                entity.Property(e => e.Filename).IsRequired().HasMaxLength(500);
            });
            modelBuilder.Entity<WoundPhoto>().HasOne(e => e.Wound).WithMany(e => e.WoundPhotos);

            // Chat
            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("Chat");
                entity.HasKey(e => e.ChatId).HasName("PK_Chat_ChatId");

                entity.Property(e => e.DoctorId).IsRequired();
                entity.Property(e => e.PatientId).IsRequired();
            });
            modelBuilder.Entity<Chat>().HasOne(e => e.Doctor).WithMany();
            modelBuilder.Entity<Chat>().HasOne(e => e.Patient).WithMany();

            // Chat Message
            modelBuilder.Entity<ChatMessage>(entity =>
            {
                entity.ToTable("ChatMessage");
                entity.HasKey(e => e.ChatMessageId).HasName("PK_ChatMessage_ChatMessageId");

                entity.Property(e => e.ChatId).IsRequired();
                entity.Property(e => e.UserId).IsRequired();

                entity.Property(e => e.Message).IsRequired().HasMaxLength(1000);
                entity.Property(e => e.MessageDate).IsRequired();
            });
            modelBuilder.Entity<ChatMessage>().HasOne(e => e.Chat).WithMany(e => e.ChatMessages).HasForeignKey(f => f.ChatId);
            modelBuilder.Entity<ChatMessage>().HasOne(e => e.User).WithMany();
        }
    }
}