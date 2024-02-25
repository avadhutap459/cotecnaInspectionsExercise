using Inspection.DbLayer.DBModel;
using Inspection.DbLayer.Script;
using Microsoft.EntityFrameworkCore;

namespace Inspection.DbLayer
{
  public class InspectionDbContext : DbContext
  {
    public InspectionDbContext(DbContextOptions<InspectionDbContext> options) : base(options) { }

    public InspectionDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      ClsForeignKeyAdd.AddForeignKey(ref modelBuilder);
      modelBuilder.ApplyConfiguration(new InspectorConf());
      base.OnModelCreating(modelBuilder);
    }
    public DbSet<ClsInspection> TxnInspections { get; set; }
    public DbSet<ClsInspector> mstInspectors { get; set; }
  }
}
