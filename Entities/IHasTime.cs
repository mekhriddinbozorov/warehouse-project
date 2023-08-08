namespace warehouse_project.Entities;
public interface IHasTime
{
    DateTime CreatedAt { get; set; }
    DateTime ModifiedAt { get; set; }
}