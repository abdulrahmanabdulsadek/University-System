using System;
using System.Collections.Generic;

namespace University_System.Models;

public partial class Course
{
    public int Cno { get; set; }

    public string Name { get; set; } = null!;

    public string? Describtion { get; set; }

    public int SchNo { get; set; }

    public virtual ICollection<DepartmentCourse> DepartmentCourses { get; set; } = new List<DepartmentCourse>();

    public virtual Schedule SchNoNavigation { get; set; } = null!;

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
