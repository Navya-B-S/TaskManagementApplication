using System.ComponentModel.DataAnnotations;

namespace TaskManagementApplication.Models
{
    public class Task
    {
        //create properties for the Task class that map to the columns of the Tasks table and mark the properties with Required attribute
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        public string Status { get; set; }
        public Task() { }

    }

}
