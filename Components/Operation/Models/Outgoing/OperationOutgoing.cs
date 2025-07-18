namespace Template.Components.Operation;

public class OperationOutgoing
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
}