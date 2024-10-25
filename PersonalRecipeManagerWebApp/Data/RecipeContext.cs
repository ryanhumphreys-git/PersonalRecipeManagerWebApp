using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;

namespace PersonalRecipeManagerWebApp.Data;

public partial class RecipeContext : DbContext
{
    public DbSet<User> User { get; set; }
    public DbSet<UserKitchen> UserKitchens { get; set; }
    public DbSet<UserRecipes> UserRecipes { get; set; }
    public DbSet<Ingredients> Ingredients { get; set; }
    public DbSet<Equipment> Equipment { get; set; }
    public DbSet<Kitchen> Kitchen { get; set; }
    public DbSet<KitchenIngredients> KitchenIngredients { get; set; }
    public DbSet<KitchenEquipment> KitchenEquipment { get; set; }
    public DbSet<KitchenType> KitchenType { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<RecipeIngredients> RecipeIngredients { get; set; }
    public DbSet<RecipeEquipment> RecipeEquipment { get; set; }

    public RecipeContext(DbContextOptions<RecipeContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(@"Server=SPARK-DEV-RTH;Database=RecipeManager;Trusted_Connection=True;Encrypt=False;");
        options.EnableDetailedErrors();
        options.LogTo(Console.WriteLine);
    }
        

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(user =>
        {
            user.HasKey(e => e.Id).HasName("PK_User");

            user.Property(e => e.Id).ValueGeneratedNever();
            user.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<UserKitchen>(user =>
        {
            user.HasKey(e => e.AutoId).HasName("PK__UserKi__6B232905495BDECC");

            user.Property(e => e.AutoId).ValueGeneratedNever();

            user.HasOne(d => d.User).WithMany(p => p.UserKitchens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserKitchens_User");

            user.HasOne(d => d.Kitchen).WithMany(p => p.UserKitchens)
                .HasForeignKey(d => d.KitchenId)
                .HasConstraintName("FK_UserKitchens_Kitchen");
        });

        modelBuilder.Entity<UserRecipes>(user =>
        {
            user.HasKey(e => e.AutoId).HasName("PK__UserRe__6B232905F0FDFDC1");

            user.Property(e => e.AutoId).ValueGeneratedNever();

            user.HasOne(d => d.User).WithMany(p => p.UserRecipes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRecipes_User");

            user.HasOne(d => d.Recipe).WithMany(p => p.UserRecipes)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_UserRecipes_Recipes");
        });

        modelBuilder.Entity<Ingredients>(user =>
        {
            user.HasKey(e => e.Id).HasName("PK__Ingredie__3214EC0772200186");

            user.Property(e => e.Id).ValueGeneratedNever();
            user.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            user.Property(e => e.Name).HasMaxLength(50);
            user.Property(e => e.UnitOfMeasurement).HasMaxLength(50);
        });

        modelBuilder.Entity<Equipment>(user =>
        {
            user.HasKey(e => e.Id).HasName("PK__ToolsAnd__3214EC07EE62D0B3");

            user.Property(e => e.Id).ValueGeneratedNever();
            user.Property(e => e.Cost).HasColumnType("decimal(18, 0)");
            user.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Kitchen>(user =>
        {
            user.HasKey(e => e.Id).HasName("PK__Kitchen__3214EC07041A77BF");

            user.ToTable("Kitchen");

            user.Property(e => e.Id).ValueGeneratedNever();

            user.HasOne(d => d.Type).WithMany(p => p.Kitchens)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_Kitchen_KitchenType");
            user.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<KitchenIngredients>(user =>
        {
            user.HasKey(e => e.AutoId).HasName("PK__KitchenI__6B2329058B1AE66A");

            user.ToTable("KitchenIngredients");

            user.Property(e => e.AutoId).ValueGeneratedNever();
            user.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

            user.HasOne(d => d.Ingredient).WithMany(p => p.KitchenIngredients)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK_KitchenIngredients_Ingredients");

            user.HasOne(d => d.Kitchen).WithMany(p => p.KitchenIngredients)
                .HasForeignKey(d => d.KitchenId)
                .HasConstraintName("FK_KitchenIngredients_Kitchen");
        });

        modelBuilder.Entity<KitchenEquipment>(user =>
        {
            user.HasKey(e => e.AutoId).HasName("PK__KitchenEquipment");

            user.ToTable("KitchenEquipment");

            user.Property(e => e.AutoId).ValueGeneratedNever();
            user.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

            user.HasOne(d => d.Kitchen).WithMany(p => p.KitchenEquipment)
                .HasForeignKey(d => d.KitchenId)
                .HasConstraintName("FK_KitchenEquipment_Kitchen");

            user.HasOne(d => d.Equipment).WithMany(p => p.KitchenEquipment)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_KitchenEquipment_Equipment");
        });

        modelBuilder.Entity<KitchenType>(user =>
        {
            user.HasKey(e => e.Id).HasName("PK__KitchenT__3214EC07692EF4AD");

            user.ToTable("KitchenType");

            user.Property(e => e.Id).ValueGeneratedNever();
            user.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<RecipeIngredients>(user =>
        {
            user.HasKey(e => new { e.RecipeId, e.IngredientId}).HasName("PK_RecipeId_IngredientId");

            user.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");
            user.Property(e => e.UnitOfMeasurement).HasMaxLength(50);

            user.HasOne(d => d.Ingredient).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.IngredientId)
                .HasConstraintName("FK_RecipeIngredients_Ingredients");

            user.HasOne(d => d.Recipes).WithMany(p => p.RecipeIngredients)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_RecipeIngredients_Recipes");
        });

        modelBuilder.Entity<RecipeEquipment>(user =>
        {
            user.HasKey(e => new { e.RecipeId, e.EquipmentId }).HasName("PK_RecipeId_EquipmentId");

            user.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

            user.HasOne(d => d.Recipe).WithMany(p => p.RecipeEquipment)
                .HasForeignKey(d => d.RecipeId)
                .HasConstraintName("FK_RecipeEquipment_Recipes");

            user.HasOne(d => d.Equipment).WithMany(p => p.RecipeEquipment)
                .HasForeignKey(d => d.EquipmentId)
                .HasConstraintName("FK_RecipeEquipment_Equipment");
        });

        modelBuilder.Entity<Recipe>(user =>
        {
            user.HasKey(e => e.Id).HasName("PK__Recipes__3214EC07AB5C1B61");

            user.Property(e => e.Id).ValueGeneratedNever();
            user.Property(e => e.Name).HasMaxLength(50);
            user.Property(e => e.Time).HasColumnType("decimal(18, 0)");
        });
        
        modelBuilder.Entity<RecipeIngredientsViewModel>().HasNoKey().ToView(null);
        modelBuilder.Entity<EquipmentViewModel>().HasNoKey().ToView(null);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}