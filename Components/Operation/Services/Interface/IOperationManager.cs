namespace Template.Components.Operation;

public interface IOperationManager
{
  Task<OperationOutgoing> AddOperation(OperationIncoming operationIncoming);
  Task<List<OperationOutgoing>> GetOperations();
  Task<OperationOutgoing?> GetOperation(int operationId);

  Task<OperationOutgoing?> UpdateOperation(
    int operationId,
    OperationIncoming operationIncoming
    );

  Task<bool> DeleteOperation(int operationId);
}
