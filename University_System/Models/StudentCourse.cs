using System;
using System.Collections.Generic;

namespace University_System.Models;

public partial class StudentCourse
{
    public int Sid { get; set; }

    public int Cno { get; set; }

    public int? Grade { get; set; }

    public virtual Course CnoNavigation { get; set; } = null!;

    public virtual Student SidNavigation { get; set; } = null!;
}
