using System;
using System.Collections.Generic;

namespace Web_api.Model;

public partial class UserInfo
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Lastname { get; set; }

    public string? Email { get; set; }
}
