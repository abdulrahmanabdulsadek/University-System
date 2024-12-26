using System;
using System.Collections.Generic;

namespace University_System.Models;

public partial class Schedule
{
    public int SchNo { get; set; }

    public string Day { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}
