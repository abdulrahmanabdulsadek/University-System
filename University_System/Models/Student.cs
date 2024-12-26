using System;
using System.Collections.Generic;

namespace University_System.Models;

public partial class Student
{
    public int Sid { get; set; }

    public string? Fname { get; set; }

    public string? Lname { get; set; }

    public string? Address { get; set; }

    public string? PhoneNumebr { get; set; }

    public int? Age { get; set; }

    public string? Email { get; set; }

    public int Dno { get; set; }

    public virtual Department DnoNavigation { get; set; } = null!;

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();
}
