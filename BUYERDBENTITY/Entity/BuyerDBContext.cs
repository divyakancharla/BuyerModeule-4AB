using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BUYERDBENTITY.Entity
{
    public partial class BuyerDBContext : DbContext
    {
        public BuyerDBContext()
        {
        }

        public BuyerDBContext(DbContextOptions<BuyerDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Buyer> Buyer { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Items> Items { get; set; }
        public virtual DbSet<Purchasehistory> Purchasehistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-2KOFJBQ\\SQLEXPRESS;Database=BuyerDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Buyer>(entity =>
            {
                entity.Property(e => e.Buyerid)
                    .HasColumnName("buyerid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Mobileno)
                    .IsRequired()
                    .HasColumnName("mobileno")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.Cartid)
                    .HasColumnName("cartid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Buyerid).HasColumnName("buyerid");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Imagename)
                    .HasColumnName("imagename")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Itemname)
                    .IsRequired()
                    .HasColumnName("itemname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Stockno).HasColumnName("stockno");

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Buyerid)
                    .HasConstraintName("FK__Cart__buyerid__2C3393D0");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__Cart__itemid__2D27B809");
            });

            modelBuilder.Entity<Items>(entity =>
            {
                entity.HasKey(e => e.Itemid)
                    .HasName("PK__Items__56A22C92497F3EF2");

                entity.Property(e => e.Itemid)
                    .HasColumnName("itemid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Imagename)
                    .HasColumnName("imagename")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Itemname)
                    .IsRequired()
                    .HasColumnName("itemname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Stockno).HasColumnName("stockno");
            });

            modelBuilder.Entity<Purchasehistory>(entity =>
            {
                entity.HasKey(e => e.Purchaseid)
                    .HasName("PK__Purchase__02662E44B6327895");

                entity.Property(e => e.Purchaseid)
                    .HasColumnName("purchaseid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Buyerid).HasColumnName("buyerid");

                entity.Property(e => e.Datetime)
                    .HasColumnName("datetime")
                    .HasColumnType("date");

                entity.Property(e => e.Itemid).HasColumnName("itemid");

                entity.Property(e => e.Itemname)
                    .IsRequired()
                    .HasColumnName("itemname")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Noofitems).HasColumnName("noofitems");

                entity.Property(e => e.Remarks)
                    .HasColumnName("remarks")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Transactionstatus)
                    .HasColumnName("transactionstatus")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Transactiontype)
                    .IsRequired()
                    .HasColumnName("transactiontype")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Buyer)
                    .WithMany(p => p.Purchasehistory)
                    .HasForeignKey(d => d.Buyerid)
                    .HasConstraintName("FK__Purchaseh__buyer__286302EC");

                entity.HasOne(d => d.Item)
                    .WithMany(p => p.Purchasehistory)
                    .HasForeignKey(d => d.Itemid)
                    .HasConstraintName("FK__Purchaseh__itemi__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
