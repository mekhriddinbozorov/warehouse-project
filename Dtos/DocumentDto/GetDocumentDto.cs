using warehouse_project.Entities;

namespace warehouse_project.Dtos.DocumentDto;
public class GetDocumentDto
{
    public GetDocumentDto(Document document)
    {
        Id = document.Id;
        Name = document.Name;
        Date = document.Date;
        Provider = document.Provider;
        CategoryId = document.CategoryId;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Provider { get; set; }
    public Guid CategoryId { get; set; }
}