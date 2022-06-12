using System;
using System.Collections.Generic;
using LOSMST.Models.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LOSMST.DataAccess.Data
{
    public partial class LOSMSTv01Context : DbContext
    {
        public LOSMSTv01Context()
        {
        }

        public LOSMSTv01Context(DbContextOptions<LOSMSTv01Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<CustomerOrder> CustomerOrders { get; set; } = null!;
        public virtual DbSet<CustomerOrderDetail> CustomerOrderDetails { get; set; } = null!;
        public virtual DbSet<ExportInventory> ExportInventories { get; set; } = null!;
        public virtual DbSet<ExportInventoryDetail> ExportInventoryDetails { get; set; } = null!;
        public virtual DbSet<ImportInventory> ImportInventories { get; set; } = null!;
        public virtual DbSet<ImportInventoryDetail> ImportInventoryDetails { get; set; } = null!;
        public virtual DbSet<Package> Packages { get; set; } = null!;
        public virtual DbSet<Price> Prices { get; set; } = null!;
        public virtual DbSet<PriceDetail> PriceDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductDetail> ProductDetails { get; set; } = null!;
        public virtual DbSet<ProductStoreRequestDetail> ProductStoreRequestDetails { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<Store> Stores { get; set; } = null!;
        public virtual DbSet<StoreCategory> StoreCategories { get; set; } = null!;
        public virtual DbSet<StoreProductDetail> StoreProductDetails { get; set; } = null!;
        public virtual DbSet<StoreRequestOrder> StoreRequestOrders { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=NAME-PC\\MSSQLSERVER2019;Database=LOSMSTv01;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .HasColumnName("district");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Password)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.RoleId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("roleId")
                    .IsFixedLength();

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('1.1')")
                    .IsFixedLength();

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.Ward)
                    .HasMaxLength(50)
                    .HasColumnName("ward");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__roleId__33D4B598");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__statusI__36B12243");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK__Account__storeId__34C8D9D1");
            });

            modelBuilder.Entity<CustomerOrder>(entity =>
            {
                entity.ToTable("CustomerOrder");

                entity.Property(e => e.Id)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.CustomerAccountId).HasColumnName("customerAccountId");

                entity.Property(e => e.EstimatedReceiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("estimatedReceiveDate");

                entity.Property(e => e.ManagerAccountId).HasColumnName("managerAccountId");

                entity.Property(e => e.OrderDate)
                    .HasColumnType("datetime")
                    .HasColumnName("orderDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("reason");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("receiveDate");

                entity.Property(e => e.StaffAccountId).HasColumnName("staffAccountId");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('2.1')")
                    .IsFixedLength();

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.HasOne(d => d.CustomerAccount)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.CustomerAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerO__custo__3A81B327");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerO__statu__3C69FB99");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.CustomerOrders)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerO__store__398D8EEE");
            });

            modelBuilder.Entity<CustomerOrderDetail>(entity =>
            {
                entity.ToTable("CustomerOrderDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CustomerOrderId)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("customerOrderId")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductDetailId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productDetailId")
                    .IsFixedLength();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.CustomerOrder)
                    .WithMany(p => p.CustomerOrderDetails)
                    .HasForeignKey(d => d.CustomerOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerO__custo__4AB81AF0");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.CustomerOrderDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CustomerO__produ__4BAC3F29");
            });

            modelBuilder.Entity<ExportInventory>(entity =>
            {
                entity.ToTable("ExportInventory");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.ExportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("exportDate");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ExportInventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExportInv__store__4E88ABD4");
            });

            modelBuilder.Entity<ExportInventoryDetail>(entity =>
            {
                entity.ToTable("ExportInventoryDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ExportInventoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("exportInventoryId")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ProductDetailId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productDetailId")
                    .IsFixedLength();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.ExportInventory)
                    .WithMany(p => p.ExportInventoryDetails)
                    .HasForeignKey(d => d.ExportInventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExportInv__expor__52593CB8");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.ExportInventoryDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExportInv__produ__5165187F");
            });

            modelBuilder.Entity<ImportInventory>(entity =>
            {
                entity.ToTable("ImportInventory");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.ImportDate)
                    .HasColumnType("datetime")
                    .HasColumnName("importDate");

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.ImportInventories)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ImportInv__store__5535A963");
            });

            modelBuilder.Entity<ImportInventoryDetail>(entity =>
            {
                entity.ToTable("ImportInventoryDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ImportInventoryId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("importInventoryId")
                    .IsFixedLength();

                entity.Property(e => e.ProductDetailId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productDetailId")
                    .IsFixedLength();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.ImportInventory)
                    .WithMany(p => p.ImportInventoryDetails)
                    .HasForeignKey(d => d.ImportInventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ImportInv__impor__59063A47");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.ImportInventoryDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ImportInv__produ__5812160E");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.ToTable("Package");

                entity.Property(e => e.Id)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Price>(entity =>
            {
                entity.ToTable("Price");

                entity.Property(e => e.Id)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("endDate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("startDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('1.1')")
                    .IsFixedLength();

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Prices)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Price__statusId__5DCAEF64");
            });

            modelBuilder.Entity<PriceDetail>(entity =>
            {
                entity.ToTable("PriceDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.PriceId)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("priceId")
                    .IsFixedLength();

                entity.Property(e => e.ProductDetailId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productDetailId")
                    .IsFixedLength();

                entity.Property(e => e.RetailPriceAfterTax).HasColumnName("retailPriceAfterTax");

                entity.Property(e => e.RetailPriceBeforeTax).HasColumnName("retailPriceBeforeTax");

                entity.Property(e => e.WholesalePriceAfterTax).HasColumnName("wholesalePriceAfterTax");

                entity.Property(e => e.WholesalePriceBeforeTax).HasColumnName("wholesalePriceBeforeTax");

                entity.HasOne(d => d.Price)
                    .WithMany(p => p.PriceDetails)
                    .HasForeignKey(d => d.PriceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PriceDeta__price__619B8048");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.PriceDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PriceDeta__produ__60A75C0F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Apply)
                    .HasMaxLength(1000)
                    .HasColumnName("apply");

                entity.Property(e => e.Brief)
                    .HasMaxLength(1000)
                    .HasColumnName("brief");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.GeneralBenefit)
                    .HasMaxLength(1000)
                    .HasColumnName("generalBenefit");

                entity.Property(e => e.Image)
                    .IsUnicode(false)
                    .HasColumnName("image");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Preserve)
                    .HasMaxLength(1000)
                    .HasColumnName("preserve");

                entity.Property(e => e.QualityLevelFeature)
                    .HasMaxLength(1000)
                    .HasColumnName("qualityLevelFeature");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('3.1')")
                    .IsFixedLength();

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__categor__403A8C7D");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product__statusI__4222D4EF");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.ToTable("ProductCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.ToTable("ProductDetail");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.PackageId)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("packageId")
                    .IsFixedLength();

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('3.1')")
                    .IsFixedLength();

                entity.Property(e => e.Volume).HasColumnName("volume");

                entity.Property(e => e.WholeSalePriceQuantity).HasColumnName("wholeSalePriceQuantity");

                entity.HasOne(d => d.Package)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.PackageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductDe__packa__45F365D3");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductDe__produ__44FF419A");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.ProductDetails)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductDe__statu__47DBAE45");
            });

            modelBuilder.Entity<ProductStoreRequestDetail>(entity =>
            {
                entity.ToTable("ProductStoreRequestDetail");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.ProductDetailId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productDetailId")
                    .IsFixedLength();

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.StoreRequestOrderId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("storeRequestOrderId")
                    .IsFixedLength();

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.ProductStoreRequestDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSt__produ__693CA210");

                entity.HasOne(d => d.StoreRequestOrder)
                    .WithMany(p => p.ProductStoreRequestDetails)
                    .HasForeignKey(d => d.StoreRequestOrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProductSt__store__6A30C649");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.Id)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.ToTable("Store");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Code)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("code")
                    .IsFixedLength();

                entity.Property(e => e.District)
                    .HasMaxLength(50)
                    .HasColumnName("district");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('1.1')")
                    .IsFixedLength();

                entity.Property(e => e.StoreCategoryId)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("storeCategoryId")
                    .HasDefaultValueSql("('CHBL')")
                    .IsFixedLength();

                entity.Property(e => e.Ward)
                    .HasMaxLength(50)
                    .HasColumnName("ward");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_Status");

                entity.HasOne(d => d.StoreCategory)
                    .WithMany(p => p.Stores)
                    .HasForeignKey(d => d.StoreCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Store_StoreCategory");
            });

            modelBuilder.Entity<StoreCategory>(entity =>
            {
                entity.ToTable("StoreCategory");

                entity.Property(e => e.Id)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<StoreProductDetail>(entity =>
            {
                entity.ToTable("StoreProductDetail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CurrentQuantity).HasColumnName("currentQuantity");

                entity.Property(e => e.ProductDetailId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("productDetailId")
                    .IsFixedLength();

                entity.Property(e => e.StoreId).HasColumnName("storeId");

                entity.HasOne(d => d.ProductDetail)
                    .WithMany(p => p.StoreProductDetails)
                    .HasForeignKey(d => d.ProductDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProductDetail_ProductDetail");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoreProductDetails)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StoreProductDetail_Store");
            });

            modelBuilder.Entity<StoreRequestOrder>(entity =>
            {
                entity.ToTable("StoreRequestOrder");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("reason");

                entity.Property(e => e.ReceiveDate)
                    .HasColumnType("datetime")
                    .HasColumnName("receiveDate");

                entity.Property(e => e.RequestDate)
                    .HasColumnType("datetime")
                    .HasColumnName("requestDate");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("statusId")
                    .HasDefaultValueSql("('2.1')")
                    .IsFixedLength();

                entity.Property(e => e.StoreRequestId).HasColumnName("storeRequestId");

                entity.Property(e => e.StoreSupplyCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("storeSupplyCode")
                    .IsFixedLength();

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.StoreRequestOrders)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreRequ__statu__66603565");

                entity.HasOne(d => d.StoreRequest)
                    .WithMany(p => p.StoreRequestOrders)
                    .HasForeignKey(d => d.StoreRequestId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreRequ__store__6477ECF3");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
