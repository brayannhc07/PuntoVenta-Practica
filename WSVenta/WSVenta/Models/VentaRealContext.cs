using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WSVenta.Models {
	public partial class VentaRealContext: DbContext {
		public VentaRealContext() {
		}

		public VentaRealContext( DbContextOptions<VentaRealContext> options )
			: base( options ) {
		}

		public virtual DbSet<Cliente> Cliente { get; set; }
		public virtual DbSet<Concepto> Concepto { get; set; }
		public virtual DbSet<Producto> Producto { get; set; }
		public virtual DbSet<Venta> Venta { get; set; }

		protected override void OnConfiguring( DbContextOptionsBuilder optionsBuilder ) {
			if( !optionsBuilder.IsConfigured ) {
				//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
				optionsBuilder.UseMySQL( "server = 127.0.0.1;database = venta_real;uid = root; password = ;sslmode = none;" );
			}
		}

		protected override void OnModelCreating( ModelBuilder modelBuilder ) {
			modelBuilder.Entity<Cliente>( entity => {
				entity.ToTable( "cliente" );

				entity.Property( e => e.Id )
					.HasColumnName( "id" )
					.HasColumnType( "int(11)" );

				entity.Property( e => e.Nombre )
					.IsRequired()
					.HasColumnName( "nombre" )
					.HasMaxLength( 50 )
					.IsUnicode( false );
			} );

			modelBuilder.Entity<Concepto>( entity => {
				entity.ToTable( "concepto" );

				entity.HasIndex( e => e.IdProducto )
					.HasName( "id_producto_idx" );

				entity.HasIndex( e => e.IdVenta )
					.HasName( "id_venta_idx" );

				entity.Property( e => e.Id )
					.HasColumnName( "id" )
					.HasColumnType( "bigint(20)" );

				entity.Property( e => e.Cantidad )
					.HasColumnName( "cantidad" )
					.HasColumnType( "int(11)" );

				entity.Property( e => e.IdProducto )
					.HasColumnName( "id_producto" )
					.HasColumnType( "int(11)" );

				entity.Property( e => e.IdVenta )
					.HasColumnName( "id_venta" )
					.HasColumnType( "bigint(20)" );

				entity.Property( e => e.Importe )
					.HasColumnName( "importe" )
					.HasColumnType( "decimal(16,2)" );

				entity.Property( e => e.PrecioUnitario )
					.HasColumnName( "precio_unitario" )
					.HasColumnType( "decimal(16,2)" );

				entity.HasOne( d => d.IdProductoNavigation )
					.WithMany( p => p.Concepto )
					.HasForeignKey( d => d.IdProducto )
					.HasConstraintName( "id_producto" );

				entity.HasOne( d => d.IdVentaNavigation )
					.WithMany( p => p.Concepto )
					.HasForeignKey( d => d.IdVenta )
					.HasConstraintName( "id_venta" );
			} );

			modelBuilder.Entity<Producto>( entity => {
				entity.ToTable( "producto" );

				entity.Property( e => e.Id )
					.HasColumnName( "id" )
					.HasColumnType( "int(11)" );

				entity.Property( e => e.Costo )
					.HasColumnName( "costo" )
					.HasColumnType( "decimal(16,2)" );

				entity.Property( e => e.Nombre )
					.IsRequired()
					.HasColumnName( "nombre" )
					.HasMaxLength( 50 )
					.IsUnicode( false );

				entity.Property( e => e.PrecioUnitario )
					.HasColumnName( "precio_unitario" )
					.HasColumnType( "decimal(16,2)" );
			} );

			modelBuilder.Entity<Venta>( entity => {
				entity.ToTable( "venta" );

				entity.HasIndex( e => e.IdCliente )
					.HasName( "id_cliente_idx" );

				entity.Property( e => e.Id )
					.HasColumnName( "id" )
					.HasColumnType( "bigint(20)" );

				entity.Property( e => e.Fecha )
					.HasColumnName( "fecha" )
					.HasDefaultValueSql( "CURRENT_TIMESTAMP" );

				entity.Property( e => e.IdCliente )
					.HasColumnName( "id_cliente" )
					.HasColumnType( "int(11)" );

				entity.Property( e => e.Total )
					.HasColumnName( "total" )
					.HasColumnType( "decimal(16,2)" );

				entity.HasOne( d => d.IdClienteNavigation )
					.WithMany( p => p.Venta )
					.HasForeignKey( d => d.IdCliente )
					.HasConstraintName( "id_cliente" );
			} );

			OnModelCreatingPartial( modelBuilder );
		}

		partial void OnModelCreatingPartial( ModelBuilder modelBuilder );
	}
}
