using Microsoft.EntityFrameworkCore;

namespace Version3.Models;

public partial class BochagovaKursovikContext : DbContext
{
    public BochagovaKursovikContext()
    {
    }

    public BochagovaKursovikContext(DbContextOptions<BochagovaKursovikContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Income> Incomes { get; set; }

    public virtual DbSet<IncomeCategory> IncomeCategories { get; set; }

    public virtual DbSet<Spend> Spends { get; set; }

    public virtual DbSet<SpendCategory> SpendCategories { get; set; }

    public virtual DbSet<SpendIncome> SpendIncomes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=NATBOK\\MSSQLSERVER2;Initial Catalog=Bochagova_Kursovik;Integrated Security=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Income>(entity =>
        {
            entity.HasKey(e => e.IncomeId).HasName("PK__Income__344C93594A378742");

            entity.ToTable("Income");

            entity.Property(e => e.IncomeId).HasColumnName("Income_Id");
            entity.Property(e => e.Cost).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.IncomeCategoryId).HasColumnName("IncomeCategory_Id");

            entity.HasOne(d => d.IncomeCategory).WithMany(p => p.Incomes)
                .HasForeignKey(d => d.IncomeCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Income__IncomeCa__73BA3083");
        });

        modelBuilder.Entity<IncomeCategory>(entity =>
        {
            entity.HasKey(e => e.IncomeCategoryId).HasName("PK__Income_C__07506BCECAB6D873");

            entity.ToTable("Income_Category");

            entity.Property(e => e.IncomeCategoryId).HasColumnName("IncomeCategory_Id");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<Spend>(entity =>
        {
            entity.HasKey(e => e.SpendId).HasName("PK__Spend__4C162704AF3CAB1D");

            entity.ToTable("Spend");

            entity.Property(e => e.SpendId).HasColumnName("Spend_Id");
            entity.Property(e => e.Cost).HasColumnType("decimal(12, 2)");
            entity.Property(e => e.SpendCategoryId).HasColumnName("SpendCategory_Id");

            entity.HasOne(d => d.SpendCategory).WithMany(p => p.Spends)
                .HasForeignKey(d => d.SpendCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Spend__SpendCate__787EE5A0");
        });

        modelBuilder.Entity<SpendCategory>(entity =>
        {
            entity.HasKey(e => e.SpendCategoryId).HasName("PK__Spend_Ca__F3F42F506EDDA54C");

            entity.ToTable("Spend_Category");

            entity.Property(e => e.SpendCategoryId).HasColumnName("SpendCategory_Id");
            entity.Property(e => e.Name).HasMaxLength(20);
        });

        modelBuilder.Entity<SpendIncome>(entity =>
        {
            entity.HasKey(e => e.SpendIncomeId).HasName("PK__SpendInc__4D4BDEFE5187596E");

            entity.ToTable("SpendIncome");

            entity.Property(e => e.SpendIncomeId).HasColumnName("SpendIncome_Id");
            entity.Property(e => e.IncomeId).HasColumnName("Income_Id");
            entity.Property(e => e.SpendId).HasColumnName("Spend_Id");

            entity.HasOne(d => d.Income).WithMany(p => p.SpendIncomes)
                .HasForeignKey(d => d.IncomeId)
                .HasConstraintName("FK__SpendInco__Incom__7B5B524B");

            entity.HasOne(d => d.Spend).WithMany(p => p.SpendIncomes)
                .HasForeignKey(d => d.SpendId)
                .HasConstraintName("FK__SpendInco__Spend__7C4F7684");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
