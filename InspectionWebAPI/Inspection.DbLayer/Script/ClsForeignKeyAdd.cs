using Inspection.DbLayer.DBModel;
using Microsoft.EntityFrameworkCore;

namespace Inspection.DbLayer.Script
{
  public static class ClsForeignKeyAdd
  {
    public static void AddForeignKey(ref ModelBuilder modelBuilder)
    {
      try
      {
        modelBuilder.Entity<ClsInspection>().HasOne(x => x.Inspector).WithMany(x => x.Inspections).HasForeignKey(x => x.InspectorId).OnDelete(DeleteBehavior.ClientSetNull);

      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}
