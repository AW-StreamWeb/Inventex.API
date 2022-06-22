﻿using Inventex.API.Security.Resources;

namespace Inventex.API.Management.Resources;

public class ContactResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Lifetime { get; set; }
    public bool Active { get; set; }
    public UserResource User { get; set; }
}