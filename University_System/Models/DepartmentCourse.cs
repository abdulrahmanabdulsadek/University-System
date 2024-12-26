using System;
using System.Collections.Generic;

namespace University_System.Models;

public partial class DepartmentCourse
{
    public int Dno { get; set; }

    public int Cno { get; set; }

    public string? Semester { get; set; }

    public virtual Course CnoNavigation { get; set; } = null!;

    public virtual Department DnoNavigation { get; set; } = null!;
}
