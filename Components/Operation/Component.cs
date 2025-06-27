using Template.Configuration;
namespace Template.Components.Operation;

public class Component : IComponent
{
    public void Register(IServiceCollection services)
    {
        services.AddScoped<IOperationManager, OperationManager>();
    }
}
