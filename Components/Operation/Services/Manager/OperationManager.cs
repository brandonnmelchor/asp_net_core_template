using Microsoft.EntityFrameworkCore;
using Template.Data;
namespace Template.Components.Operation;

public class OperationManager(AppDbContext appContext) : IOperationManager
{
    private readonly AppDbContext _appContext = appContext;

    public async Task<OperationOutgoing> AddOperation(OperationIncoming operationIncoming)
    {
        var operation = operationIncoming.ToOperation();
        await _appContext.Operations.AddAsync(operation);
        await _appContext.SaveChangesAsync();

        return operation.ToOperationOutgoing();
    }

    public async Task<List<OperationOutgoing>> GetOperations()
    {
        return await _appContext.Operations
            .Select((operation) => operation.ToOperationOutgoing())
            .ToListAsync();
    }

    public async Task<OperationOutgoing?> GetOperation(int operationId)
    {
        var operation = await _appContext.Operations
            .FirstOrDefaultAsync((operation) => operation.Id == operationId);

        return operation?.ToOperationOutgoing();
    }

    public async Task<OperationOutgoing?> UpdateOperation(
        int operationId,
        OperationIncoming operationIncoming
        )
    {
        var operation = await _appContext.Operations
            .FirstOrDefaultAsync((operation) => operation.Id == operationId);

        if (operation == null) return null;

        operation.ToOperationUpdated(operationIncoming);
        await _appContext.SaveChangesAsync();

        return operation.ToOperationOutgoing();
    }

    public async Task<bool> DeleteOperation(int operationId)
    {
        var operation = await _appContext.Operations
            .FirstOrDefaultAsync((operation) => operation.Id == operationId);

        if (operation == null) return false;

        _appContext.Operations.Remove(operation);
        await _appContext.SaveChangesAsync();

        return true;
    }
}
