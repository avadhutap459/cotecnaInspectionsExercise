

namespace Inspection.ServiceLayer.Model
{
  public class ClsInspectionsBM
  {
    public int InspectionID { get; set; }
    public DateTime? InspectionDate { get; set; }
    public string? Customer { get; set; }
    public string? Address { get; set; }
    public string? Observations { get; set; }
    public string? Status { get; set; }
    public int InspectorId { get; set; }
    public string InspectorName { get; set; }
  }
}
