using Inspection.DbLayer.DBModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inspection.DbLayer.Script
{
  public class InspectorConf : IEntityTypeConfiguration<ClsInspector>
  {
    public void Configure(EntityTypeBuilder<ClsInspector> builder)
    {

      builder.ToTable("mstInspector");

      builder.Property(x => x.InspectorId).UseIdentityColumn();
      builder.Property(s => s.InspectorName).HasDefaultValue(true);
      builder.HasData(
          new ClsInspector { InspectorId = 1, InspectorName = "Inspector 1" },
          new ClsInspector { InspectorId = 2, InspectorName = "Inspector 2" },
          new ClsInspector { InspectorId = 3, InspectorName = "Inspector 3" }
      );

    }
  }
}
