namespace warehouse_project.Entities;
public class Contact : IHasTime, IActive
{
    public long Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Vcard { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ModifiedAt { get; set; }
    public bool IsActive { get; set; }

    public virtual long UserId { get; set; }
    public virtual User User { get; set; }
}