using System;
using System.Collections.Generic;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DAL;

public partial class DnaTestingContext : DbContext
{
    public DnaTestingContext()
    {
    }

    public DnaTestingContext(DbContextOptions<DnaTestingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<CollectedSample> CollectedSamples { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<KitComponent> KitComponents { get; set; }

    public virtual DbSet<LoginHistory> LoginHistories { get; set; }

    public virtual DbSet<Participant> Participants { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<SampleType> SampleTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ServiceTestPurpose> ServiceTestPurposes { get; set; }

    public virtual DbSet<TestCategory> TestCategories { get; set; }

    public virtual DbSet<TestPurpose> TestPurposes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // Đọc connection string từ appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PK__appointm__A50828FCF994414C");

            entity.ToTable("appointment");

            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate)
                .HasPrecision(6)
                .HasColumnName("appointment_date");
            entity.Property(e => e.CollectionLocation)
                .HasMaxLength(50)
                .HasColumnName("collection_location");
            entity.Property(e => e.CollectionSampleTime)
                .HasPrecision(6)
                .HasColumnName("collection_sample_time");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .HasColumnName("district");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FingerprintFile)
                .HasMaxLength(255)
                .HasColumnName("fingerprint_file");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.KitComponentId).HasColumnName("kit_component_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.Province)
                .HasMaxLength(100)
                .HasColumnName("province");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.ServiceType)
                .HasMaxLength(50)
                .HasColumnName("service_type");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.TestCategory)
                .HasMaxLength(50)
                .HasColumnName("test_category");
            entity.Property(e => e.TestPurpose)
                .HasMaxLength(50)
                .HasColumnName("test_purpose");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Guest).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.GuestId)
                .HasConstraintName("FKt9f1l6hd3vmt6elf0i5u9pot6");

            entity.HasOne(d => d.KitComponent).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.KitComponentId)
                .HasConstraintName("FKi8truunrpqno0vab86d4q4eas");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FK5ixajc1q1xjyvjnqiasyjuqqx");

            entity.HasOne(d => d.User).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK9bsblr2lrdpogp56kjgcd6oa8");
        });

        modelBuilder.Entity<CollectedSample>(entity =>
        {
            entity.HasKey(e => e.CollectedSampleId).HasName("PK__collecte__962365D7273062FC");

            entity.ToTable("collected_sample");

            entity.Property(e => e.CollectedSampleId).HasColumnName("collected_sample_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.CollectedDate).HasColumnName("collected_date");
            entity.Property(e => e.KitComponentId).HasColumnName("kit_component_id");
            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.ReceivedDate).HasColumnName("received_date");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.SampleTypeId).HasColumnName("sample_type_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("In Transit")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Appointment).WithMany(p => p.CollectedSamples)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK6ev07jwsxdb3e9by2nfykfsv4");

            entity.HasOne(d => d.KitComponent).WithMany(p => p.CollectedSamples)
                .HasForeignKey(d => d.KitComponentId)
                .HasConstraintName("FKtm4m6s6k77tevug0chjjmqis2");

            entity.HasOne(d => d.Participant).WithMany(p => p.CollectedSamples)
                .HasForeignKey(d => d.ParticipantId)
                .HasConstraintName("FKjgu8450uh389bn4kp234bfbg");

            entity.HasOne(d => d.Sample).WithMany(p => p.CollectedSamples)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FKlsqfyrn4p0dbiylekna9hig0q");

            entity.HasOne(d => d.SampleType).WithMany(p => p.CollectedSamples)
                .HasForeignKey(d => d.SampleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK5wikg4qguj9o989ivxcgxqm9g");

            entity.HasOne(d => d.User).WithMany(p => p.CollectedSamples)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKlgwl8vuabb4o6q6r3xwrvgv0");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("PK__feedback__7A6B2B8C5CA30F0D");

            entity.ToTable("feedback");

            entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");
            entity.Property(e => e.Content)
                .HasMaxLength(255)
                .HasColumnName("content");
            entity.Property(e => e.FeedbackDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("feedback_date");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Service).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FKgk6oc7o2w429cg01itvxhu3sj");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKksmxr22ivd372kt5bwfptxw93");
        });

        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(e => e.GuestId).HasName("PK__guest__19778E35ED6BE5CD");

            entity.ToTable("guest");

            entity.Property(e => e.GuestId).HasColumnName("guest_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<KitComponent>(entity =>
        {
            entity.HasKey(e => e.KitComponentId).HasName("PK__kit_comp__5B9E6D8B92CE8D65");

            entity.ToTable("kit_components");

            entity.Property(e => e.KitComponentId).HasColumnName("kit_component_id");
            entity.Property(e => e.ComponentName)
                .HasMaxLength(100)
                .HasColumnName("component_name");
            entity.Property(e => e.Introduction)
                .HasMaxLength(255)
                .HasColumnName("introduction");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Service).WithMany(p => p.KitComponents)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FKs2kb26rcoy077mltw4gecfkpa");
        });

        modelBuilder.Entity<LoginHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__login_hi__3213E83FAC4C94E9");

            entity.ToTable("login_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.LoginTime)
                .HasPrecision(6)
                .HasColumnName("login_time");
            entity.Property(e => e.LoginType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("login_type");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.LoginHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKluob4f2nsysm8h8j81x5pf5su");
        });

        modelBuilder.Entity<Participant>(entity =>
        {
            entity.HasKey(e => e.ParticipantId).HasName("PK__particip__4E037806D86D69E0");

            entity.ToTable("participant");

            entity.Property(e => e.ParticipantId).HasColumnName("participant_id");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__payment__ED1FC9EA70EE924B");

            entity.ToTable("payment");

            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount).HasColumnName("amount");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(20)
                .HasColumnName("payment_method");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending")
                .HasColumnName("status");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Payments)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FK9vafbyi48ic7wo7n417cun7tf");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__report__779B7C585EDA12DF");

            entity.ToTable("report");

            entity.Property(e => e.ReportId).HasColumnName("report_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ReportContent).HasColumnName("report_content");
            entity.Property(e => e.ReportTitle)
                .HasMaxLength(200)
                .HasColumnName("report_title");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Reports)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FKlb1feclmesf8aqpjnvq1ttrbi");
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PK__result__AFB3C316A92A6BE8");

            entity.ToTable("result");

            entity.Property(e => e.ResultId).HasColumnName("result_id");
            entity.Property(e => e.AppointmentId).HasColumnName("appointment_id");
            entity.Property(e => e.Interpretation).HasColumnName("interpretation");
            entity.Property(e => e.ResultData)
                .HasMaxLength(255)
                .HasColumnName("result_data");
            entity.Property(e => e.ResultDate).HasColumnName("result_date");
            entity.Property(e => e.ResultFile)
                .HasMaxLength(255)
                .HasColumnName("result_file");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Pending")
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Appointment).WithMany(p => p.Results)
                .HasForeignKey(d => d.AppointmentId)
                .HasConstraintName("FKg1kv810s0ed821ur07u65rbns");

            entity.HasOne(d => d.Sample).WithMany(p => p.Results)
                .HasForeignKey(d => d.SampleId)
                .HasConstraintName("FKptls0imsb3c2aos7y4yndekqj");

            entity.HasOne(d => d.User).WithMany(p => p.Results)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKouny0funnrcncx89rbxkk3yjp");
        });

        modelBuilder.Entity<SampleType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__sample_t__3213E83F92504FC5");

            entity.ToTable("sample_type");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.KitComponentId).HasColumnName("kit_component_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.HasOne(d => d.KitComponent).WithMany(p => p.SampleTypes)
                .HasForeignKey(d => d.KitComponentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKpwoupfv36fps73w7higy2q3y7");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__services__3E0DB8AF97FDA3DD");

            entity.ToTable("services");

            entity.HasIndex(e => e.ServiceName, "UK38twoss73rtux07w58qp200r0").IsUnique();

            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ServiceName)
                .HasMaxLength(100)
                .HasColumnName("service_name");
        });

        modelBuilder.Entity<ServiceTestPurpose>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__service___3213E83FF97F6B2B");

            entity.ToTable("service_test_purpose");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.TestPurposeId).HasColumnName("test_purpose_id");

            entity.HasOne(d => d.Service).WithMany(p => p.ServiceTestPurposes)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK2wcuu22obw5lav5oyxt10oiqn");

            entity.HasOne(d => d.TestPurpose).WithMany(p => p.ServiceTestPurposes)
                .HasForeignKey(d => d.TestPurposeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FKb2s52bfrcx2p3gify7ee3kvjc");
        });

        modelBuilder.Entity<TestCategory>(entity =>
        {
            entity.HasKey(e => e.TestCategoryId).HasName("PK__test_cat__F4C5E48CF6FE692D");

            entity.ToTable("test_category");

            entity.HasIndex(e => new { e.Name, e.ServiceId }, "unique_name_service").IsUnique();

            entity.Property(e => e.TestCategoryId).HasColumnName("test_category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");

            entity.HasOne(d => d.Service).WithMany(p => p.TestCategories)
                .HasForeignKey(d => d.ServiceId)
                .HasConstraintName("FKfoa4gf6s73xuxhtorgn8oe2hx");
        });

        modelBuilder.Entity<TestPurpose>(entity =>
        {
            entity.HasKey(e => e.TestPurposeId).HasName("PK__test_pur__081C26C5C89B2B76");

            entity.ToTable("test_purpose");

            entity.HasIndex(e => e.TestPurposeName, "UKio8vaf8q4q3xk7sigw29xnpvb").IsUnique();

            entity.Property(e => e.TestPurposeId).HasColumnName("test_purpose_id");
            entity.Property(e => e.TestPurposeDescription)
                .HasMaxLength(255)
                .HasColumnName("test_purpose_description");
            entity.Property(e => e.TestPurposeName)
                .HasMaxLength(255)
                .HasColumnName("test_purpose_name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370F323C4964");

            entity.ToTable("users");

            entity.HasIndex(e => e.Username, "UK3r2pikgsxo64e847ik4b0gba1").IsUnique();

            entity.HasIndex(e => e.Email, "UKogjplnrmkbx3xm9jsjhis0y8").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .HasColumnName("address");
            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("username");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
