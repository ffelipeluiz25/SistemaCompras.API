using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutoAgg = SistemaCompra.Domain.ProdutoAggregate;
namespace SistemaCompra.Infra.Data.Produto
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoAgg.Produto>
    {
        public void Configure(EntityTypeBuilder<ProdutoAgg.Produto> builder)
        {
            builder.ToTable("Produto");
            builder.Property(x => x.Preco).HasColumnType("decimal(18,2)").IsRequired().HasColumnName("Preco");
            builder.OwnsOne(c => c.PrecoFormatado, b => b.Property("Value").HasColumnName("Preco"));
        }
    }
}