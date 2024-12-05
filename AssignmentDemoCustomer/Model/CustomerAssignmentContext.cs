using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AssignmentDemoCustomer.Model;

public partial class CustomerAssignmentContext : DbContext
{
    public CustomerAssignmentContext()
    {
    }

    public CustomerAssignmentContext(DbContextOptions<CustomerAssignmentContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderTable> OrderTables { get; set; }
    public object OrderTable { get; internal set; }
    public object OrderItem { get; internal set; }

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Data Source = LAPTOP-PHHSIL6K\\SQLEXPRESS; Initial Catalog = CustomerAssignment; Integrated Security = True; Trusted_Connection = True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__8CB382B1629C0161");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("Customer_id");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Customer_name");
            entity.Property(e => e.CustomerNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__3FB403AC10AD4BFE");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("Item_id");
            entity.Property(e => e.ItemName)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("Item_Name");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__Order_It__2F31262AA8483D93");

            entity.ToTable("Order_Item");

            entity.Property(e => e.OrderItemId).HasColumnName("OrderItem_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.UnitePrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Item).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("FK__Order_Ite__item___5070F446");
        });

        modelBuilder.Entity<OrderTable>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__OrderTab__F1E4607B158C177B");

            entity.ToTable("OrderTable");

            entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("Order_date");
            entity.Property(e => e.OrderItemId).HasColumnName("OrderItem_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK__OrderTabl__Custo__534D60F1");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.OrderTables)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("FK__OrderTabl__Order__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
