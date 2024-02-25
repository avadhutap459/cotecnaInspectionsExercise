using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inspection.DbLayer.DBModel
{
  [Table("TxnInspection")]
  public class ClsInspection
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InspectionID { get; set; }
    public DateTime InspectionDate { get; set; }
    public string? Customer { get; set; }
    public string? Address { get; set; }
    public string? Observations { get; set; }
    public string? Status { get; set; }
    public int InspectorId { get; set; }
    public ClsInspector Inspector { get; set; }

  }
}
