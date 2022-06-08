﻿namespace Inventex.API.Management.Resources;

public class InventoryResource
{
    public int Id { get; set; }
    public string Name { get; set; }
    public byte[] Image { get; set; }
    public float Price { get; set; }
    public string Category { get; set; }
    public string InventoryStatus { get; set; }
    
    //
    public UserResource User { get; set; }
}