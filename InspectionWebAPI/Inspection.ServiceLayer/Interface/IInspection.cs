using Inspection.ServiceLayer.Model;

namespace Inspection.ServiceLayer.Interface
{
  public interface IInspection
  {
    List<ClsInspectorBM> GetAllInspector();
    List<ClsInspectionsBM> GetAllInspection();
    List<ClsInspectionsBM> GetAllInspectionsByInspectorName(string InspectoName);
    List<ClsInspectionsBM> GetAllInspectionsByCondition(string ConditionValue);
    bool CheckInspectionExistForPerDay(int InspectorId, DateTime InspectionDt);
    int InsertInspection(ClsInspectionsBM objInspection);
    void UpdateInspection(ClsInspectionsBM objInspection);
    void DeleteInspection(int InspectionId);

  }
}
