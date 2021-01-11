using Nest;

namespace DocumentsService.Api.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public Attachment Attachment { get; set;}
    }
}