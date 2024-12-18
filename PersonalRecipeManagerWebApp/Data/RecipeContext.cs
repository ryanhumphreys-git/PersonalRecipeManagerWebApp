using Microsoft.EntityFrameworkCore;
using PersonalRecipeManagerWebApp.Models;
using PersonalRecipeManagerWebApp.Models.Equipment;
using PersonalRecipeManagerWebApp.Models.Ingredients;

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
    public DbSet<UserShoppingList> UserShoppingList { get; set; }
    public DbSet<ShoppingListIngredients> ShoppingListIngredients { get; set; }
    public DbSet<ShoppingListEquipment> ShoppingListEquipment { get; set; }

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
            user.HasKey(e => new { e.UserId, e.KitchenId}).HasName("PK_UserKitchens");

            user.HasOne(d => d.User).WithMany(p => p.UserKitchens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserKitchens_User");

            user.HasOne(d => d.Kitchen).WithMany(p => p.UserKitchens)
                .HasForeignKey(d => d.KitchenId)
                .HasConstraintName("FK_UserKitchens_Kitchen");
        });

        modelBuilder.Entity<UserRecipes>(user =>
        {
            user.HasKey(e => new { e.UserId, e.RecipeId }).HasName("PK_UserId_RecipeId");

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
            user.HasKey(e => new { e.KitchenId, e.IngredientId }).HasName("PK_KitchenId_IngredientId");
            user.ToTable("KitchenIngredients");

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
            user.HasKey(e => new { e.KitchenId, e.EquipmentId }).HasName("PK_KitchenId_EquipmentID");

            user.ToTable("KitchenEquipment");

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
            user.Property(e => e.Instructions).HasColumnType("NVARCHAR(MAX)");
        });

        modelBuilder.Entity<UserShoppingList>(user =>
        {
            user.HasKey(e => e.ShoppingListId).HasName("PK_UserShoppingList");

            user.Property(e => e.ShoppingListId).ValueGeneratedNever();
            user.Property(e => e.UserId).ValueGeneratedNever();
            user.Property(e => e.Name).HasMaxLength(50);

            user.HasOne(d => d.User).WithMany(p => p.UserShoppingLists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserShoppingList_User");
        });

        modelBuilder.Entity<ShoppingListEquipment>(entity =>
        {
            entity.HasKey(e => new { e.ShoppingListId, e.EquipmentId });

            entity.ToTable("ShoppingListEquipment");

            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Equipment).WithMany(p => p.ShoppingListEquipments)
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingListEquipment_Equipment");

            entity.HasOne(d => d.UserShoppingList).WithMany(p => p.ShoppingListEquipments)
                .HasForeignKey(d => d.ShoppingListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingListEquipment_UserShoppingList");
        });

        modelBuilder.Entity<ShoppingListIngredients>(entity =>
        {
            entity.HasKey(e => new { e.ShoppingListId, e.IngredientId });

            entity.Property(e => e.Quantity).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Ingredients).WithMany(p => p.ShoppingListIngredients)
                .HasForeignKey(d => d.IngredientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingListIngredients_Ingredients");

            entity.HasOne(d => d.UserShoppingList).WithMany(p => p.ShoppingListIngredients)
                .HasForeignKey(d => d.ShoppingListId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ShoppingListIngredients_UserShoppingList");
        });

        modelBuilder.Entity<RecipeIngredientsViewModel>().HasNoKey().ToView(null);
        modelBuilder.Entity<EquipmentViewModel>().HasNoKey().ToView(null);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}