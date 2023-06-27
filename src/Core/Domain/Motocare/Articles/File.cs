

namespace FSH.WebApi.Domain.Motocare.Articles;

public class File : AuditableEntity, IAggregateRoot
{
    public string Name { get; set; }
    public byte[] Data { get; set; }
    public DateTime? ProcessedDT { get; set; }
    public File(string name, byte[] data)
    {
        Name = name;
        Data = data;
    }

    public File Update(DateTime processedDT)
    {
        ProcessedDT = processedDT;
        return this;
    }
}