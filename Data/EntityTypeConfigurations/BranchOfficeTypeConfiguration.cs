using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.EntityTypeConfigurations
{
    public class BranchOfficeTypeConfiguration : IEntityTypeConfiguration<BranchOffice>
    { 
        public void Configure(EntityTypeBuilder<BranchOffice> builder)
        {
            builder.ToTable("Sucursales");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn(1, 1);

            builder.Property(x => x.Direccion)
                .HasMaxLength(255)
                .IsRequired(true);

            builder.Property(x => x.Latitud)
                .IsRequired(true);

            builder.Property(x => x.Longitud)
               .IsRequired(true);
        }
    }
}
