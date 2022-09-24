using System.ComponentModel.DataAnnotations;

namespace Jobportel.Data.Model
{
    public  class Role
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
