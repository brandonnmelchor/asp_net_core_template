namespace Template.Components.Operation;

public static class OperationMapper
{
    public static Operation ToOperation(this OperationIncoming operationIncoming)
    {
        return new Operation
        {
            Title = operationIncoming.Title,
            Description = operationIncoming.Description,

            CreatedBy = "user_id",
            CreatedDate = DateTime.Now,
            ModifiedBy = "user_id",
            ModifiedDate = DateTime.Now
        };
    }

    public static OperationOutgoing ToOperationOutgoing(this Operation operation)
    {
        return new OperationOutgoing
        {
            Id = operation.Id,
            Title = operation.Title,
            Description = operation.Description,

            CreatedBy = operation.CreatedBy,
            CreatedDate = operation.CreatedDate,
            ModifiedBy = operation.ModifiedBy,
            ModifiedDate = operation.ModifiedDate
        };
    }

    public static void ToOperationUpdated(
        this Operation operation,
        OperationIncoming operationIncoming
        )
    {
        operation.Title = operationIncoming.Title;
        operation.Description = operationIncoming.Description;

        operation.ModifiedBy = "user_id";
        operation.ModifiedDate = DateTime.Now;
    }
}