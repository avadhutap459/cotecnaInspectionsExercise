using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspection.DbLayer.DBModel
{
  [Table("mstInspector")]
  public class ClsInspector
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InspectorId { get;set; }
    public string? InspectorName { get;set; }
    public ICollection<ClsInspection> Inspections { get; set; }
  }
}
