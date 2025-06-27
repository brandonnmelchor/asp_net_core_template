using Microsoft.AspNetCore.Mvc;
namespace Template.Components.Operation;

[ApiController]
[Route("api/operations")]
public class OperationController(IOperationManager operationManager) : ControllerBase
{
    private readonly IOperationManager _operationManager = operationManager;

    [HttpPost]
    public async Task<IActionResult> AddOperation([FromBody] OperationIncoming operationIncoming)
    {
        var operation = await _operationManager.AddOperation(operationIncoming);
        return Ok(operation);
    }

    [HttpGet]
    public async Task<IActionResult> GetOperations()
    {
        var operations = await _operationManager.GetOperations();
        return Ok(operations);
    }

    [HttpGet("{operationId}")]
    public async Task<IActionResult> GetOperation([FromRoute] int operationId)
    {
        var operation = await _operationManager.GetOperation(operationId);
        return Ok(operation);
    }

    [HttpPut("{operationId}")]
    public async Task<IActionResult> UpdateOperation(
        [FromRoute] int operationId,
        [FromBody] OperationIncoming operationIncoming
        )
    {
        var operation = await _operationManager.UpdateOperation(operationId, operationIncoming);
        return Ok(operation);
    }
}
