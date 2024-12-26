using System;
using System.Collections.Generic;

namespace University_System.Models;

public partial class Department
{
    public int Dno { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DepartmentCourse> DepartmentCourses { get; set; } = new List<DepartmentCourse>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
