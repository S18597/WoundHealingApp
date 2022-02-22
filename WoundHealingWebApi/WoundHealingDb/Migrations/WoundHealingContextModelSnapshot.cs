﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WoundHealingDb;

namespace WoundHealingDb.Migrations
{
    [DbContext(typeof(WoundHealingContext))]
    partial class WoundHealingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WoundHealingDb.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("AppointmentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("AppointmentNotes")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int>("TreatmentId")
                        .HasColumnType("int");

                    b.HasKey("AppointmentId")
                        .HasName("PK_Appointment_AppointmentId");

                    b.HasIndex("TreatmentId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("WoundHealingDb.Models.Auth", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.HasKey("UserId")
                        .HasName("PK_Auth_UserId");

                    b.ToTable("Auth");
                });

            modelBuilder.Entity("WoundHealingDb.Models.Chat", b =>
                {
                    b.Property<int>("ChatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("ChatId")
                        .HasName("PK_Chat_ChatId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("WoundHealingDb.Models.ChatMessage", b =>
                {
                    b.Property<int>("ChatMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("MessageDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ChatMessageId")
                        .HasName("PK_ChatMessage_ChatMessageId");

                    b.HasIndex("ChatId");

                    b.HasIndex("UserId");

                    b.ToTable("ChatMessage");
                });

            modelBuilder.Entity("WoundHealingDb.Models.MedicalData", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("Alcohol")
                        .HasColumnType("bit");

                    b.Property<string>("Allergies")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("ChronicDeseases")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<bool>("Drugs")
                        .HasColumnType("bit");

                    b.Property<string>("Medication")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("Pregnancy")
                        .HasColumnType("bit");

                    b.Property<bool>("Tobacco")
                        .HasColumnType("bit");

                    b.HasKey("UserId")
                        .HasName("PK_MedicalData_UserId");

                    b.ToTable("MedicalData");
                });

            modelBuilder.Entity("WoundHealingDb.Models.PainLevel", b =>
                {
                    b.Property<int>("PainLevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PainLevelName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("PainLevelId")
                        .HasName("PK_PainLevel_PainLevelId");

                    b.ToTable("PainLevel");
                });

            modelBuilder.Entity("WoundHealingDb.Models.PainType", b =>
                {
                    b.Property<int>("PainTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("PainTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("PainTypeId")
                        .HasName("PK_PainType_PainTypeId");

                    b.ToTable("PainType");
                });

            modelBuilder.Entity("WoundHealingDb.Models.SurroundingSkin", b =>
                {
                    b.Property<int>("SurroundingSkinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SurroundingSkinName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("SurroundingSkinId")
                        .HasName("PK_SurroundingSkin_SurroundingSkinId");

                    b.ToTable("SurroundingSkin");
                });

            modelBuilder.Entity("WoundHealingDb.Models.Treatment", b =>
                {
                    b.Property<int>("TreatmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DoctorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("WoundId")
                        .HasColumnType("int");

                    b.HasKey("TreatmentId")
                        .HasName("PK_Treatment_TreatmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.HasIndex("WoundId")
                        .IsUnique();

                    b.ToTable("Treatment");
                });

            modelBuilder.Entity("WoundHealingDb.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200)
                        .IsUnicode(true);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<bool>("IsDoctor")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPatient")
                        .HasColumnType("bit");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50)
                        .IsUnicode(true);

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(11)")
                        .HasMaxLength(11);

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.HasKey("UserId")
                        .HasName("PK_User_UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("WoundHealingDb.Models.Wound", b =>
                {
                    b.Property<int>("WoundId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PainLevelId")
                        .HasColumnType("int");

                    b.Property<int>("PainTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SurroundingSkinId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WoundBleedingId")
                        .HasColumnType("int");

                    b.Property<int>("WoundColorId")
                        .HasColumnType("int");

                    b.Property<int>("WoundExudateId")
                        .HasColumnType("int");

                    b.Property<int>("WoundLocationId")
                        .HasColumnType("int");

                    b.Property<int>("WoundOdorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("WoundRegisterDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("WoundSizeId")
                        .HasColumnType("int");

                    b.Property<int>("WoundTypeId")
                        .HasColumnType("int");

                    b.HasKey("WoundId")
                        .HasName("PK_Wound_WoundId");

                    b.HasIndex("PainLevelId");

                    b.HasIndex("PainTypeId");

                    b.HasIndex("SurroundingSkinId");

                    b.HasIndex("UserId");

                    b.HasIndex("WoundBleedingId");

                    b.HasIndex("WoundColorId");

                    b.HasIndex("WoundExudateId");

                    b.HasIndex("WoundLocationId");

                    b.HasIndex("WoundOdorId");

                    b.HasIndex("WoundSizeId");

                    b.HasIndex("WoundTypeId");

                    b.ToTable("Wound");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundBleeding", b =>
                {
                    b.Property<int>("WoundBleedingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundBleedingName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundBleedingId")
                        .HasName("PK_WoundBleeding_WoundBleedingId");

                    b.ToTable("WoundBleeding");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundColor", b =>
                {
                    b.Property<int>("WoundColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundColorId")
                        .HasName("PK_WoundColor_WoundColorId");

                    b.ToTable("WoundColor");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundExudate", b =>
                {
                    b.Property<int>("WoundExudateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundExudateName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundExudateId")
                        .HasName("PK_WoundExudate_WoundExudateId");

                    b.ToTable("WoundExudate");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundLocation", b =>
                {
                    b.Property<int>("WoundLocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundLocationName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundLocationId")
                        .HasName("PK_WoundLocation_WoundLocationId");

                    b.ToTable("WoundLocation");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundOdor", b =>
                {
                    b.Property<int>("WoundOdorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundOdorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundOdorId")
                        .HasName("PK_WoundOdor_WoundOdorId");

                    b.ToTable("WoundOdor");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundPhoto", b =>
                {
                    b.Property<int>("WoundPhotoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("FileData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Filename")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("WoundId")
                        .HasColumnType("int");

                    b.HasKey("WoundPhotoId")
                        .HasName("PK_WoundPhoto_WoundPhotoId");

                    b.HasIndex("WoundId");

                    b.ToTable("WoundPhoto");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundSize", b =>
                {
                    b.Property<int>("WoundSizeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundSizeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundSizeId")
                        .HasName("PK_WoundSize_WoundSizeId");

                    b.ToTable("WoundSize");
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundType", b =>
                {
                    b.Property<int>("WoundTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("WoundTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("WoundTypeId")
                        .HasName("PK_WoundType_WoundTypeId");

                    b.ToTable("WoundType");
                });

            modelBuilder.Entity("WoundHealingDb.Models.Appointment", b =>
                {
                    b.HasOne("WoundHealingDb.Models.Treatment", "Treatment")
                        .WithMany("Appointments")
                        .HasForeignKey("TreatmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.Auth", b =>
                {
                    b.HasOne("WoundHealingDb.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("WoundHealingDb.Models.Auth", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.Chat", b =>
                {
                    b.HasOne("WoundHealingDb.Models.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.ChatMessage", b =>
                {
                    b.HasOne("WoundHealingDb.Models.Chat", "Chat")
                        .WithMany("ChatMessages")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.MedicalData", b =>
                {
                    b.HasOne("WoundHealingDb.Models.User", "User")
                        .WithOne()
                        .HasForeignKey("WoundHealingDb.Models.MedicalData", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.Treatment", b =>
                {
                    b.HasOne("WoundHealingDb.Models.User", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.User", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.Wound", "Wound")
                        .WithOne()
                        .HasForeignKey("WoundHealingDb.Models.Treatment", "WoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.Wound", b =>
                {
                    b.HasOne("WoundHealingDb.Models.PainLevel", "PainLevel")
                        .WithMany()
                        .HasForeignKey("PainLevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.PainType", "PainType")
                        .WithMany()
                        .HasForeignKey("PainTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.SurroundingSkin", "SurroundingSkin")
                        .WithMany()
                        .HasForeignKey("SurroundingSkinId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.User", "User")
                        .WithMany("Wounds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundBleeding", "WoundBleeding")
                        .WithMany()
                        .HasForeignKey("WoundBleedingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundColor", "WoundColor")
                        .WithMany()
                        .HasForeignKey("WoundColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundExudate", "WoundExudate")
                        .WithMany()
                        .HasForeignKey("WoundExudateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundLocation", "WoundLocation")
                        .WithMany()
                        .HasForeignKey("WoundLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundOdor", "WoundOdor")
                        .WithMany()
                        .HasForeignKey("WoundOdorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundSize", "WoundSize")
                        .WithMany()
                        .HasForeignKey("WoundSizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WoundHealingDb.Models.WoundType", "WoundType")
                        .WithMany()
                        .HasForeignKey("WoundTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WoundHealingDb.Models.WoundPhoto", b =>
                {
                    b.HasOne("WoundHealingDb.Models.Wound", "Wound")
                        .WithMany("WoundPhotos")
                        .HasForeignKey("WoundId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
