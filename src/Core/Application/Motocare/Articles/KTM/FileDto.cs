namespace FSH.WebApi.Application.Motocare.Articles.Ktm;

public class File : IDto
{
    public string Name { get; set; }
    public byte[] Data { get; set; }
    public DateTime? ProcessedDT { get; set; }
}