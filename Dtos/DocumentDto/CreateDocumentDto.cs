namespace warehouse_project.Dtos.DocumentDto;
public class CreateDocumentDto
{
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Provider { get; set; }
    public Guid CategoryId { get; set; }
}