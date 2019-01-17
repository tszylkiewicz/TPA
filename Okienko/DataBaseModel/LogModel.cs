using System.ComponentModel.DataAnnotations;

namespace DataBaseModel
{
    public class LogModel
    {
        [Key]
        public int LogEntityId { get; set; }
        public string Message { get; set; }
    }
}
